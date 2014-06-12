using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Bmse.Common;
using Bmse.Util;

namespace Bmse
{
	public enum COLOR_NUM
	{
		MEASURE_NUM,
		MEASURE_LINE,
		GRID_MAIN,
		GRID_SUB,
		VERTICAL_MAIN,
		VERTICAL_SUB,
		INFO,
		Max
	}

	public enum PEN_NUM
	{
		BGM_LIGHT,
		BGM_SHADOW,
		BPM_LIGHT,
		BPM_SHADOW,
		BGA_LIGHT,
		BGA_SHADOW,
		KEY01_LIGHT,
		KEY02_LIGHT,
		KEY03_LIGHT,
		KEY04_LIGHT,

		KEY05_LIGHT,
		KEY06_LIGHT,
		KEY07_LIGHT,
		KEY08_LIGHT,
		KEY11_LIGHT,
		KEY12_LIGHT,
		KEY13_LIGHT,
		KEY14_LIGHT,
		KEY15_LIGHT,
		KEY16_LIGHT,

		KEY17_LIGHT,
		KEY18_LIGHT,
		KEY01_SHADOW,
		KEY02_SHADOW,
		KEY03_SHADOW,
		KEY04_SHADOW,
		KEY05_SHADOW,
		KEY06_SHADOW,
		KEY07_SHADOW,
		KEY08_SHADOW,

		KEY11_SHADOW,
		KEY12_SHADOW,
		KEY13_SHADOW,
		KEY14_SHADOW,
		KEY15_SHADOW,
		KEY16_SHADOW,
		KEY17_SHADOW,
		KEY18_SHADOW,
		INV_KEY01_LIGHT,
		INV_KEY02_LIGHT,

		INV_KEY03_LIGHT,
		INV_KEY04_LIGHT,
		INV_KEY05_LIGHT,
		INV_KEY06_LIGHT,
		INV_KEY07_LIGHT,
		INV_KEY08_LIGHT,
		INV_KEY11_LIGHT,
		INV_KEY12_LIGHT,
		INV_KEY13_LIGHT,
		INV_KEY14_LIGHT,

		INV_KEY15_LIGHT,
		INV_KEY16_LIGHT,
		INV_KEY17_LIGHT,
		INV_KEY18_LIGHT,
		INV_KEY01_SHADOW,
		INV_KEY02_SHADOW,
		INV_KEY03_SHADOW,
		INV_KEY04_SHADOW,
		INV_KEY05_SHADOW,
		INV_KEY06_SHADOW,

		INV_KEY07_SHADOW,
		INV_KEY08_SHADOW,
		INV_KEY11_SHADOW,
		INV_KEY12_SHADOW,
		INV_KEY13_SHADOW,
		INV_KEY14_SHADOW,
		INV_KEY15_SHADOW,
		INV_KEY16_SHADOW,
		INV_KEY17_SHADOW,
		INV_KEY18_SHADOW,

		LONGNOTE_LIGHT,
		LONGNOTE_SHADOW,
		SELECT_OBJ_LIGHT,
		SELECT_OBJ_SHADOW,
		EDIT_FRAME,
		DELETE_FRAME,

		Max
	}

	public enum BRUSH_NUM
	{
		BGM,
		BPM,
		BGA,
		KEY01,
		KEY02,
		KEY03,
		KEY04,
		KEY05,
		KEY06,
		KEY07,

		KEY08,
		KEY11,
		KEY12,
		KEY13,
		KEY14,
		KEY15,
		KEY16,
		KEY17,
		KEY18,
		INV_KEY01,

		INV_KEY02,
		INV_KEY03,
		INV_KEY04,
		INV_KEY05,
		INV_KEY06,
		INV_KEY07,
		INV_KEY08,
		INV_KEY11,
		INV_KEY12,
		INV_KEY13,

		INV_KEY14,
		INV_KEY15,
		INV_KEY16,
		INV_KEY17,
		INV_KEY18,
		LONGNOTE,
		SELECT_OBJ,
		DELETE_FRAME,
		EDIT_FRAME,

		Max
	}

	public enum GRID
	{
		NUM_BLANK_1,

		NUM_BPM,
		NUM_STOP,

		NUM_BLANK_2,

		NUM_FOOTPEDAL,

		NUM_1P_SC_L,
		NUM_1P_1KEY,
		NUM_1P_2KEY,
		NUM_1P_3KEY,
		NUM_1P_4KEY,
		NUM_1P_5KEY,
		NUM_1P_6KEY,
		NUM_1P_7KEY,
		NUM_1P_SC_R,

		NUM_BLANK_3,

		NUM_2P_SC_L,
		NUM_2P_1KEY,
		NUM_2P_2KEY,
		NUM_2P_3KEY,
		NUM_2P_4KEY,
		NUM_2P_5KEY,
		NUM_2P_6KEY,
		NUM_2P_7KEY,
		NUM_2P_SC_R,

		NUM_BLANK_4,

		NUM_BGA,
		NUM_LAYER,
		NUM_POOR,

		NUM_BLANK_5,

		NUM_BGM
	}

	partial class Module
	{
		public const int OBJ_DIFF = -1;

		public Color[] gPenColor = new Color[(int)PEN_NUM.Max];
		public Color[] gBrushColor = new Color[(int)BRUSH_NUM.Max];
		public Color[] gSystemColor = new Color[(int)COLOR_NUM.Max];

		private Pen[] hPen = new Pen[76];
		private Brush[] hBrush = new Brush[38];

		private Obj[] mRetObj;

		public const int OBJ_WIDTH = 28;
		public const int OBJ_HEIGHT = 9;

		public const int GRID_WIDTH = OBJ_WIDTH;
		public const int GRID_HALF_WIDTH = GRID_WIDTH / 2;
		public const int GRID_HALF_EDGE_WIDTH = (GRID_WIDTH * 3) / 4;
		public const int SPACE_WIDTH = 4;
		public const int FRAME_WIDTH = GRID_WIDTH / 2;
		public const int LEFT_SPACE = FRAME_WIDTH + SPACE_WIDTH;
		public const int RIGHT_SPACE = FRAME_WIDTH + SPACE_WIDTH * 2;

		public static double[] gSin = new double[256 + 64];

