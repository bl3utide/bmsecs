using System;
using System.Drawing;
using System.Windows.Forms;
using Bmse.Common;
using Bmse.Util;

namespace Bmse.Forms
{
	public partial class FormMain : Form
	{
		private int m_intScrollDir;
		private g_udtObj[] m_retObj;
		private bool m_blnMouseDown;
		private bool m_blnPreview;

		public int lngFromString(string str)
		{
			if (mnuOptionsNumFF.Checked)
			{
				return Convert.ToInt32(str, 16);
			}
			else
			{
				return App.module.lngNumConv(str);
			}
		}

		public int lngFromLong(int value)
		{
			if (mnuOptionsNumFF.Checked)
			{
				return App.module.lngNumConv(strFromLong(value));
			}
			else
			{
				return value;
			}
		}

		public string strFromLong(int value)
		{
			if (mnuOptionsNumFF.Checked)
			{
				return StringUtil.Right("0" + Convert.ToString(value), 2);
			}
			else
			{
				return App.module.strNumConv(value);
			}
		}

		private void MoveObj(int x, int y)
		{
			int _j;
			int lngRet;
			bool blRet;
			g_udtObj oldObj = new g_udtObj();
			g_udtObj newObj = new g_udtObj();

			try
			{

				App.module.SetObjData(ref newObj, x, y);

				newObj.intCh = Module.g_intVGridNum[newObj.intCh];

				if (DataSource.DsListDispGridMain[cboDispGridMain.SelectedIndex].Value != 0)
				{
					lngRet = 192 / DataSource.DsListDispGridMain[cboDispGridMain.SelectedIndex].Value;

					newObj.lngPosition = (newObj.lngPosition / lngRet) * lngRet;

					if (mnuOptionsMoveOnGrid.Checked)
					{
						lngRet = Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].lngPosition - (Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].lngPosition / lngRet) * lngRet;

						newObj.lngPosition = newObj.lngPosition - lngRet;
					}
				}

				newObj.lngPosition = newObj.lngPosition + Module.g_Measure[newObj.intMeasure].lngY;

				App.module.CopyObj(ref oldObj, ref Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight]);

				oldObj.intCh = Module.g_intVGridNum[oldObj.intCh];

				if (DataSource.DsListDispGridMain[cboDispGridMain.SelectedIndex].Value != 0)
				{
					lngRet = 192 / DataSource.DsListDispGridMain[cboDispGridMain.SelectedIndex].Value;

					oldObj.lngPosition = (oldObj.lngPosition / lngRet) * lngRet;
				}

				oldObj.lngPosition = oldObj.lngPosition + Module.g_Measure[oldObj.intMeasure].lngY;

				// Y軸固定移動
				if (EnvUtil.Shift)
				{
					newObj.lngPosition = oldObj.lngPosition;
				}

