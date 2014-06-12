using System;
using System.Collections.Generic;
using System.Text;

namespace Bmse.Util
{
	class StringUtil
	{
		/// -----------------------------------------------------------------------------------
		/// <summary>
		///     文字列の左端から指定された文字数分の文字列を返します。</summary>
		/// <param name="stTarget">
		///     取り出す元になる文字列。</param>
		/// <param name="iLength">
		///     取り出す文字数。</param>
		/// <returns>
		///     左端から指定された文字数分の文字列。
		///     文字数を超えた場合は、文字列全体が返されます。</returns>
		/// -----------------------------------------------------------------------------------
		public static string Left(string stTarget, int iLength)
		{
			if (iLength <= stTarget.Length)
			{
				return stTarget.Substring(0, iLength);
			}

			return stTarget;
		}

		/// -----------------------------------------------------------------------------------
		/// <summary>
		///     文字列の指定された位置以降のすべての文字列を返します。</summary>
		/// <param name="stTarget">
		///     取り出す元になる文字列。</param>
		/// <param name="iStart">
		///     取り出しを開始する位置。</param>
		/// <returns>
		///     指定された位置以降のすべての文字列。</returns>
		/// -----------------------------------------------------------------------------------
		public static string Mid(string stTarget, int iStart)
		{
			if (iStart <= stTarget.Length)
			{
				return stTarget.Substring(iStart - 1);
			}

			return string.Empty;
		}

		/// -----------------------------------------------------------------------------------
		/// <summary>
		///     文字列の指定された位置から、指定された文字数分の文字列を返します。</summary>
		/// <param name="stTarget">
		///     取り出す元になる文字列。</param>
		/// <param name="iStart">
		///     取り出しを開始する位置。</param>
		/// <param name="iLength">
		///     取り出す文字数。</param>
		/// <returns>
		///     指定された位置から指定された文字数分の文字列。
		///     文字数を超えた場合は、指定された位置からすべての文字列が返されます。</returns>
		/// -----------------------------------------------------------------------------------
		public static string Mid(string stTarget, int iStart, int iLength)
		{
			if (iStart <= stTarget.Length)
			{
				if (iStart + iLength - 1 <= stTarget.Length)
				{
					return stTarget.Substring(iStart - 1, iLength);
				}

				return stTarget.Substring(iStart - 1);
			}

			return string.Empty;
		}

		/// -----------------------------------------------------------------------------------
		/// <summary>
		///     文字列の右端から指定された文字数分の文字列を返します。</summary>
		/// <param name="stTarget">
		///     取り出す元になる文字列。</param>
		/// <param name="iLength">
		///     取り出す文字数。</param>
		/// <returns>
		///     右端から指定された文字数分の文字列。
		///     文字数を超えた場合は、文字列全体が返されます。</returns>
		/// -----------------------------------------------------------------------------------
		public static string Right(string stTarget, int iLength)
		{
			if (iLength <= stTarget.Length)
			{
				return stTarget.Substring(stTarget.Length - iLength);
			}

			return stTarget;
		}
	}
}