		public void InitVerticalLine()
		{
			try
			{
				int retInt = 0;

				if (frmMain.cboDispFrame.SelectedIndex != 0)
				{
					for (int i = (int)GRID.NUM_1P_1KEY; i <= (int)GRID.NUM_1P_7KEY; i++)
					{
						gVGrid[i].width = GRID_WIDTH;
					}

					for (int i = (int)GRID.NUM_2P_1KEY; i <= (int)GRID.NUM_2P_7KEY; i++)
					{
						gVGrid[i].width = GRID_WIDTH;
					}
				}
				else
				{
					gVGrid[(int)GRID.NUM_1P_1KEY].width = GRID_HALF_EDGE_WIDTH;
					gVGrid[(int)GRID.NUM_2P_1KEY].width = GRID_HALF_EDGE_WIDTH;

					for (int i = (int)GRID.NUM_1P_2KEY; i <= (int)GRID.NUM_1P_6KEY; i++)
					{
						gVGrid[i].width = GRID_HALF_WIDTH;
					}

					for(int i = (int)GRID.NUM_2P_2KEY; i <= (int)GRID.NUM_2P_6KEY; i++)
					{
						gVGrid[i].width = GRID_HALF_WIDTH;
					}

					if (frmMain.cboDispKey.SelectedIndex != 0)
					{
						gVGrid[(int)GRID.NUM_1P_7KEY].width = GRID_HALF_EDGE_WIDTH;
						gVGrid[(int)GRID.NUM_2P_7KEY].width = GRID_HALF_EDGE_WIDTH;
					}
					else
					{
						gVGrid[(int)GRID.NUM_1P_5KEY].width = GRID_HALF_EDGE_WIDTH;
						gVGrid[(int)GRID.NUM_2P_5KEY].width = GRID_HALF_EDGE_WIDTH;
					}
				}

				switch (frmMain.cboPlayer.SelectedIndex)
				{
					case 0:
					case 1:
					case 2:	// 1P/2P/DP
						gVGrid[(int)GRID.NUM_FOOTPEDAL].visible = false;
						gVGrid[(int)GRID.NUM_2P_SC_L].visible = true;

						if (frmMain.cboDispKey.SelectedIndex == 0)
						{
							gVGrid[(int)GRID.NUM_1P_6KEY].visible = false;
							gVGrid[(int)GRID.NUM_1P_7KEY].visible = false;
						}
						else
						{
							gVGrid[(int)GRID.NUM_1P_6KEY].visible = true;
							gVGrid[(int)GRID.NUM_1P_7KEY].visible = true;
						}

						if (frmMain.cboDispKey.SelectedIndex == 0)
						{
							gVGrid[(int)GRID.NUM_1P_SC_L].visible = true;
							gVGrid[(int)GRID.NUM_1P_SC_R].visible = false;
						}
						else
						{
							gVGrid[(int)GRID.NUM_1P_SC_L].visible = false;
							gVGrid[(int)GRID.NUM_1P_SC_R].visible = true;
						}

						if (frmMain.cboPlayer.SelectedIndex != 0)
						{
							for (int i = (int)GRID.NUM_2P_SC_L; i <= (int)GRID.NUM_2P_SC_R + 1; i++)
							{
								gVGrid[i].visible = true;
							}

							if (frmMain.cboDispKey.SelectedIndex == 0)
							{
								gVGrid[(int)GRID.NUM_2P_6KEY].visible = false;
								gVGrid[(int)GRID.NUM_2P_7KEY].visible = false;
							}
							else
							{
								gVGrid[(int)GRID.NUM_2P_6KEY].visible = true;
								gVGrid[(int)GRID.NUM_2P_7KEY].visible = true;
							}

							if (frmMain.cboDispSC2P.SelectedIndex == 0)
							{
								gVGrid[(int)GRID.NUM_2P_SC_L].visible = true;
								gVGrid[(int)GRID.NUM_2P_SC_R].visible = false;
							}
							else
							{
								gVGrid[(int)GRID.NUM_2P_SC_L].visible = false;
								gVGrid[(int)GRID.NUM_2P_SC_R].visible = true;
							}
						}
						else
						{
							for (int i = (int)GRID.NUM_2P_SC_L; i <= (int)GRID.NUM_2P_SC_R + 1; i++)
							{
								gVGrid[i].visible = false;
							}
						}

						break;

					case 3:	// PMS
						gVGrid[(int)GRID.NUM_FOOTPEDAL].visible = false;
						gVGrid[(int)GRID.NUM_1P_SC_L].visible = false;
						gVGrid[(int)GRID.NUM_1P_6KEY].visible = false;
						gVGrid[(int)GRID.NUM_1P_7KEY].visible = false;
						gVGrid[(int)GRID.NUM_1P_SC_R].visible = false;
						gVGrid[(int)GRID.NUM_2P_SC_L - 1].visible = false;
						gVGrid[(int)GRID.NUM_2P_SC_L].visible = false;
						gVGrid[(int)GRID.NUM_2P_1KEY].visible = false;
						gVGrid[(int)GRID.NUM_2P_SC_R + 1].visible = true;

						for (int i = (int)GRID.NUM_2P_2KEY; i <= (int)GRID.NUM_2P_5KEY; i++)
						{
							gVGrid[i].visible = true;
						}

						for (int i = (int)GRID.NUM_2P_6KEY; i <= (int)GRID.NUM_2P_SC_R; i++)
						{
							gVGrid[(int)GRID.NUM_1P_5KEY].width = GRID_HALF_WIDTH;
							gVGrid[(int)GRID.NUM_2P_5KEY].width = GRID_HALF_EDGE_WIDTH;
						}

						break;

					case 4:	// Oct
						gVGrid[(int)GRID.NUM_FOOTPEDAL].visible = true;
						gVGrid[(int)GRID.NUM_1P_SC_L].visible = true;
						gVGrid[(int)GRID.NUM_1P_6KEY].visible = true;
						gVGrid[(int)GRID.NUM_1P_7KEY].visible = true;
						gVGrid[(int)GRID.NUM_2P_SC_L - 1].visible = false;
						gVGrid[(int)GRID.NUM_2P_1KEY].visible = false;
						gVGrid[(int)GRID.NUM_2P_SC_R].visible = true;
						gVGrid[(int)GRID.NUM_2P_SC_R + 1].visible = true;

						for (int i = (int)GRID.NUM_2P_2KEY; i <= (int)GRID.NUM_2P_7KEY; i++)
						{
							gVGrid[i].visible = true;
						}

						if (frmMain.cboDispFrame.SelectedIndex == 0)
						{
							gVGrid[(int)GRID.NUM_1P_5KEY].width = GRID_HALF_WIDTH;
							gVGrid[(int)GRID.NUM_1P_7KEY].width = GRID_HALF_WIDTH;
							gVGrid[(int)GRID.NUM_2P_5KEY].width = GRID_HALF_WIDTH;
							gVGrid[(int)GRID.NUM_2P_7KEY].width = GRID_HALF_EDGE_WIDTH;
						}

						break;

					default:
						break;
				}

				retInt = 0;

				for (int i = 0; i < 1000; i++)
				{
					gMeasure[i].y = retInt;
					retInt = retInt + gMeasure[i].len;
				}

				gDisp.maxY = gMeasure[999].y + gMeasure[999].len;

				//Redraw();
				frmMain.picMain.Refresh();
			}
			catch (Exception e)
			{
				CleanUp(e.Message, "InitVerticalLine");
			}
		}

