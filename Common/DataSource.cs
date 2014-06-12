using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Bmse.Common
{
	class DataSource
	{
		/// <summary>
		/// 指定インデックスの実際値を取得します。
		/// </summary>
		/// <param name="control">DataSourceにDSリストがあるコンボボックス</param>
		/// <param name="index">インデックス</param>
		/// <returns>実際値</returns>
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
		/// 指定インデックスの実際値を取得します。
		/// </summary>
		/// <param name="control">DataSourceにDSリストがあるコンボボックス</param>
		/// <param name="index">インデックス</param>
		/// <returns>実際値</returns>
		public static int ItemData(ToolStripComboBox control, int index)
		{
			List<Ds> ds = (List<Ds>)control.ComboBox.DataSource;

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
		/// コンボボックスのDataSourceを更新します。
		/// </summary>
		/// <param name="combobox"></param>
		public static void Update(ComboBox combobox)
		{
			object tmp = combobox.DataSource;
			combobox.DataSource = null;
			combobox.DataSource = tmp;
			combobox.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			combobox.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;
		}

		/// <summary>
		/// コンボボックスのDataSourceを更新します。
		/// </summary>
		/// <param name="combobox"></param>
		public static void Update(ToolStripComboBox combobox)
		{
			object tmp = combobox.ComboBox.DataSource;
			combobox.ComboBox.DataSource = null;
			combobox.ComboBox.DataSource = tmp;
			combobox.ComboBox.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			combobox.ComboBox.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;
		}


		public static List<Ds> DsListVScroll = new List<Ds>(new Ds[] {
			new Ds(1, "1")
		});

		public static List<Ds> DsListDispHeight = new List<Ds>(new Ds[] {
			new Ds(50, "x0.5"),
			new Ds(100, "x1.0"),
			new Ds(150, "x1.5"),
			new Ds(200, "x2.0"),
			new Ds(250, "x2.5"),
			new Ds(300, "x3.0"),
			new Ds(350, "x3.5"),
			new Ds(400, "x4.0"),
			new Ds(1000, "...")
		});

		public static List<Ds> DsListDispWidth = new List<Ds>(new Ds[] {
			new Ds(50, "x0.5"),
			new Ds(100, "x1.0"),
			new Ds(150, "x1.5"),
			new Ds(200, "x2.0"),
			new Ds(250, "x2.5"),
			new Ds(300, "x3.0"),
			new Ds(350, "x3.5"),
			new Ds(400, "x4.0"),
			new Ds(1000, "...")
		});

		public static List<Ds> DsListDispGridMain = new List<Ds>(new Ds[] {
			new Ds(4, "4"),
			new Ds(8, "8"),
			new Ds(16, "16"),
			new Ds(32, "32"),
			new Ds(64, "64"),
			new Ds(3, "3"),
			new Ds(6, "6"),
			new Ds(12, "12"),
			new Ds(24, "24"),
			new Ds(48, "48"),
			new Ds(0, "FREE")
		});

		public static List<Ds> DsListDispGridSub = new List<Ds>(new Ds[] {
			new Ds(2, "2"),
			new Ds(4, "4"),
			new Ds(8, "8"),
			new Ds(16, "16"),
			new Ds(3, "3"),
			new Ds(6, "6"),
			new Ds(12, "12"),
			new Ds(0, "NONE")
		});

		public static List<Ds> DsListPlayer = new List<Ds>(new Ds[] {
			new Ds(0, "1 Player"),
			new Ds(0, "2 Player"),
			new Ds(0, "Double Play"),
			new Ds(0, "9 Keys (PMS)"),
			new Ds(0, "13 Keys (Oct)"),
		});

		public static List<Ds> DsListPlayLevel = new List<Ds>(new Ds[] {
			new Ds(1, "1"),
			new Ds(2, "2"),
			new Ds(3, "3"),
			new Ds(4, "4"),
			new Ds(5, "5"),
			new Ds(6, "6"),
			new Ds(7, "7"),
			new Ds(8, "8"),
			new Ds(0, "0")
		});

		public static List<Ds> DsListPlayRank = new List<Ds>(new Ds[] {
			new Ds(0, "Very Hard"),
			new Ds(0, "Hard"),
			new Ds(0, "Normal"),
			new Ds(0, "Easy")
		});

		public static List<Ds> DsListDispFrame = new List<Ds>(new Ds[] {
			new Ds(0, "ハーフ"),
			new Ds(1, "セパレート")
		});

		public static List<Ds> DsListDispSC1P = new List<Ds>(new Ds[] {
			new Ds(0, "左"),
			new Ds(1, "右")
		});

		public static List<Ds> DsListDispSC2P = new List<Ds>(new Ds[] {
			new Ds(0, "左"),
			new Ds(1, "右")
		});

		public static List<Ds> DsListDispKey = new List<Ds>(new Ds[] {
			new Ds(0, "5Keys/10Keys"),
			new Ds(1, "7Keys/14Keys")
		});

		public static List<Ds> DsListDenominator = new List<Ds>(new Ds[] {
			new Ds(4, "4"),
			new Ds(8, "8"),
			new Ds(16, "16"),
			new Ds(32, "32"),
			new Ds(64, "64")
		});
	}
}
