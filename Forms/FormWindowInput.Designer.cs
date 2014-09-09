namespace Bmse.Forms
{
	partial class FormWindowInput
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
			this.lblMainDisp = new System.Windows.Forms.Label();
			this.txtMain = new System.Windows.Forms.TextBox();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdDecide = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblMainDisp
			// 
			this.lblMainDisp.AutoSize = true;
			this.lblMainDisp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblMainDisp.Location = new System.Drawing.Point(8, 4);
			this.lblMainDisp.Name = "lblMainDisp";
			this.lblMainDisp.Size = new System.Drawing.Size(64, 12);
			this.lblMainDisp.TabIndex = 0;
			this.lblMainDisp.Text = "lblMainDisp";
			// 
			// txtMain
			// 
			this.txtMain.Location = new System.Drawing.Point(4, 44);
			this.txtMain.Name = "txtMain";
			this.txtMain.Size = new System.Drawing.Size(257, 19);
			this.txtMain.TabIndex = 1;
			// 
			// cmdCancel
			// 
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(198, 66);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(62, 19);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += cmdCancel_Click;
			// 
			// cmdDecide
			// 
			this.cmdDecide.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdDecide.Location = new System.Drawing.Point(104, 66);
			this.cmdDecide.Name = "cmdDecide";
			this.cmdDecide.Size = new System.Drawing.Size(89, 19);
			this.cmdDecide.TabIndex = 2;
			this.cmdDecide.Text = "OK";
			this.cmdDecide.UseVisualStyleBackColor = true;
			this.cmdDecide.Click += cmdDecide_Click;
			// 
			// FormWindowInput
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(266, 89);
			this.Controls.Add(this.cmdDecide);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.txtMain);
			this.Controls.Add(this.lblMainDisp);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FormWindowInput";
			this.ShowInTaskbar = false;
			this.Text = "入力フォーム";
			this.Activated += Form_Activated;
			this.FormClosing += Form_Unload;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.Label lblMainDisp;
		public System.Windows.Forms.TextBox txtMain;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdDecide;
	}
}