		// RedrawメソッドをpicMainのPaintイベントメソッドに書き換え
		public void Redraw(Object sender, PaintEventArgs e)
		{
			try
			{
				int retInt = 0;

				if (!frmMain.Visible)
				{
					return;
				}

				for(int i = 0; i < gDisp.maxMeasure; i++)
				{
					retInt = retInt + gMeasure[i].len;
				}

				frmMain.vsbMain.Minimum = retInt / gDisp.resolution;

				gDisp.width = DataSource.ItemData(frmMain.cboDispWidth, frmMain.cboDispWidth.SelectedIndex) / 100.0;
				gDisp.height = DataSource.ItemData(frmMain.cboDispHeight, frmMain.cboDispHeight.SelectedIndex) / 100.0;
				gDisp.startMeasure = 999;
				gDisp.endMeasure = 999;
				gDisp.startPos = gDisp.y - OBJ_HEIGHT;
				gDisp.endPos = (int)(gDisp.y + frmMain.picMain.Height / gDisp.height);

				retInt = FRAME_WIDTH;

				for (int i = 0; i < gVGridNum.Length; i++)
				{
					gVGridNum[i] = 0;
				}

				for (int i = 0; i < gVGrid.Length; i++)
				{
					if (gVGrid[i].visible)
					{
						if (11 <= gVGrid[i].ch && gVGrid[i].ch <= 29)
						{
							gVGridNum[gVGrid[i].ch] = i;
							gVGridNum[gVGrid[i].ch + 20] = i;
							gVGridNum[gVGrid[i].ch + 40] = i;
						}
						else if (0 < gVGrid[i].ch)
						{
							gVGridNum[gVGrid[i].ch] = i;
						}

						gVGrid[i].left = retInt;

						if (gVGrid[i].ch == 15)
						{
							if (frmMain.cboDispKey.SelectedIndex == 1 || frmMain.cboPlayer.SelectedIndex > 2)
							{
								gVGrid[i].objLeft = gVGrid[i].left + (gVGrid[i].width - GRID_WIDTH) / 2;
							}
							else
							{
								gVGrid[i].objLeft = gVGrid[i].left + gVGrid[i].width - GRID_WIDTH;
							}
						}
						else if (gVGrid[i].ch == 25)
						{
							if (frmMain.cboPlayer.SelectedIndex == 4)
							{
								gVGrid[i].objLeft = gVGrid[i].left + (gVGrid[i].width - GRID_WIDTH) / 2;
							}
							else if (frmMain.cboDispKey.SelectedIndex == 0 || frmMain.cboPlayer.SelectedIndex == 3)
							{
								gVGrid[i].objLeft = gVGrid[i].left + (gVGrid[i].width - GRID_WIDTH);
							}
							else
							{
								gVGrid[i].objLeft = gVGrid[i].left + (gVGrid[i].width - GRID_WIDTH) / 2;
							}
						}
						else if (gVGrid[i].ch == 19)
						{
							if (frmMain.cboPlayer.SelectedIndex > 2)
							{
								gVGrid[i].objLeft = gVGrid[i].left + (gVGrid[i].width - GRID_WIDTH) / 2;
							}
							else
							{
								gVGrid[i].objLeft = gVGrid[i].left + gVGrid[i].width - GRID_WIDTH;
							}
						}
						else if (gVGrid[i].ch == 29)
						{
							gVGrid[i].objLeft = gVGrid[i].left + gVGrid[i].width - GRID_WIDTH;
						}
						else if ((12 <= gVGrid[i].ch && gVGrid[i].ch <= 18)
							|| 22 <= gVGrid[i].ch && gVGrid[i].ch <= 28)
						{
							gVGrid[i].objLeft = gVGrid[i].left + (gVGrid[i].width - GRID_WIDTH) / 2;
						}
						else
						{
							gVGrid[i].objLeft = retInt;
						}

						if (gVGrid[i].left + gVGrid[i].width >= gDisp.x && frmMain.picMain.Width + (gDisp.x - gVGrid[i].left) * gDisp.width >= 0)
						{
							gVGrid[i].draw = true;
						}
						else
						{
							gVGrid[i].draw = false;
						}

						retInt += gVGrid[i].width;
					}
					else
					{
						gVGrid[i].draw = false;
					}
				}

				gDisp.maxX = retInt;

				retInt = 0;

				for (int i = 0; i < 1000; i++)
				{
					retInt += gMeasure[i].len;

					if (retInt > gDisp.y)
					{
						gDisp.startMeasure = i;
						break;
					}
				}

				for (int i = gDisp.startMeasure + 1; i < 1000; i++)
				{
					retInt += gMeasure[i].len;

					if ((retInt - gDisp.y) * gDisp.height >= frmMain.picMain.Height)
					{
						gDisp.endMeasure = i;
						break;
					}
				}

				retInt = 0;

				/*
				using (var gPicMain = frmMain.picMain.CreateGraphics())
				{
					gPicMain.Clear(Color.Black);
				}*/
				e.Graphics.Clear(Color.Black);

				DrawGridBG(e.Graphics);

				DrawMeasureNum(e.Graphics);

				DrawVerticalGrayLine(e.Graphics);

				DrawHorizonalLine(e.Graphics);

				DrawVerticalWhiteLine(e.Graphics);

				DrawMeasureLine(e.Graphics);

				InitPen();

				mRetObj = new Obj[1];

				for (int i = 0; i < gObj.Length - 1; i++)
				{
					if (gObj[i].ch > 0 && gObj[i].ch < 133)
					{
						if (gVGrid[gVGridNum[gObj[i].ch]].draw)
						{
							if (gDisp.startPos <= gMeasure[gObj[i].measure].y + gObj[i].position
								&& gDisp.endPos >= gMeasure[gObj[i].measure].y + gObj[i].position)
							{
								DrawObj(ref gObj[i], e.Graphics);
							}
						}
					}
				}

				for (int i = 0; i < mRetObj.Length - 1; i++)
				{
					if(gDisp.startPos <= gMeasure[mRetObj[i].measure].y + mRetObj[i].position
						&& gDisp.endPos >= gMeasure[mRetObj[i].measure].y + mRetObj[i].position
						&& gVGrid[gVGridNum[mRetObj[i].ch]].draw
						&& mRetObj[i].ch != 0)
					{
						DrawObj(ref mRetObj[i], e.Graphics);
					}
				}

				Array.Clear(mRetObj, 0, mRetObj.Length);

				DeletePen();

				DrawGridInfo(e.Graphics);

				if ((gDisp.maxX + 16) * gDisp.width - frmMain.picMain.Width < 0)
				{
					frmMain.hsbMain.Maximum = 0;
				}
				else
				{
					frmMain.hsbMain.Maximum = (gDisp.maxX + FRAME_WIDTH) - (int)(frmMain.picMain.Width / gDisp.width);
				}

				if (gDisp.effect != 0)
				{
					// TODO: DrawEffect();
				}
			}
			catch(Exception exp)
			{
				CleanUp(exp.Message, "Redraw");
			}
		}

