﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Bmse;
using Bmse.Common;
using Bmse.Forms;
using Bmse.Util;


namespace Bmse
{
	public enum BGA_PARA
	{
		BGA_NUM,
		BGA_X1,
		BGA_Y1,
		BGA_X2,
		BGA_Y2,
		BGA_dX,
		BGA_dY
	}

	public enum CMD_LOG
	{
		NONE,
		OBJ_ADD,
		OBJ_DEL,
		OBJ_MOVE,
		OBJ_CHANGE,
		MSR_ADD,
		MSR_DEL,
		MSR_CHANGE,
		WAV_CHANGE,
		BMP_CHANGE,
		LIST_ALIGN,
		LIST_DEL
	}

	public enum Message
	{
		ERR_01,
		ERR_02,
		ERR_FILE_NOT_FOUND,
		ERR_LOAD_CANCEL,
		ERR_SAVE_ERROR,
		ERR_SAVE_CANCEL,
		ERR_OVERFLOW_LARGE,
		ERR_OVERFLOW_SMALL,
		ERR_OVERFLOW_BPM,
		ERR_OVERFLOW_STOP,
		ERR_APP_NOT_FOUND,
		ERR_FILE_ALREADY_EXIST,
		MSG_CONFIRM,
		MSG_FILE_CHANGED,
		MSG_INI_CHANGED,
		MSG_ALIGN_LIST,
		MSG_DELETE_FILE,
		INPUT_BPM,
		INPUT_STOP,
		INPUT_RENAME,
		INPUT_SIZE,
		Max
	}

	public struct RECT
	{
		public int left;
		public int top;
		public int right;
		public int bottom;
	}

	public struct m_udtMouse
	{
		public int x;
		public int y;
	}

	public struct m_udtDisplay
	{
		public int x;
		public int y;
		public double width;
		public double height;
		public int lngMaxX;
		public int lngMaxY;
		public int intStartMeasure;
		public int intEndMeasure;
		public int lngStartPos;
		public int lngEndPos;
		public int intMaxMeasure;		// 最大表示小節
		public int intResolution;		// 分解能
		public int intEffect;			// 画面効果
	}

	public struct m_udtBMS
	{
		public string strDir;			// ディレクトリ
		public string strFileName;		// BMSファイル名
		public int intPlayerType;		// #PLAYER
		public string strGenre;			// #GENRE
		public string strTitle;			// #TITLE
		public string strArtist;		// #ARTIST
		public double sngBPM;			// #BPM
		public int lngPlayLevel;		// #PLAYLEVEL
		public int intPlayRank;			// #RANK
		public double sngTotal;			// #TOTAL
		public int intVolume;			// #VOLWAV
		public string strStageFile;		// #STAGEFILE
		public bool blnSaveFlag;
	}

	public struct m_udtVerticalLine
	{
		public bool blnVisible;
		public int intCh;
		public string strText;
		public int intWidth;
		public int lngLeft;
		public int lngObjLeft;
		public Color lngBackColor;
		public int intLightNum;
		public int intShadowNum;
		public int intBrushNum;
		public bool blnDraw;
	}

	public struct g_udtObj
	{
		public int lngID;
		public int intCh;
		public int intAtt;
		public int intMeasure;
		public int lngHeight;
		public int lngPosition;
		public int sngValue;
		public int intSelect;
	}

	public struct m_udtMeasure
	{
		public int intLen;
		public int lngY;
	}

	public struct m_udtSelectArea
	{
		public bool blnFlag;
		public int x1;
		public int y1;
		public int x2;
		public int y2;
	}

	/// <summary>
	/// ビューワ情報
	/// </summary>
	public struct g_udtViewer
	{
		public string strAppName;
		public string strAppPath;
		public string strArgAll;
		public string strArgPlay;
		public string strArgStop;
	}

	partial class Module
	{
		// フォームのインスタンス
		public static FormMain frmMain;
		public static FormWindowAbout frmWindowAbout;
		public static FormWindowFind frmWindowFind;
		public static FormWindowInput frmWindowInput;
		public static FormWindowPreview frmWindowPreview;
		public static FormWindowTips frmWindowTips;
		public static FormWindowViewer frmWindowViewer;
		public static FormWindowConvert frmWindowConvert;

		private const string INI_VERSION = "3";
		public const string RELEASE_DATE = "2006-12-27T15:46:39";

		public const double PI = 3.14159265358979;

		public static string g_strAppTitle;
		public static m_udtMouse g_Mouse;
		public static m_udtDisplay g_disp;
		public static m_udtBMS g_BMS;
		public static m_udtVerticalLine[] g_VGrid = new m_udtVerticalLine[62];
		public static int[] g_intVGridNum = new int[133];
		public static g_udtObj[] g_Obj;
		public static int[] g_lngObjID;
		public static int g_lngIDNum;
		public static m_udtMeasure[] g_Measure = new m_udtMeasure[1000];
		public static string[] g_strWAV = new string[1296];
		public static string[] g_strBMP = new string[1296];
		public static string[] g_strBGA = new string[1296];
		public static double[] g_sngBPM = new double[1296];
		public static int[] g_lngSTOP = new int[1296];
		public static m_udtSelectArea g_SelectArea;
		public static string[] g_strLangFileName;		// 言語ファイルのパス一覧
		public static string[] g_strThemeFileName;
		public static string[] g_strStatusBar = new string[24];
		public static bool g_blnIgnoreInput;
		public static string g_strAppDir;
		public static string g_strHelpFileName;
		public static string g_strFiler;
		public static string[] g_strRecentFiles = new string[5];
		public static Log g_InputLog;
		public static g_udtViewer[] g_Viewer;
		public static string[] g_Message = new string[(int)Message.Max];

		private static int confCount = 0;

