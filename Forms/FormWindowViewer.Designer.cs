namespace Bmse.Forms
{
	partial class FormWindowViewer
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
			this.lstViewer = new System.Windows.Forms.ListBox();
			this.cmdDelete = new System.Windows.Forms.Button();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.fraViewer = new System.Windows.Forms.GroupBox();
			this.cmdViewerPath = new System.Windows.Forms.Button();
			this.txtStop = new System.Windows.Forms.TextBox();
			this.txtPlay = new System.Windows.Forms.TextBox();
			this.txtPlayAll = new System.Windows.Forms.TextBox();
			this.txtViewerName = new System.Windows.Forms.TextBox();
			this.lblViewerPath = new System.Windows.Forms.Label();
			this.lblNotice = new System.Windows.Forms.Label();
			this.lblStop = new System.Windows.Forms.Label();
			this.lblPlay = new System.Windows.Forms.Label();
			this.lblPlayAll = new System.Windows.Forms.Label();
			this.lblViewerName = new System.Windows.Forms.Label();
			this.txtViewerPath = new System.Windows.Forms.TextBox();
			this.fraViewer.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstViewer
			// 
			this.lstViewer.FormattingEnabled = true;
			this.lstViewer.ItemHeight = 12;
			this.lstViewer.Location = new System.Drawing.Point(8, 12);
			this.lstViewer.Name = "lstViewer";
			this.lstViewer.Size = new System.Drawing.Size(141, 244);
			this.lstViewer.TabIndex = 0;
			// 
			// cmdDelete
			// 
			this.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdDelete.Location = new System.Drawing.Point(36, 264);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(53, 21);
			this.cmdDelete.TabIndex = 1;
			this.cmdDelete.Text = "削除";
			this.cmdDelete.UseVisualStyleBackColor = true;
			// 
			// cmdAdd
			// 
			this.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdAdd.Location = new System.Drawing.Point(96, 264);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(53, 21);
			this.cmdAdd.TabIndex = 1;
			this.cmdAdd.Text = "追加";
			this.cmdAdd.UseVisualStyleBackColor = true;
			// 
			// cmdOK
			// 
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(216, 292);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(97, 25);
			this.cmdOK.TabIndex = 1;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			// 
			// cmdCancel
			// 
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(320, 292);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(73, 25);
			this.cmdCancel.TabIndex = 1;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// fraViewer
			// 
			this.fraViewer.Controls.Add(this.lblNotice);
			this.fraViewer.Controls.Add(this.lblViewerName);
			this.fraViewer.Controls.Add(this.lblPlayAll);
			this.fraViewer.Controls.Add(this.lblPlay);
			this.fraViewer.Controls.Add(this.lblStop);
			this.fraViewer.Controls.Add(this.lblViewerPath);
			this.fraViewer.Controls.Add(this.txtViewerPath);
			this.fraViewer.Controls.Add(this.txtViewerName);
			this.fraViewer.Controls.Add(this.txtPlayAll);
			this.fraViewer.Controls.Add(this.txtPlay);
			this.fraViewer.Controls.Add(this.txtStop);
			this.fraViewer.Controls.Add(this.cmdViewerPath);
			this.fraViewer.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraViewer.Location = new System.Drawing.Point(156, 4);
			this.fraViewer.Name = "fraViewer";
			this.fraViewer.Size = new System.Drawing.Size(237, 281);
			this.fraViewer.TabIndex = 2;
			this.fraViewer.TabStop = false;
			// 
			// cmdViewerPath
			// 
			this.cmdViewerPath.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdViewerPath.Location = new System.Drawing.Point(192, 72);
			this.cmdViewerPath.Name = "cmdViewerPath";
			this.cmdViewerPath.Size = new System.Drawing.Size(37, 17);
			this.cmdViewerPath.TabIndex = 1;
			this.cmdViewerPath.Text = "参照";
			this.cmdViewerPath.UseVisualStyleBackColor = true;
			// 
			// txtStop
			// 
			this.txtStop.Location = new System.Drawing.Point(8, 204);
			this.txtStop.Name = "txtStop";
			this.txtStop.Size = new System.Drawing.Size(221, 19);
			this.txtStop.TabIndex = 2;
			// 
			// txtPlay
			// 
			this.txtPlay.Location = new System.Drawing.Point(8, 160);
			this.txtPlay.Name = "txtPlay";
			this.txtPlay.Size = new System.Drawing.Size(221, 19);
			this.txtPlay.TabIndex = 2;
			// 
			// txtPlayAll
			// 
			this.txtPlayAll.Location = new System.Drawing.Point(8, 116);
			this.txtPlayAll.Name = "txtPlayAll";
			this.txtPlayAll.Size = new System.Drawing.Size(221, 19);
			this.txtPlayAll.TabIndex = 2;
			// 
			// txtViewerName
			// 
			this.txtViewerName.Location = new System.Drawing.Point(8, 28);
			this.txtViewerName.Name = "txtViewerName";
			this.txtViewerName.Size = new System.Drawing.Size(221, 19);
			this.txtViewerName.TabIndex = 2;
			// 
			// lblViewerPath
			// 
			this.lblViewerPath.AutoSize = true;
			this.lblViewerPath.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblViewerPath.Location = new System.Drawing.Point(12, 56);
			this.lblViewerPath.Name = "lblViewerPath";
			this.lblViewerPath.Size = new System.Drawing.Size(92, 12);
			this.lblViewerPath.TabIndex = 3;
			this.lblViewerPath.Text = "実行ファイルのパス";
			// 
			// lblNotice
			// 
			this.lblNotice.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblNotice.Location = new System.Drawing.Point(8, 232);
			this.lblNotice.Name = "lblNotice";
			this.lblNotice.Size = new System.Drawing.Size(221, 41);
			this.lblNotice.TabIndex = 3;
			this.lblNotice.Text = "lblNotice";
			// 
			// lblStop
			// 
			this.lblStop.AutoSize = true;
			this.lblStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblStop.Location = new System.Drawing.Point(12, 188);
			this.lblStop.Name = "lblStop";
			this.lblStop.Size = new System.Drawing.Size(75, 12);
			this.lblStop.TabIndex = 3;
			this.lblStop.Text = "「停止」の引数";
			// 
			// lblPlay
			// 
			this.lblPlay.AutoSize = true;
			this.lblPlay.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPlay.Location = new System.Drawing.Point(12, 144);
			this.lblPlay.Name = "lblPlay";
			this.lblPlay.Size = new System.Drawing.Size(141, 12);
			this.lblPlay.TabIndex = 3;
			this.lblPlay.Text = "「現在位置から再生」の引数";
			// 
			// lblPlayAll
			// 
			this.lblPlayAll.AutoSize = true;
			this.lblPlayAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPlayAll.Location = new System.Drawing.Point(12, 100);
			this.lblPlayAll.Name = "lblPlayAll";
			this.lblPlayAll.Size = new System.Drawing.Size(117, 12);
			this.lblPlayAll.TabIndex = 3;
			this.lblPlayAll.Text = "「最初から再生」の引数";
			// 
			// lblViewerName
			// 
			this.lblViewerName.AutoSize = true;
			this.lblViewerName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblViewerName.Location = new System.Drawing.Point(12, 12);
			this.lblViewerName.Name = "lblViewerName";
			this.lblViewerName.Size = new System.Drawing.Size(72, 12);
			this.lblViewerName.TabIndex = 3;
			this.lblViewerName.Text = "表示する名前";
			// 
			// txtViewerPath
			// 
			this.txtViewerPath.Location = new System.Drawing.Point(8, 72);
			this.txtViewerPath.Name = "txtViewerPath";
			this.txtViewerPath.Size = new System.Drawing.Size(181, 19);
			this.txtViewerPath.TabIndex = 2;
			// 
			// FormWindowViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 323);
			this.Controls.Add(this.fraViewer);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdAdd);
			this.Controls.Add(this.cmdDelete);
			this.Controls.Add(this.lstViewer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FormWindowViewer";
			this.Text = "FormWindowViewer";
			this.fraViewer.ResumeLayout(false);
			this.fraViewer.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox lstViewer;
		public System.Windows.Forms.Button cmdDelete;
		public System.Windows.Forms.Button cmdAdd;
		public System.Windows.Forms.Button cmdOK;
		public System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.GroupBox fraViewer;
		public System.Windows.Forms.Button cmdViewerPath;
		private System.Windows.Forms.TextBox txtStop;
		private System.Windows.Forms.TextBox txtPlay;
		private System.Windows.Forms.TextBox txtPlayAll;
		private System.Windows.Forms.TextBox txtViewerName;
		public System.Windows.Forms.Label lblViewerPath;
		public System.Windows.Forms.Label lblNotice;
		public System.Windows.Forms.Label lblStop;
		public System.Windows.Forms.Label lblPlay;
		public System.Windows.Forms.Label lblPlayAll;
		public System.Windows.Forms.Label lblViewerName;
		private System.Windows.Forms.TextBox txtViewerPath;
	}
}