		private void DrawGridBG(Graphics g)
		{
			if (frmMain.mnuOptionsLaneBG.Checked)
			{
				for (int i = 0; i < gVGrid.Length; i++)
				{
					using (Brush brush = new SolidBrush(gVGrid[i].BackColor))
					{
						if (gVGrid[i].draw)
						{
							if (gVGrid[i].ch != 0)
							{
								g.FillRectangle(brush,
									(gVGrid[i].left - gDisp.x) * (int)gDisp.width,
									0,
									((gVGrid[i].left + gVGrid[i].width + 1 - gDisp.x) * (int)gDisp.width) - (gVGrid[i].left - gDisp.x) * (int)gDisp.width,
									frmMain.picMain.Height);
							}
						}
					}
				}
			}
		}

		//private static Font MeasureFont = new Font(System.Windows.Forms.Control.DefaultFont.Name, 72.0f, FontStyle.Italic | FontStyle.Bold);
		private void DrawMeasureNum(Graphics g)
		{
			string retStr;
			SizeF size;

			FontFamily fontFamily = frmMain.picMain.Font.FontFamily;
			using (Font font = new Font(fontFamily, 72.0f, FontStyle.Italic | FontStyle.Bold))
			{
				using (Brush brush = new SolidBrush(gSystemColor[(int)COLOR_NUM.MEASURE_NUM]))
				{
					for (int i = gDisp.startMeasure; i <= gDisp.endMeasure; i++)
					{
						retStr = "#" + i.ToString("000");

						size = g.MeasureString(retStr, font);

						g.DrawString(retStr, font, brush, (frmMain.picMain.Width - size.Width) / 2.0f, frmMain.picMain.Height - size.Height - (gMeasure[i].y - gDisp.y) * (float)gDisp.height);
					}
				}
			}
		}

		private void DrawVerticalGrayLine(Graphics g)
		{
			using (Pen pen = new Pen(gSystemColor[(int)COLOR_NUM.VERTICAL_SUB], 1.0f))
			{
				for (int i = 0; i < gVGrid.Length; i++)
				{
					if (gVGrid[i].draw)
					{
						if (gVGrid[i].ch != 0)
						{
							g.DrawLine(pen, gVGrid[i].left + gVGrid[i].width, gDisp.y, gVGrid[i].left + gVGrid[i].width, frmMain.picMain.Height);
							// PrintLine
						}
					}
				}
			}
		}

		private void DrawHorizonalLine(Graphics g)
		{
			int retInt;

			using (Pen pen = new Pen(gSystemColor[(int)COLOR_NUM.GRID_MAIN]))
			{
				for (int i = gDisp.startMeasure; i <= gDisp.endMeasure; i++)
				{
					int itemdata = DataSource.DsListDispGridMain[frmMain.cboDispGridMain.SelectedIndex].Value;
					if (itemdata != 0)
					{
						retInt = 192 / itemdata;

						for (int j = 0; j <= gMeasure[i].len; j += retInt)
						{
							g.DrawLine(pen, LEFT_SPACE, frmMain.picMain.Height - (gMeasure[i].y + j), gDisp.maxX - FRAME_WIDTH, frmMain.picMain.Height - (gMeasure[i].y + j));
							// Print Line
						}
					}
				}
			}
			using (Pen pen = new Pen(gSystemColor[(int)COLOR_NUM.GRID_SUB], 1.0f))
			{
				for (int i = gDisp.startMeasure; i <= gDisp.endMeasure; i++)
				{
					int itemdata = DataSource.DsListDispGridSub[frmMain.cboDispGridSub.SelectedIndex].Value;
					if (itemdata != 0)
					{
						retInt = 192 / itemdata;

						for (int j = 0; j <= gMeasure[i].len; j += retInt)
						{
							g.DrawLine(pen, FRAME_WIDTH, frmMain.picMain.Height - (gMeasure[i].y + j), gDisp.maxX - RIGHT_SPACE, frmMain.picMain.Height - (gMeasure[i].y + j));
							// PrintLine
						}
					}
				}
			}
		}

		private void DrawVerticalWhiteLine(Graphics g)
		{
			using (Pen pen = new Pen(gSystemColor[(int)COLOR_NUM.VERTICAL_MAIN], 1.0f))
			{
				for (int i = 0; i < gVGrid.Length; i++)
				{
					if (gVGrid[i].draw)
					{
						if (gVGrid[i].ch == 0)
						{
							g.DrawLine(pen, gVGrid[i].left, gDisp.y, gVGrid[i].left, frmMain.picMain.Height);
							// PrintLine
							g.DrawLine(pen, gVGrid[i].left + gVGrid[i].width, gDisp.y, gVGrid[i].left + gVGrid[i].width, frmMain.picMain.Height);
							// PrintLine
						}
					}
				}
			}
		}

