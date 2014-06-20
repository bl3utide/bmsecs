using System;
using System.Windows.Forms;
using Bmse.Common;
using Bmse.Util;

namespace Bmse.Forms
{
	public partial class FormMain : Form
	{
		private int mScrollDir;
		private Obj[] mObj;
		private bool mMouseDown;
		private bool mPreview;

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
			Obj oldObj = new Obj();
			Obj newObj = new Obj();

			try
			{

				App.module.SetObjData(ref newObj, x, y);

				newObj.ch = Module.gVGridNum[newObj.ch];

				if (DataSource.DsListDispGridMain[cboDispGridMain.SelectedIndex].Value != 0)
				{
					lngRet = 192 / DataSource.DsListDispGridMain[cboDispGridMain.SelectedIndex].Value;

					newObj.position = (newObj.position / lngRet) * lngRet;

					if (mnuOptionsMoveOnGrid.Checked)
					{
						lngRet = Module.gObj[Module.gObj[Module.gObj.Length - 1].height].position - (Module.gObj[Module.gObj[Module.gObj.Length - 1].height].position / lngRet) * lngRet;

						newObj.position = newObj.position - lngRet;
					}
				}

				newObj.position = newObj.position + Module.gMeasure[newObj.measure].y;

				App.module.CopyObj(ref oldObj, ref Module.gObj[Module.gObj[Module.gObj.Length - 1].height]);

				oldObj.ch = Module.gVGridNum[oldObj.ch];

				if (DataSource.DsListDispGridMain[cboDispGridMain.SelectedIndex].Value != 0)
				{
					lngRet = 192 / DataSource.DsListDispGridMain[cboDispGridMain.SelectedIndex].Value;

					oldObj.position = (oldObj.position / lngRet) * lngRet;
				}

				oldObj.position = oldObj.position + Module.gMeasure[oldObj.measure].y;

				// Y軸固定移動
				if (EnvUtil.Shift)
				{
					newObj.position = oldObj.position;
				}

				if (newObj.ch != oldObj.ch || newObj.position != oldObj.position)
				{
					if (newObj.ch > oldObj.ch)
					{
						for (int j = oldObj.ch; j < newObj.ch; j++)
						{
							if (Module.gVGrid[j].draw && Module.gVGrid[j].ch != 0)
							{
								newObj.att++;
							}
						}
					}
					else if (newObj.ch < oldObj.ch)
					{
						for (int j = oldObj.ch; j >= newObj.ch + 1; j--)
						{
							if (Module.gVGrid[j].visible && Module.gVGrid[j].ch != 0)
							{
								newObj.att++;
							}
						}
					}

					blRet = newObj.ch != oldObj.ch
						&& newObj.ch != 0
						&& oldObj.ch != 0
						&& newObj.ch != Module.gVGrid.Length - 1
						&& oldObj.ch != Module.gVGrid.Length - 1;

					for (int i = 0; i < Module.gObj.Length - 1; i++)
					{
						if (Module.gObj[i].select == 1)
						{
							Module.gObj[i].position += newObj.position - oldObj.position;

							while (Module.gObj[i].position >= Module.gMeasure[Module.gObj[i].measure].len)
							{
								if (Module.gObj[i].measure < 999)
								{
									Module.gObj[i].position -= Module.gMeasure[Module.gObj[i].measure].len;
									Module.gObj[i].measure++;
								}
								else
								{
									Module.gObj[i].measure = 999;
									break;
								}
							}

							while (Module.gObj[i].position < 0)
							{
								if (Module.gObj[i].measure > 0)
								{
									Module.gObj[i].position = Module.gMeasure[Module.gObj[i].measure - 1].len + Module.gObj[i].position;
									Module.gObj[i].measure--;
								}
								else
								{
									Module.gObj[i].measure = 0;
									break;
								}
							}

							if (blRet)
							{
								if (Module.gObj[i].ch < 0)
								{
									_j = Module.gObj[i].ch;
								}
								else if (Module.gObj[i].ch > 1000)
								{
									_j = Module.gObj[i].ch - 1000;
								}
								else
								{
									_j = Module.gVGridNum[Module.gObj[i].ch];
								}

								if (newObj.ch > oldObj.ch)
								{
									for (int k = 1; k <= newObj.att; k++)
									{
										while (true)
										{
											_j++;

											if (_j < 0 || _j > Module.gVGrid.Length - 1)
											{
												break;
											}

											if (Module.gVGrid[_j].visible && Module.gVGrid[_j].ch != 0)
											{
												break;
											}
										}
									}
								}
								else
								{
									for (int k = 1; k <= newObj.att; k++)
									{
										while (true)
										{
											_j--;

											if (_j < 0 || _j > Module.gVGrid.Length - 1)
											{
												break;
											}

											if (Module.gVGrid[_j].visible && Module.gVGrid[_j].ch != 0)
											{
												break;
											}
										}
									}
								}

								if (_j < 0)
								{
									Module.gObj[i].ch = _j;
								}
								else if (_j > Module.gVGrid.Length - 1)
								{
									Module.gObj[i].ch = 1000 + _j;
								}
								else
								{
									Module.gObj[i].ch = Module.gVGrid[_j].ch;
								}

								switch (Module.gObj[i].ch)
								{
									case 8:
										break;

									case 9:
										if (Module.gObj[i].value < 0)
										{
											Module.gObj[i].value = 1;
										}

										break;

									default:
										if (Module.gObj[i].value < 0)
										{
											Module.gObj[i].value = 1;
										}
										else if (Module.gObj[i].value > 1295)
										{
											Module.gObj[i].value = 1295;
										}

										break;
								}
							}
						}
					}

					App.module.DrawStatusBar(ref Module.gObj[Module.gObj[Module.gObj.Length - 1].height]);
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

		private void mnuFileNew_Click(Object sender, EventArgs e)
		{
			if (App.module.SaveCheck() != 0)
			{
				return;
			}

			this.Text = Module.gAppTitle + " - Now Initializing";

			App.module.DeleteFile(Module.gBms.dir + "___bmse_temp.bms");

			Module.gBms.dir = "";
			Module.gBms.fileName = "";

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
			Obj retObj = new Obj();
			string[] array;

			try
			{
				if (Module.gIgnoreInput)
				{
					return;
				}

				mMouseDown = true;

				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					if (tlbMenuDelete.Checked)
					{
						if (Module.gObj[Module.gObj.Length - 1].ch != 0)
						{
							Module.gInputLog.AddData(App.module.strNumConv((int)CMD_LOG.OBJ_DEL)
													+ App.module.strNumConv(Module.gObj[Module.gObj[Module.gObj.Length - 1].height].id, 4)
													+ StringUtil.Right("0" + string.Format("{0:X}", Module.gObj[Module.gObj[Module.gObj.Length - 1].height].ch), 2)
													+ Module.gObj[Module.gObj[Module.gObj.Length - 1].height].att
													+ App.module.strNumConv(Module.gObj[Module.gObj[Module.gObj.Length - 1].height].measure)
													+ App.module.strNumConv(Module.gObj[Module.gObj[Module.gObj.Length - 1].height].position, 3)
													+ Module.gObj[Module.gObj[Module.gObj.Length - 1].height].value
													+ ",");

							App.module.RemoveObj(Module.gObj[Module.gObj.Length - 1].height);

							App.module.ArrangeObj();

							App.module.RemoveObj(Module.gObj.Length - 1);
						}

						App.module.ObjSelectCancel();

						picMain.Refresh();
					}
					else if (tlbMenuEdit.Checked)
					{
						if (Module.gObj[Module.gObj.Length - 1].ch != 0)	// オブジェのあるところで押したっぽい
						{
							if (DataSource.DsListDispGridMain[cboDispGridMain.SelectedIndex].Value != 0)
							{
								lngRet = 192 / DataSource.DsListDispGridMain[cboDispGridMain.SelectedIndex].Value;
								lngRet = Module.gObj[Module.gObj[Module.gObj.Length - 1].height].position
									- (Module.gObj[Module.gObj[Module.gObj.Length - 1].height].position / lngRet) * lngRet;
							}

							if (Module.gObj[Module.gObj[Module.gObj.Length - 1].height].select != 0)	// 複数選択っぽい
							{
								if (EnvUtil.Control)
								{
									App.module.CopyObj(ref retObj, ref Module.gObj[Module.gObj.Length - 1]);

									array = new string[1];

									for (int i = 0; i < Module.gObj.Length - 1; i++)
									{
										if (Module.gObj[i].select != 0)
										{

											App.module.CopyObj(ref Module.gObj[Module.gObj.Length - 1], ref Module.gObj[i]);
											Module.gObj[Module.gObj.Length - 1].id = Module.gIDNum;

											array[array.Length - 1] = App.module.strNumConv((int)CMD_LOG.OBJ_ADD)
																	+ App.module.strNumConv(Module.gIDNum, 4)
																	+ StringUtil.Right("0" + string.Format("{0:X}", Module.gObj[i].ch), 2)
																	+ Module.gObj[i].att
																	+ App.module.strNumConv(Module.gObj[i].measure)
																	+ App.module.strNumConv(Module.gObj[i].position, 3)
																	+ Module.gObj[i].value;
											Array.Resize(ref array, array.Length + 1);

											Module.gObjID[Module.gIDNum] = Module.gObj.Length - 1;
											Module.gIDNum++;
											Array.Resize(ref Module.gObjID, Module.gIDNum + 1);

											Module.gObj[i].select = 0;

											if (i == retObj.height)
											{
												retObj.height = Module.gObj.Length - 1;
											}

											Array.Resize(ref Module.gObj, Module.gObj.Length + 1);
										}
									}

									if (array.Length - 1 != 0)
									{
										Array.Resize(ref array, array.Length - 1);

										Module.gInputLog.AddData(string.Join(",", array) + ",");

										App.module.CopyObj(ref Module.gObj[Module.gObj.Length - 1], ref retObj);
									}
								}

								mObj = new Obj[1];

								for (int i = 0; i < Module.gObj.Length - 1; i++)
								{
									if (Module.gObj[i].select != 0)
									{
										App.module.CopyObj(ref mObj[mObj.Length - 1], ref Module.gObj[i]);

										Module.gObj[i].height = mObj.Length - 1;

										Array.Resize(ref mObj, mObj.Length + 1);
									}
								}

								App.module.CopyObj(ref mObj[mObj.Length - 1], ref Module.gObj[Module.gObj[Module.gObj.Length - 1].height]);

								if(mnuOptionsSelectPreview.Checked
									&& (Module.gObj[Module.gObj[Module.gObj.Length - 1].height].ch >= 11 && Module.gObj[Module.gObj[Module.gObj.Length - 1].height].ch <= 29)
									|| Module.gObj[Module.gObj[Module.gObj.Length - 1].height].ch > 100)
								{
									strRet = Module.gWAV[Module.gObj[Module.gObj[Module.gObj.Length - 1].height].value];

									if(!"".Equals(strRet) && !"".Equals(FileUtil.Dir(Module.gBms.dir + strRet)))
									{
										//TODO: PreviewWAV(strRet);
									}
								}
							}
							else
							{	// 単数選択っぽい
								if(!EnvUtil.Control)
								{
									App.module.ObjSelectCancel();
								}

								Module.gObj[Module.gObj[Module.gObj.Length - 1].height].select = 1;

								App.module.MoveSelectedObj();

								// TODO: 6293行目から
							}
						}
					}
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
					if (!mMouseDown)
					{
						return;
					}
				}

				if (!Module.gSelectArea.flag)
				{
					// 選択範囲なし

					if (e.Button == System.Windows.Forms.MouseButtons.Left
						&& tlbMenuEdit.Checked
						&& Module.gObj[Module.gObj.Length - 1].ch != 0)
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

					Module.gMouse.x = e.X;
					Module.gMouse.y = e.Y;

					Module.gSelectArea.x2 = e.X / (int)Module.gDisp.width + Module.gDisp.x;
					Module.gSelectArea.y2 = (picMain.Height - e.Y) / (int)Module.gDisp.height + Module.gDisp.y;

					if (Module.gSelectArea.x1 > Module.gSelectArea.x2)
					{
						retRect.left = Module.gSelectArea.x2;
						retRect.right = Module.gSelectArea.x1;
					}
					else
					{
						retRect.left = Module.gSelectArea.x1;
						retRect.right = Module.gSelectArea.x2;
					}

					if (Module.gSelectArea.y1 > Module.gSelectArea.y2)
					{
						retRect.top = Module.gSelectArea.y2;
						retRect.bottom = Module.gSelectArea.y1;
					}
					else
					{
						retRect.top = Module.gSelectArea.y1;
						retRect.bottom = Module.gSelectArea.y2;
					}

					select = new bool[Module.gVGrid.Length];

					for (int i = 0; i < Module.gVGrid.Length; i++)
					{
						select[i] = false;
						if (Module.gVGrid[i].visible)
						{
							if (Module.gVGrid[i].ch != 0)
							{
								if (Module.gVGrid[i].left + Module.gVGrid[i].width > retRect.left
									&& Module.gVGrid[i].left < retRect.right)
								{
									select[i] = true;
								}
							}
						}
					}

					for (int i = 0; i < Module.gObj.Length - 1; i++)
					{
						if (select[Module.gVGridNum[Module.gObj[i].ch]])
						{
							lngRet = Module.gMeasure[Module.gObj[i].measure].y + Module.gObj[i].position;

							if (lngRet + Module.OBJ_HEIGHT / Module.gDisp.height >= retRect.top && lngRet <= retRect.bottom)
							{
								if (Module.gObj[i].select < 5)
								{
									Module.gObj[i].select = 4;
								}
								else
								{
									Module.gObj[i].select = 6;
								}
							}
							else
							{
								if (Module.gObj[i].select < 5)
								{
									Module.gObj[i].select = 0;
								}
								else
								{
									Module.gObj[i].select = 5;
								}
							}
						}
						else
						{
							if (Module.gObj[i].select < 5)
							{
								Module.gObj[i].select = 0;
							}
							else
							{
								Module.gObj[i].select = 5;
							}
						}
					}

					App.module.DrawSelectArea();

					if (Module.gDisp.effect != 0)
					{
						// TODO: DrawEffect();
					}

					Module.gMouse.x = e.X;
					if (YAxisFixed)
					{
						Module.gMouse.y = e.Y;
					}

					mScrollDir = 0;

					if (e.X < 0)
					{
						mScrollDir = 20;
					}
					else if (e.X > picMain.Width)
					{
						mScrollDir = 10;
					}

					if (!YAxisFixed)
					{
						if (e.Y < 0)
						{
							mScrollDir += 1;
						}
						else if (e.Y > picMain.Height)
						{
							mScrollDir += 2;
						}
					}

					if (mScrollDir != 0)
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