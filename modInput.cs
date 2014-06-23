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
				frmMain.Text = g_strAppTitle + " - Now Loading";

				LoadBMSStart();

				LoadBMSData();

				Array.Resize(ref g_Obj, g_Obj.Length);

				for (int i = 0; i < g_Obj.Length - 1; i++)
				{
					g_Obj[i].lngPosition = (g_Measure[g_Obj[i].intMeasure].intLen / g_Obj[i].lngHeight) * g_Obj[i].lngPosition;

					if (g_Obj[i].intCh == 3)	// BPM
					{
						g_Obj[i].intCh = 8;
					}
					else if (g_Obj[i].intCh == 8)	// 拡張BPM
					{
						if (g_sngBPM[g_Obj[i].sngValue] == 0)
						{
							g_Obj[i].intCh = 0;
						}
						else
						{
							g_Obj[i].sngValue = (int)g_sngBPM[g_Obj[i].sngValue];
						}
					}
					else if (g_Obj[i].intCh == 9)	// ストップシーケンス
					{
						g_Obj[i].sngValue = g_lngSTOP[g_Obj[i].sngValue];
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
					g_strWAV[i] = "";
					g_strBMP[i] = "";
					g_strBGA[i] = "";
					g_sngBPM[i] = 0;
					g_lngSTOP[i] = 0;
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
					g_Measure[i].intLen = 192;
					frmMain.lstMeasureLen.Items[i] = "#" + i.ToString("000") + ":4/4";
				}

				g_BMS.intPlayerType = 1;
				g_BMS.strGenre = "";
				g_BMS.strTitle = "";
				g_BMS.strArtist = "";
				g_BMS.sngBPM = 120;
				g_BMS.lngPlayLevel = 1;
				g_BMS.intPlayRank = 3;
				g_BMS.sngTotal = 0;
				g_BMS.intVolume = 0;
				g_BMS.strStageFile = "";

				g_disp.intMaxMeasure = 0;
				ChangeMaxMeasure(15);
				ChangeResolution();

				g_InputLog.Clear();

				g_Obj = new g_udtObj[1];
				g_lngObjID = new int[1];
				g_lngIDNum = 0;

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

				frmMain.Text = g_strAppTitle;

				if (g_BMS.strDir.Length != 0)
				{
					if (frmMain.mnuOptionsFileNameOnly.Checked)
					{
						frmMain.Text = frmMain.Text + " - " + g_BMS.strFileName;
					}
					else
					{
						frmMain.Text = frmMain.Text + " - " + g_BMS.strDir + g_BMS.strFileName;
					}
				}

				ChangeResolution();

				frmMain.Enabled = true;

				if ("PMS".Equals(StringUtil.Right(g_BMS.strFileName, 3).ToUpper()))
				{
					frmMain.cboPlayer.SelectedIndex = 3;
					g_BMS.intPlayerType = 4;
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
					g_Measure[i].intLen = 192;
				}

				using (StreamReader reader = new StreamReader(g_BMS.strDir + g_BMS.strFileName))
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
				MessageBox.Show(g_Message[(int)Message.ERR_FILE_NOT_FOUND] + "\r\n" + g_Message[(int)Message.ERR_LOAD_CANCEL], g_strAppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

				LoadBMSStart();
				LoadBMSEnd();
				return;
			}
			catch (Exception e)
			{
				CleanUp(e.Message, "LoadBMSData");
			}
		}

		public void LoadBMSDataSub(string lineData, bool directInput)
		{
			string[] array;
			string strRet;
			string strParam;

			try
			{
				array = StringUtil.StringReplace(lineData, " ", ":", 1).Split(':');

				if (array.Length - 1 > 0)
				{
					strParam = StringUtil.Right(lineData, lineData.Length - (array[0].Length + 1));

					switch (array[0])
					{
						case "#PLAYER":
							g_BMS.intPlayerType = int.Parse(strParam);
							frmMain.cboPlayer.SelectedIndex = int.Parse(strParam) - 1;
							break;

						case "#GENRE":
							g_BMS.strGenre = strParam;
							frmMain.txtGenre.Text = strParam;
							break;

						case "#TITLE":
							g_BMS.strTitle = strParam;
							frmMain.txtTitle.Text = strParam;
							break;

						case "#ARTIST":
							g_BMS.strArtist = strParam;
							frmMain.txtArtist.Text = strParam;
							break;

						case "#BPM":
							g_BMS.sngBPM = double.Parse(strParam);
							frmMain.txtBPM.Text = strParam;
							break;

						case "#PLAYLEVEL":
							g_BMS.lngPlayLevel = int.Parse(strParam);
							frmMain.cboPlayLevel.Text = strParam;
							break;

						case "#RANK":
							g_BMS.intPlayRank = int.Parse(strParam);
							if (g_BMS.intPlayRank < 0)
							{
								g_BMS.intPlayRank = 0;
							}
							if (g_BMS.intPlayRank > 3)
							{
								g_BMS.intPlayRank = 3;
							}
							frmMain.cboPlayRank.SelectedIndex = g_BMS.intPlayRank;
							break;

						case "#TOTAL":
							g_BMS.sngTotal = double.Parse(strParam);
							frmMain.txtTotal.Text = strParam;
							break;

						case "#VOLWAV":
							g_BMS.intVolume = int.Parse(strParam);
							frmMain.txtVolume.Text = strParam;
							break;

						case "#STAGEFILE":
							g_BMS.strStageFile = strParam;
							frmMain.txtStageFile.Text = strParam;
							break;

						case "#IF":
						case "#RANDOM":
						case "#RONDAM":
						case "#ENDIF":
							if (!directInput)
							{
								mReadFlag = false;
								mExInfo = mExInfo + lineData + "\r\n";
							}
							break;

						default:
							strRet = StringUtil.Right(array[0], 2).ToUpper();
							switch (StringUtil.Left(array[0], 4).ToUpper())
							{
								case "#WAV":
									if (!"00".Equals(strRet) && !directInput)
									{
										g_strWAV[lngNumConv(strRet)] = StringUtil.Right(lineData, lineData.Length - 7);

										if ((int)(StringUtil.Left(strRet, 1)[0]) > (int)('F')
											|| (int)(StringUtil.Right(strRet, 1)[0]) > (int)('F'))
										{
											frmMain.mnuOptionsNumFF.Checked = false;
										}
									}
									break;

								case "#BMP":
									if (!"00".Equals(strRet) && !directInput)
									{
										g_strBMP[lngNumConv(strRet)] = StringUtil.Right(lineData, lineData.Length - 7);

										if ((int)(StringUtil.Left(strRet, 1)[0]) > (int)('F')
											|| (int)(StringUtil.Right(strRet, 1)[0]) > (int)('F'))
										{
											frmMain.mnuOptionsNumFF.Checked = false;
										}
									}
									else
									{
										frmMain.txtMissBMP.Text = StringUtil.Right(lineData, lineData.Length - 7);
									}
									break;

								case "#BGA":
									if (!"00".Equals(strRet) && !directInput)
									{
										g_strBGA[lngNumConv(strRet)] = StringUtil.Right(lineData, lineData.Length - 7);

										if ((int)(StringUtil.Left(strRet, 1)[0]) > (int)('F')
											|| (int)(StringUtil.Right(strRet, 1)[0]) > (int)('F'))
										{
											frmMain.mnuOptionsNumFF.Checked = false;
										}
									}
									break;

								case "#BPM":
									if ("00".Equals(strRet) && !directInput)
									{
										g_sngBPM[lngNumConv(strRet)] = double.Parse(StringUtil.Right(lineData, lineData.Length - 7));
									}
									break;

								default:
									if ("#STOP".Equals(StringUtil.Left(array[0], 5).ToUpper()))
									{
										if ("00".Equals(strRet) && !directInput)
										{
											g_lngSTOP[lngNumConv(strRet)] = int.Parse(StringUtil.Right(lineData, lineData.Length - 8));
										}
									}
									else if (StringUtil.IsNumeric(StringUtil.Mid(array[0], 2)))
									{
										if (mReadFlag)
										{
											LoadBMSObject(lineData);
										}
										else
										{
											mExInfo = mExInfo + lineData + "\r\n";
										}
									}
									else
									{
										mExInfo = mExInfo + lineData + "\r\n";
									}
									break;
							}
							break;
					}
				}
				else if ("#ENDIF".Equals(StringUtil.Left(lineData, 6).ToUpper()))
				{
					mReadFlag = true;
					mExInfo = mExInfo + lineData + "\r\n";
				}
				else
				{
					mExInfo = mExInfo + lineData + "\r\n";
				}
			}
			catch (Exception e)
			{
				CleanUp(e.Message, "LoadBMSDataSub");
			}
		}

		public void LoadBMSDataSub(string lineData)
		{
			LoadBMSDataSub(lineData, false);
		}

		private void LoadBMSObject(string strRet)
		{
			int intRet = 0;
			int intMeasure;
			int intCh;
			string strParam;
			int lngSepaNum;

			try
			{
				intMeasure = int.Parse(StringUtil.Mid(strRet, 2, 3));
				intCh = int.Parse(StringUtil.Mid(strRet, 5, 2));
				strParam = GetParam(strRet).Trim().ToUpper();

				lngSepaNum = strParam.Length / 2;

				if (intCh == 2)
				{
					if (int.Parse(strParam) == 0 || int.Parse(strParam) == 1)
					{
						return;
					}

					intRet = GCD(192 * int.Parse(strParam), 192);

					if (intRet <= 2)
					{
						intRet = 3;
					}

					if (intRet >= 48)
					{
						intRet = 48;
					}

					g_Measure[intMeasure].intLen = 192 * int.Parse(strParam);

					if (g_Measure[intMeasure].intLen < 3)
					{
						g_Measure[intMeasure].intLen = 3;
					}

					while (g_Measure[intMeasure].intLen / intRet > 64)
					{
						if (intRet >= 48)
						{
							g_Measure[intMeasure].intLen = 3072;
							break;
						}

						intRet = intRet * 2;
					}

					frmMain.lstMeasureLen.Items[intMeasure]
						= "#" + intMeasure.ToString("000")
						+ ":" + (g_Measure[intMeasure].intLen / intRet).ToString()
						+ "/" + (192 / intRet).ToString();

					return;
				}

				if (intCh == 1)
				{
					for (int j = 0; j < 32; j++)
					{
						if (!mBGM[intMeasure * 32 + j])
						{
							mBGM[intMeasure * 32 + j] = true;
							intRet = 101 + j;
							break;
						}
					}
				}

				for (int i = 1; i <= lngSepaNum; i++)
				{
					if (!"00".Equals(StringUtil.Mid(strParam, i * 2 - 1, 2)))
					{
						g_Obj[g_Obj.Length - 1].lngID = g_lngIDNum;
						g_lngObjID[g_lngIDNum] = g_lngIDNum;
						g_Obj[g_Obj.Length - 1].lngPosition = i - 1;
						g_Obj[g_Obj.Length - 1].lngHeight = lngSepaNum;
						g_Obj[g_Obj.Length - 1].intMeasure = intMeasure;
						g_Obj[g_Obj.Length - 1].intCh = intCh;

						ChangeMaxMeasure(g_Obj[g_Obj.Length - 1].intMeasure);

						switch (intCh)
						{
							case 1:	// BGM
								g_Obj[g_Obj.Length - 1].sngValue = lngNumConv(StringUtil.Mid(strParam, i * 2 - 1, 2));
								g_Obj[g_Obj.Length - 1].intCh = intRet;
								break;

							case 4:	// BGA
							case 6:	// Poor
							case 7:	// Layer
							case 8:	// 拡張BPM
							case 9:	// ストップシーケンス
								g_Obj[g_Obj.Length - 1].sngValue = lngNumConv(StringUtil.Mid(strParam, i * 2 - 1, 2));
								break;

							case 3:	// BPM
								g_Obj[g_Obj.Length - 1].sngValue = Convert.ToInt32(StringUtil.Mid(strParam, i * 2 - 1, 2), 16);
								break;

							case 11:	// キー音
							case 12:
							case 13:
							case 14:
							case 15:
							case 16:
							case 18:
							case 19:
							case 21:
							case 22:
							case 23:
							case 24:
							case 25:
							case 26:
							case 28:
							case 29:
								g_Obj[g_Obj.Length - 1].sngValue = lngNumConv(StringUtil.Mid(strParam, i * 2 - 1, 2));
								break;

							case 31:	// キー音
							case 32:
							case 33:
							case 34:
							case 35:
							case 36:
							case 38:
							case 39:
							case 41:
							case 42:
							case 43:
							case 44:
							case 45:
							case 46:
							case 48:
							case 49:
								g_Obj[g_Obj.Length - 1].sngValue = lngNumConv(StringUtil.Mid(strParam, i * 2 - 1, 2));
								g_Obj[g_Obj.Length - 1].intCh = g_Obj[g_Obj.Length - 1].intCh - 20;
								g_Obj[g_Obj.Length - 1].intAtt = 1;
								break;

							case 51:	// キー音
							case 52:
							case 53:
							case 54:
							case 55:
							case 56:
							case 58:
							case 59:
							case 61:
							case 62:
							case 63:
							case 64:
							case 65:
							case 66:
							case 68:
							case 69:
								g_Obj[g_Obj.Length - 1].sngValue = lngNumConv(StringUtil.Mid(strParam, i * 2 - 1, 2));
								g_Obj[g_Obj.Length - 1].intCh -= 40;
								g_Obj[g_Obj.Length - 1].intAtt = 2;
								break;
								
							default:
								return;
						}

						Array.Resize(ref g_Obj, g_Obj.Length + 1);

						g_lngIDNum++;
						Array.Resize(ref g_lngObjID, g_lngIDNum + 1);
					}
				}
			}
			catch (Exception e)
			{
				CleanUp(e.Message, "LoadBMSObject");
			}
		}

		public void SwapObj(int Obj1Num, int Obj2Num)
		{
			g_udtObj dummyObj;

			dummyObj.lngID = g_Obj[Obj1Num].lngID;
			dummyObj.intCh = g_Obj[Obj1Num].intCh;
			dummyObj.sngValue = g_Obj[Obj1Num].sngValue;
			dummyObj.intMeasure = g_Obj[Obj1Num].intMeasure;
			dummyObj.lngPosition = g_Obj[Obj1Num].lngPosition;
			dummyObj.lngHeight = g_Obj[Obj1Num].lngHeight;
			dummyObj.intSelect = g_Obj[Obj1Num].intSelect;
			dummyObj.intAtt = g_Obj[Obj1Num].intAtt;

			g_lngObjID[g_Obj[Obj1Num].lngID]	= Obj2Num;
			g_Obj[Obj1Num].lngID				= g_Obj[Obj2Num].lngID;
			g_Obj[Obj1Num].intCh				= g_Obj[Obj2Num].intCh;
			g_Obj[Obj1Num].sngValue				= g_Obj[Obj2Num].sngValue;
			g_Obj[Obj1Num].intMeasure			= g_Obj[Obj2Num].intMeasure;
			g_Obj[Obj1Num].lngPosition			= g_Obj[Obj2Num].lngPosition;
			g_Obj[Obj1Num].lngHeight			= g_Obj[Obj2Num].lngHeight;
			g_Obj[Obj1Num].intSelect			= g_Obj[Obj2Num].intSelect;
			g_Obj[Obj1Num].intAtt				= g_Obj[Obj2Num].intAtt;

			g_lngObjID[g_Obj[Obj2Num].lngID] = Obj1Num;
			g_Obj[Obj2Num].lngID = dummyObj.lngID;
			g_Obj[Obj2Num].intCh = dummyObj.intCh;
			g_Obj[Obj2Num].sngValue = dummyObj.sngValue;
			g_Obj[Obj2Num].intMeasure = dummyObj.intMeasure;
			g_Obj[Obj2Num].lngPosition = dummyObj.lngPosition;
			g_Obj[Obj2Num].lngHeight = dummyObj.lngHeight;
			g_Obj[Obj2Num].intSelect = dummyObj.intSelect;
			g_Obj[Obj2Num].intAtt = dummyObj.intAtt;
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

		private string GetParam(string strRet)
		{
			string[] array;

			array = strRet.Split(':');

			if (array.Length - 1 > 0)
			{
				return array[array.Length - 1];
			}
			else
			{
				return "";
			}
		}

		public int GCD(int m, int n)
		{
			if (m <= 0 || n <= 0)
			{
				return 0;
			}

			if (m % n == 0)
			{
				return n;
			}
			else
			{
				return GCD(n, m % n);
			}
		}
	}
}
