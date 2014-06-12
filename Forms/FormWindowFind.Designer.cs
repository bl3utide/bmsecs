namespace Bmse.Forms
{
	partial class FormWindowFind
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.fraSearchObject = new System.Windows.Forms.GroupBox();
			this.optSearchAll = new System.Windows.Forms.RadioButton();
			this.optSearchSelect = new System.Windows.Forms.RadioButton();
			this.fraSearchMeasure = new System.Windows.Forms.GroupBox();
			this.lblMeasure = new System.Windows.Forms.Label();
			this.txtMeasureMin = new System.Windows.Forms.TextBox();
			this.txtMeasureMax = new System.Windows.Forms.TextBox();
			this.fraSearchNum = new System.Windows.Forms.GroupBox();
			this.txtNumMax = new System.Windows.Forms.TextBox();
			this.txtNumMin = new System.Windows.Forms.TextBox();
			this.lblNum = new System.Windows.Forms.Label();
			this.lblNotice = new System.Windows.Forms.Label();
			this.fraSearchGrid = new System.Windows.Forms.GroupBox();
			this.listBox0 = new System.Windows.Forms.ListBox();
			this.listBox3 = new System.Windows.Forms.ListBox();
			this.listBox2 = new System.Windows.Forms.ListBox();
			this.lstGrid1 = new System.Windows.Forms.ListBox();
			this.lblPlayer1 = new System.Windows.Forms.Label();
			this.lblPlayer2 = new System.Windows.Forms.Label();
			this.lblEtc = new System.Windows.Forms.Label();
			this.lblBGM = new System.Windows.Forms.Label();
			this.cmdInvert = new System.Windows.Forms.Button();
			this.cmdReset = new System.Windows.Forms.Button();
			this.cmdSelect = new System.Windows.Forms.Button();
			this.fraProcess = new System.Windows.Forms.GroupBox();
			this.txtReplace = new System.Windows.Forms.TextBox();
			this.cmdDecide = new System.Windows.Forms.Button();
			this.cmdClose = new System.Windows.Forms.Button();
			this.optProcessReplace = new System.Windows.Forms.RadioButton();
			this.optProcessDelete = new System.Windows.Forms.RadioButton();
			this.optProcessSelect = new System.Windows.Forms.RadioButton();
			this.fraSearchObject.SuspendLayout();
			this.fraSearchMeasure.SuspendLayout();
			this.fraSearchNum.SuspendLayout();
			this.fraSearchGrid.SuspendLayout();
			this.fraProcess.SuspendLayout();
			this.SuspendLayout();
			// 
			// fraSearchObject
			// 
			this.fraSearchObject.Controls.Add(this.optSearchAll);
			this.fraSearchObject.Controls.Add(this.optSearchSelect);
			this.fraSearchObject.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraSearchObject.Location = new System.Drawing.Point(8, 4);
			this.fraSearchObject.Name = "fraSearchObject";
			this.fraSearchObject.Size = new System.Drawing.Size(177, 61);
			this.fraSearchObject.TabIndex = 0;
			this.fraSearchObject.TabStop = false;
			this.fraSearchObject.Text = "検索対象の指定";
			// 
			// optSearchAll
			// 
			this.optSearchAll.AutoSize = true;
			this.optSearchAll.Location = new System.Drawing.Point(8, 16);
			this.optSearchAll.Name = "optSearchAll";
			this.optSearchAll.Size = new System.Drawing.Size(89, 16);
			this.optSearchAll.TabIndex = 0;
			this.optSearchAll.TabStop = true;
			this.optSearchAll.Text = "全てのオブジェ";
			this.optSearchAll.UseVisualStyleBackColor = true;
			// 
			// optSearchSelect
			// 
			this.optSearchSelect.AutoSize = true;
			this.optSearchSelect.Location = new System.Drawing.Point(8, 36);
			this.optSearchSelect.Name = "optSearchSelect";
			this.optSearchSelect.Size = new System.Drawing.Size(129, 16);
			this.optSearchSelect.TabIndex = 0;
			this.optSearchSelect.TabStop = true;
			this.optSearchSelect.Text = "選択されているオブジェ";
			this.optSearchSelect.UseVisualStyleBackColor = true;
			// 
			// fraSearchMeasure
			// 
			this.fraSearchMeasure.Controls.Add(this.lblMeasure);
			this.fraSearchMeasure.Controls.Add(this.txtMeasureMin);
			this.fraSearchMeasure.Controls.Add(this.txtMeasureMax);
			this.fraSearchMeasure.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraSearchMeasure.Location = new System.Drawing.Point(8, 68);
			this.fraSearchMeasure.Name = "fraSearchMeasure";
			this.fraSearchMeasure.Size = new System.Drawing.Size(177, 41);
			this.fraSearchMeasure.TabIndex = 0;
			this.fraSearchMeasure.TabStop = false;
			this.fraSearchMeasure.Text = "小節範囲の指定";
			// 
			// lblMeasure
			// 
			this.lblMeasure.AutoSize = true;
			this.lblMeasure.Location = new System.Drawing.Point(70, 20);
			this.lblMeasure.Name = "lblMeasure";
			this.lblMeasure.Size = new System.Drawing.Size(17, 12);
			this.lblMeasure.TabIndex = 1;
			this.lblMeasure.Text = "～";
			// 
			// txtMeasureMin
			// 
			this.txtMeasureMin.Location = new System.Drawing.Point(28, 16);
			this.txtMeasureMin.MaxLength = 3;
			this.txtMeasureMin.Name = "txtMeasureMin";
			this.txtMeasureMin.Size = new System.Drawing.Size(37, 19);
			this.txtMeasureMin.TabIndex = 0;
			this.txtMeasureMin.Text = "0";
			// 
			// txtMeasureMax
			// 
			this.txtMeasureMax.Location = new System.Drawing.Point(92, 16);
			this.txtMeasureMax.MaxLength = 3;
			this.txtMeasureMax.Name = "txtMeasureMax";
			this.txtMeasureMax.Size = new System.Drawing.Size(37, 19);
			this.txtMeasureMax.TabIndex = 0;
			this.txtMeasureMax.Text = "999";
			// 
			// fraSearchNum
			// 
			this.fraSearchNum.Controls.Add(this.txtNumMax);
			this.fraSearchNum.Controls.Add(this.txtNumMin);
			this.fraSearchNum.Controls.Add(this.lblNum);
			this.fraSearchNum.Controls.Add(this.lblNotice);
			this.fraSearchNum.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraSearchNum.Location = new System.Drawing.Point(8, 112);
			this.fraSearchNum.Name = "fraSearchNum";
			this.fraSearchNum.Size = new System.Drawing.Size(177, 77);
			this.fraSearchNum.TabIndex = 0;
			this.fraSearchNum.TabStop = false;
			this.fraSearchNum.Text = "オブジェ番号の指定";
			// 
			// txtNumMax
			// 
			this.txtNumMax.Location = new System.Drawing.Point(92, 16);
			this.txtNumMax.MaxLength = 2;
			this.txtNumMax.Name = "txtNumMax";
			this.txtNumMax.Size = new System.Drawing.Size(37, 19);
			this.txtNumMax.TabIndex = 2;
			this.txtNumMax.Text = "ZZ";
			// 
			// txtNumMin
			// 
			this.txtNumMin.Location = new System.Drawing.Point(28, 16);
			this.txtNumMin.MaxLength = 2;
			this.txtNumMin.Name = "txtNumMin";
			this.txtNumMin.Size = new System.Drawing.Size(37, 19);
			this.txtNumMin.TabIndex = 2;
			this.txtNumMin.Text = "01";
			// 
			// lblNum
			// 
			this.lblNum.AutoSize = true;
			this.lblNum.Location = new System.Drawing.Point(70, 20);
			this.lblNum.Name = "lblNum";
			this.lblNum.Size = new System.Drawing.Size(17, 12);
			this.lblNum.TabIndex = 1;
			this.lblNum.Text = "～";
			// 
			// lblNotice
			// 
			this.lblNotice.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblNotice.Location = new System.Drawing.Point(8, 39);
			this.lblNotice.Name = "lblNotice";
			this.lblNotice.Size = new System.Drawing.Size(161, 28);
			this.lblNotice.TabIndex = 0;
			this.lblNotice.Text = "This item doesn\'t influence BPM/STOP object";
			// 
			// fraSearchGrid
			// 
			this.fraSearchGrid.Controls.Add(this.listBox0);
			this.fraSearchGrid.Controls.Add(this.listBox3);
			this.fraSearchGrid.Controls.Add(this.listBox2);
			this.fraSearchGrid.Controls.Add(this.lstGrid1);
			this.fraSearchGrid.Controls.Add(this.lblPlayer1);
			this.fraSearchGrid.Controls.Add(this.lblPlayer2);
			this.fraSearchGrid.Controls.Add(this.lblEtc);
			this.fraSearchGrid.Controls.Add(this.lblBGM);
			this.fraSearchGrid.Controls.Add(this.cmdInvert);
			this.fraSearchGrid.Controls.Add(this.cmdReset);
			this.fraSearchGrid.Controls.Add(this.cmdSelect);
			this.fraSearchGrid.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraSearchGrid.Location = new System.Drawing.Point(192, 4);
			this.fraSearchGrid.Name = "fraSearchGrid";
			this.fraSearchGrid.Size = new System.Drawing.Size(237, 185);
			this.fraSearchGrid.TabIndex = 0;
			this.fraSearchGrid.TabStop = false;
			this.fraSearchGrid.Text = "列の指定";
			// 
			// listBox0
			// 
			this.listBox0.FormattingEnabled = true;
			this.listBox0.ItemHeight = 12;
			this.listBox0.Location = new System.Drawing.Point(8, 36);
			this.listBox0.Name = "listBox0";
			this.listBox0.Size = new System.Drawing.Size(53, 112);
			this.listBox0.TabIndex = 2;
			// 
			// listBox3
			// 
			this.listBox3.FormattingEnabled = true;
			this.listBox3.ItemHeight = 12;
			this.listBox3.Location = new System.Drawing.Point(176, 36);
			this.listBox3.Name = "listBox3";
			this.listBox3.Size = new System.Drawing.Size(53, 112);
			this.listBox3.TabIndex = 2;
			// 
			// listBox2
			// 
			this.listBox2.FormattingEnabled = true;
			this.listBox2.ItemHeight = 12;
			this.listBox2.Location = new System.Drawing.Point(120, 36);
			this.listBox2.Name = "listBox2";
			this.listBox2.Size = new System.Drawing.Size(53, 112);
			this.listBox2.TabIndex = 2;
			// 
			// lstGrid1
			// 
			this.lstGrid1.FormattingEnabled = true;
			this.lstGrid1.ItemHeight = 12;
			this.lstGrid1.Location = new System.Drawing.Point(64, 36);
			this.lstGrid1.Name = "lstGrid1";
			this.lstGrid1.Size = new System.Drawing.Size(53, 112);
			this.lstGrid1.TabIndex = 2;
			// 
			// lblPlayer1
			// 
			this.lblPlayer1.AutoSize = true;
			this.lblPlayer1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPlayer1.Location = new System.Drawing.Point(8, 20);
			this.lblPlayer1.Name = "lblPlayer1";
			this.lblPlayer1.Size = new System.Drawing.Size(47, 12);
			this.lblPlayer1.TabIndex = 1;
			this.lblPlayer1.Text = "Player 1";
			// 
			// lblPlayer2
			// 
			this.lblPlayer2.AutoSize = true;
			this.lblPlayer2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPlayer2.Location = new System.Drawing.Point(64, 20);
			this.lblPlayer2.Name = "lblPlayer2";
			this.lblPlayer2.Size = new System.Drawing.Size(47, 12);
			this.lblPlayer2.TabIndex = 1;
			this.lblPlayer2.Text = "Player 2";
			// 
			// lblEtc
			// 
			this.lblEtc.AutoSize = true;
			this.lblEtc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblEtc.Location = new System.Drawing.Point(176, 20);
			this.lblEtc.Name = "lblEtc";
			this.lblEtc.Size = new System.Drawing.Size(22, 12);
			this.lblEtc.TabIndex = 1;
			this.lblEtc.Text = "Etc";
			// 
			// lblBGM
			// 
			this.lblBGM.AutoSize = true;
			this.lblBGM.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblBGM.Location = new System.Drawing.Point(120, 20);
			this.lblBGM.Name = "lblBGM";
			this.lblBGM.Size = new System.Drawing.Size(30, 12);
			this.lblBGM.TabIndex = 1;
			this.lblBGM.Text = "BGM";
			// 
			// cmdInvert
			// 
			this.cmdInvert.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdInvert.Location = new System.Drawing.Point(20, 156);
			this.cmdInvert.Name = "cmdInvert";
			this.cmdInvert.Size = new System.Drawing.Size(61, 21);
			this.cmdInvert.TabIndex = 0;
			this.cmdInvert.Text = "反転";
			this.cmdInvert.UseVisualStyleBackColor = true;
			// 
			// cmdReset
			// 
			this.cmdReset.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdReset.Location = new System.Drawing.Point(84, 156);
			this.cmdReset.Name = "cmdReset";
			this.cmdReset.Size = new System.Drawing.Size(61, 21);
			this.cmdReset.TabIndex = 0;
			this.cmdReset.Text = "全解除";
			this.cmdReset.UseVisualStyleBackColor = true;
			// 
			// cmdSelect
			// 
			this.cmdSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdSelect.Location = new System.Drawing.Point(148, 156);
			this.cmdSelect.Name = "cmdSelect";
			this.cmdSelect.Size = new System.Drawing.Size(81, 21);
			this.cmdSelect.TabIndex = 0;
			this.cmdSelect.Text = "全選択";
			this.cmdSelect.UseVisualStyleBackColor = true;
			// 
			// fraProcess
			// 
			this.fraProcess.Controls.Add(this.optProcessSelect);
			this.fraProcess.Controls.Add(this.optProcessDelete);
			this.fraProcess.Controls.Add(this.optProcessReplace);
			this.fraProcess.Controls.Add(this.txtReplace);
			this.fraProcess.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraProcess.Location = new System.Drawing.Point(436, 4);
			this.fraProcess.Name = "fraProcess";
			this.fraProcess.Size = new System.Drawing.Size(125, 81);
			this.fraProcess.TabIndex = 0;
			this.fraProcess.TabStop = false;
			this.fraProcess.Text = "処理";
			// 
			// txtReplace
			// 
			this.txtReplace.Location = new System.Drawing.Point(92, 56);
			this.txtReplace.MaxLength = 2;
			this.txtReplace.Name = "txtReplace";
			this.txtReplace.Size = new System.Drawing.Size(25, 19);
			this.txtReplace.TabIndex = 0;
			this.txtReplace.Text = "01";
			// 
			// cmdDecide
			// 
			this.cmdDecide.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdDecide.Location = new System.Drawing.Point(436, 164);
			this.cmdDecide.Name = "cmdDecide";
			this.cmdDecide.Size = new System.Drawing.Size(125, 25);
			this.cmdDecide.TabIndex = 1;
			this.cmdDecide.Text = "実行";
			this.cmdDecide.UseVisualStyleBackColor = true;
			// 
			// cmdClose
			// 
			this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdClose.Location = new System.Drawing.Point(436, 136);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(125, 25);
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "閉じる";
			this.cmdClose.UseVisualStyleBackColor = true;
			// 
			// optProcessReplace
			// 
			this.optProcessReplace.AutoSize = true;
			this.optProcessReplace.Location = new System.Drawing.Point(8, 58);
			this.optProcessReplace.Name = "optProcessReplace";
			this.optProcessReplace.Size = new System.Drawing.Size(81, 16);
			this.optProcessReplace.TabIndex = 1;
			this.optProcessReplace.TabStop = true;
			this.optProcessReplace.Text = "置換  番号:";
			this.optProcessReplace.UseVisualStyleBackColor = true;
			// 
			// optProcessDelete
			// 
			this.optProcessDelete.AutoSize = true;
			this.optProcessDelete.Location = new System.Drawing.Point(8, 38);
			this.optProcessDelete.Name = "optProcessDelete";
			this.optProcessDelete.Size = new System.Drawing.Size(47, 16);
			this.optProcessDelete.TabIndex = 1;
			this.optProcessDelete.TabStop = true;
			this.optProcessDelete.Text = "削除";
			this.optProcessDelete.UseVisualStyleBackColor = true;
			// 
			// optProcessSelect
			// 
			this.optProcessSelect.AutoSize = true;
			this.optProcessSelect.Location = new System.Drawing.Point(8, 18);
			this.optProcessSelect.Name = "optProcessSelect";
			this.optProcessSelect.Size = new System.Drawing.Size(47, 16);
			this.optProcessSelect.TabIndex = 1;
			this.optProcessSelect.TabStop = true;
			this.optProcessSelect.Text = "選択";
			this.optProcessSelect.UseVisualStyleBackColor = true;
			// 
			// FormWindowFind
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(566, 193);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.cmdDecide);
			this.Controls.Add(this.fraProcess);
			this.Controls.Add(this.fraSearchGrid);
			this.Controls.Add(this.fraSearchNum);
			this.Controls.Add(this.fraSearchMeasure);
			this.Controls.Add(this.fraSearchObject);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FormWindowFind";
			this.ShowInTaskbar = false;
			this.Text = "FormWindowFind";
			this.fraSearchObject.ResumeLayout(false);
			this.fraSearchObject.PerformLayout();
			this.fraSearchMeasure.ResumeLayout(false);
			this.fraSearchMeasure.PerformLayout();
			this.fraSearchNum.ResumeLayout(false);
			this.fraSearchNum.PerformLayout();
			this.fraSearchGrid.ResumeLayout(false);
			this.fraSearchGrid.PerformLayout();
			this.fraProcess.ResumeLayout(false);
			this.fraProcess.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.GroupBox fraSearchObject;
		public System.Windows.Forms.GroupBox fraSearchMeasure;
		public System.Windows.Forms.GroupBox fraSearchNum;
		public System.Windows.Forms.GroupBox fraSearchGrid;
		public System.Windows.Forms.GroupBox fraProcess;
		public System.Windows.Forms.Button cmdDecide;
		public System.Windows.Forms.Button cmdClose;
		public System.Windows.Forms.RadioButton optSearchAll;
		public System.Windows.Forms.RadioButton optSearchSelect;
		public System.Windows.Forms.Label lblMeasure;
		private System.Windows.Forms.TextBox txtMeasureMax;
		private System.Windows.Forms.TextBox txtMeasureMin;
		public System.Windows.Forms.Label lblNotice;
		public System.Windows.Forms.Label lblNum;
		private System.Windows.Forms.TextBox txtNumMin;
		private System.Windows.Forms.TextBox txtNumMax;
		public System.Windows.Forms.Button cmdInvert;
		public System.Windows.Forms.Button cmdReset;
		public System.Windows.Forms.Button cmdSelect;
		private System.Windows.Forms.Label lblBGM;
		private System.Windows.Forms.Label lblPlayer1;
		private System.Windows.Forms.Label lblPlayer2;
		private System.Windows.Forms.Label lblEtc;
		private System.Windows.Forms.ListBox lstGrid1;
		private System.Windows.Forms.ListBox listBox0;
		private System.Windows.Forms.ListBox listBox3;
		private System.Windows.Forms.ListBox listBox2;
		private System.Windows.Forms.TextBox txtReplace;
		public System.Windows.Forms.RadioButton optProcessReplace;
		public System.Windows.Forms.RadioButton optProcessDelete;
		public System.Windows.Forms.RadioButton optProcessSelect;
	}
}