﻿using System;
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

					picMain.Refresh();
				}
			}
			catch (Exception e)
			{
				App.module.CleanUp(e.Message, "MoveObj");
			}
		}

		public void SaveChanges()
		{
			Module.g_BMS.blnSaveFlag = false;

			this.Text = Module.g_strAppTitle;

			if (Module.g_BMS.strDir != null && Module.g_BMS.strDir.Length != 0)
			{
				if (mnuOptionsFileNameOnly.Checked)
				{
					this.Text = this.Text + " - " + Module.g_BMS.strFileName;
				}
				else
				{
					this.Text = this.Text + " - " + Module.g_BMS.strDir + Module.g_BMS.strFileName;
				}
			}

			this.Text = this.Text + " *";
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

		private void Form_Load(Object sender, EventArgs e)
		{
			m_blnPreview = true;
		}

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
			MessageBox.Show("");
		}

		private void tlbMenuOpen_ButtonMenuClick(Object sender, ToolStripItemClickedEventArgs e)
		{
			mnuRecentFiles[tlbMenuOpen.DropDownItems.IndexOf(e.ClickedItem)].PerformClick();
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

		private void mnuEditMode0_Click(Object sender, EventArgs e)
		{
			tlbMenuEdit.Checked = true;
			tlbMenuWrite.Checked = false;
			tlbMenuDelete.Checked = false;

			staMainMode.Text = Module.g_strStatusBar[19];
		}

		private void mnuEditMode1_Click(Object sender, EventArgs e)
		{
			tlbMenuEdit.Checked = false;
			tlbMenuWrite.Checked = true;
			tlbMenuDelete.Checked = false;

			staMainMode.Text = Module.g_strStatusBar[20];
		}

		private void mnuEditMode2_Click(Object sender, EventArgs e)
		{
			tlbMenuEdit.Checked = false;
			tlbMenuWrite.Checked = false;
			tlbMenuDelete.Checked = true;

			staMainMode.Text = Module.g_strStatusBar[21];
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

							//App.module.DrawSelectArea();
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
					Module.g_Mouse.x = e.X;
					Module.g_Mouse.y = e.Y;

					App.module.DrawObjMax(e.X, e.Y);

					// スポイト機能
					if (EnvUtil.Shift)
					{
						if (Module.g_Obj[Module.g_Obj.Length - 1].lngHeight < Module.g_Obj.Length - 1)
						{
							if (Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh == 4
								|| Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh == 6
								|| Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh == 7
								|| Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh > 10)
							{
								int temp;
								string str;

								if (mnuOptionsNumFF.Checked)
								{
									str = App.module.strNumConv(Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].sngValue);

									// もし 01-FF じゃなかったら 01-ZZ 表示に移行する
									// ASCII 文字セットでは 0-9 < A-Z < a-z
									if ((int)(StringUtil.Left(str, 1)[0]) > (int)('F')
										|| (int)(StringUtil.Right(str, 1)[0]) > (int)('F'))
									{
										mnuOptionsNumFF.PerformClick();
										temp = Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].sngValue;
									}
									else
									{
										temp = Convert.ToInt32(str, 16);
									}
								}
								else
								{
									temp = Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].sngValue;
								}

								m_blnPreview = false;

								if (Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh > 10)
								{
									lstWAV.SelectedIndex = temp - 1;
								}
								else
								{
									if (optChangeBottom.SelectedTab == optChangeBottom2)
									{
										lstBGA.SelectedIndex = temp - 1;
									}
									else
									{
										lstBMP.SelectedIndex = temp - 1;
									}
								}

								m_blnPreview = true;
							}

							return;
						}
					}

					if (mnuOptionsRightClickDelete.Checked)
					{
						if (Module.g_Obj[Module.g_Obj.Length - 1].lngHeight < Module.g_Obj.Length - 1)
						{
							Module.g_InputLog.AddData(App.module.strNumConv((int)CMD_LOG.OBJ_DEL)
													+ App.module.strNumConv(Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].lngID, 4)
													+ StringUtil.Right("0" + string.Format("{0:X}", Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh), 2)
													+ Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intAtt
													+ App.module.strNumConv(Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intMeasure)
													+ App.module.strNumConv(Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].lngPosition, 3)
													+ Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].sngValue + ",");
							
							App.module.RemoveObj(Module.g_Obj[Module.g_Obj.Length - 1].lngHeight);

							Module.g_Obj[Module.g_Obj.Length - 1].intCh = 0;
							Module.g_Obj[Module.g_Obj.Length - 1].lngHeight = Module.g_Obj.Length - 1;

							App.module.ArrangeObj();

							picMain.Refresh();

							return;
						}
					}

					mnuContext.Show(Cursor.Position);
				}

				Module.g_blnIgnoreInput = false;
			}
			catch (Exception exception)
			{
				App.module.CleanUp(exception.Message, "picMain_MouseDown");
			}
		}

		private void picMain_MouseUp(Object sender, MouseEventArgs e)
		{
			int lngRet = 0;
			string strRet;
			int lngArg = 0;
			string[] array;

			try
			{
				m_intScrollDir = 0;

				if (Module.g_blnIgnoreInput)
				{
					Module.g_blnIgnoreInput = false;
					return;
				}

				if (!m_blnMouseDown)
				{
					return;
				}

				m_blnMouseDown = false;

				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					if (tlbMenuWrite.Checked)
					{
						if (Module.g_Obj[Module.g_Obj.Length - 1].intCh == 8)
						{	// BPM
							
							Module.frmWindowInput.lblMainDisp.Text = Module.g_Message[(int)Message.INPUT_BPM];
							Module.frmWindowInput.txtMain.Text = "";

							Module.frmWindowInput.ShowDialog(Module.frmMain);

							if ("".Equals(Module.frmWindowInput.txtMain.Text) || int.Parse(Module.frmWindowInput.txtMain.Text) == 0)
							{
								return;
							}
							else if (int.Parse(Module.frmWindowInput.txtMain.Text) > 65535)
							{
								MessageBox.Show(Module.g_Message[(int)Message.ERR_OVERFLOW_LARGE], Module.g_strAppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

								return;
							}
							else if (int.Parse(Module.frmWindowInput.txtMain.Text) < -65535)
							{
								MessageBox.Show(Module.g_Message[(int)Message.ERR_OVERFLOW_SMALL], Module.g_strAppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

								return;
							}
							else
							{
								Module.g_Obj[Module.g_Obj.Length - 1].sngValue = int.Parse(Module.frmWindowInput.txtMain.Text);
								picMain.Focus();
							}
						}
						else if (Module.g_Obj[Module.g_Obj.Length - 1].intCh == 9)
						{	// STOP

							Module.frmWindowInput.lblMainDisp.Text = Module.g_Message[(int)Message.INPUT_STOP];
							Module.frmWindowInput.txtMain.Text = "";

							Module.frmWindowInput.ShowDialog(Module.frmMain);

							if ("".Equals(Module.frmWindowInput.txtMain.Text) || int.Parse(Module.frmWindowInput.txtMain.Text) <= 0)
							{
								return;
							}
							else if (int.Parse(Module.frmWindowInput.txtMain.Text) > 65535)
							{
								MessageBox.Show(Module.g_Message[(int)Message.ERR_OVERFLOW_LARGE], Module.g_strAppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

								return;
							}
							else
							{
								Module.g_Obj[Module.g_Obj.Length - 1].sngValue = int.Parse(Module.frmWindowInput.txtMain.Text);
								picMain.Focus();
							}
						}
						else if (51 <= Module.g_Obj[Module.g_Obj.Length - 1].intCh
								&& Module.g_Obj[Module.g_Obj.Length - 1].intCh <= 69)
						{
							Module.g_Obj[Module.g_Obj.Length - 1].intCh -= 40;
							Module.g_Obj[Module.g_Obj.Length - 1].intAtt = 2;
						}

						if (Module.g_Obj[Module.g_Obj.Length - 1].sngValue == 0)
						{
							return;
						}

						SaveChanges();

						if (App.module.ChangeMaxMeasure(Module.g_Obj[Module.g_Obj.Length - 1].intMeasure) != 0)
						{
							App.module.ChangeResolution();
						}

						strRet = "";

						for (int i = Module.g_Obj.Length - 2; i >= 0; i--)
						{
							if (Module.g_Obj[i].intMeasure == Module.g_Obj[Module.g_Obj.Length - 1].intMeasure
								&& Module.g_Obj[i].lngPosition == Module.g_Obj[Module.g_Obj.Length - 1].lngPosition
								&& Module.g_Obj[i].intCh == Module.g_Obj[Module.g_Obj.Length - 1].intCh)
							{
								if (Module.g_Obj[i].intAtt / 2 == Module.g_Obj[Module.g_Obj.Length - 1].intAtt / 2)
								{	// Undo

									strRet = strRet
											+ App.module.strNumConv((int)CMD_LOG.OBJ_DEL)
											+ App.module.strNumConv(Module.g_Obj[i].lngID, 4)
											+ StringUtil.Right("0" + string.Format("{0:X}", Module.g_Obj[i].intCh), 2)
											+ Module.g_Obj[i].intAtt
											+ App.module.strNumConv(Module.g_Obj[i].intMeasure)
											+ App.module.strNumConv(Module.g_Obj[i].lngPosition, 3)
											+ Module.g_Obj[i].sngValue
											+ ",";

									App.module.RemoveObj(i);
								}
							}
						}

						// Undo
						Module.g_Obj[Module.g_Obj.Length - 1].lngID = Module.g_lngIDNum;
						strRet = strRet
								+ App.module.strNumConv((int)CMD_LOG.OBJ_ADD)
								+ App.module.strNumConv(Module.g_Obj[Module.g_Obj.Length - 1].lngID, 4)
								+ StringUtil.Right("0" + string.Format("{0:X}", Module.g_Obj[Module.g_Obj.Length - 1].intCh), 2)
								+ Module.g_Obj[Module.g_Obj.Length - 1].intAtt
								+ App.module.strNumConv(Module.g_Obj[Module.g_Obj.Length - 1].intMeasure)
								+ App.module.strNumConv(Module.g_Obj[Module.g_Obj.Length - 1].lngPosition, 3)
								+ Module.g_Obj[Module.g_Obj.Length - 1].sngValue
								+ ",";
						Module.g_InputLog.AddData(strRet);

						Module.g_lngObjID[Module.g_lngIDNum] = Module.g_Obj.Length - 1;
						Module.g_lngIDNum++;
						Array.Resize(ref Module.g_lngObjID, Module.g_lngIDNum + 1);

						Array.Resize(ref Module.g_Obj, Module.g_Obj.Length + 1);

						App.module.ArrangeObj();

						picMain.Refresh();
					}
					else if (tlbMenuEdit.Checked)
					{
						if (Module.g_SelectArea.blnFlag)
						{
							Module.g_SelectArea.blnFlag = false;

							for (int i = 0; i < Module.g_Obj.Length - 1; i++)
							{
								if (Module.g_Obj[i].intSelect == 1
								|| Module.g_Obj[i].intSelect == 4
								|| Module.g_Obj[i].intSelect == 5)
								{
									Module.g_Obj[i].intSelect = 1;
								}
								else
								{
									Module.g_Obj[i].intSelect = 0;
								}
							}

							App.module.MoveSelectedObj();
						}
						else
						{	// 複数選択っぽい

							// TODO: 6744行目から
							for (int i = 0; i < Module.g_Obj.Length - 1; i++)
							{
								if (Module.g_Obj[i].intSelect != 0)
								{
									if (Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].lngPosition + Module.g_Measure[Module.g_Obj[Module.g_Obj[Module.g_Obj.Length -
 1].lngHeight].intMeasure].lngY != m_retObj[m_retObj.Length - 1].lngPosition + Module.g_Measure[m_retObj[m_retObj.Length - 1].intMeasure].lngY
										|| Module.g_Obj[Module.g_Obj[Module.g_Obj.Length - 1].lngHeight].intCh != m_retObj[m_retObj.Length - 1].intCh)
									{
										lngRet = 1;
									}

									break;
								}
							}

							if (lngRet != 0)
							{
								array = new string[1];

								for (int i = 0; i < Module.g_Obj.Length - 1; i++)
								{
									if (Module.g_Obj[i].intCh <= 0
										|| Module.g_Obj[i].intCh > 1000
										|| (Module.g_Obj[i].intMeasure == 0 && Module.g_Obj[i].lngPosition < 0)
										|| (Module.g_Obj[i].intMeasure == 999 && Module.g_Obj[i].lngPosition > Module.g_Measure[999].intLen))
									{
										array[array.Length - 1] = App.module.strNumConv((int)CMD_LOG.OBJ_DEL)
											+ App.module.strNumConv(m_retObj[Module.g_Obj[i].lngHeight].lngID, 4)
											+ StringUtil.Right("0" + string.Format("{0:X}", m_retObj[Module.g_Obj[i].lngHeight].intCh), 2)
											+ m_retObj[Module.g_Obj[i].lngHeight].intAtt
											+ App.module.strNumConv(m_retObj[Module.g_Obj[i].lngHeight].intMeasure)
											+ App.module.strNumConv(m_retObj[Module.g_Obj[i].lngHeight].lngPosition, 3)
											+ m_retObj[Module.g_Obj[i].lngHeight].sngValue;
										Array.Resize(ref array, array.Length + 1);

										App.module.RemoveObj(i);
									}
									else if (Module.g_Obj[i].intSelect != 0)
									{
										array[array.Length - 1] = App.module.strNumConv((int)CMD_LOG.OBJ_MOVE)
											+ App.module.strNumConv(Module.g_Obj[i].lngID, 4)
											+ StringUtil.Right("0" + string.Format("{0:X}", m_retObj[Module.g_Obj[i].lngHeight].intCh), 2)
											+ App.module.strNumConv(m_retObj[Module.g_Obj[i].lngHeight].intMeasure)
											+ App.module.strNumConv(m_retObj[Module.g_Obj[i].lngHeight].lngPosition, 3)
											+ StringUtil.Right("0" + string.Format("{0:X}", Module.g_Obj[i].intCh), 2)
											+ App.module.strNumConv(Module.g_Obj[i].intMeasure)
											+ App.module.strNumConv(Module.g_Obj[i].lngPosition, 3);
										Array.Resize(ref array, array.Length + 1);
									}

									if (App.module.ChangeMaxMeasure(Module.g_Obj[i].intMeasure) != 0)
									{
										lngArg = 1;
									}
								}

								if (lngArg != 0)
								{
									App.module.ChangeResolution();
								}

								Module.g_InputLog.AddData(string.Join(",", array) + ",");
							}

							App.module.ArrangeObj();
						}

						picMain.Refresh();
					}
				}
			}
			catch (Exception exception)
			{
				App.module.CleanUp(exception.Message, "picMain_MouseUp");
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

					//App.module.DrawSelectArea();
					picMain.Refresh();

					if (Module.g_disp.intEffect != 0)
					{
						// TODO: DrawEffect();
					}
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
			catch (Exception exception)
			{
				App.module.CleanUp(exception.Message, "picMain_MouseMove");
			}
		}

		private void hsbMain_Change(Object sender, EventArgs e)
		{
			Module.g_disp.x = hsbMain.Value;

			picMain.Refresh();
		}

		private void hsbMain_Scroll(Object sender, ScrollEventArgs e)
		{
			hsbMain_Change(null, null);
		}

		private void vsbMain_Change(Object sender, EventArgs e)
		{
			Module.g_disp.y = vsbMain.Value * Module.g_disp.intResolution;

			picMain.Refresh();
		}

		private void vsbMain_Scroll(Object sender, ScrollEventArgs e)
		{
			vsbMain_Change(null, null);
		}
	}
}