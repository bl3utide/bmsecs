using System;
using System.Collections.Generic;
using System.Text;

namespace Bmse.Common
{
	class CommonConst
	{
		//----------------------------------------------------------------------
		// XML関連
		//----------------------------------------------------------------------
		private static string SUFFIX_XML = "*.xml";

		public static string XML_COMMON = "common.xml";
		public static string XML_VIEWER = "viewer.xml";

		public static string XMLROOT_COMMON = "common";
		public static string XMLROOT_VIEWER = "viewer";

		public static string CONFDIR_LANG = "lang\\";
		public static string CONFDIR_THEME = "theme\\";

		public static string ALL_LANG_FILES = CONFDIR_LANG + SUFFIX_XML;
		public static string ALL_THEME_FILES = CONFDIR_THEME + SUFFIX_XML;



		//----------------------------------------------------------------------
		// フォーム関連
		//----------------------------------------------------------------------
		public static string CBO_VALUE_MEMBER = "Value";
		public static string CBO_DISPLAY_MEMBER = "Text";

		
	}
}