		private void DrawMeasureLine(Graphics g)
		{
			using (Pen pen = new Pen(gSystemColor[(int)COLOR_NUM.MEASURE_LINE], 1.0f))
			{
				for (int i = gDisp.startMeasure; i <= gDisp.endMeasure; i++)
				{
					g.DrawLine(pen, FRAME_WIDTH, frmMain.picMain.Height - gMeasure[i].y, gDisp.maxX - FRAME_WIDTH, frmMain.picMain.Height - gMeasure[i].y);
					// PrintLine
				}

				if (gDisp.endMeasure == 999)
				{
					g.DrawLine(pen, FRAME_WIDTH, frmMain.picMain.Height - gMeasure[999].y + gMeasure[999].len, gDisp.maxX - FRAME_WIDTH, frmMain.picMain.Height - gMeasure[999].y);
					// PrintLine
				}
			}
		}

		private void DrawGridInfo(Graphics g)
		{
			int intRet;
			int lngRet;
			string strRet;
			SizeF retSize;

			FontFamily fontFamily = frmMain.picMain.Font.FontFamily;
			using (Font font = new Font(fontFamily, 9.0f))
			{
				using (Brush brush = new SolidBrush(gSystemColor[(int)COLOR_NUM.INFO]))
				{
					for (int i = 0; i < gVGrid.Length; i++)
					{
						if (gVGrid[i].draw)
						{
							if (gVGrid[i].ch != 0)
							{
								if (frmMain.mnuOptionsVertical.Checked)
								{
									lngRet = (int)((gVGrid[i].left + (gVGrid[i].width / 2) - gDisp.x) * gDisp.width);

									for (int j = 0; j < gVGrid[i].text.Length; j++)
									{
										strRet = StringUtil.Mid(gVGrid[i].text, j + 1, 1);
										intRet = strRet.Length;
										retSize = g.MeasureString(strRet, font);

										int x = lngRet - (int)(retSize.Width / 2.0f);

										g.DrawString(strRet, font, Brushes.Black, x, 0 + 11 * j);
										g.DrawString(strRet, font, Brushes.Black, x - 1, 1 + 11 * j);
										g.DrawString(strRet, font, Brushes.Black, x + 1, 1 + 11 * j);
										g.DrawString(strRet, font, Brushes.Black, x, 2 + 11 * j);

										g.DrawString(strRet, font, brush, x, 1 + 11 * j);
									}
								}
								else
								{
									intRet = gVGrid[i].text.Length;
									retSize = g.MeasureString(gVGrid[i].text, font);

									int x = (int)((gVGrid[i].left + gVGrid[i].width / 2 - gDisp.x) * gDisp.width - (int)(retSize.Width / 2.0f) + 1);

									g.DrawString(gVGrid[i].text, font, Brushes.Black, x, 0);
									g.DrawString(gVGrid[i].text, font, Brushes.Black, x - 1, 1);
									g.DrawString(gVGrid[i].text, font, Brushes.Black, x + 1, 1);
									g.DrawString(gVGrid[i].text, font, Brushes.Black, x, 2);
									g.DrawString(gVGrid[i].text, font, brush, x, 1);
								}
							}
						}
					}
				}
			}
		}

		private void PrintLine(int x, int y, int w, int h)
		{
			w = w * (int)gDisp.width;

			if (x - gDisp.x < 0)
			{
				if (w != 0)
				{
					w += (x - gDisp.x) * (int)gDisp.width;
				}

				x = 0;
			}
			else
			{
				x = (x - gDisp.x) * (int)gDisp.width;
			}

			if (y + gDisp.y < 0)
			{
				if (h != 0)
				{
					h += y - gDisp.y;
				}

				y = 0;
			}
			else
			{
				y = (y - gDisp.y) * (int)gDisp.height;
			}

			// 1145行目
			// 線の描画だけど、めんどくせえからPrintLine使わない方向でいこうぜ
		}

