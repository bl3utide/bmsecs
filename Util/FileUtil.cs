using System;
using System.Collections.Generic;
using System.Text;

namespace Bmse.Util
{
	class FileUtil
	{
		/// <summary>
		/// 指定されたパスのファイル名を取得する
		/// </summary>
		/// <param name="pathName"></param>
		/// <param name="attribute"></param>
		/// <returns></returns>
		public static string Dir(string pathName)
		{
			if (System.IO.File.Exists(pathName))
			{
				return System.IO.Path.GetFileName(pathName);
			}
			else
			{
				return "";
			}
		}

		/// <summary>
		/// 指定されたパスのファイル名一覧を取得する
		/// </summary>
		/// <param name="dir">検索するディレクトリパス</param>
		/// <param name="searchPattern">ファイル名のパターン(ワイルドカード使用可)</param>
		/// <param name="attribute"></param>
		/// <returns></returns>
		public static string[] Files(string dir, string searchPattern)
		{
			string[] files;

			files = System.IO.Directory.GetFiles(dir, searchPattern);

			for (int i = 0; i < files.Length; i++)
			{
				files[i] = Dir(files[i]);
			}

			return files;
		}
	}
}
