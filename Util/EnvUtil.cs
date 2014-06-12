using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Bmse.Util
{
	class EnvUtil
	{
		public static string GetCommand()
		{
			int cmd_num = System.Environment.GetCommandLineArgs().Length;
			if (cmd_num == 1)
			{
				return "";
			}
			string[] cmds = new string[cmd_num];
			Array.Copy(System.Environment.GetCommandLineArgs(), 1, cmds, 0, cmd_num - 1);
			return string.Join(" ", cmds);
		}

		public static bool Shift
		{
			get
			{
				return (System.Windows.Forms.Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			}
		}

		public static bool Control
		{
			get
			{
				return (System.Windows.Forms.Control.ModifierKeys & Keys.Control) == Keys.Control;
			}
		}

		public static string AppMajor
		{
			get
			{
				return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileMajorPart.ToString();
			}
		}

		public static string AppMinor
		{
			get
			{
				return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileMinorPart.ToString();
			}
		}

		public static string AppRevision
		{
			get
			{
				return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileBuildPart.ToString();
			}
		}
	}
}