		//private static Font ObjFont = new Font(System.Windows.Forms.Control.DefaultFont.Name, 8.0f, FontStyle.Regular);
		private void DrawObj(ref Obj retObj, Graphics g)
		{
			int intRet;
			string text;
			string[] array;
			int x;
			int y;
			int width;
			SizeF retSize;
			int lightNum = 0;
			int shadowNum = 0;
			int brushNum = 0;

			try
			{
				if (gVGridNum[retObj.ch] == 0)
				{
					return;
				}

				x = (gVGrid[gVGridNum[retObj.ch]].objLeft - gDisp.x) * (int)gDisp.width + 1;
				y = frmMain.picMain.Height + OBJ_DIFF - (gMeasure[retObj.measure].y + retObj.position - gDisp.y) * (int)gDisp.height;
				width = GRID_WIDTH * (int)gDisp.width - 1;

				switch (retObj.ch)
				{
					case 3:
					case 8:
					case 9:
						text = retObj.value.ToString();
						break;

					case 4:
					case 6:
					case 7:
						text = gBMP[retObj.value];
						if (frmMain.mnuOptionsObjectFileName.Checked && text.Length > 0)
						{
							array = text.Split('.');
							text = StringUtil.Left(text, text.Length - (array[array.Length - 1].Length + 1));
						}
						else
						{
							text = NumConv(retObj.value);
						}
						break;

					default:
						text = gWAV[retObj.value];

						if (frmMain.mnuOptionsObjectFileName.Checked && text.Length > 0)
						{
							array = text.Split('.');
							text = StringUtil.Left(text, text.Length - (array[array.Length - 1].Length + 1));
						}
						else
						{
							text = NumConv(retObj.value);
						}

						if (retObj.att == 2 || (retObj.ch >= 51 && retObj.ch < 69))
						{
							x += 3;
							width -= 6;
						}

						if (retObj.att == 2 && retObj.ch >= 11 && retObj.ch <= 29)
						{
							// TODO: CopyObj(mRetObj[mRetObj.Length - 1], retObj);
							mRetObj[mRetObj.Length - 1].ch = retObj.ch + 40;

							Array.Resize(ref mRetObj, mRetObj.Length + 1);
						}

						break;
				}

				switch (retObj.select)
				{
					case 0:
					case 4:
					case 5:
					case 6:
						if (retObj.ch < 10 || retObj.ch > 100)
						{
							lightNum = gVGrid[gVGridNum[retObj.ch]].lightNum;
							shadowNum = gVGrid[gVGridNum[retObj.ch]].shadowNum;
							brushNum = gVGrid[gVGridNum[retObj.ch]].brushNum;
						}
						else if (retObj.ch > 50)
						{
							lightNum = (int)PEN_NUM.LONGNOTE_LIGHT;
							shadowNum = (int)PEN_NUM.LONGNOTE_SHADOW;
							brushNum = (int)BRUSH_NUM.LONGNOTE;
						}
						else
						{
							if (retObj.att == 0)
							{
								lightNum = gVGrid[gVGridNum[retObj.ch]].lightNum;
								shadowNum = gVGrid[gVGridNum[retObj.ch]].shadowNum;
								brushNum = gVGrid[gVGridNum[retObj.ch]].brushNum;
							}
							else
							{
								intRet = retObj.ch % 10;

								if (11 <= retObj.ch && retObj.ch <= 15)
								{
									lightNum = (int)PEN_NUM.INV_KEY01_LIGHT + intRet - 1;
									shadowNum = (int)PEN_NUM.INV_KEY01_SHADOW + intRet - 1;
									brushNum = (int)BRUSH_NUM.INV_KEY01 + intRet - 1;
								}
								else if (retObj.ch == 18)
								{
									lightNum = (int)PEN_NUM.KEY06_LIGHT;
									shadowNum = (int)PEN_NUM.INV_KEY06_SHADOW;
									brushNum = (int)BRUSH_NUM.INV_KEY06;
								}
								else if (retObj.ch == 19)
								{
									lightNum = (int)PEN_NUM.KEY07_LIGHT;
									shadowNum = (int)PEN_NUM.INV_KEY07_SHADOW;
									brushNum = (int)BRUSH_NUM.INV_KEY07;
								}
								else if (retObj.ch == 16)
								{
									lightNum = (int)PEN_NUM.KEY08_LIGHT;
									shadowNum = (int)PEN_NUM.INV_KEY08_SHADOW;
									brushNum = (int)BRUSH_NUM.INV_KEY08;
								}
								else if (21 <= retObj.ch && retObj.ch <= 25)
								{
									lightNum = (int)PEN_NUM.INV_KEY11_LIGHT + intRet - 1;
									shadowNum = (int)PEN_NUM.INV_KEY11_SHADOW + intRet - 1;
									brushNum = (int)BRUSH_NUM.INV_KEY11 + intRet - 1;
								}
								else if (retObj.ch == 28)
								{
									lightNum = (int)PEN_NUM.KEY16_LIGHT;
									shadowNum = (int)PEN_NUM.INV_KEY16_SHADOW;
									brushNum = (int)BRUSH_NUM.INV_KEY16;
								}
								else if (retObj.ch == 29)
								{
									lightNum = (int)PEN_NUM.KEY17_LIGHT;
									shadowNum = (int)PEN_NUM.INV_KEY17_SHADOW;
									brushNum = (int)BRUSH_NUM.INV_KEY17;
								}
								else if (retObj.ch == 26)
								{
									lightNum = (int)PEN_NUM.KEY18_LIGHT;
									shadowNum = (int)PEN_NUM.INV_KEY18_SHADOW;
									brushNum = (int)BRUSH_NUM.INV_KEY18;
								}
							}
						}

						break;
						
					case 1:
						lightNum = (int)PEN_NUM.SELECT_OBJ_LIGHT;
						shadowNum = (int)PEN_NUM.SELECT_OBJ_SHADOW;
						brushNum = (int)BRUSH_NUM.SELECT_OBJ;

						break;

					default:
						if (retObj.select == 2)
						{
							lightNum = (int)PEN_NUM.EDIT_FRAME;
						}
						else
						{
							lightNum = (int)PEN_NUM.DELETE_FRAME;
						}

						brushNum = hBrush.Length - 1;

						g.FillRectangle(hBrush[brushNum], x - 1, y - OBJ_HEIGHT - 1, x + width + 1, y + 2);
						g.DrawRectangle(hPen[lightNum], x - 1, y - OBJ_HEIGHT - 1, x + width + 1, y + 2);

						return;
				}

				g.FillRectangle(hBrush[brushNum], x, y - OBJ_HEIGHT, x + width, y + 1);
				g.DrawRectangle(hPen[lightNum], x, y - OBJ_HEIGHT, x + width, y + 1);

				g.DrawLine(hPen[shadowNum], x, y, x + width - 1, y);
				g.DrawLine(hPen[shadowNum], x + width - 1, y, x + width - 1, y - OBJ_HEIGHT);

				intRet = text.Length;

				FontFamily fontFamily = frmMain.picMain.Font.FontFamily;
				using (Font font = new Font(fontFamily, 8.0f))
				{
					retSize = g.MeasureString(text, font);

					y -= (OBJ_HEIGHT + (int)retSize.Height) / 2 + 1;

					if (retObj.select == 1)
					{
						g.DrawString(text, font, Brushes.White, x + 3, y);
						g.DrawString(text, font, Brushes.Black, x + 2, y);
					}
					else
					{
						g.DrawString(text, font, Brushes.Black, x + 3, y);
						g.DrawString(text, font, Brushes.White, x + 2, y);
					}
				}
			}
			catch(Exception e)
			{
				CleanUp(e.Message, "DrawObj");
			}
		}

		public void DrawObjRect(int num)
		{
			int x;
			int y;
			int width;

			try
			{
				if (gVGridNum[gObj[num].ch] == 0)
				{
					return;
				}

				x = (gVGrid[gVGridNum[gObj[num].ch]].objLeft - gDisp.x) * (int)gDisp.width + 1;
				y = frmMain.picMain.Height + OBJ_DIFF - (gMeasure[gObj[num].measure].y + gObj[num].position - gDisp.y) * (int)gDisp.height;

				width = GRID_WIDTH * (int)gDisp.width - 1;

				if(gObj[num].att == 2 || (gObj[num].ch >= 51 && gObj[num].ch <= 69))
				{
					x += 3;
					width -= 6;
				}

				using(Graphics gPicMain = frmMain.picMain.CreateGraphics())
				{
					using(Pen pen = new Pen(gPenColor[(int)PEN_NUM.EDIT_FRAME]))
					{
						gPicMain.DrawRectangle(pen, x - 1, y - OBJ_HEIGHT - 1, x + width + 1, y + 2);
					}
				}
			}
			catch(Exception e)
			{
				CleanUp(e.Message, "DrawObjRect");
			}
		}

