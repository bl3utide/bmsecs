using System;
using System.Collections.Generic;
using System.Text;

namespace Bmse
{
	partial class Module
	{
		public string NumConv(int lngNum, int Length)
		{
			string _ret;

			string strRet = "";

			while (lngNum != 0)
			{
				strRet = SubNumConv(lngNum % 36) + strRet;
				lngNum = lngNum / 36;
			}

			while (strRet.Length < Length)
			{
				strRet = "0" + strRet;
			}

			_ret = strRet.Substring(strRet.Length - Length);

			return _ret;
		}

		public string NumConv(int lngNum)
		{
			return NumConv(lngNum, 2);
		}

		public string SubNumConv(int b)
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
