namespace Bmse.Forms
{
	partial class FormWindowAbout
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
			this.picMain = new System.Windows.Forms.PictureBox();
			this.tmrMain = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
			this.SuspendLayout();
			// 
			// picMain
			// 
			this.picMain.Image = global::Bmse.Properties.Resources.bmse_title;
			this.picMain.Location = new System.Drawing.Point(0, 0);
			this.picMain.Name = "picMain";
			this.picMain.Size = new System.Drawing.Size(526, 196);
			this.picMain.TabIndex = 0;
			this.picMain.TabStop = false;
			// 
			// FormWindowAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(526, 196);
			this.Controls.Add(this.picMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FormWindowAbout";
			this.ShowInTaskbar = false;
			this.Text = "FormWindowAbout";
			((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picMain;
		private System.Windows.Forms.Timer tmrMain;
	}
}