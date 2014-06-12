namespace Bmse.Forms
{
	partial class FormWindowConvert
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
			this.chkSortByName = new System.Windows.Forms.CheckBox();
			this.txtExtension = new System.Windows.Forms.TextBox();
			this.chkFileRecycle = new System.Windows.Forms.CheckBox();
			this.chkDeleteFile = new System.Windows.Forms.CheckBox();
			this.chkFileNameConvert = new System.Windows.Forms.CheckBox();
			this.chkUseOldFormat = new System.Windows.Forms.CheckBox();
			this.chkListAlign = new System.Windows.Forms.CheckBox();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdDecide = new System.Windows.Forms.Button();
			this.lblNotice = new System.Windows.Forms.Label();
			this.lblExtension = new System.Windows.Forms.Label();
			this.chkDeleteUnusedFile = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// chkSortByName
			// 
			this.chkSortByName.AutoSize = true;
			this.chkSortByName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkSortByName.Location = new System.Drawing.Point(24, 184);
			this.chkSortByName.Name = "chkSortByName";
			this.chkSortByName.Size = new System.Drawing.Size(144, 17);
			this.chkSortByName.TabIndex = 0;
			this.chkSortByName.Text = "ファイル名順でソートする";
			this.chkSortByName.UseVisualStyleBackColor = true;
			// 
			// txtExtension
			// 
			this.txtExtension.Location = new System.Drawing.Point(112, 76);
			this.txtExtension.Name = "txtExtension";
			this.txtExtension.Size = new System.Drawing.Size(144, 19);
			this.txtExtension.TabIndex = 2;
			this.txtExtension.Text = "wav,mp3,bmp,jpg,gif";
			// 
			// chkFileRecycle
			// 
			this.chkFileRecycle.AutoSize = true;
			this.chkFileRecycle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkFileRecycle.Location = new System.Drawing.Point(24, 96);
			this.chkFileRecycle.Name = "chkFileRecycle";
			this.chkFileRecycle.Size = new System.Drawing.Size(204, 17);
			this.chkFileRecycle.TabIndex = 0;
			this.chkFileRecycle.Text = "ごみ箱に移動しないですぐに削除する";
			this.chkFileRecycle.UseVisualStyleBackColor = true;
			// 
			// chkDeleteFile
			// 
			this.chkDeleteFile.AutoSize = true;
			this.chkDeleteFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkDeleteFile.Location = new System.Drawing.Point(8, 48);
			this.chkDeleteFile.Name = "chkDeleteFile";
			this.chkDeleteFile.Size = new System.Drawing.Size(244, 17);
			this.chkDeleteFile.TabIndex = 0;
			this.chkDeleteFile.Text = "フォルダ内の使用していないファイルを削除 (*)";
			this.chkDeleteFile.UseVisualStyleBackColor = true;
			// 
			// chkFileNameConvert
			// 
			this.chkFileNameConvert.AutoSize = true;
			this.chkFileNameConvert.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkFileNameConvert.Location = new System.Drawing.Point(8, 224);
			this.chkFileNameConvert.Name = "chkFileNameConvert";
			this.chkFileNameConvert.Size = new System.Drawing.Size(216, 17);
			this.chkFileNameConvert.TabIndex = 0;
			this.chkFileNameConvert.Text = "ファイル名を連番 (01 - ZZ) に変換 (*)";
			this.chkFileNameConvert.UseVisualStyleBackColor = true;
			// 
			// chkUseOldFormat
			// 
			this.chkUseOldFormat.AutoSize = true;
			this.chkUseOldFormat.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkUseOldFormat.Location = new System.Drawing.Point(24, 160);
			this.chkUseOldFormat.Name = "chkUseOldFormat";
			this.chkUseOldFormat.Size = new System.Drawing.Size(228, 17);
			this.chkUseOldFormat.TabIndex = 0;
			this.chkUseOldFormat.Text = "可能なら古いフォーマット (01 - FF) を使う";
			this.chkUseOldFormat.UseVisualStyleBackColor = true;
			// 
			// chkListAlign
			// 
			this.chkListAlign.AutoSize = true;
			this.chkListAlign.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkListAlign.Location = new System.Drawing.Point(8, 136);
			this.chkListAlign.Name = "chkListAlign";
			this.chkListAlign.Size = new System.Drawing.Size(112, 17);
			this.chkListAlign.TabIndex = 0;
			this.chkListAlign.Text = "定義リストの整列";
			this.chkListAlign.UseVisualStyleBackColor = true;
			// 
			// cmdCancel
			// 
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(216, 296);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(89, 25);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "キャンセル";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// cmdDecide
			// 
			this.cmdDecide.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdDecide.Location = new System.Drawing.Point(88, 296);
			this.cmdDecide.Name = "cmdDecide";
			this.cmdDecide.Size = new System.Drawing.Size(121, 25);
			this.cmdDecide.TabIndex = 3;
			this.cmdDecide.Text = "実行";
			this.cmdDecide.UseVisualStyleBackColor = true;
			// 
			// lblNotice
			// 
			this.lblNotice.AutoSize = true;
			this.lblNotice.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblNotice.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblNotice.Location = new System.Drawing.Point(8, 264);
			this.lblNotice.Name = "lblNotice";
			this.lblNotice.Size = new System.Drawing.Size(207, 12);
			this.lblNotice.TabIndex = 4;
			this.lblNotice.Text = "(*)・・・この操作はやり直しができません";
			// 
			// lblExtension
			// 
			this.lblExtension.AutoSize = true;
			this.lblExtension.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblExtension.Location = new System.Drawing.Point(26, 80);
			this.lblExtension.Name = "lblExtension";
			this.lblExtension.Size = new System.Drawing.Size(86, 12);
			this.lblExtension.TabIndex = 4;
			this.lblExtension.Text = "検索する拡張子:";
			// 
			// chkDeleteUnusedFile
			// 
			this.chkDeleteUnusedFile.AutoSize = true;
			this.chkDeleteUnusedFile.Location = new System.Drawing.Point(8, 16);
			this.chkDeleteUnusedFile.Name = "chkDeleteUnusedFile";
			this.chkDeleteUnusedFile.Size = new System.Drawing.Size(274, 16);
			this.chkDeleteUnusedFile.TabIndex = 5;
			this.chkDeleteUnusedFile.Text = "使用していない #WAV・#BMP・#BGA の定義を消去";
			this.chkDeleteUnusedFile.UseVisualStyleBackColor = true;
			// 
			// FormWindowConvert
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(312, 330);
			this.Controls.Add(this.chkDeleteUnusedFile);
			this.Controls.Add(this.lblExtension);
			this.Controls.Add(this.lblNotice);
			this.Controls.Add(this.cmdDecide);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.txtExtension);
			this.Controls.Add(this.chkListAlign);
			this.Controls.Add(this.chkUseOldFormat);
			this.Controls.Add(this.chkFileNameConvert);
			this.Controls.Add(this.chkDeleteFile);
			this.Controls.Add(this.chkFileRecycle);
			this.Controls.Add(this.chkSortByName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FormWindowConvert";
			this.ShowInTaskbar = false;
			this.Text = "FormWindowConvert";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.CheckBox chkSortByName;
		private System.Windows.Forms.TextBox txtExtension;
		public System.Windows.Forms.CheckBox chkFileRecycle;
		public System.Windows.Forms.CheckBox chkDeleteFile;
		public System.Windows.Forms.CheckBox chkFileNameConvert;
		public System.Windows.Forms.CheckBox chkUseOldFormat;
		public System.Windows.Forms.CheckBox chkListAlign;
		public System.Windows.Forms.Button cmdCancel;
		public System.Windows.Forms.Button cmdDecide;
		public System.Windows.Forms.Label lblNotice;
		public System.Windows.Forms.Label lblExtension;
		public System.Windows.Forms.CheckBox chkDeleteUnusedFile;
	}
}