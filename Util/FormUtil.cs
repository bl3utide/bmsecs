using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Bmse.Common;

namespace Bmse.Util
{
	class FormUtil
	{
		/// <summary>
		/// コンボボックスにおける指定インデックスの実際値を取得します。
		/// </summary>
		/// <param name="control"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public static int ItemData(ComboBox control, int index)
		{
			List<Ds> ds = (List<Ds>)control.DataSource;

			if (ds != null)
			{
				return ds[index].Value;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// コンボボックスの指定インデックスに新しいデータを挿入します。
		/// </summary>
		/// <param name="control"></param>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <param name="text"></param>
		public static void AddItem(ComboBox control, int index, int value, string text)
		{
			List<Ds> ds = (List<Ds>)control.DataSource;

			if (ds != null)
			{
				ds.Insert(index, new Ds(value, text));
			}
		}
	}
}
