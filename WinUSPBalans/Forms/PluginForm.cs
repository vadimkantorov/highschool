#region Using directives

using System;
using Ini;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

#endregion

namespace WinBalans.Forms
{
	partial class PluginForm : Form
	{
		bool redr = false;
		UIParameters pm;
		Plugin p;
		SetUIParameters s = new SetUIParameters(TempParameters.ParametersInstance.UIParameters);
		Rectangle theRectangle;
		int deltax;
		int deltay;
		bool isDrag = false;
		public PluginForm(Plugin p)
		{
			InitializeComponent();

			this.p = p;
			if ((p.Name + " " + p.Version).Length <= 17)
				Text = p.Name + " " + p.Version;
			if (p == null)
				paintdMenuItem.Enabled = false;

		}
		#region Готовые методы и обработчики событий
		private void helpMenuItem_Click(object sender, System.EventArgs e)
		{
			new About().ShowDialog();
		}
		private void updateAllStatMenuItem_Click(object sender, System.EventArgs e)
		{
			UpdateAllStat();
			statLabel.Text = p.GetValue();
		}
		private void changeBordersMenuItem_Click(object sender, System.EventArgs e)
		{
			FormBorderStyle = (FormBorderStyle == FormBorderStyle.None) ? FormBorderStyle.FixedToolWindow : FormBorderStyle.None;
		}
		private void depaintdMenuItem_Click(object sender, System.EventArgs e)
		{
			if (RestoreDesktop())
			{
				paintdMenuItem.Text = "Перерисовать рабочий стол";
				paintdMenuItem.Click -= new EventHandler(depaintdMenuItem_Click);
				paintdMenuItem.Click += new System.EventHandler(paintdMenuItem_Click);
			}
		}
		private void paintdMenuItem_Click(object sender, System.EventArgs e)
		{
			if (RedrawDesktop())
			{
				paintdMenuItem.Text = "Вернуть рабочий стол";
				paintdMenuItem.Click -= new System.EventHandler(paintdMenuItem_Click);
				paintdMenuItem.Click +=new EventHandler(depaintdMenuItem_Click);
			}
		}
		private void moveButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isDrag = true;
				theRectangle = new Rectangle(Location, Size);
				ControlPaint.DrawReversibleFrame(theRectangle, BackColor, FrameStyle.Thick);
				//?
				Control sender_ = (Control)sender;
				deltax = e.X - sender_.Location.X;
				deltay = sender_.Location.Y - e.Y;
			}

		}
		private void moveButton_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (isDrag)
			{
				//Location = new Point(Cursor.Position.X - moveButton.Location.X, Cursor.Position.Y - moveButton.Location.Y);
				Location = new Point(Cursor.Position.X - deltax, Cursor.Position.Y + deltay);
				ControlPaint.DrawReversibleFrame(theRectangle, BackColor, FrameStyle.Thick);
			}
			isDrag = false;

		}
		private void moveButton_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (isDrag)
			{
				ControlPaint.DrawReversibleFrame(theRectangle, BackColor, FrameStyle.Thick);
				Control sender_ = (Control)sender;
				Point newLocation = new Point(Cursor.Position.X - deltax, Cursor.Position.Y + deltay);
				theRectangle = new Rectangle(/*((Control)sender).PointToScreen(new Point(e.X, e.Y))*/newLocation, Size);
				ControlPaint.DrawReversibleFrame(theRectangle, BackColor, FrameStyle.Thick);
			}
		}
		private void UpdateAllStat()
		{
			foreach (Plugin p in TempParameters.ParametersInstance.PluginList)
				p.Update();
		}
		#endregion
		#region Методы

		private Bitmap MakeBMP(Font myFont, LinearGradientBrush myBrush, Point po)
		{

			//if (ParametersInstance != null)
			//{
				TempParameters.Firstpath = TempParameters.Desktop;
				Bitmap myBitmap = new Bitmap(TempParameters.Firstpath);

				Graphics g = Graphics.FromImage(myBitmap);
				//g.DrawString("Look at this text", myFont, myBrush, new RectangleF(10, 10, 100, 200));
				g.DrawString(p.LastString, myFont, myBrush, po.X, po.Y);

				return myBitmap;
			//}
			//return null;
		}

		private Graphics MakeGraphics(Font myFont, LinearGradientBrush myBrush, Point po)
		{

			//if (ParametersInstance != null)
			//{
				Graphics g = WinAPI.Desktop;
				g.DrawString(p.LastString, myFont, myBrush, po.X, po.Y);
				return g;
			//}
			//return null;
		}

		private bool RedrawDesktop()
		{
			if (/*ParametersInstance != null && */TempParameters.Desktop != "" && !redr)
			{
				redr = true;
				LinearGradientBrush myBrush = new LinearGradientBrush(ClientRectangle, TempParameters.ParametersInstance.UIParameters.DesktopLabelColor1,
				TempParameters.ParametersInstance.UIParameters.DesktopLabelColor2, TempParameters.ParametersInstance.UIParameters.DesktopLabelEffect);
				Bitmap b = MakeBMP(TempParameters.ParametersInstance.UIParameters.DesktopLabelFont, myBrush, TempParameters.ParametersInstance.UIParameters.DesktopLabelCoordinates);
				File.Delete(TempParameters.Secondpath);
				b.Save(TempParameters.Secondpath);
				WinAPI.ChangeWalpaper(TempParameters.Secondpath);
				return true;
			}
			return false;
		}

		private bool RestoreDesktop()
		{
			if (redr)
			{
				redr = false; //?
				WinAPI.ChangeWalpaper(TempParameters.Firstpath);
				File.Delete(TempParameters.Secondpath);
				return true;
			}
			return false;
		}

		private void ChangeValues(UIParameters vp, Plugin p)
		{
			if (pm != null)
			{
				pm = (UIParameters)vp.Clone();

				statLabel.Font = vp.TextFont;
				statLabel.ForeColor = vp.TextColor;

				BackColor = vp.Color;
				Opacity = vp.Opacity;
			}
			if (p != null)
			{
				this.p = p;
				statLabel.Text = p.Update();
			}
		}
		#endregion


		private void exitMenuItem_Click(object sender, System.EventArgs e)
		{
			RestoreDesktop();
			//pm.Save(fp.pathToViewParameters);
			Application.Exit();
		}

		private void UpdateStat(object sender, EventArgs e)
		{
			MenuItem parent = (MenuItem)((MenuItem)sender).Parent;
			statLabel.Text = TempParameters.ParametersInstance.PluginList[parent.Text].Update();
		}

		private void PlugProp(object sender, EventArgs e)
		{
			MenuItem parent = (MenuItem)((MenuItem)sender).Parent;
			Plugin p = TempParameters.ParametersInstance.PluginList[parent.Text];
			PluginProperties pf = new PluginProperties(p);
			pf.ShowDialog(this);
			TempParameters.ParametersInstance.PluginList[TempParameters.ParametersInstance.PluginList.IndexOf(p)] = pf.GetPlugin();
		}

		private void selectPluginInContextMenu_Click(object sender, EventArgs e)
		{
			MenuItem _sender = (MenuItem)sender;
			int index = TempParameters.ParametersInstance.PluginList.GetIndex(((MenuItem)_sender.Parent).Text);
			foreach (MenuItem m in _sender.Parent.MenuItems)
				m.Checked = false;
			_sender.Checked = true;
			ChangeValues(null, (Plugin)TempParameters.ParametersInstance.PluginList[index]);
		}

		private void UpdateMenuList(object sender, EventArgs e)
		{
			if (TempParameters.ParametersInstance.PluginList.Count == 0)
			{
				pluginsMenuItem.Enabled = false;
				return;
			}
			else
				pluginsMenuItem.Enabled = true;
			pluginsMenuItem.MenuItems.Clear();
			foreach (Plugin p in TempParameters.ParametersInstance.PluginList)
			{
				MenuItem m = new MenuItem(p.Name);
					MenuItem u = new MenuItem("Обновить", new EventHandler(UpdateStat));
					MenuItem pr = new MenuItem("Свойства", new EventHandler(PlugProp));
					MenuItem cr = new MenuItem("Создать окно", new EventHandler(CreateWindow));
					MenuItem sel = new MenuItem("Кликнуть", new EventHandler(selectPluginInContextMenu_Click));

				m.Checked = p.Checked;
				m.RadioCheck = true;
				m.MenuItems.AddRange(new MenuItem[] { u, pr, cr, sel });
				pluginsMenuItem.MenuItems.Add(m);
			}
		}

		private void setUIParametersMenuItem_Click(object sender, EventArgs e)
		{
			s.ShowDialog();
			pm = s.GetViewParameters();
			ChangeValues(pm, null);
		}

		private void CreateWindow(object sender, EventArgs e)
		{
			MenuItem parent = (MenuItem)((MenuItem)sender).Parent;
			Plugin pl = TempParameters.ParametersInstance.PluginList[parent.Text];
			TempParameters.CreateWindow(pl);
		}

		private void closeFormMenuItem_Click(object sender, EventArgs e)
		{
			p.Checked = false;
			Hide();
			TempParameters.FormList.Remove(p);
		}

		private void updateStatMenuItem_Click(object sender, EventArgs e)
		{
			statLabel.Text = p.Update();
		}
	}
}