		public void MyMain()
		{
			//Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			frmMain = new FormMain();

			string retStr;
			int retInt;

			// 実行ファイルのディレクトリを設定
			string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			if (StringUtil.Right(appPath, 1).Equals("\\"))
			{
				g_strAppDir = appPath;
			}
			else
			{
				g_strAppDir = appPath + "\\";
			}

			// アプリケーションのタイトルを設定
			string appmajor = EnvUtil.AppMajor;
			string appminor = EnvUtil.AppMinor;
			string apprevision = EnvUtil.AppRevision;
			g_strAppTitle = "BMx Sequence Editor " + appmajor + "." + appminor + "." + apprevision;

			// いくつか初期化
			g_strLangFileName = new string[1];
			g_InputLog = new Log();
			g_Viewer = new g_udtViewer[2];

			// viewer設定ファイルの初期化
			if (!ConfigManager.Instance.Exist(CommonConst.XML_VIEWER))
			{
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "uBMplay", "path", "uBMplay.exe");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "uBMplay", "all", "-P -N0 <filename>");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "uBMplay", "play", "-P -N<measure> <filename>");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "uBMplay", "stop", "-S");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "WAview", "path", "C:\\Program Files\\Winamp\\Plugins\\WAview.exe");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "WAview", "all", "-Lbml <filename>");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "WAview", "play", "-N<measure>");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "WAview", "stop", "-S");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "nBMplay", "path", "nbmplay.exe");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "nBMplay", "all", "-P -N0 <filename>");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "nBMplay", "play", "-P -N<measure> <filename>");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "nBMplay", "stop", "-S");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "BMEV", "path", "BMEV.exe");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "BMEV", "all", "-P -N0 <filename>");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "BMEV", "play", "-P -N<measure> <filename>");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "BMEV", "stop", "-S");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "BMS Viewer", "path", "bmview.exe");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "BMS Viewer", "all", "-S -P -N0 <filename>");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "BMS Viewer", "play", "-S -P -N<measure> <filename>");
				ConfigManager.Instance.SetValue(CommonConst.XML_VIEWER, "BMS Viewer", "stop", "-S");
			}

			// viewer設定を読み込み、メインフォームのコンボボックスにセットする
			List<Dictionary<string, string>> list = ConfigManager.Instance.ItemList(CommonConst.XML_VIEWER);
			foreach (Dictionary<string, string> viewer in list)
			{
				if (viewer["name"].Length == 0)
				{
					continue;
				}
				else
				{
					g_Viewer[g_Viewer.Length - 1].strAppName = viewer["name"];
				}

				if (viewer["path"].Length == 0)
				{
					continue;
				}
				else
				{
					g_Viewer[g_Viewer.Length - 1].strAppPath = viewer["path"];
				}

				g_Viewer[g_Viewer.Length - 1].strArgAll = viewer["all"];
				g_Viewer[g_Viewer.Length - 1].strArgPlay = viewer["play"];
				g_Viewer[g_Viewer.Length - 1].strArgStop = viewer["stop"];

				frmMain.cboViewer.Items.Add(g_Viewer[g_Viewer.Length - 1].strAppName);
				Array.Resize(ref g_Viewer, g_Viewer.Length + 1);
			}

			Array.Resize(ref g_Viewer, frmMain.cboViewer.Items.Count + 1);

			if (frmMain.cboViewer.Items.Count == 0)
			{
				frmMain.tlbMenuPlayAll.Enabled = false;
				frmMain.tlbMenuPlay.Enabled = false;
				frmMain.tlbMenuStop.Enabled = false;
				frmMain.mnuToolsPlayAll.Enabled = false;
				frmMain.mnuToolsPlay.Enabled = false;
				frmMain.mnuToolsPlayStop.Enabled = false;
				frmMain.cboViewer.Enabled = false;
			}

			//---------------------------------------------------------------------------
			// 言語ファイル読み込み
			string[] langFiles = FileUtil.Files(g_strAppDir, CommonConst.ALL_LANG_FILES);
			retInt = 0;

			foreach (string langFile in langFiles)
			{
				if ("BMSE".Equals(ConfigManager.Instance.GetValue(CommonConst.CONFDIR_LANG + langFile, "Main", "Key", "")))
				{
					// ファイルが存在する言語をメニューに追加

					Array.Resize(ref g_strLangFileName, retInt + 1);
					g_strLangFileName[retInt] = langFile;

					ToolStripItem langMenu = new ToolStripMenuItem();

					langMenu.Text = "&" + (ConfigManager.Instance.GetValue(CommonConst.CONFDIR_LANG + langFile, "Main", "Language", langFile));

					if ("&".Equals(langMenu.Text))
					{
						langMenu.Text = "&" + langFile;
					}

					langMenu.Visible = true;

					// 各言語メニューのクリックイベントを設定
					langMenu.Click += new EventHandler((object sender, EventArgs e) =>
					{
						for (int _i = 0; _i < frmMain.mnuLanguage.DropDownItems.Count; _i++)
						{
							((ToolStripMenuItem)frmMain.mnuLanguage.DropDownItems[_i]).Checked = false;
						}

						int _index = frmMain.mnuLanguage.DropDownItems.IndexOf((ToolStripItem)sender);
						((ToolStripMenuItem)frmMain.mnuLanguage.DropDownItems[_index]).Checked = true;

						LoadLanguageFile(CommonConst.CONFDIR_LANG + g_strLangFileName[_index]);

						//Redraw();
						frmMain.picMain.Refresh();
					});

					frmMain.mnuLanguage.DropDownItems.Add(langMenu);

					retInt++;
				}
			}

			if (retInt == 0)
			{
				frmMain.mnuLanguage.Enabled = false;
			}

			//---------------------------------------------------------------------------
			// テーマファイル読み込み
			string[] themeFiles = FileUtil.Files(g_strAppDir, CommonConst.ALL_THEME_FILES);
			retInt = 0;

			foreach (string themeFile in themeFiles)
			{
				if ("BMSE".Equals(ConfigManager.Instance.GetValue(CommonConst.CONFDIR_THEME + themeFile, "Main", "Key", "")))
				{
					// ファイルが存在するテーマをメニューに追加

					Array.Resize(ref g_strThemeFileName, retInt + 1);
					g_strThemeFileName[retInt] = themeFile;

					ToolStripItem themeMenu = new ToolStripMenuItem();

					themeMenu.Text = "&" + (ConfigManager.Instance.GetValue(CommonConst.CONFDIR_THEME + themeFile, "Main", "Name", themeFile));

					if ("&".Equals(themeMenu.Text))
					{
						themeMenu.Text = "&" + themeFile;
					}

					themeMenu.Visible = true;

					// 各テーマメニューのクリックイベントを設定
					themeMenu.Click += new EventHandler((object sender, EventArgs e) =>
					{
						for (int _i = 0; _i < frmMain.mnuTheme.DropDownItems.Count; _i++)
						{
							((ToolStripMenuItem)frmMain.mnuTheme.DropDownItems[_i]).Checked = false;
						}

						int _index = frmMain.mnuTheme.DropDownItems.IndexOf((ToolStripItem)sender);
						((ToolStripMenuItem)frmMain.mnuTheme.DropDownItems[_index]).Checked = true;

						LoadThemeFile(CommonConst.CONFDIR_THEME + g_strThemeFileName[_index]);

						//Redraw();
						frmMain.picMain.Refresh();
					});

					frmMain.mnuTheme.DropDownItems.Add(themeMenu);

					retInt++;
				}
			}

			if (retInt == 0)
			{
				frmMain.mnuTheme.Enabled = false;
			}

			//---------------------------------------------------------------------------
			// 初期化
			g_BMS.intPlayerType = 1;
			g_BMS.strGenre = "";
			g_BMS.strTitle = "";
			g_BMS.strArtist = "";
			g_BMS.sngBPM = double.Parse(frmMain.txtBPM.Text);
			g_BMS.lngPlayLevel = 1;
			g_BMS.intPlayRank = 3;
			g_BMS.sngTotal = 0;
			g_BMS.intVolume = 0;
			g_BMS.blnSaveFlag = true;

			g_Obj = new g_udtObj[1];
			g_lngObjID = new int[1];
			g_lngIDNum = 0;

			for (int i = 0; i < 256 + 64; i++)
			{
				g_sngSin[i] = Math.Sin(i * PI / 128);
			}

			// 開くボタンの履歴メニューを初期化
			for (int i = 0; i < 5; i++)
			{
				frmMain.tlbMenuOpen.DropDownItems.Add("&" + (i + 1) + ":");
				frmMain.tlbMenuOpen.DropDownItems[i].Enabled = false;
				frmMain.tlbMenuOpen.DropDownItems[i].Visible = false;
			}

			// 最近使ったファイルのメニューを非表示
			for (int i = 0; i < frmMain.mnuRecentFiles.Length; i++)
			{
				frmMain.mnuRecentFiles[i].Visible = false;
			}

			frmMain.mnuLineRecent.Visible = false;
			frmMain.mnuHelpOpen.Enabled = false;

			// フォームタイトル設定
			frmMain.Text = g_strAppTitle;

			frmWindowAbout = new FormWindowAbout();
			frmWindowFind = new FormWindowFind();
			frmWindowInput = new FormWindowInput();
			frmWindowPreview = new FormWindowPreview();
			frmWindowTips = new FormWindowTips();
			frmWindowViewer = new FormWindowViewer();
			frmWindowConvert = new FormWindowConvert();

			while (true)
			{
				try
				{
					LoadConfig();
				}
				catch (Exception e)
				{
					confCount++;

					if (confCount > 5)
					{
						CleanUp(e.Message, "LoadConfig");
					}
					else
					{
						CreateConfig();
						continue;
					}
				}

				break;
			}

			frmMain.dlgMainOpen.InitialDirectory = g_strAppDir;
			frmMain.dlgMainSave.InitialDirectory = g_strAppDir;
			frmMain.dlgMainOpen.CheckFileExists = true;
			frmMain.dlgMainOpen.CheckPathExists = true;
			//frmMain.dlgMainSave.CheckFileExists = true;
			frmMain.dlgMainSave.CheckPathExists = true;
			frmMain.dlgMainSave.OverwritePrompt = true;

			g_disp.lngMaxY = frmMain.vsbMain.Maximum;	// UNDONE: スクロールバー絡みの問題
			g_disp.lngMaxX = frmMain.hsbMain.Minimum;	// UNDONE: スクロールバー絡みの問題

			frmMain.hsbMain.SmallChange = OBJ_WIDTH;
			frmMain.hsbMain.LargeChange = OBJ_WIDTH * 4;

			frmMain.cboPlayer.SelectedIndex = g_BMS.intPlayerType - 1;
			frmMain.cboPlayLevel.Text = g_BMS.lngPlayLevel.ToString();
			frmMain.cboPlayRank.SelectedIndex = g_BMS.intPlayRank;

			if (ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "Options", "UseOldFormat", false))
			{
				for (int i = 1; i <= 255; i++)
				{
					string numStr = "0" + string.Format("{0:X}", i);
					retStr = numStr.Substring(numStr.Length - 2);
					frmMain.lstWAV.Items.Insert(i - 1, "#WAV" + retStr + ":");
					frmMain.lstBMP.Items.Insert(i - 1, "#BMP" + retStr + ":");
					frmMain.lstBGA.Items.Insert(i - 1, "#BGA" + retStr + ":");
				}

				frmWindowPreview.cmdPreviewEnd.Text = "FF";
			}
			else
			{
				for (int i = 1; i <= 1295; i++)
				{
					retStr = strNumConv(i);
					frmMain.lstWAV.Items.Insert(i - 1, "#WAV" + retStr + ":");
					frmMain.lstBMP.Items.Insert(i - 1, "#BMP" + retStr + ":");
					frmMain.lstBGA.Items.Insert(i - 1, "#BGA" + retStr + ":");
				}
			}

			for (int i = 0; i < 1000; i++)
			{
				frmMain.lstMeasureLen.Items.Insert(i, "#" + i.ToString("000") + ":4/4");
				g_Measure[i].intLen = 192;
			}

			int[] chArray = { 0, 8, 9, 0, 21, 16, 11, 12, 13, 14, 15, 18, 19, 16, 0, 26, 21, 22, 23, 24, 25, 28, 29, 26, 0, 4, 7, 6, 0, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 0 };
			for (int i = 0; i < g_VGrid.Length; i++)
			{
				g_VGrid[i].intCh = chArray[i];
				g_VGrid[i].blnVisible = true;

				if (g_VGrid[i].intCh == 3 || g_VGrid[i].intCh == 8 || g_VGrid[i].intCh == 9) //'BPM/STOP
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.BPM_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.BPM_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.BPM;
				}
				else if (g_VGrid[i].intCh == 4 || g_VGrid[i].intCh == 6 || g_VGrid[i].intCh == 7) //'BGA/Layer/Poor
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.BGA_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.BGA_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.BGA;
				}
				else if (g_VGrid[i].intCh == 11)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY01_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY01_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY01;
				}
				else if (g_VGrid[i].intCh == 12)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY02_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY02_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY02;
				}
				else if (g_VGrid[i].intCh == 13)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY03_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY03_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY03;
				}
				else if (g_VGrid[i].intCh == 14)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY04_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY04_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY04;
				}
				else if (g_VGrid[i].intCh == 15)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY05_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY05_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY05;
				}
				else if (g_VGrid[i].intCh == 18)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY06_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY06_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY06;
				}
				else if (g_VGrid[i].intCh == 19)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY07_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY07_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY07;
				}
				else if (g_VGrid[i].intCh == 16)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY08_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY08_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY08;
				}
				else if (g_VGrid[i].intCh == 21)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY11_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY11_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY11;
				}
				else if (g_VGrid[i].intCh == 22)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY12_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY12_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY12;
				}
				else if (g_VGrid[i].intCh == 23)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY13_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY13_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY13;
				}
				else if (g_VGrid[i].intCh == 24)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY14_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY14_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY14;
				}
				else if (g_VGrid[i].intCh == 25)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY15_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY15_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY15;
				}
				else if (g_VGrid[i].intCh == 28)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY16_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY16_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY16;
				}
				else if (g_VGrid[i].intCh == 29)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY17_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY17_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY17;
				}
				else if (g_VGrid[i].intCh == 26)
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.KEY18_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.KEY18_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.KEY18;
				}
				else if (g_VGrid[i].intCh > 100) //'BGM
				{
					g_VGrid[i].intLightNum = (int)PEN_NUM.BGM_LIGHT;
					g_VGrid[i].intShadowNum = (int)PEN_NUM.BGM_SHADOW;
					g_VGrid[i].intBrushNum = (int)BRUSH_NUM.BGM;
				}

				if (g_VGrid[i].intCh != 0)
				{
					g_VGrid[i].intWidth = GRID_WIDTH;
				}
				else
				{
					g_VGrid[i].intWidth = SPACE_WIDTH;
				}
			}

			frmMain.lstWAV.SelectedIndex = 0;
			frmMain.lstBMP.SelectedIndex = 0;
			frmMain.lstBGA.SelectedIndex = 0;
			frmMain.lstMeasureLen.SelectedIndex = 0;

			frmMain.optChangeTop0.Select();
			frmMain.optChangeBottom0.Select();

			for (int i = 0; i < 64; i++)
			{
				frmMain.cboNumerator.Items.Insert(i, (i + 1).ToString());
			}
			frmMain.cboNumerator.SelectedIndex = 3;
			frmMain.cboDenominator.SelectedIndex = 0;

			g_disp.intMaxMeasure = 0;
			ChangeMaxMeasure(15);
			ChangeResolution();

			GetCmdLine();

			g_BMS.blnSaveFlag = true;

			frmMain.lstWAV.SetSelected(0, true);
			frmMain.lstBMP.SetSelected(0, true);
			frmMain.lstBGA.SetSelected(0, true);
			frmMain.lstMeasureLen.SetSelected(0, true);

			g_blnIgnoreInput = false;

			if (ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "EasterEgg", "Tips", 0) != 0)
			{
				frmWindowTips.Left = frmMain.Left + (frmMain.Width - frmWindowTips.Width) / 2;
				frmWindowTips.Top= frmMain.Top+ (frmMain.Height- frmWindowTips.Height) / 2;

				frmWindowTips.ShowDialog(frmMain);
			}

			// 各コントロールのイベントハンドラを設定
			SetEventHandlers();

			Application.Run(frmMain);
		}

		public void SetEventHandlers()
		{
			frmMain.cboDispGridMain.SelectedIndexChanged += frmMain.cboDispGridMain_SelectedIndexChanged;
			frmMain.cboDispGridSub.SelectedIndexChanged += frmMain.cboDispGridSub_SelectedIndexChanged;
		}

		public void CleanUp(string expMessage, string errProcedure)
		{
			g_InputLog = null;

			// TODO: SaveConfig();

			// TODO: 音声デバイスの停止

			DeleteFile(g_BMS.strDir + "___bmse_temp.bms");
			DeleteFile(g_strAppDir + "___bmse_temp.bms");

			if (g_BMS.strDir == null || g_BMS.strDir.Length == 0)
			{
				g_BMS.strDir = g_strAppDir;
			}

			for (int i = 0; i < 10000; i++)
			{
				g_BMS.strFileName = "temp" + i.ToString("0000") + ".bms";

				if (i == 9999)
				{
					// TODO: CreateBMS(gBms.dir + gBms.fileName);
				}
				else if ("".Equals(FileUtil.Dir(g_BMS.strDir + g_BMS.strFileName)))
				{
					// TODO: CreateBMS(gBms.dir + gBms.fileName);
					break;
				}
			}

			DebugOutput(expMessage, errProcedure, true);
		}

		public void DebugOutput(string description, string errProcedure, bool cleanup)
		{
			// 追記モードでエラーログを出力
			using(StreamWriter writer = new StreamWriter("error.txt", true))
			{
				DateTime dtToday = DateTime.Now;
				writer.WriteLine(dtToday.ToString() + ": " + description + "@" + errProcedure + "/BMSE_" + EnvUtil.AppMajor + "." + EnvUtil.AppMinor + "." + EnvUtil.AppRevision);
			}

			string error = "Error: " + description + "@" + errProcedure;

			if(cleanup)
			{
				string crlf = Environment.NewLine;
				error = g_Message[(int)Message.ERR_01] + crlf + error + crlf;
				error = error + g_Message[(int)Message.ERR_02] + crlf;
				error = error + g_BMS.strDir + g_BMS.strFileName;
			}

			frmMain.Show();

			MessageBox.Show(error, g_strAppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		public int DeleteFile(string fileName)
		{
			try
			{
				File.Delete(fileName);
				return 0;
			}
			catch
			{
				return 1;
			}
		}

		public int SaveCheck()
		{
			//int lngRet;
			//string[] array;

			try
			{
				if (frmMain.cboPlayer.SelectedIndex + 1 != g_BMS.intPlayerType
					|| !frmMain.txtGenre.Text.Equals(g_BMS.strGenre)
					|| !frmMain.txtTitle.Text.Equals(g_BMS.strTitle)
					|| !frmMain.txtArtist.Text.Equals(g_BMS.strArtist)
					|| int.Parse(frmMain.cboPlayLevel.Text) != g_BMS.lngPlayLevel
					|| double.Parse(frmMain.txtBPM.Text) != g_BMS.sngBPM
					|| frmMain.cboPlayRank.SelectedIndex != g_BMS.intPlayRank
					|| double.Parse(frmMain.txtTotal.Text) != g_BMS.sngTotal
					|| int.Parse(frmMain.txtVolume.Text) != g_BMS.intVolume
					|| !frmMain.txtStageFile.Text.Equals(g_BMS.strStageFile))
				{
					g_BMS.blnSaveFlag = false;
				}

				if (g_BMS.blnSaveFlag)
				{
					return 0;
				}

				frmMain.Show();

				DialogResult res
					= MessageBox.Show(g_Message[(int)Message.MSG_FILE_CHANGED],
										g_strAppTitle,
										MessageBoxButtons.YesNoCancel,
										MessageBoxIcon.Exclamation);

				if(res == DialogResult.Yes)
				{
					if (( g_BMS.strDir != null && !"".Equals(g_BMS.strDir) )
						&& ( g_BMS.strFileName != null && !"".Equals(g_BMS.strFileName) ))
					{
						//TODO: CreateBMS(gBms.dir + gBms.fileName);
					}
					else
					{
						frmMain.dlgMainSave.Filter
							= "BMS files (*.bms,*.bme,*.bml,*.pms)|*.bms;*.bme;*.bml;*.pms|All files (*.*)|*.*";
						frmMain.dlgMainSave.FileName = g_BMS.strFileName;

						frmMain.dlgMainSave.ShowDialog();

						g_BMS.strDir = System.IO.Path.GetDirectoryName(frmMain.dlgMainSave.FileName) + "\\";
						g_BMS.strFileName = Path.GetFileName(frmMain.dlgMainSave.FileName);

						// TODO: CreateBMS(gBms.dir + gBms.fileName);
						
						// TODO: RecentFileRotation(gBms.dir + gBms.fileName);

						frmMain.dlgMainOpen.InitialDirectory = g_BMS.strDir;
						frmMain.dlgMainSave.InitialDirectory = g_BMS.strDir;
					}

					return 0;
				}
				else if(res == DialogResult.No)
				{
					return 0;
				}
				else 
				{
					return 1;
				}
			}
			catch (Exception e)
			{
				return 1;
			}
		}

		private void GetCmdLine()
		{
			string retStr;
			string[] cmdArray;
			string[] array;
			bool readLock = false;
			bool readFlag = false;

			cmdArray = System.Environment.GetCommandLineArgs();

			if (cmdArray.Length == 1)
			{
				return;
			}

			for (int i = 1; i < cmdArray.Length; i++)
			{
				if(".BMS".Equals(cmdArray[i].Substring(cmdArray[i].Length - 4, 4).ToUpper())
					|| ".BME".Equals(cmdArray[i].Substring(cmdArray[i].Length - 4, 4).ToUpper())
					|| ".BML".Equals(cmdArray[i].Substring(cmdArray[i].Length - 4, 4).ToUpper())
					|| ".PMS".Equals(cmdArray[i].Substring(cmdArray[i].Length - 4, 4).ToUpper()))
				{
					g_BMS.strFileName = Path.GetFileName(cmdArray[i]);
					g_BMS.strDir = System.IO.Path.GetDirectoryName(cmdArray[i]) + "\\";
					frmMain.dlgMainOpen.InitialDirectory = g_BMS.strDir;
					frmMain.dlgMainSave.InitialDirectory = g_BMS.strDir;

					Console.WriteLine(g_BMS.strFileName);
					Console.WriteLine(g_BMS.strDir);

					LoadBMS();
					// TODO: RecentFilesRotation(gBms.dir + gBms.fileName);
				}
			}

			/* 消す予定
			retStr = EnvUtil.GetCommand().Trim();

			if ("".Equals(retStr))
			{
				return;
			}

			cmdArray = new string[1];

			for (int i = 0; i < retStr.Length; i++)
			{
				if (' '.Equals(retStr[i])) //'スペース
				{
					if (!readLock)
					{
						Array.Resize(ref cmdArray, cmdArray.Length + 1);
					}
					else
					{
						cmdArray[cmdArray.Length - 1] = cmdArray[cmdArray.Length - 1] + " ";
					}
				}
				else if ('\"'.Equals(retStr[i])) //'ダブルクオーテーション
				{
					readLock = !readLock;
				}
				else
				{
					cmdArray[cmdArray.Length - 1] = cmdArray[cmdArray.Length - 1] + retStr[i];
				}
			}

			for (int i = 0; i < cmdArray.Length; i++)
			{
				if (!"".Equals(cmdArray[i]))
				{
					if (cmdArray[i].IndexOf(":\\") != -1
					&& ".BMS".Equals(cmdArray[i].Substring(cmdArray[i].Length - 4, 4).ToUpper())
					|| ".BME".Equals(cmdArray[i].Substring(cmdArray[i].Length - 4, 4).ToUpper())
					|| ".BML".Equals(cmdArray[i].Substring(cmdArray[i].Length - 4, 4).ToUpper())
					|| ".PMS".Equals(cmdArray[i].Substring(cmdArray[i].Length - 4, 4).ToUpper()))
					{
						if (readFlag)
						{
							// 新しいプロセスを起動してBMSファイルを読み込ませる
						}
						else
						{
							array = cmdArray[i].Split('\\');
							gBms.fileName = cmdArray[i].Substring(cmdArray[i].Length - array[array.Length - 1].Length);
							gBms.dir = cmdArray[i].Substring(0, cmdArray[i].Length - array[array.Length - 1].Length);
							frmMain.dlgMainOpen.InitialDirectory = gBms.dir;
							frmMain.dlgMainSave.InitialDirectory = gBms.dir;
							readFlag = true;

							// LoadBms();
							// RecentFilesRotation(gBms.dir + gBms.fileName);
						}
					}
				}
			}
			*/
		}

		public void LoadThemeFile(string fileName)
		{
			string[] array;
			int retInt;
			Color retColor;

			array = ConfigManager.Instance.GetValue(fileName, "Main", "Background", "0,0,0").Split(',');
			frmMain.picMain.BackColor = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

			array = ConfigManager.Instance.GetValue(fileName, "Main", "MeasureNum", "64,64,64").Split(',');
			g_lngSystemColor[(int)COLOR_NUM.MEASURE_NUM] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

			array = ConfigManager.Instance.GetValue(fileName, "Main", "MeasureLine", "255,255,255").Split(',');
			g_lngSystemColor[(int)COLOR_NUM.MEASURE_LINE] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

			array = ConfigManager.Instance.GetValue(fileName, "Main", "GridMain", "96,96,96").Split(',');
			g_lngSystemColor[(int)COLOR_NUM.GRID_MAIN] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

			array = ConfigManager.Instance.GetValue(fileName, "Main", "GridSub", "192,192,192").Split(',');
			g_lngSystemColor[(int)COLOR_NUM.GRID_SUB] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

			array = ConfigManager.Instance.GetValue(fileName, "Main", "VerticalMain", "255,255,255").Split(',');
			g_lngSystemColor[(int)COLOR_NUM.VERTICAL_MAIN] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

			array = ConfigManager.Instance.GetValue(fileName, "Main", "VerticalSub", "128,128,128").Split(',');
			g_lngSystemColor[(int)COLOR_NUM.VERTICAL_SUB] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

			array = ConfigManager.Instance.GetValue(fileName, "Main", "Info", "0,255,0").Split(',');
			g_lngSystemColor[(int)COLOR_NUM.INFO] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

			for (int i = 0; i < (int)BRUSH_NUM.Max; i++)
			{
				switch (i)
				{
					case (int)BRUSH_NUM.BGM:
						array = ConfigManager.Instance.GetValue(fileName, "BGM", "Background", "48,0,0").Split(',');
						retColor = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						array = ConfigManager.Instance.GetValue(fileName, "BGM", "Text", "B01,B02,B03,B04,B05,B06,B07,B08,B09,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32").Split(',');

						for (int j = 0; j < 32; j++)
						{
							g_VGrid[(int)GRID.NUM_BGM + j].strText = array[j];
							g_VGrid[(int)GRID.NUM_BGM + j].lngBackColor = retColor;
						}

						array = ConfigManager.Instance.GetValue(fileName, "BGM", "ObjectLight", "255,0,0").Split(',');
						g_lngPenColor[(int)PEN_NUM.BGM_LIGHT] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						array = ConfigManager.Instance.GetValue(fileName, "BGM", "ObjectShawdow", "96,0,0").Split(',');
						g_lngPenColor[(int)PEN_NUM.BGM_SHADOW] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						array = ConfigManager.Instance.GetValue(fileName, "BGM", "ObjectColor", "128,0,0").Split(',');
						g_lngBrushColor[(int)BRUSH_NUM.BGM] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

						break;

					case (int)BRUSH_NUM.BPM:
						array = ConfigManager.Instance.GetValue(fileName, "BPM", "Text", "BPM,STOP").Split(',');
						g_VGrid[(int)GRID.NUM_BPM].strText = array[0];
						g_VGrid[(int)GRID.NUM_STOP].strText = array[1];

						array = ConfigManager.Instance.GetValue(fileName, "BPM", "Background", "48,48,48").Split(',');
						retColor = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_VGrid[(int)GRID.NUM_BPM].lngBackColor = retColor;
						g_VGrid[(int)GRID.NUM_STOP].lngBackColor= retColor;

						array = ConfigManager.Instance.GetValue(fileName, "BPM", "ObjectLight", "192,192,0").Split(',');
						g_lngPenColor[(int)PEN_NUM.BPM_LIGHT] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						array = ConfigManager.Instance.GetValue(fileName, "BPM", "ObjectShadow", "128,128,0").Split(',');
						g_lngPenColor[(int)PEN_NUM.BPM_SHADOW] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						array = ConfigManager.Instance.GetValue(fileName, "BPM", "ObjectColor", "160,160,0").Split(',');
						g_lngBrushColor[(int)BRUSH_NUM.BPM] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

						break;

					case (int)BRUSH_NUM.BGA:
						array = ConfigManager.Instance.GetValue(fileName, "BGA", "Text", "BGA,LAYER,POOR").Split(',');
						g_VGrid[(int)GRID.NUM_BGA].strText = array[0];
						g_VGrid[(int)GRID.NUM_LAYER].strText = array[1];
						g_VGrid[(int)GRID.NUM_POOR].strText = array[2];

						array = ConfigManager.Instance.GetValue(fileName, "BGA", "Background", "0,24,0").Split(',');
						retColor = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_VGrid[(int)GRID.NUM_BGA].lngBackColor = retColor;
						g_VGrid[(int)GRID.NUM_LAYER].lngBackColor = retColor;
						g_VGrid[(int)GRID.NUM_POOR].lngBackColor = retColor;

						array = ConfigManager.Instance.GetValue(fileName, "BGA", "ObjectLight", "0,255,0").Split(',');
						g_lngPenColor[(int)PEN_NUM.BGA_LIGHT] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						array = ConfigManager.Instance.GetValue(fileName, "BGA", "ObjectShadow", "0,96,0").Split(',');
						g_lngPenColor[(int)PEN_NUM.BGA_SHADOW] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						array = ConfigManager.Instance.GetValue(fileName, "BGA", "ObjectColor", "0,128,0").Split(',');
						g_lngBrushColor[(int)BRUSH_NUM.BGA] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

						break;

					case (int)BRUSH_NUM.KEY01:
					case (int)BRUSH_NUM.KEY03:
					case (int)BRUSH_NUM.KEY05:
					case (int)BRUSH_NUM.KEY07:
						retInt = (i - (int)BRUSH_NUM.KEY01) + 1;

						//gVGrid[(int)GRID.NUM_1P_1KEY + retInt - 1].text = ConfigManager.Instance.GetValue(fileName, "KEY_1P_0" + retInt.ToString(), "Text", retInt).ToString();
						g_VGrid[(int)GRID.NUM_1P_1KEY + retInt - 1].strText = ConfigManager.Instance.GetValue(fileName, "KEY_1P_0" + retInt.ToString(), "Text", retInt.ToString());

						array = ConfigManager.Instance.GetValue(fileName, "KEY_1P_0" + retInt, "Background", "32,32,32").Split(',');
						g_VGrid[(int)GRID.NUM_1P_1KEY + retInt - 1].lngBackColor = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

						array = ConfigManager.Instance.GetValue(fileName, "KEY_1P_0" + retInt, "ObjectLight", "192,192,192").Split(',');
						g_lngPenColor[(int)PEN_NUM.KEY01_LIGHT + retInt - 1] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngPenColor[(int)PEN_NUM.INV_KEY01_LIGHT + retInt - 1] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);
						array = ConfigManager.Instance.GetValue(fileName, "KEY_1P_0" + retInt, "ObjectShadow", "96,96,96").Split(',');
						g_lngPenColor[(int)PEN_NUM.KEY01_SHADOW+ retInt - 1] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngPenColor[(int)PEN_NUM.INV_KEY01_SHADOW+ retInt - 1] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);
						array = ConfigManager.Instance.GetValue(fileName, "KEY_1P_0" + retInt, "ObjectColor", "128,128,128").Split(',');
						g_lngBrushColor[(int)BRUSH_NUM.KEY01 + retInt - 1] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngBrushColor[(int)BRUSH_NUM.INV_KEY01 + retInt - 1] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);

						break;

					case (int)BRUSH_NUM.KEY02:
					case (int)BRUSH_NUM.KEY04:
					case (int)BRUSH_NUM.KEY06:
						retInt = (i - (int)BRUSH_NUM.KEY01) + 1;

						//gVGrid[(int)GRID.NUM_1P_1KEY + retInt - 1].text = ConfigManager.Instance.GetValue(fileName, "KEY_1P_0" + retInt.ToString(), "Text", retInt).ToString();
						g_VGrid[(int)GRID.NUM_1P_1KEY + retInt - 1].strText = ConfigManager.Instance.GetValue(fileName, "KEY_1P_0" + retInt.ToString(), "Text", retInt.ToString());

						array = ConfigManager.Instance.GetValue(fileName, "KEY_1P_0" + retInt, "Background", "0,0,40").Split(',');
						g_VGrid[(int)GRID.NUM_1P_1KEY + retInt - 1].lngBackColor = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

						array = ConfigManager.Instance.GetValue(fileName, "KEY_1P_0" + retInt, "ObjectLight", "96,96,255").Split(',');
						g_lngPenColor[(int)PEN_NUM.KEY01_LIGHT + retInt - 1] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngPenColor[(int)PEN_NUM.INV_KEY01_LIGHT + retInt - 1] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);
						array = ConfigManager.Instance.GetValue(fileName, "KEY_1P_0" + retInt, "ObjectShadow", "0,0,128").Split(',');
						g_lngPenColor[(int)PEN_NUM.KEY01_SHADOW+ retInt - 1] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngPenColor[(int)PEN_NUM.INV_KEY01_SHADOW + retInt - 1] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);
						array = ConfigManager.Instance.GetValue(fileName, "KEY_1P_0" + retInt, "ObjectColor", "0,0,255").Split(',');
						g_lngBrushColor[(int)BRUSH_NUM.KEY01 + retInt - 1] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngBrushColor[(int)BRUSH_NUM.INV_KEY01 + retInt - 1] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);

						break;

					case (int)BRUSH_NUM.KEY08:
						g_VGrid[(int)GRID.NUM_1P_SC_L].strText = ConfigManager.Instance.GetValue(fileName, "KEY_1P_SC", "Text", "SC");
						g_VGrid[(int)GRID.NUM_1P_SC_R].strText = ConfigManager.Instance.GetValue(fileName, "KEY_1P_SC", "Text", "SC");

						array = ConfigManager.Instance.GetValue(fileName, "KEY_1P_SC", "Background", "48,0,0").Split(',');
						g_VGrid[(int)GRID.NUM_1P_SC_L].lngBackColor = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_VGrid[(int)GRID.NUM_1P_SC_R].lngBackColor = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

						array = ConfigManager.Instance.GetValue(fileName, "KEY_1P_SC", "ObjectLight", "255,96,96").Split(',');
						g_lngPenColor[(int)PEN_NUM.KEY08_LIGHT] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngPenColor[(int)PEN_NUM.INV_KEY08_LIGHT] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);
						array = ConfigManager.Instance.GetValue(fileName, "KEY_1P_SC", "ObjectShadow", "128,0,0").Split(',');
						g_lngPenColor[(int)PEN_NUM.KEY08_SHADOW] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngPenColor[(int)PEN_NUM.INV_KEY08_SHADOW] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);
						array = ConfigManager.Instance.GetValue(fileName, "KEY_1P_SC", "ObjectColor", "255,0,0").Split(',');
						g_lngBrushColor[(int)BRUSH_NUM.KEY08] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngBrushColor[(int)BRUSH_NUM.INV_KEY08] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);

						break;

					case (int)BRUSH_NUM.KEY11:
					case (int)BRUSH_NUM.KEY13:
					case (int)BRUSH_NUM.KEY15:
					case (int)BRUSH_NUM.KEY17:
						retInt = (i - (int)BRUSH_NUM.KEY11) + 1;

						//gVGrid[(int)GRID.NUM_2P_1KEY + retInt - 1].text = ConfigManager.Instance.GetValue(fileName, "KEY_2P_0" + retInt.ToString(), "Text", retInt).ToString();
						g_VGrid[(int)GRID.NUM_2P_1KEY + retInt - 1].strText = ConfigManager.Instance.GetValue(fileName, "KEY_2P_0" + retInt.ToString(), "Text", retInt.ToString());

						array = ConfigManager.Instance.GetValue(fileName, "KEY_2P_0" + retInt, "Background", "32,32,32").Split(',');
						g_VGrid[(int)GRID.NUM_2P_1KEY + retInt - 1].lngBackColor = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

						array = ConfigManager.Instance.GetValue(fileName, "KEY_2P_0" + retInt, "ObjectLight", "192,192,192").Split(',');
						g_lngPenColor[(int)PEN_NUM.KEY11_LIGHT + retInt - 1] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngPenColor[(int)PEN_NUM.INV_KEY11_LIGHT + retInt - 1] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);
						array = ConfigManager.Instance.GetValue(fileName, "KEY_2P_0" + retInt, "ObjectShadow", "96,96,96").Split(',');
						g_lngPenColor[(int)PEN_NUM.KEY11_SHADOW+ retInt - 1] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngPenColor[(int)PEN_NUM.INV_KEY11_SHADOW+ retInt - 1] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);
						array = ConfigManager.Instance.GetValue(fileName, "KEY_2P_0" + retInt, "ObjectColor", "128,128,128").Split(',');
						g_lngBrushColor[(int)BRUSH_NUM.KEY11 + retInt - 1] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngBrushColor[(int)BRUSH_NUM.INV_KEY11 + retInt - 1] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);

						if (i == (int)BRUSH_NUM.KEY11)
						{
							//gVGrid[(int)GRID.NUM_FOOTPEDAL].text = ConfigManager.Instance.GetValue(fileName, "KEY_2P_01", "Text", retInt).ToString();
							g_VGrid[(int)GRID.NUM_FOOTPEDAL].strText = ConfigManager.Instance.GetValue(fileName, "KEY_2P_01", "Text", retInt.ToString());
							array = ConfigManager.Instance.GetValue(fileName, "KEY_2P_01", "Background", "32,32,32").Split(',');
							g_VGrid[(int)GRID.NUM_FOOTPEDAL].lngBackColor = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
							array = ConfigManager.Instance.GetValue(fileName, "KEY_2P_0" + retInt, "ObjectLight", "192,192,192").Split(',');
							// UNDONE: 元のソースコードでもここで終わってるけど不自然に感じる
						}

						break;

					case (int)BRUSH_NUM.KEY12:
					case (int)BRUSH_NUM.KEY14:
					case (int)BRUSH_NUM.KEY16:
						retInt = (i - (int)BRUSH_NUM.KEY11) + 1;

						//gVGrid[(int)GRID.NUM_2P_1KEY + retInt - 1].text = ConfigManager.Instance.GetValue(fileName, "KEY_2P_0" + retInt.ToString(), "Text", retInt).ToString();
						g_VGrid[(int)GRID.NUM_2P_1KEY + retInt - 1].strText = ConfigManager.Instance.GetValue(fileName, "KEY_2P_0" + retInt.ToString(), "Text", retInt.ToString());

						array = ConfigManager.Instance.GetValue(fileName, "KEY_2P_0" + retInt, "Background", "0,0,40").Split(',');
						g_VGrid[(int)GRID.NUM_2P_1KEY + retInt - 1].lngBackColor = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

						array = ConfigManager.Instance.GetValue(fileName, "KEY_2P_0" + retInt, "ObjectLight", "96,96,255").Split(',');
						g_lngPenColor[(int)PEN_NUM.KEY11_LIGHT + retInt - 1] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngPenColor[(int)PEN_NUM.INV_KEY11_LIGHT + retInt - 1] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);
						array = ConfigManager.Instance.GetValue(fileName, "KEY_2P_0" + retInt, "ObjectShadow", "0,0,128").Split(',');
						g_lngPenColor[(int)PEN_NUM.KEY11_SHADOW + retInt - 1] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngPenColor[(int)PEN_NUM.INV_KEY11_SHADOW + retInt - 1] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);
						array = ConfigManager.Instance.GetValue(fileName, "KEY_2P_0" + retInt, "ObjectColor", "0,0,255").Split(',');
						g_lngBrushColor[(int)BRUSH_NUM.KEY11 + retInt - 1] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngBrushColor[(int)BRUSH_NUM.INV_KEY11 + retInt - 1] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);

						break;

					case (int)BRUSH_NUM.KEY18:
						g_VGrid[(int)GRID.NUM_2P_SC_L].strText = ConfigManager.Instance.GetValue(fileName, "KEY_2P_SC", "Text", "SC");
						g_VGrid[(int)GRID.NUM_2P_SC_R].strText = ConfigManager.Instance.GetValue(fileName, "KEY_2P_SC", "Text", "SC");

						array = ConfigManager.Instance.GetValue(fileName, "KEY_2P_SC", "Background", "48,0,0").Split(',');
						g_VGrid[(int)GRID.NUM_2P_SC_L].lngBackColor = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_VGrid[(int)GRID.NUM_2P_SC_R].lngBackColor = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

						array = ConfigManager.Instance.GetValue(fileName, "KEY_2P_SC", "ObjectLight", "255,96,96").Split(',');
						g_lngPenColor[(int)PEN_NUM.KEY18_LIGHT] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngPenColor[(int)PEN_NUM.INV_KEY18_LIGHT] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);
						array = ConfigManager.Instance.GetValue(fileName, "KEY_2P_SC", "ObjectShadow", "128,0,0").Split(',');
						g_lngPenColor[(int)PEN_NUM.KEY18_SHADOW] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngPenColor[(int)PEN_NUM.INV_KEY18_SHADOW] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);
						array = ConfigManager.Instance.GetValue(fileName, "KEY_2P_SC", "ObjectColor", "255,0,0").Split(',');
						g_lngBrushColor[(int)BRUSH_NUM.KEY18] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						g_lngBrushColor[(int)BRUSH_NUM.INV_KEY18] = Color.FromArgb(int.Parse(array[0]) / 2, int.Parse(array[1]) / 2, int.Parse(array[2]) / 2);

						break;

					case (int)BRUSH_NUM.LONGNOTE:
						array = ConfigManager.Instance.GetValue(fileName, "KEY_LONGNOTE", "ObjectLight", "0,128,0").Split(',');
						g_lngPenColor[(int)PEN_NUM.LONGNOTE_LIGHT] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						array = ConfigManager.Instance.GetValue(fileName, "KEY_LONGNOTE", "ObjectShadow", "0,32,0").Split(',');
						g_lngPenColor[(int)PEN_NUM.LONGNOTE_SHADOW] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						array = ConfigManager.Instance.GetValue(fileName, "KEY_LONGNOTE", "ObjectColor", "0,64,0").Split(',');
						g_lngBrushColor[(int)BRUSH_NUM.LONGNOTE] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

						break;

					case (int)BRUSH_NUM.SELECT_OBJ:
						array = ConfigManager.Instance.GetValue(fileName, "SELECT", "ObjectLight", "255,255,255").Split(',');
						g_lngPenColor[(int)PEN_NUM.SELECT_OBJ_LIGHT] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						array = ConfigManager.Instance.GetValue(fileName, "SELECT", "ObjectShadow", "128,128,128").Split(',');
						g_lngPenColor[(int)PEN_NUM.SELECT_OBJ_SHADOW] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
						array = ConfigManager.Instance.GetValue(fileName, "SELECT", "ObjectColor", "0,255,255").Split(',');
						g_lngBrushColor[(int)BRUSH_NUM.SELECT_OBJ] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

						break;

					case (int)BRUSH_NUM.EDIT_FRAME:
						array = ConfigManager.Instance.GetValue(fileName, "SELECT", "EditFrame", "255,255,255").Split(',');
						g_lngPenColor[(int)PEN_NUM.EDIT_FRAME] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

						break;

					case (int)BRUSH_NUM.DELETE_FRAME:
						array = ConfigManager.Instance.GetValue(fileName, "SELECT", "DeleteFrame", "255,255,255").Split(',');
						g_lngPenColor[(int)PEN_NUM.DELETE_FRAME] = Color.FromArgb(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

						break;
				}
			}
		}

		public void LoadLanguageFile(string fileName)
		{
			g_strStatusBar[0] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "CH_01", "BGA");
			g_strStatusBar[3] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "CH_04", "BGA");
			g_strStatusBar[5] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "CH_06", "BGA Poor");
			g_strStatusBar[6] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "CH_07", "BGA Layer");
			g_strStatusBar[7] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "CH_08", "BPM Change");
			g_strStatusBar[8] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "CH_09", "Stop Sequence");
			g_strStatusBar[10] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "CH_KEY_1P", "1P Key");
			g_strStatusBar[11] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "CH_KEY_2P", "2P Key");
			g_strStatusBar[12] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "CH_SCRATCH_1P", "1P Scratch");
			g_strStatusBar[13] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "CH_SCRATCH_2P", "2P Scratch");
			g_strStatusBar[14] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "CH_INVISIBLE", "(Invisible)");
			g_strStatusBar[15] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "CH_LONGNOTE", "(LongNote)");
			g_strStatusBar[19] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "MODE_EDIT", "Edit Mode");
			g_strStatusBar[20] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "MODE_WRITE", "Write Mode");
			g_strStatusBar[21] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "MODE_DELETE", "Delete Mode");
			g_strStatusBar[22] = ConfigManager.Instance.GetValue(fileName, "StatusBar", "MEASURE", "Measure");

			frmMain.mnuFile.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "FILE", "&File");
			frmMain.mnuFileNew.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "FILE_NEW", "&New");
			frmMain.mnuFileOpen.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "FILE_OPEN", "&Open");
			frmMain.mnuFileSave.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "FILE_SAVE", "&Save");
			frmMain.mnuFileSaveAs.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "FILE_SAVE_AS", "Save &As");
			frmMain.mnuFileOpenDirectory.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "FILE_OPEN_DIRECTORY", "Open &Directory");
			frmMain.mnuFileConvertWizard.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "FILE_CONVERT_WIZARD", "Show &Conversion Wizard");
			frmMain.mnuFileExit.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "FILE_EXIT", "&Exit");

			frmMain.mnuEdit.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT", "&Edit");
			frmMain.mnuEditUndo.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_UNDO", "&Undo");
			frmMain.mnuEditRedo.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_REDO", "&Redo");
			frmMain.mnuEditCut.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_CUT", "&Cut");
			frmMain.mnuEditCopy.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_COPY", "&Copy");
			frmMain.mnuEditPaste.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_PASTE", "&Paste");
			frmMain.mnuEditDelete.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_DELETE", "&Delete");
			frmMain.mnuEditSelectAll.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_SELECT_ALL", "&Select All");
			frmMain.mnuEditFind.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_FIND", "&Find/Replace/Delete");
			frmMain.mnuEditMode0.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_MODE_EDIT", "Edit &Mode");
			frmMain.mnuEditMode1.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_MODE_WRITE", "Write &Mode");
			frmMain.mnuEditMode2.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_MODE_DELETE", "Delete &Mode");

			frmMain.mnuView.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "VIEW", "&View");
			frmMain.mnuViewToolBar.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "VIEW_TOOL_BAR", "&Tool Bar");
			frmMain.mnuViewDirectInput.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "VIEW_DIRECT_INPUT", "&Direct Input");
			frmMain.mnuViewStatusBar.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "VIEW_STATUS_BAR", "&Status Bar");

			frmMain.mnuOptions.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "OPTIONS", "&Options");
			frmMain.mnuOptionsActiveIgnore.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "OPTIONS_IGNORE_ACTIVE", "&Control Unavailable When Active");
			frmMain.mnuOptionsFileNameOnly.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "OPTIONS_FILE_NAME_ONLY", "Display &File Name Only");
			frmMain.mnuOptionsVertical.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "OPTIONS_VERTICAL", "&Vertical Grid Info");
			frmMain.mnuOptionsLaneBG.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "OPTIONS_LANE_BG", "&Background Color");
			frmMain.mnuOptionsSelectPreview.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "OPTIONS_SINGLE_SELECT_PREVIEW", "&Preview Upon Object Selection");
			frmMain.mnuOptionsObjectFileName.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "OPTIONS_OBJECT_FILE_NAME", "Show &Objects' File Names");
			frmMain.mnuOptionsMoveOnGrid.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "OPTIONS_MOVE_ON_GRID", "Restrict Objects' &Movement Onto Grid");
			frmMain.mnuOptionsNumFF.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "OPTIONS_USE_OLD_FORMAT", "&Use Old Format (01-FF)");
			frmMain.mnuOptionsRightClickDelete.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "OPTIONS_RIGHT_CLICK_DELETE", "&Right Click To Delete Objects");

			frmMain.mnuTools.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "TOOLS", "&Tools");
			frmMain.mnuToolsPlayAll.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "TOOLS_PLAY_FIRST", "Play &All");
			frmMain.mnuToolsPlay.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "TOOLS_PLAY", "&Play From Current Position");
			frmMain.mnuToolsPlayStop.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "TOOLS_STOP", "&Stop");
			frmMain.mnuToolsSetting.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "TOOLS_SETTING", "&Viewer Setting");

			frmMain.mnuHelp.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "HELP", "&Help");
			frmMain.mnuHelpOpen.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "HELP_OPEN", "&Help");
			frmMain.mnuHelpWeb.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "HELP_WEB", "Open &Website");
			frmMain.mnuHelpAbout.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "HELP_ABOUT", "&About BMSE");

			frmMain.mnuContext.Visible = false;
			frmMain.mnuContextInsertMeasure.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "CONTEXT_MEASURE_INSERT", "&Insert Measure");
			frmMain.mnuContextDeleteMeasure.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "CONTEXT_MEASURE_DELETE", "&Delete Measure");
			frmMain.mnuContextEditCut.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_CUT", "Cu&t");
			frmMain.mnuContextEditCopy.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_COPY", "&Copy");
			frmMain.mnuContextEditPaste.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_PASTE", "&Paste");
			frmMain.mnuContextEditDelete.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "EDIT_DELETE", "&Delete");

			frmMain.mnuContextList.Visible = false;
			frmMain.mnuContextListLoad.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "CONTEXT_LIST_LOAD", "&Load");
			frmMain.mnuContextListDelete.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "CONTEXT_LIST_DELETE", "&Delete");
			frmMain.mnuContextListRename.Text = ConfigManager.Instance.GetValue(fileName, "Menu", "CONTEXT_LIST_RENAME", "&Rename");

			frmMain.optChangeTop0.Text = ConfigManager.Instance.GetValue(fileName, "Header", "TAB_BASIC", "Basic");
			frmMain.optChangeTop1.Text = ConfigManager.Instance.GetValue(fileName, "Header", "TAB_EXPAND", "Expand");
			frmMain.optChangeTop2.Text = ConfigManager.Instance.GetValue(fileName, "Header", "TAB_CONFIG", "Config");
			
			frmMain.lblPlayMode.Text = ConfigManager.Instance.GetValue(fileName, "Header", "BASIC_PLAYER", "#PLAYER");
			DataSource.DsListPlayer[0].Text = ConfigManager.Instance.GetValue(fileName, "Header", "BASIC_PLAYER_1P", "1 Player");
			DataSource.DsListPlayer[1].Text = ConfigManager.Instance.GetValue(fileName, "Header", "BASIC_PLAYER_2P", "2 Player");
			DataSource.DsListPlayer[2].Text = ConfigManager.Instance.GetValue(fileName, "Header", "BASIC_PLAYER_DP", "Double Play");
			DataSource.DsListPlayer[3].Text = ConfigManager.Instance.GetValue(fileName, "Header", "BASIC_PLAYER_PMS", "9 Keys (PMS)");
			DataSource.DsListPlayer[4].Text = ConfigManager.Instance.GetValue(fileName, "Header", "BASIC_PLAYER_OCT", "13 Keys (Oct)");
			DataSource.Update(frmMain.cboPlayer);
			frmMain.lblGenre.Text = ConfigManager.Instance.GetValue(fileName, "Header", "BASIC_GENRE", "#GENRE");
			frmMain.lblTitle.Text = ConfigManager.Instance.GetValue(fileName, "Header", "BASIC_TITLE", "#TITLE");
			frmMain.lblArtist.Text = ConfigManager.Instance.GetValue(fileName, "Header", "BASIC_ARTIST", "#ARTIST");
			frmMain.lblPlayLevel.Text = ConfigManager.Instance.GetValue(fileName, "Header", "BASIC_PLAYLEVEL", "#PLAYLEVEL");
			frmMain.lblBPM.Text = ConfigManager.Instance.GetValue(fileName, "Header", "BASIC_BPM", "#BPM");

			frmMain.lblPlayRank.Text = ConfigManager.Instance.GetValue(fileName, "Header", "EXPAND_RANK", "#RANK");
			DataSource.DsListPlayRank[0].Text = ConfigManager.Instance.GetValue(fileName, "Header", "EXPAND_RANK_VERY_HARD", "Very Hard");
			DataSource.DsListPlayRank[1].Text = ConfigManager.Instance.GetValue(fileName, "Header", "EXPAND_RANK_HARD", "Hard");
			DataSource.DsListPlayRank[2].Text = ConfigManager.Instance.GetValue(fileName, "Header", "EXPAND_RANK_NORMAL", "Normal");
			DataSource.DsListPlayRank[3].Text = ConfigManager.Instance.GetValue(fileName, "Header", "EXPAND_RANK_EASY", "Easy");
			DataSource.Update(frmMain.cboPlayRank);
			frmMain.lblTotal.Text = ConfigManager.Instance.GetValue(fileName, "Header", "EXPAND_TOTAL", "#TOTAL");
			frmMain.lblVolume.Text = ConfigManager.Instance.GetValue(fileName, "Header", "EXPAND_VOLWAV", "#VOLWAV");
			frmMain.lblStageFile.Text = ConfigManager.Instance.GetValue(fileName, "Header", "EXPAND_STAGEFILE", "#STAGEFILE");
			frmMain.lblMissBMP.Text = ConfigManager.Instance.GetValue(fileName, "Header", "EXPAND_BPM_MISS", "#BMP00");
			frmMain.cmdLoadMissBMP.Text = ConfigManager.Instance.GetValue(fileName, "Header", "EXPAND_SET_FILE", "...");
			frmMain.cmdLoadStageFile.Text = ConfigManager.Instance.GetValue(fileName, "Header", "EXPAND_SET_FILE", "...");

			frmMain.lblDispFrame.Text = ConfigManager.Instance.GetValue(fileName, "Header", "CONFIG_KEY_FRAME", "Key Frame");
			DataSource.DsListDispFrame[0].Text = ConfigManager.Instance.GetValue(fileName, "Header", "CONFIG_KEY_HALF", "Half");
			DataSource.DsListDispFrame[1].Text = ConfigManager.Instance.GetValue(fileName, "Header", "CONFIG_KEY_SEPARATE", "Separate");
			DataSource.Update(frmMain.cboDispFrame);
			frmMain.lblDispKey.Text = ConfigManager.Instance.GetValue(fileName, "Header", "CONFIG_KEY_POSITION", "Key Position");
			DataSource.DsListDispKey[0].Text = ConfigManager.Instance.GetValue(fileName, "Header", "CONFIG_KEY_5KEYS", "5Keys/10Keys");
			DataSource.DsListDispKey[1].Text = ConfigManager.Instance.GetValue(fileName, "Header", "CONFIG_KEY_7KEYS", "7Keys/14Keys");
			DataSource.Update(frmMain.cboDispKey);
			frmMain.lblDispSC1P.Text = ConfigManager.Instance.GetValue(fileName, "Header", "CONFIG_SCRATCH_1P", "Scratch 1P");
			DataSource.DsListDispSC1P[0].Text = ConfigManager.Instance.GetValue(fileName, "Header", "CONFIG_SCRATCH_LEFT", "L");
			DataSource.DsListDispSC1P[1].Text = ConfigManager.Instance.GetValue(fileName, "Header", "CONFIG_SCRATCH_RIGHT", "R");
			DataSource.Update(frmMain.cboDispSC1P);
			frmMain.lblDispSC2P.Text = ConfigManager.Instance.GetValue(fileName, "Header", "CONFIG_SCRATCH_2P", "2P");
			DataSource.DsListDispSC2P[0].Text = ConfigManager.Instance.GetValue(fileName, "Header", "CONFIG_SCRATCH_LEFT", "L");
			DataSource.DsListDispSC2P[1].Text = ConfigManager.Instance.GetValue(fileName, "Header", "CONFIG_SCRATCH_RIGHT", "R");
			DataSource.Update(frmMain.cboDispSC2P);

			frmMain.optChangeBottom0.Text = ConfigManager.Instance.GetValue(fileName, "Material", "TAB_WAV", "#WAV");
			frmMain.optChangeBottom1.Text = ConfigManager.Instance.GetValue(fileName, "Material", "TAB_BMP", "#BMP");
			frmMain.optChangeBottom2.Text = ConfigManager.Instance.GetValue(fileName, "Material", "TAB_BGA", "#BGA");
			frmMain.optChangeBottom3.Text = ConfigManager.Instance.GetValue(fileName, "Material", "TAB_BEAT", "Beat");
			frmMain.optChangeBottom4.Text = ConfigManager.Instance.GetValue(fileName, "Material", "TAB_EXPAND", "Expand");

			frmMain.cmdSoundStop.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_STOP", "Stop");
			frmMain.cmdSoundExcUp.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_EXCHANGE_UP", "<");
			frmMain.cmdSoundExcDown.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_EXCHANGE_DOWN", ">");
			frmMain.cmdSoundDelete.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_DELETE", "Del");
			frmMain.cmdSoundLoad.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_SET_FILE", "...");

			frmMain.cmdBMPPreview.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_PREVIEW", "Preview");
			frmMain.cmdBMPExcUp.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_EXCHANGE_UP", "<");
			frmMain.cmdBMPExcDown.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_EXCHANGE_DOWN", ">");
			frmMain.cmdBMPDelete.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_DELETE", "Del");
			frmMain.cmdBMPLoad.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_SET_FILE", "...");

			frmMain.cmdBGAPreview.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_PREVIEW", "Preview");
			frmMain.cmdBGAExcUp.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_EXCHANGE_UP", "<");
			frmMain.cmdBGAExcDown.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_EXCHANGE_DOWN", ">");
			frmMain.cmdBGADelete.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_DELETE", "Del");
			frmMain.cmdBGASet.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_INPUT", "Input");

			frmMain.cmdMeasureSelectAll.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_SELECT_ALL", "All");

			frmMain.cmdInputMeasureLen.Text = ConfigManager.Instance.GetValue(fileName, "Material", "MATERIAL_INPUT", "Input");

			frmMain.lblGridMain.Text = ConfigManager.Instance.GetValue(fileName, "ToolBar", "GRID_MAIN", "Grid");
			frmMain.lblGridSub.Text = ConfigManager.Instance.GetValue(fileName, "ToolBar", "GRID_SUB", "Sub");
			frmMain.lblDispHeight.Text = ConfigManager.Instance.GetValue(fileName, "ToolBar", "DISP_HEIGHT", "Height");
			frmMain.lblDispWidth.Text = ConfigManager.Instance.GetValue(fileName, "ToolBar", "DISP_WIDTH", "Width");
			frmMain.lblVScroll.Text = ConfigManager.Instance.GetValue(fileName, "ToolBar", "VSCROLL", "VScroll");

			if (frmMain.tlbMenuEdit.Checked)
			{
				frmMain.staMainMode.Text = g_strStatusBar[19];
			}
			else if (frmMain.tlbMenuWrite.Checked)
			{
				frmMain.staMainMode.Text = g_strStatusBar[20];
			}
			else
			{
				frmMain.staMainMode.Text = g_strStatusBar[21];
			}

			frmMain.tlbMenuNew.ToolTipText = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_NEW", "New");
			frmMain.tlbMenuNew.AccessibleDescription= ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_NEW", "New");
			frmMain.tlbMenuOpen.ToolTipText = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_OPEN", "Open");
			frmMain.tlbMenuOpen.AccessibleDescription = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_OPEN", "Open");
			frmMain.tlbMenuReload.ToolTipText = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_RELOAD", "Reload");
			frmMain.tlbMenuReload.AccessibleDescription = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_RELOAD", "Reload");
			frmMain.tlbMenuSave.ToolTipText = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_SAVE", "Save");
			frmMain.tlbMenuSave.AccessibleDescription = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_SAVE", "Save");
			frmMain.tlbMenuSaveAs.ToolTipText = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_SAVE_AS", "Save As");
			frmMain.tlbMenuSaveAs.AccessibleDescription = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_SAVE_AS", "Save As");

			frmMain.tlbMenuEdit.ToolTipText = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_MODE_EDIT", "Edit Mode");
			frmMain.tlbMenuEdit.AccessibleDescription = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_MODE_EDIT", "Edit Mode");
			frmMain.tlbMenuWrite.ToolTipText = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_MODE_WRITE", "Write Mode");
			frmMain.tlbMenuWrite.AccessibleDescription = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_MODE_WRITE", "Write Mode");
			frmMain.tlbMenuDelete.ToolTipText = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_MODE_DELETE", "Delete Mode");
			frmMain.tlbMenuDelete.AccessibleDescription = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_MODE_DELETE", "Delete Mode");

			frmMain.tlbMenuPlayAll.ToolTipText = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_PLAY_FIRST", "Play All");
			frmMain.tlbMenuPlayAll.AccessibleDescription = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_PLAY_FIRST", "Play All");
			frmMain.tlbMenuPlay.ToolTipText = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_PLAY", "Play From Current Position");
			frmMain.tlbMenuPlay.AccessibleDescription = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_PLAY", "Play From Current Position");
			frmMain.tlbMenuStop.ToolTipText = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_STOP", "Stop");
			frmMain.tlbMenuStop.AccessibleDescription = ConfigManager.Instance.GetValue(fileName, "ToolBar", "TOOLTIP_STOP", "Stop");

			frmWindowFind.Text = ConfigManager.Instance.GetValue(fileName, "Find", "TITLE", "Find/Delete/Replace");

			frmWindowFind.fraSearchObject.Text = ConfigManager.Instance.GetValue(fileName, "Find", "FRAME_SEARCH", "Range");
			frmWindowFind.fraSearchMeasure.Text = ConfigManager.Instance.GetValue(fileName, "Find", "FRAME_MEASURE", "Range of measure");
			frmWindowFind.fraSearchNum.Text = ConfigManager.Instance.GetValue(fileName, "Find", "FRAME_OBJ_NUM", "Range of object number");
			frmWindowFind.fraSearchGrid.Text = ConfigManager.Instance.GetValue(fileName, "Find", "FRAME_GRID", "Lane");
			frmWindowFind.fraProcess.Text = ConfigManager.Instance.GetValue(fileName, "Find", "FRAME_PROCESS", "Replace to");

			frmWindowFind.optSearchAll.Text = ConfigManager.Instance.GetValue(fileName, "Find", "OPT_OBJ_ALL", "All object");
			frmWindowFind.optSearchSelect.Text = ConfigManager.Instance.GetValue(fileName, "Find", "OPT_OBJ_SELECT", "Selected object");
			frmWindowFind.optProcessSelect.Text = ConfigManager.Instance.GetValue(fileName, "Find", "OPT_PROCESS_SELECT", "Select");
			frmWindowFind.optProcessDelete.Text = ConfigManager.Instance.GetValue(fileName, "Find", "OPT_PROCESS_DELETE", "Delete");
			frmWindowFind.optProcessReplace.Text = ConfigManager.Instance.GetValue(fileName, "Find", "OPT_PROCESS_REPLACE", "Replace to");

			frmWindowFind.cmdInvert.Text = ConfigManager.Instance.GetValue(fileName, "Find", "CMD_INVERT", "Invert");
			frmWindowFind.cmdReset.Text = ConfigManager.Instance.GetValue(fileName, "Find", "CMD_RESET", "Reset");
			frmWindowFind.cmdSelect.Text = ConfigManager.Instance.GetValue(fileName, "Find", "CMD_SELECT", "Select All");
			frmWindowFind.cmdClose.Text = ConfigManager.Instance.GetValue(fileName, "Find", "CMD_CLOSE", "Close");
			frmWindowFind.cmdDecide.Text = ConfigManager.Instance.GetValue(fileName, "Find", "CMD_DECIDE", "Run");

			frmWindowFind.lblNotice.Text = ConfigManager.Instance.GetValue(fileName, "Find", "LBL_NOTICE", "This item doesn't influence BPM/STOP object");

			frmWindowFind.lblNotice.Text = ConfigManager.Instance.GetValue(fileName, "Find", "LBL_NOTICE", "This item doesn't influence BPM/STOP object");
			frmWindowFind.lblMeasure.Text = ConfigManager.Instance.GetValue(fileName, "Find", "LBL_DASH", "to");
			frmWindowFind.lblNum.Text = ConfigManager.Instance.GetValue(fileName, "Find", "LBL_DASH", "to");

			frmWindowInput.Text = ConfigManager.Instance.GetValue(fileName, "Input", "TITLE", "Input Form");

			frmWindowViewer.Text = ConfigManager.Instance.GetValue(fileName, "Viewer", "TITLE", "Viewer Setting");

			frmWindowViewer.cmdViewerPath.Text = ConfigManager.Instance.GetValue(fileName, "Viewer", "CMD_SET", "...");
			frmWindowViewer.cmdAdd.Text = ConfigManager.Instance.GetValue(fileName, "Viewer", "CMD_ADD", "Add");
			frmWindowViewer.cmdDelete.Text = ConfigManager.Instance.GetValue(fileName, "Viewer", "CMD_DELETE", "Delete");
			frmWindowViewer.cmdOK.Text = ConfigManager.Instance.GetValue(fileName, "Viewer", "CMD_OK", "OK");
			frmWindowViewer.cmdCancel.Text = ConfigManager.Instance.GetValue(fileName, "Viewer", "CMD_CANCEL", "Cancel");

			frmWindowViewer.lblViewerName.Text = ConfigManager.Instance.GetValue(fileName, "Viewer", "LBL_APP_NAME", "Player name");
			frmWindowViewer.lblViewerPath.Text = ConfigManager.Instance.GetValue(fileName, "Viewer", "LBL_APP_PATH", "Path");
			frmWindowViewer.lblPlayAll.Text = ConfigManager.Instance.GetValue(fileName, "Viewer", "LBL_ARG_PLAY_ALL", "Argument of \"Play All\"");
			frmWindowViewer.lblPlay.Text = ConfigManager.Instance.GetValue(fileName, "Viewer", "LBL_ARG_PLAY", "Argument of \"Play\"");
			frmWindowViewer.lblStop.Text = ConfigManager.Instance.GetValue(fileName, "Viewer", "LBL_ARG_STOP", "Argument of \"Stop\"");
			frmWindowViewer.lblNotice.Text = ConfigManager.Instance.GetValue(fileName, "Viewer", "LBL_INFO", "Syntax reference:\n<filename> File name\n<measure> Current measure");

			frmWindowConvert.Text = ConfigManager.Instance.GetValue(fileName, "Convert", "TITLE", "Conversion Wizard");

			frmWindowConvert.chkDeleteUnusedFile.Text = ConfigManager.Instance.GetValue(fileName, "Convert", "CHK_DELETE_LIST", "Clear unused definition from a list");

			frmWindowConvert.chkDeleteFile.Text = ConfigManager.Instance.GetValue(fileName, "Convert", "CHK_DELETE_FILE", "Delete unused files in this BMS folder (*)");
			frmWindowConvert.lblExtension.Text = ConfigManager.Instance.GetValue(fileName, "Convert", "LBL_EXTENSION", "Search extensions:");
			frmWindowConvert.chkFileRecycle.Text = ConfigManager.Instance.GetValue(fileName, "Convert", "CHK_RECYCLE", "Delete soon with no through recycled");

			frmWindowConvert.chkListAlign.Text = ConfigManager.Instance.GetValue(fileName, "Convert", "CHK_ALIGN_LIST", "Sort definition list");
			frmWindowConvert.chkUseOldFormat.Text = ConfigManager.Instance.GetValue(fileName, "Convert", "CHK_USE_OLD_FORMAT", "Use old Format [01 - FF] if possible");
			frmWindowConvert.chkSortByName.Text = ConfigManager.Instance.GetValue(fileName, "Convert", "CHK_SORT_BY_NAME", "Sorting by filename");

			frmWindowConvert.chkFileNameConvert.Text = ConfigManager.Instance.GetValue(fileName, "Convert", "CHK_CONVERT_FILENAME", "Change filename to list number [01 - ZZ] (*)");

			frmWindowConvert.lblNotice.Text = ConfigManager.Instance.GetValue(fileName, "Convert", "LBL_NOTICE", "(*) Cannot undo this command");

			frmWindowConvert.cmdDecide.Text = ConfigManager.Instance.GetValue(fileName, "Convert", "CMD_DECIDE", "Run");
			frmWindowConvert.cmdCancel.Text = ConfigManager.Instance.GetValue(fileName, "Convert", "CMD_CANCEL", "Cancel");

			g_Message[(int)Message.ERR_01] = ConfigManager.Instance.GetValue(fileName, "Message", "ERROR_MESSAGE_01", "The unexpected error occurred. Program will shut down.\nRefer to the \"error.txt\" for the details of an error.");
			g_Message[(int)Message.ERR_02] = ConfigManager.Instance.GetValue(fileName, "Message", "ERROR_MESSAGE_02", "Temporary file is saved to...");
			g_Message[(int)Message.ERR_FILE_NOT_FOUND] = ConfigManager.Instance.GetValue(fileName, "Message", "ERROR_FILE_NOT_FOUND", "File not found.");
			g_Message[(int)Message.ERR_LOAD_CANCEL] = ConfigManager.Instance.GetValue(fileName, "Message", "ERROR_LOAD_CANCEL", "Loading will be aborted.");
			g_Message[(int)Message.ERR_SAVE_ERROR] = ConfigManager.Instance.GetValue(fileName, "Message", "ERROR_SAVE_ERROR", "Error occured while saving.");
			g_Message[(int)Message.ERR_SAVE_CANCEL] = ConfigManager.Instance.GetValue(fileName, "Message", "ERROR_SAVE_CANCEL", "Saving will be aborted.");
			g_Message[(int)Message.ERR_OVERFLOW_LARGE] = ConfigManager.Instance.GetValue(fileName, "Message", "ERROR_OVERFLOW_LARGE", "Error:\nValue is too large.");
			g_Message[(int)Message.ERR_OVERFLOW_SMALL] = ConfigManager.Instance.GetValue(fileName, "Message", "ERROR_OVERFLOW_SMALL", "Error:\nValue is too small.");
			g_Message[(int)Message.ERR_OVERFLOW_BPM] = ConfigManager.Instance.GetValue(fileName, "Message", "ERROR_OVERFLOW_BPM", "You have used more than 1295 BPM change command.\nNumber of commands should be less than 1295.");
			g_Message[(int)Message.ERR_OVERFLOW_STOP] = ConfigManager.Instance.GetValue(fileName, "Message", "ERROR_OVERFLOW_STOP", "You have used more than 1295 STOP command.\nNumber of commands should be less than 1295.");
			g_Message[(int)Message.ERR_APP_NOT_FOUND] = ConfigManager.Instance.GetValue(fileName, "Message", "ERROR_APP_NOT_FOUND", " is not found.");
			g_Message[(int)Message.ERR_FILE_ALREADY_EXIST] = ConfigManager.Instance.GetValue(fileName, "Message", "ERROR_FILE_ALREADY_EXIST", "File already exist.");

			g_Message[(int)Message.MSG_CONFIRM] = ConfigManager.Instance.GetValue(fileName, "Message", "INFO_CONFIRM", "This command cannot be undone, OK?");
			g_Message[(int)Message.MSG_FILE_CHANGED] = ConfigManager.Instance.GetValue(fileName, "Message", "INFO_FILE_CHANGED", "Do you want to save changes?");
			g_Message[(int)Message.MSG_INI_CHANGED] = ConfigManager.Instance.GetValue(fileName, "Message", "INFO_INI_CHANGED", "ini format has changed.\n(All setting will reset)");
			g_Message[(int)Message.MSG_ALIGN_LIST] = ConfigManager.Instance.GetValue(fileName, "Message", "INFO_ALIGN_LIST", "Do you want the filelist to be rewrited into the old format [01 - FF]?\n(Attention: Some programs are compatible only with old format files.)");
			g_Message[(int)Message.MSG_DELETE_FILE] = ConfigManager.Instance.GetValue(fileName, "Message", "INFO_DELETE_FILE", "They have been deleted:");

			g_Message[(int)Message.INPUT_BPM] = ConfigManager.Instance.GetValue(fileName, "Message", "INPUT_BPM", "Enter the BPM you wish to change to.\n(Decimal number can be used. Enter 0 to cancel)");
			g_Message[(int)Message.INPUT_STOP] = ConfigManager.Instance.GetValue(fileName, "Message", "INPUT_STOP", "Enter the length of stoppage 1 corresponds to 1/192 of the measure.\n(Enter under 0 to cancel)");
			g_Message[(int)Message.INPUT_RENAME] = ConfigManager.Instance.GetValue(fileName, "Message", "INPUT_RENAME", "Please enter new filename.");
			g_Message[(int)Message.INPUT_SIZE] = ConfigManager.Instance.GetValue(fileName, "Message", "INPUT_SIZE", "Type your display magnification.\n(Maximum 16.00. Enter under 0 to cancel)");

			string defaultFont = System.Windows.Forms.Control.DefaultFont.Name;
			LoadFont(ConfigManager.Instance.GetValue(fileName, "Main", "Font", defaultFont), ConfigManager.Instance.GetValue(fileName, "Main", "FixedFont", defaultFont), ConfigManager.Instance.GetValue(fileName, "Main", "Charset", 1));

			frmMain.Form_Resize(null, null);
		}

		private void LoadFont(string mainFont, string fixedFont, int charset)
		{
			try
			{
				Form[] forms = { frmMain, frmWindowAbout, frmWindowConvert, frmWindowFind, frmWindowInput, frmWindowPreview, frmWindowTips, frmWindowViewer };

				foreach (Form form in forms)
				{
					form.Font = new Font(mainFont, 9);

					foreach (Control control in form.Controls)
					{
						if (control is Label || control is TextBox || control is ComboBox || control is Button
							|| control is RadioButton || control is CheckBox || control is GroupBox)
						{
							float curSize = control.Font.Size;
							control.Font = new Font(mainFont, curSize);
						}
						else if (control is PictureBox || control is ListBox)
						{
							float curSize = control.Font.Size;
							control.Font = new Font(fixedFont, curSize);
						}

						LoadChildFont(control, mainFont, fixedFont);
					}
				}
			}
			catch(Exception e)
			{
				CleanUp(e.Message, "LoadFont");
			}
		}

		private void LoadChildFont(Control c, string mainFont, string fixedFont)
		{
			foreach (Control control in c.Controls)
			{
				if(control is Label || control is TextBox || control is ComboBox || control is Button || control is RadioButton || control is CheckBox || control is GroupBox)
				{
					control.Font = new Font(mainFont, 9);
				}
				else if (control is PictureBox || control is ListBox)
				{
					control.Font = new Font(fixedFont, 9);
				}

				// 再帰的にコントロールを取得
				LoadChildFont(control, mainFont, fixedFont);
			}
		}

		public void LoadConfig()
		{
			int i;
			string retStr;
			int retInt;

			// bmse.xmlの検証
			if (!"BMSE".Equals(ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "Main", "Key", "")))
			{
				throw new Exception();
			}

			retStr = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "Main", "Language", "english.xml");

			for (i = 0; i < frmMain.mnuLanguage.DropDownItems.Count; i++)
			{
				if (retStr.Equals(g_strLangFileName[i]))
				{
					((ToolStripMenuItem)frmMain.mnuLanguage.DropDownItems[i]).Checked = true;
					break;
				}
			}

			LoadLanguageFile(CommonConst.CONFDIR_LANG + retStr);

			// TODO: frmWindowPreview.SetWindowSize()
			
			if(!INI_VERSION.Equals(ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "Main", "ini", "")))
			{
				MessageBox.Show(g_Message[(int)Message.MSG_INI_CHANGED], g_strAppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
				throw new Exception();
			}

			retStr = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "Main", "Theme", "default.xml");

			for (i = 0; i < frmMain.mnuTheme.DropDownItems.Count; i++)
			{
				if (retStr.Equals(g_strThemeFileName[i]))
				{
					((ToolStripMenuItem)frmMain.mnuTheme.DropDownItems[i]).Checked = true;
					break;
				}
			}

			LoadThemeFile(CommonConst.CONFDIR_THEME + retStr);

			g_strHelpFileName = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "Main", "Help", "");
			g_strFiler = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "Main", "Filer", "");

			if (!"".Equals(g_strHelpFileName))
			{
				frmMain.mnuHelpOpen.Enabled = true;
			}

			// cboDispWidthの選択状態を設定ファイルから読み込む
			retInt = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "Width", 100);
			
			for (i = 0; i < frmMain.cboDispWidth.Items.Count - 1; i++)	// コンボボックスの最後の要素はデータではないので、最後の要素は省く
			{
				if (DataSource.DsListDispWidth[i].Value == retInt)
				{
					frmMain.cboDispWidth.SelectedIndex = i;
					break;
				}
				else if (DataSource.DsListDispWidth[i].Value > retInt)
				{
					DataSource.DsListDispWidth.Insert(i, new Ds(retInt, "x" + ((double)retInt / 100).ToString("0.00")));
					DataSource.Update(frmMain.cboDispWidth);
					frmMain.cboDispWidth.SelectedIndex = i;
					break;
				}
			}

			// cboDispHeightの選択状態を設定ファイルから読み込む
			retInt = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "Height", 100);

			for (i = 0; i < frmMain.cboDispHeight.Items.Count - 1; i++)	// コンボボックスの最後の要素はデータではないので、最後の要素は省く
			{
				if (DataSource.DsListDispHeight[i].Value == retInt)
				{
					frmMain.cboDispHeight.SelectedIndex = i;
					break;
				}
				else if (DataSource.DsListDispHeight[i].Value > retInt)
				{
					DataSource.DsListDispHeight.Insert(i, new Ds(retInt, "x" + ((double)retInt / 100).ToString("0.00")));
					DataSource.Update(frmMain.cboDispHeight);
					frmMain.cboDispHeight.SelectedIndex = i;
					break;
				}
			}

			frmMain.cboDispGridMain.SelectedIndex = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "VGridMain", 1);
			frmMain.cboDispGridSub.SelectedIndex = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "VGridSub", 1);
			frmMain.cboDispFrame.SelectedIndex = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "Frame", 1);
			frmMain.cboDispKey.SelectedIndex = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "Key", 1);
			frmMain.cboDispSC1P.SelectedIndex = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "SC_1P", 1);
			frmMain.cboDispSC2P.SelectedIndex = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "SC_2P", 1);

			frmMain.mnuViewToolBar.Checked = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "ToolBar", true);
			frmMain.mnuViewDirectInput.Checked = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "DirectInput", true);
			frmMain.mnuViewStatusBar.Checked = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "StatusBar", true);

			if (frmMain.cboViewer.Items.Count != 0)
			{
				if (frmMain.cboViewer.Items.Count > ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "ViewerNum", 0))
				{
					frmMain.cboViewer.SelectedIndex = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "ViewerNum", 0);
				}
				else
				{
					frmMain.cboViewer.SelectedIndex = 0;
				}
			}

			frmMain.mnuOptionsActiveIgnore.Checked = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "Active", true);
			frmMain.mnuOptionsFileNameOnly.Checked = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "FileNameOnly", false);
			frmMain.mnuOptionsVertical.Checked = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "VerticalWriting", false);
			frmMain.mnuOptionsLaneBG.Checked = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "LaneBG", true);
			frmMain.mnuOptionsSelectPreview.Checked = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "SelectSound", true);
			frmMain.mnuOptionsMoveOnGrid.Checked = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "MoveOnGrid", true);
			frmMain.mnuOptionsObjectFileName.Checked = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "ObjectFileName", false);
			frmMain.mnuOptionsNumFF.Checked = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "UseOldFormat", false);
			frmMain.mnuOptionsRightClickDelete.Checked = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "View", "RightClickDelete", false);

			frmMain.tlbMenuNew.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "New", true);
			frmMain.tlbMenuOpen.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Open", true);
			frmMain.tlbMenuReload.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Reload", false);
			frmMain.tlbMenuSave.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Save", true);
			frmMain.tlbMenuSaveAs.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "SaveAs", true);

			frmMain.tlbMenuSepMode.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Mode", true);
			frmMain.tlbMenuPlayAll.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Mode", true);
			frmMain.tlbMenuPlay.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Mode", true);
			frmMain.tlbMenuStop.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Mode", true);

			frmMain.lblGridMain.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Grid", true);
			frmMain.cboDispGridMain.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Grid", true);
			frmMain.lblGridSub.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Grid", true);
			frmMain.cboDispGridMain.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Grid", true);

			frmMain.lblDispHeight.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Size", true);
			frmMain.cboDispHeight.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Size", true);
			frmMain.lblDispWidth.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Size", true);
			frmMain.cboDispWidth.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Size", true);

			frmMain.lblVScroll.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Resolution", false);
			frmMain.cboVScroll.Visible = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "ToolBar", "Resolution", false);

			for (i = 0; i < g_strRecentFiles.Length; i++)
			{
				g_strRecentFiles[i] = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "RecentFiles", i.ToString(), "");

				string istr = (i + 1).ToString();

				if (g_strRecentFiles[i].Length != 0)
				{
					frmMain.mnuRecentFiles[i].Text = "&" + istr.Substring(istr.Length - 1) + ":" + g_strRecentFiles[i];
					frmMain.mnuRecentFiles[i].Enabled = true;
					frmMain.mnuRecentFiles[i].Visible = true;

					frmMain.tlbMenuOpen.DropDownItems[i].Text = "&" + istr.Substring(istr.Length - 1) + ":" + g_strRecentFiles[i];
					frmMain.tlbMenuOpen.DropDownItems[i].Enabled = true;
					frmMain.tlbMenuOpen.DropDownItems[i].Visible = true;

					frmMain.mnuLineRecent.Visible = true;
				}
			}

			frmMain.Visible = false;
			frmMain.Width = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "Main", "Width", 800);
			frmMain.Height = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "Main", "Height", 600);
			frmMain.Left = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "Main", "X", 0);
			frmMain.Top = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "Main", "Y", 0);

			// TODO: modEasterEgg.InitEffect();

			frmWindowPreview.Left = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "Preview", "X", ((frmMain.Left + frmMain.Width / 2) - frmWindowPreview.Width / 2));
			if (frmWindowPreview.Left < 0)
			{
				frmWindowPreview.Left = 0;
			}
			if (frmWindowPreview.Left > Screen.PrimaryScreen.Bounds.Width)
			{
				frmWindowPreview.Left = 0;
			}

			frmWindowPreview.Top = ConfigManager.Instance.GetValue(CommonConst.XML_COMMON, "Preview", "Y", ((frmMain.Top + frmMain.Height / 2) - frmWindowPreview.Height / 2));
			if (frmWindowPreview.Top < 0)
			{
				frmWindowPreview.Top = 0;
			}
			if (frmWindowPreview.Top > Screen.PrimaryScreen.Bounds.Height)
			{
				frmWindowPreview.Top = 0;
			}
		}

		private void CreateConfig()
		{
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Main", "Key", "BMSE");
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Main", "ini", INI_VERSION);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Main", "X", 0);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Main", "Y", 0);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Main", "Width", "800");
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Main", "Height", "600");
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Main", "Language", "english.xml");
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Main", "Theme", "default.xml");
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Main", "Help", "");

			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "View", "Width", 100);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "View", "Height", 100);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "View", "VGridMain", 1);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "View", "VGridSub", 1);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "View", "Frame", 1);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "View", "Key", 1);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "View", "SC_1P", 0);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "View", "SC_2P", 1);

			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "View", "ToolBar", true);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "View", "DirectInput", true);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "View", "StatusBar", true);

			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "View", "ViewerNum", 0);

			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "ToolBar", "New", true);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "ToolBar", "Open", true);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "ToolBar", "Reload", false);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "ToolBar", "Save", true);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "ToolBar", "SaveAs", true);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "ToolBar", "Mode", true);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "ToolBar", "Preview", true);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "ToolBar", "Grid", true);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "ToolBar", "Size", true);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "ToolBar", "Resolution", false);

			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Options", "Active", true);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Options", "FileNameOnly", false);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Options", "VerticalWriting", false);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Options", "LaneBG", true);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Options", "SelectSound", true);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Options", "MoveOnGrid", true);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Options", "ObjectFileName", false);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Options", "UseOldFormat", false);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Options", "RightClickDelete", false);

			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Preview", "X", 0);
			ConfigManager.Instance.SetValue(CommonConst.XML_COMMON, "Preview", "Y", 0);
		}
	}
}
