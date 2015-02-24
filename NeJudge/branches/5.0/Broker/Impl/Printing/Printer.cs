using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;
using System.Web;
using System.Windows.Forms;

namespace Broker.Impl.Printing
{
	public class Printer : IPrinter
	{
		public void PrintText(string text, string waterMark)
		{
			Task.Factory.StartNew(() => PrintOnStaThread(text, waterMark), CancellationToken.None, TaskCreationOptions.None, Sta).Wait();
		}

		void PrintOnStaThread(string text, string waterMark)
		{
			const short PRINT_WAITFORCOMPLETION = 2;
			const int OLECMDID_PRINT = 6;
			const int OLECMDEXECOPT_DONTPROMPTUSER = 2;

			using(var browser = new WebBrowser())
			{
				var htmlPath = PrepareHtml(text, waterMark);
				browser.Navigate(htmlPath);

				while(browser.ReadyState != WebBrowserReadyState.Complete)
					Application.DoEvents();
				
				dynamic ie = browser.ActiveXInstance;
				ie.ExecWB(OLECMDID_PRINT, OLECMDEXECOPT_DONTPROMPTUSER, PRINT_WAITFORCOMPLETION);
			}
		}

		public Printer(string printerDirPath)
		{
			printerDir = new DirectoryInfo(printerDirPath);
			PreparePrinterDirectory();
		}

		static void SaveWatermark(string waterMark, string path)
		{
			using (var image = new Bitmap(300, 100))
			using (var g = Graphics.FromImage(image))
			{
				g.DrawString(waterMark, new Font(FontFamily.GenericSerif, 20f), Brushes.Gray, 0f, 0f);
				image.Save(path, ImageFormat.Png);
			}
		}

		static void SaveHtml(string text, string htmlPath)
		{
			var html = string.Format(Template, HttpUtility.HtmlEncode(text));
			File.WriteAllText(htmlPath, html);
		}

		string PrepareHtml(string text, string waterMark)
		{
			string tempDir = printerDir.CreateSubdirectory(Guid.NewGuid().ToString()).FullName;
			var htmlPath = Path.Combine(tempDir, FileName);
			
			SaveWatermark(waterMark, Path.Combine(tempDir, ImageName));
			SaveHtml(text, htmlPath);

			return htmlPath;
		}

		private static string ReadTemplate()
		{
			var resourceType = typeof(Printer);
			string resourceName = ResName(resourceType, TemplateResourceName);
			
			using(var stream = resourceType.Assembly.GetManifestResourceStream(resourceName))
			using(var sr = new StreamReader(stream))
				return sr.ReadToEnd();
		}

		void PreparePrinterDirectory()
		{
			if (printerDir.Exists)
				printerDir.Delete(true);
			printerDir.Create();

			var resources = GetType().Assembly.GetManifestResourceNames();
			foreach (var resDir in new[] { "highlighter", "styles" })
			{
				var dir = printerDir.CreateSubdirectory(resDir);
				var resPrefix = ResName(GetType(), resDir);

				foreach (var resource in resources.Where(x => x.StartsWith(resPrefix)))
					DumpFromResource(resource, dir.FullName);
			}
		}

		static void DumpFromResource(string resource, string targetDir)
		{
			var fileName = string.Join(".", LastTwo(resource.Split('.')));
			using (var stream = typeof(Printer).Assembly.GetManifestResourceStream(resource))
			{
				var bytes = new byte[stream.Length];
				stream.Read(bytes, 0, bytes.Length);
				File.WriteAllBytes(Path.Combine(targetDir, fileName), bytes);
			}
		}

		static T[] LastTwo<T>(T[] elems)
		{
			return new[] { elems[elems.Length - 2], elems[elems.Length - 1] };
		}

		static string ResName(Type resType, string n)
		{
			return resType.Namespace + ".PrinterDirTemplate." + n;
		}

		readonly DirectoryInfo printerDir;
		static readonly string Template = ReadTemplate();
		static readonly TaskScheduler Sta = new StaTaskScheduler(1);

		const string TemplateResourceName = "PrintSourceTemplate.htm";
		const string FileName = "PrintSource.htm";
		const string ImageName = "Watermark.png";
	}
}