		public void DrawObjMax(int x, int y)
		{
			int lngRet;
			Obj retObj = new Obj();

			try
			{
				if (gIgnoreInput)
				{
					return;
				}

				SetObjData(ref retObj, x, y);

				if (frmMain.tlbMenuWrite.Pressed)
				{
					if (retObj.ch >= 11 && retObj.ch <= 29)
					{
						if (EnvUtil.Control)
						{
							retObj.att = 1;
						}
						else if (EnvUtil.Shift)
						{
							retObj.ch += 40;
							retObj.att = 2;
						}
					}

					if (DataSource.DsListDispGridMain[frmMain.cboDispGridMain.SelectedIndex].Value != 0)
					{
						lngRet = 192 / DataSource.DsListDispGridMain[frmMain.cboDispGridMain.SelectedIndex].Value;
						retObj.position = (retObj.position / lngRet) * lngRet;
					}
				}

				if (!frmMain.tlbMenuWrite.Pressed)
				{
					// TODO: 1490行目から
				}
			}
			catch (Exception e)
			{
			}
		}

		public void SetObjData(ref Obj retObj, int x, int y)
		{
			int lngRet;

			if (x < 0)
			{
				x = 0;
			}
			else if (x > frmMain.picMain.Width)
			{
				x = frmMain.picMain.Width;
			}

			lngRet = x / (int)gDisp.width + gDisp.x;

			retObj.ch = 8;

			for (int i = 0; i < gVGrid.Length; i++)
			{
				if (gVGrid[i].draw && gVGrid[i].ch != 0)
				{
					if (gVGrid[i].left <= lngRet)
					{
						retObj.ch = gVGrid[i].ch;
					}
					else
					{
						break;
					}
				}
			}

			retObj.id = gIDNum;
			retObj.height = gObj.Length - 1;

			if (y < 1)
			{
				y = 1;
			}
			else if (y > frmMain.picMain.Height + OBJ_DIFF)
			{
				y = frmMain.picMain.Height + OBJ_DIFF;
			}

			lngRet = (frmMain.picMain.Height - y + OBJ_DIFF) / (int)gDisp.height + gDisp.y;

			for (int i = 0; i < 1000; i++)
			{
				if (gMeasure[i].y <= lngRet)
				{
					retObj.measure = i;
					retObj.position = lngRet - gMeasure[i].y;

					if (retObj.position > gMeasure[i].len)
					{
						retObj.position = gMeasure[i].len - 1;
					}
				}
				else
				{
					break;
				}
			}

			switch (retObj.ch)
			{
				case 3:
				case 8:
				case 9:
					retObj.value = 0;
					break;

				case 4:
				case 6:
				case 7:
					// TODO: retObj.value = frmMain.FromLong(frmMain.lstBMP.SelectedIndex + 1);
					break;

				default:
					// TODO: retObj.value = frmMain.FromLong(frmMain.lstWAV.SelectedIndex + 1);
					break;
			}
		}

		public void DrawStatusBar(ref Obj retObj)
		{
			string strRet;
			int lngRet;
			string[] array;

			strRet = "Position:  " + retObj.measure + gStatusBar[23] + "  ";

			int _cboData = DataSource.DsListDispGridMain[frmMain.cboDispGridMain.SelectedIndex].Value;
			lngRet = _cboData;

			if (lngRet != 0)
			{
				if (retObj.select > 1 && retObj.position != 0)
				{
					// TODO: lngRet = GCD(retObj.position, gMeasure[retObj.measure].len);

					if (lngRet > 192 / _cboData)
					{
						lngRet = _cboData;
					}
					else
					{
						lngRet = 192 / lngRet;
					}
				}

				strRet = strRet + retObj.position * lngRet / 192 + "/" + gMeasure[retObj.measure].len * lngRet / 192;
			}
			else
			{
				strRet = strRet + retObj.position + "/" + gMeasure[retObj.measure].len;
			}

			strRet = strRet + "  ";

			if (retObj.ch > 100)
			{
				strRet = strRet + gStatusBar[1] + " " + (retObj.ch - 100).ToString("00");
			}
			else if (retObj.ch < 10)
			{
				strRet = strRet + gStatusBar[retObj.ch];
			}
			else if (11 <= retObj.ch && retObj.ch <= 15)
			{
				strRet = strRet + gStatusBar[11] + (retObj.ch - 10).ToString();
			}
			else if (retObj.ch == 16)
			{
				strRet = strRet + gStatusBar[13];
			}
			else if (retObj.ch == 18 || retObj.ch == 19)
			{
				strRet = strRet + gStatusBar[11] + (retObj.ch - 12).ToString();
			}
			else if (21 <= retObj.ch && retObj.ch <= 25)
			{
				strRet = strRet + gStatusBar[12].Length + (retObj.ch - 20).ToString();
			}
			else if (retObj.ch == 26)
			{
				strRet = strRet + gStatusBar[14];
			}
			else if (retObj.ch == 28 || retObj.ch == 29)
			{
				strRet = strRet + gStatusBar[12] + (retObj.ch - 22).ToString();
			}
			else if (51 <= retObj.ch && retObj.ch <= 55)
			{
				strRet = strRet + gStatusBar[11] + (retObj.ch - 50).ToString();
			}
			else if (retObj.ch == 56)
			{
				strRet = strRet + gStatusBar[13];
			}
			else if (retObj.ch == 58 || retObj.ch == 59)
			{
				strRet = strRet + gStatusBar[11] + (retObj.ch - 52).ToString();
			}
			else if (61 <= retObj.ch && retObj.ch <= 65)
			{
				strRet = strRet + gStatusBar[12] + (retObj.ch - 60).ToString();
			}
			else if(retObj.ch == 66)
			{
				strRet = strRet + gStatusBar[14];
			}
			else if(retObj.ch == 68 || retObj.ch == 69)
			{
				strRet = strRet + gStatusBar[12] + (retObj.ch - 62).ToString();
			}

			if (retObj.ch >= 11 && retObj.ch <= 29)
			{
				if (retObj.att == 1)
				{
					strRet = strRet + " " + gStatusBar[15];
				}
				else if (retObj.att == 2)
				{
					strRet = strRet + " " + gStatusBar[16];
				}
			}
			else if (retObj.ch >= 51 && retObj.ch <= 69)
			{
				strRet = strRet + " " + gStatusBar[16];
			}

			frmMain.staMainPosition.Text = strRet;

			array = StringUtil.Mid(frmMain.lstMeasureLen.Items[retObj.measure].ToString(), 6).Split('/');

			frmMain.staMainMeasure.Text = StringUtil.Right(" " + array[0], 2) + "/" + StringUtil.Left(array[1] + " ", 2);
		}

