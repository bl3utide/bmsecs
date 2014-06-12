using System;
using System.Collections.Generic;
using System.Text;

namespace Bmse.Common
{
	/// <summary>
	/// コントロール内部の数値と表示文字列を表現するクラス
	/// </summary>
	class Ds
	{
		private int value;
		private string text;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="value">実際値</param>
		/// <param name="text">表示値</param>
		public Ds(int value, string text)
		{
			this.value = value;
			this.text = text;
		}

		/// <summary>
		/// 実際値を取得または設定します。
		/// </summary>
		public int Value
		{
			get { return this.value; }
			set { this.value = value; }
		}

		/// <summary>
		/// 表示値を取得または設定します。
		/// </summary>
		public string Text
		{
			get { return this.text; }
			set { this.text = value; }
		}
	}
}
