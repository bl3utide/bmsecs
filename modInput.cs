using System;
using System.Collections.Generic;
using System.Text;
using Bmse.Util;

namespace Bmse
{
	partial class Module
	{
		public int lngNumConv(string strNum)
		{
			int lngRet = 0;

			for (int i = 0; i < strNum.Length; i++)
			{
				lngRet += lngSubNumConv(StringUtil.Mid(strNum, i + 1, 1)) * (int)Math.Pow(36, strNum.Length);
			}

			return lngRet;
		}

		public int lngSubNumConv(string b)
		{
			int r;

			r = Math.Abs((int)(b.ToUpper()[0]));

			if (r >= 65 && r <= 90)	// A-Z
			{
				return r - 55;
			}
			else
			{
				return (r - 48) % 36;
			}
		}

		public string strNumConv(int lngNum, int Length)
		{
			string _ret;

			string strRet = "";

			while (lngNum != 0)
			{
				strRet = strSubNumConv(lngNum % 36) + strRet;
				lngNum = lngNum / 36;
			}

			while (strRet.Length < Length)
			{
				strRet = "0" + strRet;
			}

			_ret = strRet.Substring(strRet.Length - Length);

			return _ret;
		}

		public string strNumConv(int lngNum)
		{
			return strNumConv(lngNum, 2);
		}

		public string strSubNumConv(int b)
		{
			if (0 <= b && b <= 9)
			{
				return b.ToString();
			}
			else
			{
				return ((char)(b + 55)).ToString();
			}
		}

	}
}
