using System;
using System.Windows.Forms;
using Bmse.Common;

namespace Bmse.Forms
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
			InitializeItemData();
		}

		public void InitializeItemData()
		{
			cboVScroll.ComboBox.BindingContext = new BindingContext();
			cboVScroll.ComboBox.DataSource = DataSource.DsListVScroll;
			cboVScroll.ComboBox.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboVScroll.ComboBox.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispHeight.ComboBox.BindingContext = new BindingContext();
			cboDispHeight.ComboBox.DataSource = DataSource.DsListDispHeight;
			cboDispHeight.ComboBox.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispHeight.ComboBox.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispWidth.ComboBox.BindingContext = new BindingContext();
			cboDispWidth.ComboBox.DataSource = DataSource.DsListDispWidth;
			cboDispWidth.ComboBox.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispWidth.ComboBox.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispGridMain.ComboBox.BindingContext = new BindingContext();
			cboDispGridMain.ComboBox.DataSource = DataSource.DsListDispGridMain;
			cboDispGridMain.ComboBox.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispGridMain.ComboBox.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispGridSub.ComboBox.BindingContext = new BindingContext();
			cboDispGridSub.ComboBox.DataSource = DataSource.DsListDispGridSub;
			cboDispGridSub.ComboBox.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispGridSub.ComboBox.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboPlayer.BindingContext = new BindingContext();
			cboPlayer.DataSource = DataSource.DsListPlayer;
			cboPlayer.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboPlayer.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboPlayLevel.BindingContext = new BindingContext();
			cboPlayLevel.DataSource = DataSource.DsListPlayLevel;
			cboPlayLevel.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboPlayLevel.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboPlayRank.BindingContext = new BindingContext();
			cboPlayRank.DataSource = DataSource.DsListPlayRank;
			cboPlayRank.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboPlayRank.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispFrame.BindingContext = new BindingContext();
			cboDispFrame.DataSource = DataSource.DsListDispFrame;
			cboDispFrame.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispFrame.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispSC1P.BindingContext = new BindingContext();
			cboDispSC1P.DataSource = DataSource.DsListDispSC1P;
			cboDispSC1P.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispSC1P.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispSC2P.BindingContext = new BindingContext();
			cboDispSC2P.DataSource = DataSource.DsListDispSC2P;
			cboDispSC2P.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispSC2P.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispKey.BindingContext = new BindingContext();
			cboDispKey.DataSource = DataSource.DsListDispKey;
			cboDispKey.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispKey.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDenominator.BindingContext = new BindingContext();
			cboDenominator.DataSource = DataSource.DsListDenominator;
			cboDenominator.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDenominator.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;
		}



		public void Form_Resize(Object sender, EventArgs e)
		{
			// UNDONE: かなり簡潔に実装してしまったから不安

			const int MENUBAR_HEIGHT = 21;
			const int TOOLBAR_HEIGHT = 30;
			const int HEADER_HEIGHT = 150;
			const int LINE_HEIGHT = 2;
			const int STATUSBAR_HEIGHT = 22;
			const int DIRECTINPUT_HEIGHT = 29;
			const int HSCROLLBAR_HEIGHT = 23;

			int topLineY = 0;
			int bottomLeftLineY = 0;
			int bottomRightLineY = 0;

			// 上端を設定
			topLineY = mnuViewToolBar.Checked
				? MENUBAR_HEIGHT + TOOLBAR_HEIGHT
				: MENUBAR_HEIGHT;

			// 右側の下端を設定
			bottomRightLineY = mnuViewStatusBar.Checked
				? Module.frmMain.ClientSize.Height + 1 - STATUSBAR_HEIGHT
				: Module.frmMain.ClientSize.Height + 1;

			// 左側の下端を設定
			bottomLeftLineY = mnuViewDirectInput.Checked
				? bottomRightLineY - DIRECTINPUT_HEIGHT
				: bottomRightLineY;

			// 他のコントロールの移動
			linVertical.Top = topLineY;							// linVertical
			picMain.Top = topLineY + 3;							// 描画領域
			vsbMain.Top = topLineY + 3;							// 縦スクロールバー
			hsbMain.Top = bottomLeftLineY - HSCROLLBAR_HEIGHT;	// 横スクロールバー
			linDirectInput.Top = hsbMain.Bottom + 4;			// linDirectInput
			lblDirectInput.Top = linDirectInput.Top + 5;		// ダイレクト入力枠ラベル
			cboDirectInput.Top = linDirectInput.Top + 5;		// ダイレクト入力枠入力
			cmdDirectInput.Top = linDirectInput.Top + 4;		// ダイレクト入力枠ボタン
			optChangeTop.Top = topLineY + 3;					// ヘッダ領域
			linHeader.Top = topLineY + 3 + HEADER_HEIGHT;		// linHeader
			optChangeBottom.Top = topLineY + 3 + HEADER_HEIGHT + LINE_HEIGHT + 4;	// 素材領域

			picMain.Height = bottomLeftLineY - picMain.Top - HSCROLLBAR_HEIGHT;
			vsbMain.Height = picMain.Height;
			linVertical.Height = bottomRightLineY - linVertical.Top;
			optChangeBottom.Height = bottomRightLineY - optChangeBottom.Top;

			// ステータスバーの表示・非表示
			staMain.Visible = mnuViewStatusBar.Checked;
			linStatusBar.Visible = mnuViewStatusBar.Checked;
			
			// ツールバーの表示・非表示
			tlbMenu.Visible = mnuViewToolBar.Checked;

			// ダイレクト入力枠の表示・非表示
			lblDirectInput.Visible = mnuViewDirectInput.Checked;
			cboDirectInput.Visible = mnuViewDirectInput.Checked;
			cmdDirectInput.Visible = mnuViewDirectInput.Checked;
			linDirectInput.Visible = mnuViewDirectInput.Checked;

			App.module.InitVerticalLine();
		}





				
		//////////////////////////////////////////////////////////////////////////////
		// イベントハンドラ
		//////////////////////////////////////////////////////////////////////////////

		private void Form_Activeted(Object sender, EventArgs e)
		{
			Form_Resize(null, null);
		}

		public void cboDispGridMain_SelectedIndexChanged(Object sender, EventArgs e)
		{
			//App.module.Redraw();
			picMain.Refresh();
		}

		public void cboDispGridSub_SelectedIndexChanged(Object sender, EventArgs e)
		{
			//App.module.Redraw();
			picMain.Refresh();
		}

		private void mnuViewToolBar_Click(Object sender, EventArgs e)
		{
			Form_Resize(null, null);
		}

		private void mnuViewDirectInput_Click(Object sender, EventArgs e)
		{
			Form_Resize(null, null);
		}

		private void mnuViewStatusBar_Click(Object sender, EventArgs e)
		{
			Form_Resize(null, null);
		}

		private void mnuOptionsVertical_Click(Object sender, EventArgs e)
		{
			picMain.Refresh();
		}

		private void picMain_MouseMove(Object sender, MouseEventArgs e)
		{
			/* テスト描画
			using (System.Drawing.Graphics g = picMain.CreateGraphics())
			{
				picMain.Refresh();
				g.DrawRectangle(System.Drawing.Pens.Red, e.X - 20, e.Y - 10, 20, 10);
				g.DrawRectangle(System.Drawing.Pens.Green, e.X, e.Y - 10, 20, 10);
			}*/
		}
	}
}