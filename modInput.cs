using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Bmse.Util;

namespace Bmse
{
	partial class Module
	{
		private bool mReadFlag;
		private string mExInfo;
		private bool[] mBGM;

		public void LoadBMS()
		{
			try
			{
				frmMain.Text = gAppTitle + " - Now Loading";

				LoadBMSStart();

				LoadBMSData();

				Array.Resize(ref gObj, gObj.Length);

				for (int i = 0; i < gObj.Length - 1; i++)
				{
					gObj[i].position = (gMeasure[gObj[i].measure].len / gObj[i].height) * gObj[i].position;

					if (gObj[i].ch == 3)	// BPM
					{
						gObj[i].ch = 8;
					}
					else if (gObj[i].ch == 8)	// 拡張BPM
					{
						if (gBPM[gObj[i].value] == 0)
						{
							gObj[i].ch = 0;
						}
						else
						{
							gObj[i].value = (int)gBPM[gObj[i].value];
						}
					}
					else if (gObj[i].ch == 9)	// ストップシーケンス
					{
						gObj[i].value = gSTOP[gObj[i].value];
					}
				}

				LoadBMSEnd();
			}
			catch (Exception e)
			{
				CleanUp(e.Message, "LoadBMS");
			}
		}

		public void LoadBMSStart()
		{
			try
			{
				for (int i = 0; i < 1296; i++)
				{
					gWAV[i] = "";
					gBMP[i] = "";
					gBGA[i] = "";
					gBPM[i] = 0;
					gSTOP[i] = 0;
				}

				frmMain.cboPlayer.SelectedIndex = 0;
				frmMain.txtGenre.Text = "";
				frmMain.txtTitle.Text = "";
				frmMain.txtArtist.Text = "";
				frmMain.cboPlayLevel.Text = "1";
				frmMain.txtBPM.Text = "120";
				frmMain.cboPlayRank.SelectedIndex = 3;
				frmMain.txtTotal.Text = "";
				frmMain.txtVolume.Text = "";
				frmMain.txtStageFile.Text = "";
				frmMain.txtMissBMP.Text = "";
				frmMain.lstWAV.SelectedIndex = 0;
				frmMain.lstBMP.SelectedIndex = 0;
				frmMain.lstBGA.SelectedIndex = 0;
				frmMain.lstMeasureLen.SelectedIndex = 0;
				frmMain.lstMeasureLen.Visible = false;
				frmMain.txtExInfo.Text = "";
				frmMain.Enabled = false;

				frmMain.vsbMain.Value = frmMain.vsbMain.Maximum - 0;
				frmMain.hsbMain.Value = frmMain.hsbMain.Maximum - 0;

				for (int i = 0; i < 1000; i++)
				{
					gMeasure[i].len = 192;
					frmMain.lstMeasureLen.Items[i] = "#" + i.ToString("000") + ":4/4";
				}

				gBms.playerType = 1;
				gBms.genre = "";
				gBms.title = "";
				gBms.artist = "";
				gBms.bpm = 120;
				gBms.playLevel = 1;
				gBms.playRank = 3;
				gBms.total = 0;
				gBms.volume = 0;
				gBms.stageFile = "";

				gDisp.maxMeasure = 0;
				ChangeMaxMeasure(15);
				ChangeResolution();

				gInputLog.Clear();

				gObj = new Obj[1];
				gObjID = new int[1];
				gIDNum = 0;

				mReadFlag = true;
				mExInfo = "";

				mBGM = new bool[32000];

				for (int i = 0; i < mBGM.Length; i++)
				{
					mBGM[i] = false;
				}
			}
			catch (Exception e)
			{
				CleanUp(e.Message, "LoadBMSStart");
			}
		}

		public void LoadBMSEnd()
		{
			try
			{
				//TODO: LoadEffect();

				//TODO: frmMain.RefreshList()

				frmMain.lstMeasureLen.Visible = true;

				frmMain.Text = gAppTitle;

				if (gBms.dir.Length != 0)
				{
					if (frmMain.mnuOptionsFileNameOnly.Checked)
					{
						frmMain.Text = frmMain.Text + " - " + gBms.fileName;
					}
					else
					{
						frmMain.Text = frmMain.Text + " - " + gBms.dir + gBms.fileName;
					}
				}

				ChangeResolution();

				frmMain.Enabled = true;

				if ("PMS".Equals(StringUtil.Right(gBms.fileName, 3).ToUpper()))
				{
					frmMain.cboPlayer.SelectedIndex = 3;
					gBms.playerType = 4;
				}

				mReadFlag = true;
				frmMain.txtExInfo.Text = mExInfo;
				mExInfo = "";

				Array.Clear(mBGM, 0, mBGM.Length);

				InitVerticalLine();

				frmMain.Show();

				frmMain.picMain.Focus();
			}
			catch (Exception e)
			{
				CleanUp(e.Message, "LoadBMSEnd");
			}
		}

		public void LoadBMSData()
		{
			string[] array;
			string strRet;

			try
			{
				for (int i = 0; i < 1000; i++)
				{
					gMeasure[i].len = 192;
				}

				using (StreamReader reader = new StreamReader(gBms.dir + gBms.fileName))
				{
					while (reader.Peek() >= 0)
					{
						strRet = reader.ReadLine();
						array = strRet.Split('\n');

						for (int i = 0; i < array.Length; i++)
						{
							if ("#".Equals(StringUtil.Left(array[i], 1)))
							{
								LoadBMSDataSub(array[i]);
							}
						}
					}
				}
			}
			catch (FileNotFoundException e)
			{
				MessageBox.Show(gMessage[(int)Message.ERR_FILE_NOT_FOUND] + "\r\n" + gMessage[(int)Message.ERR_LOAD_CANCEL], gAppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

				LoadBMSStart();
				LoadBMSEnd();
				return;
			}
			catch (Exception e)
			{
				CleanUp(e.Message, "LoadBMSData");
			}
		}

		public void LoadBMSDataSub(string lineData)
		{
			string[] array;
			string strRet;
			string strParam;

			try
			{
				// TODO: 280行目から
			}
			catch (Exception e)
			{
				CleanUp(e.Message, "LoadBMSDataSub");
			}
		}

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
