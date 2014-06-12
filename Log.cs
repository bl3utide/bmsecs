using System;
using System.Collections.Generic;
using System.Text;

namespace Bmse
{
	class Log
	{
		private string[] array;
		private int pos;
		private int max;

		public Log()
		{
			array = new string[1];
			pos = 0;
			max = 0;
		}

		/// <summary>
		/// データの追加
		/// </summary>
		/// <param name="str"></param>
		public void AddData(ref string str)
		{
			array[pos] = str;

			pos++;
			max = pos;

			if(array.Length - 1 < max)
			{
				Array.Resize<string>(ref array, array.Length * 2);
			}

			// TODO: frmMain.SaveChanges
		}

		/// <summary>
		/// データの取得
		/// </summary>
		/// <returns></returns>
		public string GetData()
		{
			return array[pos - 1];
		}

		/// <summary>
		/// 現在位置の取得
		/// </summary>
		/// <returns></returns>
		public int GetPos
		{
			get
			{
				return pos;
			}
			set
			{
				this.pos = value;
			}
		}

		/// <summary>
		/// 進む
		/// </summary>
		public void Forward()
		{
			pos++;

			if (pos > max)
			{
				pos = max;
			}
		}

		/// <summary>
		/// 戻る
		/// </summary>
		public void Back()
		{
			pos--;

			if (pos < 0)
			{
				pos = 0;
			}
		}

		/// <summary>
		/// 最大サイズの取得
		/// </summary>
		/// <returns></returns>
		public int Max
		{
			get
			{
				return max;
			}
			set
			{
				this.max = value;
			}
		}

		/// <summary>
		/// 使用しているメモリ量の取得
		/// </summary>
		/// <returns></returns>
		public int GetBufferSize()
		{
			int i;
			int ret = 0;

			for (i = 0; i < array.Length; i++)
			{
				ret += array[i].Length;
			}

			return ret;
		}
	}
}
