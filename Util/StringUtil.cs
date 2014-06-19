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

		/// <summary>
		/// 指定した文字列内の指定した文字列を別の文字列に置換する。
		/// </summary>
		/// <param name="input">置換する文字列のある文字列。</param>
		/// <param name="oldValue">検索文字列。</param>
		/// <param name="newValue">置換文字列。</param>
		/// <param name="count">置換する回数。負の数が指定されたときは、すべて置換する。</param>
		/// <param name="compInfo">文字列の検索に使用するCompareInfo。</param>
		/// <param name="compOptions">文字列の検索に使用するCompareOptions。</param>
		/// <returns>置換された結果の文字列。</returns>
		private static string StringReplace(
			string input, string oldValue, string newValue, int count,
			System.Globalization.CompareInfo compInfo,
			System.Globalization.CompareOptions compOptions)
		{
			if (input == null || input.Length == 0 ||
				oldValue == null || oldValue.Length == 0 ||
				count == 0)
			{
				return input;
			}

			if (compInfo == null)
			{
				compInfo = System.Globalization.CultureInfo.InvariantCulture.CompareInfo;
				compOptions = System.Globalization.CompareOptions.Ordinal;
			}

			int inputLen = input.Length;
			int oldValueLen = oldValue.Length;
			System.Text.StringBuilder buf = new System.Text.StringBuilder(inputLen);

			int currentPoint = 0;
			int foundPoint = -1;
			int currentCount = 0;

			do
			{
				//文字列を検索する
				foundPoint = compInfo.IndexOf(input, oldValue, currentPoint, compOptions);
				if (foundPoint < 0)
				{
					buf.Append(input.Substring(currentPoint));
					break;
				}

				//見つかった文字列を新しい文字列に換える
				buf.Append(input.Substring(currentPoint, foundPoint - currentPoint));
				buf.Append(newValue);

				//次の検索開始位置を取得
				currentPoint = foundPoint + oldValueLen;

				//指定回数置換したか調べる
				currentCount++;
				if (currentCount == count)
				{
					buf.Append(input.Substring(currentPoint));
					break;
				}
			}
			while (currentPoint < inputLen);

			return buf.ToString();
		}

		/// <summary>
		/// 指定した文字列内の指定した文字列を別の文字列に置換する。
		/// </summary>
		/// <param name="input">置換する文字列のある文字列。</param>
		/// <param name="oldValue">検索文字列。</param>
		/// <param name="newValue">置換文字列。</param>
		/// <param name="count">置換する回数。負の数が指定されたときは、すべて置換する。</param>
		/// <param name="ignoreCase">大文字と小文字を区別しない時はTrue。</param>
		/// <returns>置換された結果の文字列。</returns>
		public static string StringReplace(
			string input, string oldValue, string newValue, int count, bool ignoreCase)
		{
			if (ignoreCase)
			{
				return StringReplace(input, oldValue, newValue, count,
					System.Globalization.CultureInfo.InvariantCulture.CompareInfo,
					System.Globalization.CompareOptions.OrdinalIgnoreCase);
			}
			else
			{
				return StringReplace(input, oldValue, newValue, count,
					System.Globalization.CultureInfo.InvariantCulture.CompareInfo,
					System.Globalization.CompareOptions.Ordinal);
			}
		}

		/// <summary>
		/// 指定した文字列内の指定した文字列を別の文字列に置換する。
		/// </summary>
		/// <param name="input">置換する文字列のある文字列。</param>
		/// <param name="oldValue">検索文字列。</param>
		/// <param name="newValue">置換文字列。</param>
		/// <param name="count">置換する回数。負の数が指定されたときは、すべて置換する。</param>
		/// <returns>置換された結果の文字列。</returns>
		public static string StringReplace(
			string input, string oldValue, string newValue, int count)
		{
			return StringReplace(input, oldValue, newValue, count,
				System.Globalization.CultureInfo.InvariantCulture.CompareInfo,
				System.Globalization.CompareOptions.Ordinal);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		///     文字列が数値であるかどうかを返します。</summary>
		/// <param name="stTarget">
		///     検査対象となる文字列。<param>
		/// <returns>
		///     指定した文字列が数値であれば true。それ以外は false。</returns>
		/// -----------------------------------------------------------------------------
		public static bool IsNumeric(string stTarget)
		{
			double dNullable;

			return double.TryParse(
				stTarget,
				System.Globalization.NumberStyles.Any,
				null,
				out dNullable
			);
		}
	}
}
