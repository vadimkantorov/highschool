using Config;
using System;
using System.IO;
using System.Data;
using System.Timers;
using System.Drawing;
using Microsoft.Win32;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace WinBalans
{
	public class General : System.Windows.Forms.Form
	{
		#region Создание Controls
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.MenuItem Paintd;
		private System.Windows.Forms.MenuItem depaintd;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;

		
		private Assembly curas;
		private bool redr;
		private TempParameters tp;
		private FirstParameters fp;
		
		private ViewParameters pm;
		private System.Windows.Forms.Button showPluginList;
		private PluginInfo pi;

		private ArrayList pluginlist;
		private Settings s;

		#endregion
		#region Конструктор
		public General()
		{
			redr = false;
			pluginlist = new ArrayList();
			tp = new TempParameters();
			if((FirstParameters.Load(tp.basedirectory))  != null)
				fp = FirstParameters.Load(tp.basedirectory);
			else 
				fp = FirstParameters.CreateDefaultFirstParameters(this.tp.basedirectory);
			if(fp.firstTimeStarted == 1)
			{
				fp = FirstParameters.CreateDefaultFirstParameters(this.tp.basedirectory);
				pm = ViewParameters.CreateDefaultViewParameters();
				pi = PluginInfo.CreateDefaultPluginInfo();
			}
			else
			{
				try
				{
					pm = ViewParameters.Load(fp.pathToViewParameters);
				}
				catch (Exception e) 
				{
					MessageBox.Show(e.Message,"Ошибка!",MessageBoxButtons.OK,MessageBoxIcon.Error);
					Application.Exit();
				}
				try
				{
					pi = PluginInfo.Load(fp.pathToCurrentPluginInfo);
				}
				catch (Exception e) 
				{
					MessageBox.Show(e.Message,"Ошибка!",MessageBoxButtons.OK,MessageBoxIcon.Error);
					Application.Exit();
				}
			}

			Update_curas();
			InitializeComponent();
		}
		#endregion
		#region "Удаляем" объекты
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion
		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.Paintd = new System.Windows.Forms.MenuItem();
			this.depaintd = new System.Windows.Forms.MenuItem();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.showPluginList = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 32);
			this.label1.TabIndex = 1;
			this.label1.Text = "Здесь появится состояние счёта";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label1.DoubleClick += new System.EventHandler(this.GetStat);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3,
																						 this.menuItem4,
																						 this.Paintd,
																						 this.depaintd});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Настройки";
			this.menuItem1.Click += new System.EventHandler(this.SettingsEvent);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Справка";
			this.menuItem2.Click += new System.EventHandler(this.AboutEvent);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "Выход";
			this.menuItem3.Click += new System.EventHandler(this.ExitEvent);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "Получить статистику";
			this.menuItem4.Click += new System.EventHandler(this.GetStat);
			// 
			// Paintd
			// 
			this.Paintd.Index = 4;
			this.Paintd.Text = "Перерисовать рабочий стол";
			this.Paintd.Click += new System.EventHandler(this.RedrawDesktop);
			// 
			// depaintd
			// 
			this.depaintd.Index = 5;
			this.depaintd.Text = "Вернуть всё на место";
			this.depaintd.Click += new System.EventHandler(this.RestoreDesktop);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenu = this.contextMenu1;
			this.notifyIcon1.Text = "WinUSPBalans";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(24, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(136, 16);
			this.button1.TabIndex = 2;
			this.button1.Text = "Поменять границу";
			this.button1.Click += new System.EventHandler(this.ChangeBorderStyle);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "WinBalans Plug-ins (*.dll)|*.dll|All files (*.*)|*.*";
			// 
			// showPluginList
			// 
			this.showPluginList.Dock = System.Windows.Forms.DockStyle.Left;
			this.showPluginList.Location = new System.Drawing.Point(0, 0);
			this.showPluginList.Name = "showPluginList";
			this.showPluginList.Size = new System.Drawing.Size(16, 56);
			this.showPluginList.TabIndex = 3;
			this.showPluginList.Text = "<<";
			this.showPluginList.Click += new System.EventHandler(this.showPluginList_Click);
			// 
			// General
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Brown;
			this.ClientSize = new System.Drawing.Size(162, 56);
			this.ContextMenu = this.contextMenu1;
			this.ControlBox = false;
			this.Controls.Add(this.showPluginList);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.Name = "General";
			this.Opacity = 0.5;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WinUSPBalans 1.0";
			this.ResumeLayout(false);

		}
		#endregion
		#region Обработчики событий
		
		private void GetStat(object sender, System.EventArgs e)
		{
			label1.Text = GetStat();
		}
		
		private void ExitEvent(object sender, System.EventArgs e)
		{
			if(this.redr)
				RestoreDesktop();
			pm.Save(fp.pathToViewParameters);
			fp.Save(tp.basedirectory);
            Application.Exit();
		}

		private void AboutEvent(object sender, System.EventArgs e)
		{
			new About().ShowDialog();
		}
		private void SettingsEvent(object sender, System.EventArgs e)
		{
			s = new Settings();
			mysettings.ChangeValues(pm, pi);
			mysettings.ShowDialog();
			if(mysettings.Apply)
			{
				pm = mysettings.GetViewParameters();
				pi = mysettings.GetPluginInfo();
				Update_curas();
			}
		}
		
		private void notifyIcon1_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.Visible == false)
				this.Visible = true;
			else
				this.Visible = false;
		}
		private void RestoreDesktop(object sender, System.EventArgs e)
		{
			if(this.redr)
                RestoreDesktop();
		}
		private void RedrawDesktop(object sender, System.EventArgs e)
		{
			redr = true;
			RedrawDesktop();
		}
		#endregion
		[STAThread]
		static void Main() 
		{
			new General();
            Application.Run();
		}
		
		#region Методы
		private void ChangeBorderStyle(object sender, System.EventArgs e)
		{
			if(this.FormBorderStyle == FormBorderStyle.None)
				this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			else
				this.FormBorderStyle = FormBorderStyle.None;
		}
		private string GetStat()
		{
			Type myType = curas.GetType( "IB" );
			MethodInfo mi =  myType.GetMethod("GetValue");
			if(pi.username == "" || pi.password == "")
			{
				Auth myauth = new Auth();
				if(myauth.IsOk)
				{
					pi.username = myauth.username;
					pi.password = myauth.password;
					Object[] args = new Object[] {pi.username, pi.password};
					return (string)myType.InvokeMember(mi.Name,BindingFlags.DeclaredOnly | 
						BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance |
						BindingFlags.InvokeMethod,null,null,args);
				}
				else
					return "Введите имя и пароль";
				
			}
			Object[] argz = new Object[] {pi.username, pi.password};
			return (string)myType.InvokeMember(mi.Name,BindingFlags.DeclaredOnly | 
				BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance |
				BindingFlags.InvokeMethod,null,null,argz);
		}
		private void MakeBMP(Font myFont,LinearGradientBrush myBrush,float x, float y)
		{
			Bitmap myBitmap = new Bitmap(tp.firstpath);
			Graphics g = Graphics.FromImage(myBitmap);
			
			//g.DrawString("Look at this text!", myFont, myBrush, new RectangleF(10, 10, 100, 200));
			g.DrawString(GetStat(), myFont, myBrush, x, y);
			File.Delete(tp.secondpath);
			myBitmap.Save(tp.secondpath);
		}
		
		private void RedrawDesktop()
		{
			LinearGradientBrush myBrush = new LinearGradientBrush(ClientRectangle,
				pm.DeskColor1,pm.DeskColor2,pm.DeskEffect);
			MakeBMP(pm.DeskFont,myBrush,pm.DeskX,pm.DeskY);
			WinAPI.SystemParametersInfo(20, 0, tp.secondpath,  0x1 | 0x2 );
		}
		
		private void RestoreDesktop()
		{
			WinAPI.SystemParametersInfo(20, 0,tp.firstpath,  0x1 | 0x2 );
			File.Delete(tp.secondpath);
		}
		private void Update_curas()
		{
			curas = this.pi.GetAssembly();
		}
		#endregion

		private void showPluginList_Click(object sender, System.EventArgs e)
		{
			ChoosePlugin c = new ChoosePlugin();
			c.ShowDialog(this);
			
		}
	}
}
