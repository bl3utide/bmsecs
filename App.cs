using System;
using System.Collections.Generic;
using System.Text;

namespace Bmse
{
	class App
	{
		public static Module module;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			module = new Module();
			module.MyMain();
		}
	}
}
