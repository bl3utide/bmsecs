using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bmse.Forms
{
	public partial class FormWindowInput : Form
	{
		public FormWindowInput()
		{
			InitializeComponent();
		}






		//////////////////////////////////////////////////////////////////////////////
		// イベントハンドラ
		//////////////////////////////////////////////////////////////////////////////

		private void cmdCancel_Click(Object sender, EventArgs e)
		{
			Form_Unload(null, null);
		}

		private void cmdDecide_Click(Object sender, EventArgs e)
		{
			Module.frmWindowInput.Hide();

			Module.frmMain.picMain.Focus();
		}

		private void Form_Activated(Object sender, EventArgs e)
		{
			txtMain.SelectionStart = 0;

			txtMain.SelectionLength = txtMain.Text.Length;

			lblMainDisp.AutoSize = true;

			// UNDONE: ラベル、テキストボックス、ボタンのMoveは要らんのじゃないか？

			this.Location = new Point(Module.frmMain.Left + (Module.frmMain.Width - this.Width) / 2,
										Module.frmMain.Top + (Module.frmMain.Height - this.Height) / 2);

			txtMain.Focus();
		}

		private void Form_Unload(Object sender, EventArgs e)
		{
			txtMain.Text = "";

			Module.frmWindowInput.Hide();

			Module.frmMain.picMain.Focus();
		}
	}
}
