#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
#endregion

namespace WinBalans.Forms
{
	partial class SetUIParameters : Form
	{
		UIParameters v1;
		UIParameters v2;
		//TODO: Наклепать цветные кнопки
		public SetUIParameters(UIParameters v)
		{
			InitializeComponent();
			v2 = (UIParameters)v.Clone();
			v1 = (UIParameters)v.Clone();
		}
		public UIParameters GetViewParameters()
		{
			return v2;
		}
		private void color1Chose_Click_1(object sender, EventArgs e)
		{
			colorDialog.Color = v2.DesktopLabelColor1;
			colorDialog.ShowDialog();
			v2.DesktopLabelColor1 = colorDialog.Color;

			color1ChooseButton.BackColor = colorDialog.Color;
			color1ChooseButton.ForeColor = (colorDialog.Color.GetBrightness() < 0.5) ? Color.White : Color.Black;
		}

		private void color2Chose_Click(object sender, EventArgs e)
		{
			colorDialog.Color = v2.DesktopLabelColor2;
			colorDialog.ShowDialog();
			v2.DesktopLabelColor2 = colorDialog.Color;
			
			color2ChooseButton.BackColor = colorDialog.Color;
			color2ChooseButton.ForeColor = (colorDialog.Color.GetBrightness() < 0.5) ? Color.White : Color.Black;
		}

		private void mainTextColor_Click(object sender, EventArgs e)
		{
			colorDialog.Color = v2.TextColor;
			colorDialog.ShowDialog();
			v2.TextColor = colorDialog.Color;
			
			mainTextColorChooseButton.BackColor = colorDialog.Color;
			mainTextColorChooseButton.ForeColor = (colorDialog.Color.GetBrightness() < 0.5) ? Color.White : Color.Black;
		}

		private void colorChooseButton_Click(object sender, EventArgs e)
		{
			colorDialog.Color = v2.Color;
			colorDialog.ShowDialog();
			v2.Color = colorDialog.Color;

			colorChooseButton.BackColor = colorDialog.Color;
			colorChooseButton.ForeColor = (colorDialog.Color.GetBrightness() < 0.5) ? Color.White : Color.Black;
		}

		private void mainFontChoose_Click(object sender, EventArgs e)
		{
			fontDialog.Font = v2.TextFont;
			fontDialog.ShowDialog();
			v2.TextFont = fontDialog.Font;

			mainTextFontChooseButton.Font = new Font(fontDialog.Font.FontFamily, 8.25F, fontDialog.Font.Style, GraphicsUnit.Point, (byte)204);
		}

		private void chooseDeskFontButton_Click(object sender, EventArgs e)
		{
			fontDialog.Font = v2.DesktopLabelFont;
			fontDialog.ShowDialog();
			v2.DesktopLabelFont = fontDialog.Font;

			desktopChooseFontButton.Font = new Font(fontDialog.Font.FontFamily, 8.25F, fontDialog.Font.Style, GraphicsUnit.Point, (byte)204);
		}

		private void deskXnumeric_ValueChanged(object sender, EventArgs e)
		{
			v2.DesktopLabelCoordinates = new Point((int)deskXnumeric.Value, v2.DesktopLabelCoordinates.Y);
		}

		private void deskYnumeric_ValueChanged(object sender, EventArgs e)
		{
			v2.DesktopLabelCoordinates = new Point(v2.DesktopLabelCoordinates.X, (int)deskYnumeric.Value);
		}

		private void opacityNumeric_ValueChanged(object sender, EventArgs e)
		{
			v2.Opacity = (double)opacityNumeric.Value;
		}
		private void setDefaultsButton_Click(object sender, EventArgs e)
		{
			ChangeValues(UIParameters.Default);
			//v2 = UIParameters.Default;
		}

		private void Exit(object sender, EventArgs e)
		{
			//if (DialogResult == DialogResult.Cancel) ChangeValues(v1);
			Hide();
		}
		private void ChangeValues(UIParameters v)
		{
			v2 = (UIParameters)v.Clone();
			
			color1ChooseButton.BackColor = v.DesktopLabelColor1;
			color1ChooseButton.ForeColor = (colorDialog.Color.GetBrightness() < 0.5) ? Color.White : Color.Black;

			color2ChooseButton.BackColor = v.DesktopLabelColor2;
			color2ChooseButton.ForeColor = (colorDialog.Color.GetBrightness() < 0.5) ? Color.White : Color.Black;

			colorChooseButton.BackColor = v.Color;
			colorChooseButton.ForeColor = (colorDialog.Color.GetBrightness() < 0.5) ? Color.White : Color.Black;

			mainTextColorChooseButton.BackColor = v.TextColor;
			mainTextColorChooseButton.ForeColor = (colorDialog.Color.GetBrightness() < 0.5) ? Color.White : Color.Black;

			mainTextFontChooseButton.Font = new Font(v.TextFont.FontFamily, 8.25F, v.TextFont.Style, GraphicsUnit.Point, (byte)204);
			desktopChooseFontButton.Font = new Font(v.DesktopLabelFont.FontFamily, 8.25F, v.DesktopLabelFont.Style, GraphicsUnit.Point, (byte)204);


			opacityNumeric.Value = (decimal)v2.Opacity;
			deskXnumeric.Value = (decimal)v2.DesktopLabelCoordinates.X;
			deskYnumeric.Value = (decimal)v2.DesktopLabelCoordinates.Y;
			switch (v2.DesktopLabelEffect)
			{
				case LinearGradientMode.BackwardDiagonal:
					desktopLabelEffectListBox.SelectedIndex = 0;
					break;
				case LinearGradientMode.ForwardDiagonal:
					desktopLabelEffectListBox.SelectedIndex = 1;
					break;
				case LinearGradientMode.Horizontal:
					desktopLabelEffectListBox.SelectedIndex = 2;
					break;
				case LinearGradientMode.Vertical:
					desktopLabelEffectListBox.SelectedIndex = 3;
					break;
			}
		}

		private void SetUIParameters_VisibleChanged(object sender, EventArgs e)
		{
			if (Visible == false)
				if (DialogResult == DialogResult.OK)
					v1 = (UIParameters)v2.Clone();
				else
					v2 = (UIParameters)v1.Clone();

			else
				ChangeValues(v2);
		}

		private void desktopLabelEffectListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (desktopLabelEffectListBox.SelectedItem.ToString())
			{
				case "BackwardDiagonal":
					v2.DesktopLabelEffect = LinearGradientMode.BackwardDiagonal;
					break;
				case "ForwardDiagonal":
					v2.DesktopLabelEffect = LinearGradientMode.ForwardDiagonal;
					break;
				case "Horizontal":
					v2.DesktopLabelEffect = LinearGradientMode.Horizontal;
					break;
				case "Vertical":
					v2.DesktopLabelEffect = LinearGradientMode.Vertical;
					break;
			}
		}
	}
}