				if (newObj.intCh != oldObj.intCh || newObj.lngPosition != oldObj.lngPosition)
				{
					if (newObj.intCh > oldObj.intCh)
					{
						for (int j = oldObj.intCh; j < newObj.intCh; j++)
						{
							if (Module.g_VGrid[j].blnDraw && Module.g_VGrid[j].intCh != 0)
							{
								newObj.intAtt++;
							}
						}
					}
					else if (newObj.intCh < oldObj.intCh)
					{
						for (int j = oldObj.intCh; j >= newObj.intCh + 1; j--)
						{
							if (Module.g_VGrid[j].blnVisible && Module.g_VGrid[j].intCh != 0)
							{
								newObj.intAtt++;
							}
						}
					}

					blRet = newObj.intCh != oldObj.intCh
						&& newObj.intCh != 0
						&& oldObj.intCh != 0
						&& newObj.intCh != Module.g_VGrid.Length - 1
						&& oldObj.intCh != Module.g_VGrid.Length - 1;

					for (int i = 0; i < Module.g_Obj.Length - 1; i++)
					{
						if (Module.g_Obj[i].intSelect == 1)
						{
							Module.g_Obj[i].lngPosition += newObj.lngPosition - oldObj.lngPosition;

							while (Module.g_Obj[i].lngPosition >= Module.g_Measure[Module.g_Obj[i].intMeasure].intLen)
							{
								if (Module.g_Obj[i].intMeasure < 999)
								{
									Module.g_Obj[i].lngPosition -= Module.g_Measure[Module.g_Obj[i].intMeasure].intLen;
									Module.g_Obj[i].intMeasure++;
								}
								else
								{
									Module.g_Obj[i].intMeasure = 999;
									break;
								}
							}

							while (Module.g_Obj[i].lngPosition < 0)
							{
								if (Module.g_Obj[i].intMeasure > 0)
								{
									Module.g_Obj[i].lngPosition = Module.g_Measure[Module.g_Obj[i].intMeasure - 1].intLen + Module.g_Obj[i].lngPosition;
									Module.g_Obj[i].intMeasure--;
								}
								else
								{
									Module.g_Obj[i].intMeasure = 0;
									break;
								}
							}

							if (blRet)
							{
								if (Module.g_Obj[i].intCh < 0)
								{
									_j = Module.g_Obj[i].intCh;
								}
								else if (Module.g_Obj[i].intCh > 1000)
								{
									_j = Module.g_Obj[i].intCh - 1000;
								}
								else
								{
									_j = Module.g_intVGridNum[Module.g_Obj[i].intCh];
								}

								if (newObj.intCh > oldObj.intCh)
								{
									for (int k = 1; k <= newObj.intAtt; k++)
									{
										while (true)
										{
											_j++;

											if (_j < 0 || _j > Module.g_VGrid.Length - 1)
											{
												break;
											}

											if (Module.g_VGrid[_j].blnVisible && Module.g_VGrid[_j].intCh != 0)
											{
												break;
											}
										}
									}
								}
								else
								{
									for (int k = 1; k <= newObj.intAtt; k++)
									{
										while (true)
										{
											_j--;

											if (_j < 0 || _j > Module.g_VGrid.Length - 1)
											{
												break;
											}

											if (Module.g_VGrid[_j].blnVisible && Module.g_VGrid[_j].intCh != 0)
											{
												break;
											}
										}
									}
								}

								if (_j < 0)
								{
									Module.g_Obj[i].intCh = _j;
								}
								else if (_j > Module.g_VGrid.Length - 1)
								{
									Module.g_Obj[i].intCh = 1000 + _j;
								}
								else
								{
									Module.g_Obj[i].intCh = Module.g_VGrid[_j].intCh;
								}

								switch (Module.g_Obj[i].intCh)
								{
									case 8:
										break;

									case 9:
										if (Module.g_Obj[i].sngValue < 0)
										{
											Module.g_Obj[i].sngValue = 1;
										}

										break;

									default:
										if (Module.g_Obj[i].sngValue < 0)
										{
											Module.g_Obj[i].sngValue = 1;
										}
										else if (Module.g_Obj[i].sngValue > 1295)
										{
											Module.g_Obj[i].sngValue = 1295;
										}

										break;
								}
							}
						}
					}

					App.module.DrawStatusBar(ref Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight]);
				}
			}
			catch (Exception e)
			{
				App.module.CleanUp(e.Message, "MoveObj");
			}
		}

		public FormMain()
		{
			InitializeComponent();
			InitializeItemData();
		}

		public void InitializeItemData()
		{
			cboVScroll.ComboBox.BindingContext = new BindingContext();
			cboVScroll.ComboBox.DataSource = DataSource.DsListVScroll;
			cboVScroll.ComboBox.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboVScroll.ComboBox.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispHeight.ComboBox.BindingContext = new BindingContext();
			cboDispHeight.ComboBox.DataSource = DataSource.DsListDispHeight;
			cboDispHeight.ComboBox.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispHeight.ComboBox.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispWidth.ComboBox.BindingContext = new BindingContext();
			cboDispWidth.ComboBox.DataSource = DataSource.DsListDispWidth;
			cboDispWidth.ComboBox.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispWidth.ComboBox.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispGridMain.ComboBox.BindingContext = new BindingContext();
			cboDispGridMain.ComboBox.DataSource = DataSource.DsListDispGridMain;
			cboDispGridMain.ComboBox.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispGridMain.ComboBox.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispGridSub.ComboBox.BindingContext = new BindingContext();
			cboDispGridSub.ComboBox.DataSource = DataSource.DsListDispGridSub;
			cboDispGridSub.ComboBox.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispGridSub.ComboBox.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboPlayer.BindingContext = new BindingContext();
			cboPlayer.DataSource = DataSource.DsListPlayer;
			cboPlayer.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboPlayer.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboPlayLevel.BindingContext = new BindingContext();
			cboPlayLevel.DataSource = DataSource.DsListPlayLevel;
			cboPlayLevel.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboPlayLevel.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboPlayRank.BindingContext = new BindingContext();
			cboPlayRank.DataSource = DataSource.DsListPlayRank;
			cboPlayRank.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboPlayRank.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispFrame.BindingContext = new BindingContext();
			cboDispFrame.DataSource = DataSource.DsListDispFrame;
			cboDispFrame.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispFrame.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispSC1P.BindingContext = new BindingContext();
			cboDispSC1P.DataSource = DataSource.DsListDispSC1P;
			cboDispSC1P.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispSC1P.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispSC2P.BindingContext = new BindingContext();
			cboDispSC2P.DataSource = DataSource.DsListDispSC2P;
			cboDispSC2P.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispSC2P.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDispKey.BindingContext = new BindingContext();
			cboDispKey.DataSource = DataSource.DsListDispKey;
			cboDispKey.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDispKey.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;

			cboDenominator.BindingContext = new BindingContext();
			cboDenominator.DataSource = DataSource.DsListDenominator;
			cboDenominator.ValueMember = CommonConst.CBO_VALUE_MEMBER;
			cboDenominator.DisplayMember = CommonConst.CBO_DISPLAY_MEMBER;
		}



		public void Form_Resize(Object sender, EventArgs e)
		{
			// UNDONE: かなり簡潔に実装してしまったから不安

			const int MENUBAR_HEIGHT = 21;
			const int TOOLBAR_HEIGHT = 30;
			const int HEADER_HEIGHT = 150;
			const int LINE_HEIGHT = 2;
			const int STATUSBAR_HEIGHT = 22;
			const int DIRECTINPUT_HEIGHT = 29;
			const int HSCROLLBAR_HEIGHT = 23;

			int topLineY = 0;
			int bottomLeftLineY = 0;
			int bottomRightLineY = 0;

			// 上端を設定
			topLineY = mnuViewToolBar.Checked
				? MENUBAR_HEIGHT + TOOLBAR_HEIGHT
				: MENUBAR_HEIGHT;

			// 右側の下端を設定
			bottomRightLineY = mnuViewStatusBar.Checked
				? Module.frmMain.ClientSize.Height + 1 - STATUSBAR_HEIGHT
				: Module.frmMain.ClientSize.Height + 1;

			// 左側の下端を設定
			bottomLeftLineY = mnuViewDirectInput.Checked
				? bottomRightLineY - DIRECTINPUT_HEIGHT
				: bottomRightLineY;

			// 他のコントロールの移動
			linVertical.Top = topLineY;							// linVertical
			picMain.Top = topLineY + 3;							// 描画領域
			vsbMain.Top = topLineY + 3;							// 縦スクロールバー
			hsbMain.Top = bottomLeftLineY - HSCROLLBAR_HEIGHT;	// 横スクロールバー
			linDirectInput.Top = hsbMain.Bottom + 4;			// linDirectInput
			lblDirectInput.Top = linDirectInput.Top + 5;		// ダイレクト入力枠ラベル
			cboDirectInput.Top = linDirectInput.Top + 5;		// ダイレクト入力枠入力
			cmdDirectInput.Top = linDirectInput.Top + 4;		// ダイレクト入力枠ボタン
			optChangeTop.Top = topLineY + 3;					// ヘッダ領域
			linHeader.Top = topLineY + 3 + HEADER_HEIGHT;		// linHeader
			optChangeBottom.Top = topLineY + 3 + HEADER_HEIGHT + LINE_HEIGHT + 4;	// 素材領域

			picMain.Height = bottomLeftLineY - picMain.Top - HSCROLLBAR_HEIGHT;
			vsbMain.Height = picMain.Height;
			linVertical.Height = bottomRightLineY - linVertical.Top;
			optChangeBottom.Height = bottomRightLineY - optChangeBottom.Top;

			// ステータスバーの表示・非表示
			staMain.Visible = mnuViewStatusBar.Checked;
			linStatusBar.Visible = mnuViewStatusBar.Checked;
			
			// ツールバーの表示・非表示
			tlbMenu.Visible = mnuViewToolBar.Checked;

			// ダイレクト入力枠の表示・非表示
			lblDirectInput.Visible = mnuViewDirectInput.Checked;
			cboDirectInput.Visible = mnuViewDirectInput.Checked;
			cmdDirectInput.Visible = mnuViewDirectInput.Checked;
			linDirectInput.Visible = mnuViewDirectInput.Checked;

			App.module.InitVerticalLine();
		}





				
		//////////////////////////////////////////////////////////////////////////////
		// イベントハンドラ
		//////////////////////////////////////////////////////////////////////////////

		private void Form_Activeted(Object sender, EventArgs e)
		{
			Form_Resize(null, null);
		}

		public void cboDispGridMain_SelectedIndexChanged(Object sender, EventArgs e)
		{
			//App.module.Redraw();
			picMain.Refresh();
		}

		public void cboDispGridSub_SelectedIndexChanged(Object sender, EventArgs e)
		{
			//App.module.Redraw();
			picMain.Refresh();
		}

		private void tlbMenuNew_Click(Object sender, EventArgs e)
		{
			mnuFileNew.PerformClick();
		}

		private void tlbMenuOpen_Click(Object sender, EventArgs e)
		{
			mnuFileOpen.PerformClick();
		}

		private void tlbMenuReload_Click(Object sender, EventArgs e)
		{
			mnuRecentFiles[0].PerformClick();
		}

		private void tlbMenuSave_Click(Object sender, EventArgs e)
		{
			mnuFileSave.PerformClick();
		}

		private void tlbMenuSaveAs_Click(Object sender, EventArgs e)
		{
			mnuFileSaveAs.PerformClick();
		}

		private void tlbMenuEdit_Click(Object sender, EventArgs e)
		{
			mnuEditMode0.PerformClick();
		}

		private void tlbMenuWrite_Click(Object sender, EventArgs e)
		{
			mnuEditMode1.PerformClick();
		}

		private void tlbMenuDelete_Click(Object sender, EventArgs e)
		{
			mnuEditMode2.PerformClick();
		}

		private void tlbMenuPlayAll_Click(Object sender, EventArgs e)
		{
			mnuToolsPlayAll.PerformClick();
		}

		private void tlbMenuPlay_Click(Object sender, EventArgs e)
		{
			mnuToolsPlay.PerformClick();
		}

		private void tlbMenuStop_Click(Object sender, EventArgs e)
		{
			mnuToolsPlayStop.PerformClick();
		}

		private void mnuFileNew_Click(Object sender, EventArgs e)
		{
			if (App.module.SaveCheck() != 0)
			{
				return;
			}

			this.Text = Module.g_strAppTitle + " - Now Initializing";

			App.module.DeleteFile(Module.g_BMS.strDir + "___bmse_temp.bms");

			Module.g_BMS.strDir = "";
			Module.g_BMS.strFileName = "";

			App.module.LoadBMSStart();
			App.module.LoadBMSEnd();
		}

		private void mnuViewToolBar_Click(Object sender, EventArgs e)
		{
			Form_Resize(null, null);
		}

		private void mnuViewDirectInput_Click(Object sender, EventArgs e)
		{
			Form_Resize(null, null);
		}

		private void mnuViewStatusBar_Click(Object sender, EventArgs e)
		{
			Form_Resize(null, null);
		}

		private void mnuOptionsVertical_Click(Object sender, EventArgs e)
		{
			picMain.Refresh();
		}

		private void picMain_MouseDown(Object sender, MouseEventArgs e)
		{
			string strRet;
			int lngRet;
			g_udtObj retObj = new g_udtObj();
			string[] array;

			try
			{
				if (Module.g_blnIgnoreInput)
				{
					return;
				}

				m_blnMouseDown = true;

				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					if (tlbMenuDelete.Checked)
					{
						if (Module.g_Obj[Module.g_Obj.Length - 1].intCh != 0)
						{
							Module.g_InputLog.AddData(App.module.strNumConv((int)CMD_LOG.OBJ_DEL)
													+ App.module.strNumConv(Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].lngID, 4)
													+ StringUtil.Right("0" + string.Format("{0:X}", Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh), 2)
													+ Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intAtt
													+ App.module.strNumConv(Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intMeasure)
													+ App.module.strNumConv(Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].lngPosition, 3)
													+ Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].sngValue
													+ ",");

							App.module.RemoveObj(Module.g_Obj[Module.g_Obj.Length - 1].lngHeight);

							App.module.ArrangeObj();

							App.module.RemoveObj(Module.g_Obj.Length - 1);
						}

						App.module.ObjSelectCancel();

						picMain.Refresh();
					}
					else if (tlbMenuEdit.Checked)
					{
						if (Module.g_Obj[Module.g_Obj.Length - 1].intCh != 0)	// オブジェのあるところで押したっぽい
						{
							if (DataSource.DsListDispGridMain[cboDispGridMain.SelectedIndex].Value != 0)
							{
								lngRet = 192 / DataSource.DsListDispGridMain[cboDispGridMain.SelectedIndex].Value;
								lngRet = Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].lngPosition
									- (Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].lngPosition / lngRet) * lngRet;
							}

							if (Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intSelect != 0)	// 複数選択っぽい
							{
								if (EnvUtil.Control)
								{
									App.module.CopyObj(ref retObj, ref Module.g_Obj[Module.g_Obj.Length - 1]);

									array = new string[1];

									for (int i = 0; i < Module.g_Obj.Length - 1; i++)
									{
										if (Module.g_Obj[i].intSelect != 0)
										{

											App.module.CopyObj(ref Module.g_Obj[Module.g_Obj.Length - 1], ref Module.g_Obj[i]);
											Module.g_Obj[Module.g_Obj.Length - 1].lngID = Module.g_lngIDNum;

											array[array.Length - 1] = App.module.strNumConv((int)CMD_LOG.OBJ_ADD)
																	+ App.module.strNumConv(Module.g_lngIDNum, 4)
																	+ StringUtil.Right("0" + string.Format("{0:X}", Module.g_Obj[i].intCh), 2)
																	+ Module.g_Obj[i].intAtt
																	+ App.module.strNumConv(Module.g_Obj[i].intMeasure)
																	+ App.module.strNumConv(Module.g_Obj[i].lngPosition, 3)
																	+ Module.g_Obj[i].sngValue;
											Array.Resize(ref array, array.Length + 1);

											Module.g_lngObjID[Module.g_lngIDNum] = Module.g_Obj.Length - 1;
											Module.g_lngIDNum++;
											Array.Resize(ref Module.g_lngObjID, Module.g_lngIDNum + 1);

											Module.g_Obj[i].intSelect = 0;

											if (i == retObj.lngHeight)
											{
												retObj.lngHeight = Module.g_Obj.Length - 1;
											}

											Array.Resize(ref Module.g_Obj, Module.g_Obj.Length + 1);
										}
									}

									if (array.Length - 1 != 0)
									{
										Array.Resize(ref array, array.Length - 1);

										Module.g_InputLog.AddData(string.Join(",", array) + ",");

										App.module.CopyObj(ref Module.g_Obj[Module.g_Obj.Length - 1], ref retObj);
									}
								}

								m_retObj = new g_udtObj[1];

								for (int i = 0; i < Module.g_Obj.Length - 1; i++)
								{
									if (Module.g_Obj[i].intSelect != 0)
									{
										App.module.CopyObj(ref m_retObj[m_retObj.Length - 1], ref Module.g_Obj[i]);

										Module.g_Obj[i].lngHeight = m_retObj.Length - 1;

										Array.Resize(ref m_retObj, m_retObj.Length + 1);
									}
								}

								App.module.CopyObj(ref m_retObj[m_retObj.Length - 1], ref Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight]);

								if (mnuOptionsSelectPreview.Checked
									&& (Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh >= 11 && Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh <= 29)
									|| Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh > 100)
								{
									strRet = Module.g_strWAV[Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].sngValue];

									if (!"".Equals(strRet) && !"".Equals(FileUtil.Dir(Module.g_BMS.strDir + strRet)))
									{
										//TODO: PreviewWAV(strRet);
									}
								}
							}
							else
							{	// 単数選択っぽい
								if (!EnvUtil.Control)
								{
									App.module.ObjSelectCancel();
								}

								Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intSelect = 1;

								App.module.MoveSelectedObj();

								// TODO: 6293行目から
								m_retObj = new g_udtObj[1];

								for (int i = 0; i < Module.g_Obj.Length - 1; i++)
								{
									App.module.CopyObj(ref m_retObj[m_retObj.Length - 1], ref Module.g_Obj[i]);
									Module.g_Obj[i].lngHeight = m_retObj.Length - 1;
									Array.Resize(ref m_retObj, m_retObj.Length + 1);
								}

								App.module.CopyObj(ref m_retObj[m_retObj.Length - 1], ref Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight]);

								if (mnuOptionsSelectPreview.Checked)
								{
									if ((11 <= Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh
										&& Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh <= 29)
										|| Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh > 100)
									{
										strRet = Module.g_strWAV[Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].sngValue];

										if (!"".Equals(strRet) && !"".Equals(FileUtil.Dir(Module.g_BMS.strDir + strRet)))
										{
											// TODO: PreviewWAV(strRet);
										}
									}
									else if (Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh == 4
										|| Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh == 6
										|| Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh == 7)
									{
										if (Module.g_strBGA[Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].sngValue].Length != 0)
										{
											// TODO: PreviewBGA(Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].sngValue);
										}
										else
										{
											strRet = Module.g_strBMP[Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].sngValue];

											if (!"".Equals(strRet) && !"".Equals(FileUtil.Dir(Module.g_BMS.strDir + strRet)))
											{
												// TODO: PreviewBMP(strRet);
											}
										}
									}
								}
							}

							picMain.Refresh();
						}
						else
						{	// オブジェのないところで押したっぽい
							if (!EnvUtil.Control)
							{
								App.module.ObjSelectCancel();
								picMain.Refresh();
							}
							else
							{
								for (int i = 0; i < Module.g_Obj.Length - 1; i++)
								{
									if (Module.g_Obj[i].intSelect != 0)
									{
										Module.g_Obj[i].intSelect = 5;
									}
								}

								picMain.Refresh();
							}

							Module.g_SelectArea.blnFlag = true;
							Module.g_SelectArea.x1 = e.X / (int)Module.g_disp.width + Module.g_disp.x;
							Module.g_SelectArea.y1 = (picMain.Height - e.Y) / (int)Module.g_disp.height + Module.g_disp.y;
							Module.g_SelectArea.x2 = Module.g_SelectArea.x1;
							Module.g_SelectArea.y2 = Module.g_SelectArea.y1;

							App.module.DrawSelectArea();
						}

						if (Module.g_disp.intEffect != 0)
						{
							//TODO: App.module.DrawEffect();
						}
					}
					else
					{	//// tlbMenuWrite.Checked = true
						
						App.module.ObjSelectCancel();
						picMain.Refresh();

						App.module.InitPen();
						using(Graphics gPicMain = picMain.CreateGraphics())
						{
							App.module.DrawObj(ref Module.g_Obj[Module.g_Obj.Length - 1], gPicMain);
						}
						App.module.DeletePen();
					}
				}
				else if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					// TODO: 6434行目から
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void picMain_MouseMove(Object sender, MouseEventArgs e)
		{
			int lngRet = 0;
			RECT retRect;
			bool[] select;
			bool YAxisFixed = false; ;

			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					if (!m_blnMouseDown)
					{
						return;
					}
				}

				if (!Module.g_SelectArea.blnFlag)
				{
					// 選択範囲なし

					if (e.Button == System.Windows.Forms.MouseButtons.Left
						&& tlbMenuEdit.Checked
						&& Module.g_Obj[Module.g_Obj.Length - 1].intCh != 0)
					{
						// オブジェ移動中

						MoveObj(e.X, e.Y);

						if (EnvUtil.Shift)
						{
							YAxisFixed = true;
						}
					}
					else
					{
						// それ以外

						App.module.DrawObjMax(e.X, e.Y);
					}
				}
				else
				{
					// 選択範囲あり

					Module.g_Mouse.x = e.X;
					Module.g_Mouse.y = e.Y;

					Module.g_SelectArea.x2 = e.X / (int)Module.g_disp.width + Module.g_disp.x;
					Module.g_SelectArea.y2 = (picMain.Height - e.Y) / (int)Module.g_disp.height + Module.g_disp.y;

					if (Module.g_SelectArea.x1 > Module.g_SelectArea.x2)
					{
						retRect.left = Module.g_SelectArea.x2;
						retRect.right = Module.g_SelectArea.x1;
					}
					else
					{
						retRect.left = Module.g_SelectArea.x1;
						retRect.right = Module.g_SelectArea.x2;
					}

					if (Module.g_SelectArea.y1 > Module.g_SelectArea.y2)
					{
						retRect.top = Module.g_SelectArea.y2;
						retRect.bottom = Module.g_SelectArea.y1;
					}
					else
					{
						retRect.top = Module.g_SelectArea.y1;
						retRect.bottom = Module.g_SelectArea.y2;
					}

					select = new bool[Module.g_VGrid.Length];

					for (int i = 0; i < Module.g_VGrid.Length; i++)
					{
						select[i] = false;
						if (Module.g_VGrid[i].blnVisible)
						{
							if (Module.g_VGrid[i].intCh != 0)
							{
								if (Module.g_VGrid[i].lngLeft + Module.g_VGrid[i].intWidth > retRect.left
									&& Module.g_VGrid[i].lngLeft < retRect.right)
								{
									select[i] = true;
								}
							}
						}
					}

					for (int i = 0; i < Module.g_Obj.Length - 1; i++)
					{
						if (select[Module.g_intVGridNum[Module.g_Obj[i].intCh]])
						{
							lngRet = Module.g_Measure[Module.g_Obj[i].intMeasure].lngY + Module.g_Obj[i].lngPosition;

							if (lngRet + Module.OBJ_HEIGHT / Module.g_disp.height >= retRect.top && lngRet <= retRect.bottom)
							{
								if (Module.g_Obj[i].intSelect < 5)
								{
									Module.g_Obj[i].intSelect = 4;
								}
								else
								{
									Module.g_Obj[i].intSelect = 6;
								}
							}
							else
							{
								if (Module.g_Obj[i].intSelect < 5)
								{
									Module.g_Obj[i].intSelect = 0;
								}
								else
								{
									Module.g_Obj[i].intSelect = 5;
								}
							}
						}
						else
						{
							if (Module.g_Obj[i].intSelect < 5)
							{
								Module.g_Obj[i].intSelect = 0;
							}
							else
							{
								Module.g_Obj[i].intSelect = 5;
							}
						}
					}

					App.module.DrawSelectArea();

					if (Module.g_disp.intEffect != 0)
					{
						// TODO: DrawEffect();
					}

					Module.g_Mouse.x = e.X;
					if (YAxisFixed)
					{
						Module.g_Mouse.y = e.Y;
					}

					m_intScrollDir = 0;

					if (e.X < 0)
					{
						m_intScrollDir = 20;
					}
					else if (e.X > picMain.Width)
					{
						m_intScrollDir = 10;
					}

					if (!YAxisFixed)
					{
						if (e.Y < 0)
						{
							m_intScrollDir += 1;
						}
						else if (e.Y > picMain.Height)
						{
							m_intScrollDir += 2;
						}
					}

					if (m_intScrollDir != 0)
					{
						tmrMain.Enabled = true;
					}
					else
					{
						tmrMain.Enabled = false;
					}
				}
			}
			catch (Exception exception)
			{
				App.module.CleanUp(exception.Message, "picMain_MouseMove");
			}
		}
	}
}