		public void DrawSelectArea()
		{
			int lngRet;
			RECT retRect;

			retRect.top = (gSelectArea.y1 - gDisp.y) * (- (int)gDisp.height) + frmMain.picMain.Height;
			retRect.left = (gSelectArea.x1 - gDisp.x) * (int)gDisp.width;
			retRect.right = gMouse.x;
			retRect.bottom = gMouse.y;

			using (Graphics gPicMain = frmMain.picMain.CreateGraphics())
			{
				using(Pen pen = new Pen(gPenColor[(int)PEN_NUM.EDIT_FRAME]))
				{
					gPicMain.DrawRectangle(pen, retRect.left, retRect.top, retRect.right - retRect.left, retRect.bottom - retRect.top);
				}
			}

			for (int i = 0; i < gObj.Length - 1; i++)
			{
				if (gObj[i].select == 4 || gObj[i].select == 5)
				{
					lngRet = gMeasure[gObj[i].measure].y + gObj[i].position;

					if (gDisp.startPos <= lngRet && gDisp.endPos >= lngRet)
					{
						DrawObjRect(i);
					}
				}
			}
		}

		public int ChangeMaxMeasure(int measure)
		{
			if (measure + 16 > gDisp.maxMeasure)
			{
				gDisp.maxMeasure = measure + 16;

				if (gDisp.maxMeasure > 999)
				{
					gDisp.maxMeasure = 999;
				}

				return 1;
			}

			return 0;
		}

		public void ChangeResolution()
		{
			int retInt;
			int retLng = 0;
			double retDouble;

			retInt = gDisp.resolution;

			for (int i = 0; i <= gDisp.maxMeasure; i++)
			{
				retLng = retLng + gMeasure[i].len;
			}

			retDouble = retLng / 32000;

			if (retDouble > 48)
			{
				gDisp.resolution = 96;
			}
			else if (retDouble > 24)
			{
				gDisp.resolution = 48;
			}
			else if (retDouble > 12)
			{
				gDisp.resolution = 24;
			}
			else if (retDouble > 6)
			{
				gDisp.resolution = 12;
			}
			else if (retDouble > 3)
			{
				gDisp.resolution = 6;
			}
			else if (retDouble > 1)
			{
				gDisp.resolution = 3;
			}
			else
			{
				gDisp.resolution = 1;
			}

			if (retInt == gDisp.resolution)
			{
				return;
			}

			// UNDONE: スクロールバー絡みの問題
			frmMain.vsbMain.Value = (frmMain.vsbMain.Maximum - ((frmMain.vsbMain.Maximum - frmMain.vsbMain.Value) / gDisp.resolution) * retInt);

			DataSource.DsListVScroll.Clear();
			retInt = 0;

			for (int i = 0; i < 6; i++)
			{
				retLng = (int)Math.Pow(2, i + 1) * 3;

				if (retLng >= gDisp.resolution)
				{
					DataSource.DsListVScroll.Insert(retInt, new Ds(retLng / gDisp.resolution, retLng.ToString()));

					retInt++;
				}
			}

			DataSource.Update(frmMain.cboVScroll);

			frmMain.cboVScroll.SelectedIndex = frmMain.cboVScroll.Items.Count - 2;
		}

		public void CopyObj(ref Obj destObj, ref Obj srcObj)
		{
			destObj.id = srcObj.id;
			destObj.ch = srcObj.ch;
			destObj.height = srcObj.height;
			destObj.measure = srcObj.measure;
			destObj.position = srcObj.position;
			destObj.select = srcObj.select;
			destObj.value = srcObj.value;
			destObj.att = srcObj.att;
		}

		public void RemoveObj(int num)
		{
			try
			{
				gObjID[gObj[num].id] = -1;
				gObj[num].id = 0;
				gObj[num].ch = 0;
				gObj[num].height = 0;
				gObj[num].measure = 0;
				gObj[num].position = 0;
				gObj[num].select = 0;
				gObj[num].value = 0;
				gObj[num].att = 0;
			}
			catch (Exception e)
			{
				CleanUp(e.Message, "RemoveObj");
			}
		}

		public void ArrangeObj()
		{
			int lngRet = 0;

			for (int i = 0; i < gObj.Length - 1; i++)
			{
				if (gObj[i].ch != 0)
				{
					//SwapObj(lngRet, i);

					if (i == gObj[gObj.Length - 1].height)
					{
						gObj[gObj.Length - 1].height = lngRet;
					}

					lngRet++;
				}
			}

			CopyObj(ref gObj[lngRet], ref gObj[gObj.Length - 1]);

			Array.Resize(ref gObj, lngRet + 1);
		}

		public void MoveSelectedObj()
		{
			int j;
			int lngRet = 0;

			try
			{
				for (int i = 0; i < gObj.Length - 1; i++)
				{
					if (gObj[i].select != 0)
					{
						lngRet++;
					}
				}

				if (lngRet == 0)
				{
					return;
				}

				j = gObj.Length - 1;

				Array.Resize(ref gObj, j + lngRet + 1);

				//TODO: SwapObj(gObj.Length - 1, j);

				lngRet = 0;

				for (int i = 0; i < j; i++)
				{
					if (gObj[i].select != 0)
					{
						//TODO: SwapObj(i, j + lngRet);

						if (i == gObj[gObj.Length - 1].height)
						{
							gObj[gObj.Length - 1].height = j + lngRet;
						}

						lngRet++;
					}
				}

				ArrangeObj();
			}
			catch (Exception e)
			{
				CleanUp(e.Message, "MoveSelectedObj");
			}
		}

		public void ObjSelectCancel()
		{
			for (int i = 0; i < gObj.Length - 1; i++)
			{
				gObj[i].select = 0;
			}
		}

		public void InitPen()
		{
			// ペン生成
			for (int i = 0; i < hPen.Length; i++)
			{
				hPen[i] = new Pen(gPenColor[i], 1.0f);
			}

			// ブラシ生成
			for (int i = 0; i < hBrush.Length - 1; i++)
			{
				hBrush[i] = new SolidBrush(gBrushColor[i]);
			}

			hBrush[hBrush.Length - 1] = new SolidBrush(Color.FromArgb(0, Color.Black));
		}

		public void DeletePen()
		{
			// ペン削除
			for (int i = 0; i < hPen.Length; i++)
			{
				hPen[i].Dispose();
			}

			// ブラシ削除
			for (int i = 0; i < hBrush.Length; i++)
			{
				hBrush[i].Dispose();
			}
		}
	}
}
