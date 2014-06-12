namespace Bmse.Forms
{
	partial class FormWindowTips
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
			this.components = new System.ComponentModel.Container();
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.chkNextDisp = new System.Windows.Forms.CheckBox();
			this.cmdNext = new System.Windows.Forms.Button();
			this.cmdClose = new System.Windows.Forms.Button();
			this.tmrMain = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// picIcon
			// 
			this.picIcon.Image = global::Bmse.Properties.Resources.tips;
			this.picIcon.Location = new System.Drawing.Point(0, 0);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(32, 64);
			this.picIcon.TabIndex = 0;
			this.picIcon.TabStop = false;
			// 
			// chkNextDisp
			// 
			this.chkNextDisp.AutoSize = true;
			this.chkNextDisp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkNextDisp.Location = new System.Drawing.Point(8, 236);
			this.chkNextDisp.Name = "chkNextDisp";
			this.chkNextDisp.Size = new System.Drawing.Size(146, 17);
			this.chkNextDisp.TabIndex = 1;
			this.chkNextDisp.Text = "Launch at next startup";
			this.chkNextDisp.UseVisualStyleBackColor = true;
			// 
			// cmdNext
			// 
			this.cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdNext.Location = new System.Drawing.Point(204, 232);
			this.cmdNext.Name = "cmdNext";
			this.cmdNext.Size = new System.Drawing.Size(101, 25);
			this.cmdNext.TabIndex = 2;
			this.cmdNext.Text = "次へ";
			this.cmdNext.UseVisualStyleBackColor = true;
			// 
			// cmdClose
			// 
			this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdClose.Location = new System.Drawing.Point(312, 232);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(97, 25);
			this.cmdClose.TabIndex = 2;
			this.cmdClose.Text = "閉じる";
			this.cmdClose.UseVisualStyleBackColor = true;
			// 
			// FormWindowTips
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(418, 264);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.cmdNext);
			this.Controls.Add(this.chkNextDisp);
			this.Controls.Add(this.picIcon);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FormWindowTips";
			this.ShowInTaskbar = false;
			this.Text = "BMSE Tips (Sorry Japanese Language Only!!!!!!!111)";
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.CheckBox chkNextDisp;
		private System.Windows.Forms.Button cmdNext;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.Timer tmrMain;
	}
}