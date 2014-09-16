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

		public Color[] g_lngPenColor = new Color[(int)PEN_NUM.Max];
		public Color[] g_lngBrushColor = new Color[(int)BRUSH_NUM.Max];
		public Color[] g_lngSystemColor = new Color[(int)COLOR_NUM.Max];

		private Pen[] m_hPen = new Pen[76];
		private Brush[] m_hBrush = new Brush[38];

		private g_udtObj[] m_retObj;

		public const int OBJ_WIDTH = 28;
		public const int OBJ_HEIGHT = 9;

		public const int GRID_WIDTH = OBJ_WIDTH;
		public const int GRID_HALF_WIDTH = GRID_WIDTH / 2;
		public const int GRID_HALF_EDGE_WIDTH = (GRID_WIDTH * 3) / 4;
		public const int SPACE_WIDTH = 4;
		public const int FRAME_WIDTH = GRID_WIDTH / 2;
		public const int LEFT_SPACE = FRAME_WIDTH + SPACE_WIDTH;
		public const int RIGHT_SPACE = FRAME_WIDTH + SPACE_WIDTH * 2;

		public static double[] g_sngSin = new double[256 + 64];

		public void InitVerticalLine()
		{
			try
			{
				int retInt = 0;

				if (frmMain.cboDispFrame.SelectedIndex != 0)
				{
					for (int i = (int)GRID.NUM_1P_1KEY; i <= (int)GRID.NUM_1P_7KEY; i++)
					{
						g_VGrid[i].intWidth = GRID_WIDTH;
					}

					for (int i = (int)GRID.NUM_2P_1KEY; i <= (int)GRID.NUM_2P_7KEY; i++)
					{
						g_VGrid[i].intWidth = GRID_WIDTH;
					}
				}
				else
				{
					g_VGrid[(int)GRID.NUM_1P_1KEY].intWidth = GRID_HALF_EDGE_WIDTH;
					g_VGrid[(int)GRID.NUM_2P_1KEY].intWidth = GRID_HALF_EDGE_WIDTH;

					for (int i = (int)GRID.NUM_1P_2KEY; i <= (int)GRID.NUM_1P_6KEY; i++)
					{
						g_VGrid[i].intWidth = GRID_HALF_WIDTH;
					}

					for(int i = (int)GRID.NUM_2P_2KEY; i <= (int)GRID.NUM_2P_6KEY; i++)
					{
						g_VGrid[i].intWidth = GRID_HALF_WIDTH;
					}

					if (frmMain.cboDispKey.SelectedIndex != 0)
					{
						g_VGrid[(int)GRID.NUM_1P_7KEY].intWidth = GRID_HALF_EDGE_WIDTH;
						g_VGrid[(int)GRID.NUM_2P_7KEY].intWidth = GRID_HALF_EDGE_WIDTH;
					}
					else
					{
						g_VGrid[(int)GRID.NUM_1P_5KEY].intWidth = GRID_HALF_EDGE_WIDTH;
						g_VGrid[(int)GRID.NUM_2P_5KEY].intWidth = GRID_HALF_EDGE_WIDTH;
					}
				}

				switch (frmMain.cboPlayer.SelectedIndex)
				{
					case 0:
					case 1:
					case 2:	// 1P/2P/DP
						g_VGrid[(int)GRID.NUM_FOOTPEDAL].blnVisible = false;
						g_VGrid[(int)GRID.NUM_2P_SC_L].blnVisible = true;

						if (frmMain.cboDispKey.SelectedIndex == 0)
						{
							g_VGrid[(int)GRID.NUM_1P_6KEY].blnVisible = false;
							g_VGrid[(int)GRID.NUM_1P_7KEY].blnVisible = false;
						}
						else
						{
							g_VGrid[(int)GRID.NUM_1P_6KEY].blnVisible = true;
							g_VGrid[(int)GRID.NUM_1P_7KEY].blnVisible = true;
						}

						if (frmMain.cboDispKey.SelectedIndex == 0)
						{
							g_VGrid[(int)GRID.NUM_1P_SC_L].blnVisible = true;
							g_VGrid[(int)GRID.NUM_1P_SC_R].blnVisible = false;
						}
						else
						{
							g_VGrid[(int)GRID.NUM_1P_SC_L].blnVisible = false;
							g_VGrid[(int)GRID.NUM_1P_SC_R].blnVisible = true;
						}

						if (frmMain.cboPlayer.SelectedIndex != 0)
						{
							for (int i = (int)GRID.NUM_2P_SC_L; i <= (int)GRID.NUM_2P_SC_R + 1; i++)
							{
								g_VGrid[i].blnVisible = true;
							}

							if (frmMain.cboDispKey.SelectedIndex == 0)
							{
								g_VGrid[(int)GRID.NUM_2P_6KEY].blnVisible = false;
								g_VGrid[(int)GRID.NUM_2P_7KEY].blnVisible = false;
							}
							else
							{
								g_VGrid[(int)GRID.NUM_2P_6KEY].blnVisible = true;
								g_VGrid[(int)GRID.NUM_2P_7KEY].blnVisible = true;
							}

							if (frmMain.cboDispSC2P.SelectedIndex == 0)
							{
								g_VGrid[(int)GRID.NUM_2P_SC_L].blnVisible = true;
								g_VGrid[(int)GRID.NUM_2P_SC_R].blnVisible = false;
							}
							else
							{
								g_VGrid[(int)GRID.NUM_2P_SC_L].blnVisible = false;
								g_VGrid[(int)GRID.NUM_2P_SC_R].blnVisible = true;
							}
						}
						else
						{
							for (int i = (int)GRID.NUM_2P_SC_L; i <= (int)GRID.NUM_2P_SC_R + 1; i++)
							{
								g_VGrid[i].blnVisible = false;
							}
						}

						break;

					case 3:	// PMS
						g_VGrid[(int)GRID.NUM_FOOTPEDAL].blnVisible = false;
						g_VGrid[(int)GRID.NUM_1P_SC_L].blnVisible = false;
						g_VGrid[(int)GRID.NUM_1P_6KEY].blnVisible = false;
						g_VGrid[(int)GRID.NUM_1P_7KEY].blnVisible = false;
						g_VGrid[(int)GRID.NUM_1P_SC_R].blnVisible = false;
						g_VGrid[(int)GRID.NUM_2P_SC_L - 1].blnVisible = false;
						g_VGrid[(int)GRID.NUM_2P_SC_L].blnVisible = false;
						g_VGrid[(int)GRID.NUM_2P_1KEY].blnVisible = false;
						g_VGrid[(int)GRID.NUM_2P_SC_R + 1].blnVisible = true;

						for (int i = (int)GRID.NUM_2P_2KEY; i <= (int)GRID.NUM_2P_5KEY; i++)
						{
							g_VGrid[i].blnVisible = true;
						}

						for (int i = (int)GRID.NUM_2P_6KEY; i <= (int)GRID.NUM_2P_SC_R; i++)
						{
							g_VGrid[(int)GRID.NUM_1P_5KEY].intWidth = GRID_HALF_WIDTH;
							g_VGrid[(int)GRID.NUM_2P_5KEY].intWidth = GRID_HALF_EDGE_WIDTH;
						}

						break;

					case 4:	// Oct
						g_VGrid[(int)GRID.NUM_FOOTPEDAL].blnVisible = true;
						g_VGrid[(int)GRID.NUM_1P_SC_L].blnVisible = true;
						g_VGrid[(int)GRID.NUM_1P_6KEY].blnVisible = true;
						g_VGrid[(int)GRID.NUM_1P_7KEY].blnVisible = true;
						g_VGrid[(int)GRID.NUM_2P_SC_L - 1].blnVisible = false;
						g_VGrid[(int)GRID.NUM_2P_1KEY].blnVisible = false;
						g_VGrid[(int)GRID.NUM_2P_SC_R].blnVisible = true;
						g_VGrid[(int)GRID.NUM_2P_SC_R + 1].blnVisible = true;

						for (int i = (int)GRID.NUM_2P_2KEY; i <= (int)GRID.NUM_2P_7KEY; i++)
						{
							g_VGrid[i].blnVisible = true;
						}

						if (frmMain.cboDispFrame.SelectedIndex == 0)
						{
							g_VGrid[(int)GRID.NUM_1P_5KEY].intWidth = GRID_HALF_WIDTH;
							g_VGrid[(int)GRID.NUM_1P_7KEY].intWidth = GRID_HALF_WIDTH;
							g_VGrid[(int)GRID.NUM_2P_5KEY].intWidth = GRID_HALF_WIDTH;
							g_VGrid[(int)GRID.NUM_2P_7KEY].intWidth = GRID_HALF_EDGE_WIDTH;
						}

						break;

					default:
						break;
				}

				retInt = 0;

				for (int i = 0; i < 1000; i++)
				{
					g_Measure[i].lngY = retInt;
					retInt = retInt + g_Measure[i].intLen;
				}

				g_disp.lngMaxY = g_Measure[999].lngY + g_Measure[999].intLen;

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

				for(int i = 0; i <= g_disp.intMaxMeasure; i++)
				{
					retInt = retInt + g_Measure[i].intLen;
				}

				frmMain.vsbMain.Maximum = retInt / g_disp.intResolution;

				g_disp.width = DataSource.ItemData(frmMain.cboDispWidth, frmMain.cboDispWidth.SelectedIndex) / 100.0;
				g_disp.height = DataSource.ItemData(frmMain.cboDispHeight, frmMain.cboDispHeight.SelectedIndex) / 100.0;
				g_disp.intStartMeasure = 999;
				g_disp.intEndMeasure = 999;
				g_disp.lngStartPos = g_disp.y - OBJ_HEIGHT;
				g_disp.lngEndPos = (int)(g_disp.y + frmMain.picMain.Height / g_disp.height);

				retInt = FRAME_WIDTH;

				for (int i = 0; i < g_intVGridNum.Length; i++)
				{
					g_intVGridNum[i] = 0;
				}

				for (int i = 0; i < g_VGrid.Length; i++)
				{
					if (g_VGrid[i].blnVisible)
					{
						if (11 <= g_VGrid[i].intCh && g_VGrid[i].intCh <= 29)
						{
							g_intVGridNum[g_VGrid[i].intCh] = i;
							g_intVGridNum[g_VGrid[i].intCh + 20] = i;
							g_intVGridNum[g_VGrid[i].intCh + 40] = i;
						}
						else if (0 < g_VGrid[i].intCh)
						{
							g_intVGridNum[g_VGrid[i].intCh] = i;
						}

						g_VGrid[i].lngLeft = retInt;

						if (g_VGrid[i].intCh == 15)
						{
							if (frmMain.cboDispKey.SelectedIndex == 1 || frmMain.cboPlayer.SelectedIndex > 2)
							{
								g_VGrid[i].lngObjLeft = g_VGrid[i].lngLeft + (g_VGrid[i].intWidth - GRID_WIDTH) / 2;
							}
							else
							{
								g_VGrid[i].lngObjLeft = g_VGrid[i].lngLeft + g_VGrid[i].intWidth - GRID_WIDTH;
							}
						}
						else if (g_VGrid[i].intCh == 25)
						{
							if (frmMain.cboPlayer.SelectedIndex == 4)
							{
								g_VGrid[i].lngObjLeft = g_VGrid[i].lngLeft + (g_VGrid[i].intWidth - GRID_WIDTH) / 2;
							}
							else if (frmMain.cboDispKey.SelectedIndex == 0 || frmMain.cboPlayer.SelectedIndex == 3)
							{
								g_VGrid[i].lngObjLeft = g_VGrid[i].lngLeft + (g_VGrid[i].intWidth - GRID_WIDTH);
							}
							else
							{
								g_VGrid[i].lngObjLeft = g_VGrid[i].lngLeft + (g_VGrid[i].intWidth - GRID_WIDTH) / 2;
							}
						}
						else if (g_VGrid[i].intCh == 19)
						{
							if (frmMain.cboPlayer.SelectedIndex > 2)
							{
								g_VGrid[i].lngObjLeft = g_VGrid[i].lngLeft + (g_VGrid[i].intWidth - GRID_WIDTH) / 2;
							}
							else
							{
								g_VGrid[i].lngObjLeft = g_VGrid[i].lngLeft + g_VGrid[i].intWidth - GRID_WIDTH;
							}
						}
						else if (g_VGrid[i].intCh == 29)
						{
							g_VGrid[i].lngObjLeft = g_VGrid[i].lngLeft + g_VGrid[i].intWidth - GRID_WIDTH;
						}
						else if ((12 <= g_VGrid[i].intCh && g_VGrid[i].intCh <= 18)
							|| (22 <= g_VGrid[i].intCh && g_VGrid[i].intCh <= 28))
						{
							g_VGrid[i].lngObjLeft = g_VGrid[i].lngLeft + (g_VGrid[i].intWidth - GRID_WIDTH) / 2;
						}
						else
						{
							g_VGrid[i].lngObjLeft = retInt;
						}

						if (g_VGrid[i].lngLeft + g_VGrid[i].intWidth >= g_disp.x && frmMain.picMain.Width + (g_disp.x - g_VGrid[i].lngLeft) * g_disp.width >= 0)
						{
							g_VGrid[i].blnDraw = true;
						}
						else
						{
							g_VGrid[i].blnDraw = false;
						}

						retInt += g_VGrid[i].intWidth;
					}
					else
					{
						g_VGrid[i].blnDraw = false;
					}
				}

				g_disp.lngMaxX = retInt;

				retInt = 0;

				for (int i = 0; i <= 999; i++)
				{
					retInt += g_Measure[i].intLen;

					if (retInt > g_disp.y)
					{
						g_disp.intStartMeasure = i;
						break;
					}
				}

				for (int i = g_disp.intStartMeasure + 1; i <= 999; i++)
				{
					retInt += g_Measure[i].intLen;

					if ((retInt - g_disp.y) * g_disp.height >= frmMain.picMain.Height)
					{
						g_disp.intEndMeasure = i;
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

				m_retObj = new g_udtObj[1];

				for (int i = 0; i < g_Obj.Length - 1; i++)
				{
					if (g_Obj[i].intCh > 0 && g_Obj[i].intCh < 133)
					{
						if (g_VGrid[g_intVGridNum[g_Obj[i].intCh]].blnDraw)
						{
							if (g_disp.lngStartPos <= g_Measure[g_Obj[i].intMeasure].lngY + g_Obj[i].lngPosition
								&& g_disp.lngEndPos >= g_Measure[g_Obj[i].intMeasure].lngY + g_Obj[i].lngPosition)
							{
								DrawObj(ref g_Obj[i], e.Graphics);
							}
						}
					}
				}

				for (int i = 0; i < m_retObj.Length - 1; i++)
				{
					if(g_disp.lngStartPos <= g_Measure[m_retObj[i].intMeasure].lngY + m_retObj[i].lngPosition
						&& g_disp.lngEndPos >= g_Measure[m_retObj[i].intMeasure].lngY + m_retObj[i].lngPosition
						&& g_VGrid[g_intVGridNum[m_retObj[i].intCh]].blnDraw
						&& m_retObj[i].intCh != 0)
					{
						DrawObj(ref m_retObj[i], e.Graphics);
					}
				}

				Array.Clear(m_retObj, 0, m_retObj.Length);

				DeletePen();

				DrawGridInfo(e.Graphics);

				if ((g_disp.lngMaxX + 16) * g_disp.width - frmMain.picMain.Width < 0)
				{
					frmMain.hsbMain.Maximum = 0;
				}
				else
				{
					frmMain.hsbMain.Maximum = (g_disp.lngMaxX + FRAME_WIDTH) - (int)(frmMain.picMain.Width / g_disp.width);
				}

				if (g_SelectArea.blnFlag)
				{
					DrawSelectArea(e.Graphics);
				}

				if (g_disp.intEffect != 0)
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
				for (int i = 0; i < g_VGrid.Length; i++)
				{
					using (Brush brush = new SolidBrush(g_VGrid[i].lngBackColor))
					{
						if (g_VGrid[i].blnDraw)
						{
							if (g_VGrid[i].intCh != 0)
							{
								g.FillRectangle(brush,	// 座標修正済み
									(g_VGrid[i].lngLeft - g_disp.x) * (int)g_disp.width,
									0,
									((g_VGrid[i].lngLeft + g_VGrid[i].intWidth + 1 - g_disp.x) * (int)g_disp.width) - (g_VGrid[i].lngLeft - g_disp.x) * (int)g_disp.width,
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
				using (Brush brush = new SolidBrush(g_lngSystemColor[(int)COLOR_NUM.MEASURE_NUM]))
				{
					for (int i = g_disp.intStartMeasure; i <= g_disp.intEndMeasure; i++)
					{
						retStr = "#" + i.ToString("000");

						size = g.MeasureString(retStr, font);

						g.DrawString(retStr, font, brush, (frmMain.picMain.Width - size.Width) / 2.0f, frmMain.picMain.Height - size.Height - (g_Measure[i].lngY - g_disp.y) * (float)g_disp.height);
					}
				}
			}
		}

		private void DrawVerticalGrayLine(Graphics g)
		{
			using (Pen pen = new Pen(g_lngSystemColor[(int)COLOR_NUM.VERTICAL_SUB], 1.0f))
			{
				for (int i = 0; i < g_VGrid.Length; i++)
				{
					if (g_VGrid[i].blnDraw)
					{
						if (g_VGrid[i].intCh != 0)
						{
							//g.DrawLine(pen, g_VGrid[i].lngLeft + g_VGrid[i].intWidth, g_disp.y, g_VGrid[i].lngLeft + g_VGrid[i].intWidth, g_disp.y + frmMain.picMain.Height);
							//g.DrawLine(pen, g_VGrid[i].lngLeft + g_VGrid[i].intWidth, g_disp.y, g_VGrid[i].lngLeft + g_VGrid[i].intWidth, g_disp.y + frmMain.picMain.Height);
							PrintLine(g, pen, g_VGrid[i].lngLeft + g_VGrid[i].intWidth, g_disp.y, 0, frmMain.picMain.Height);
						}
					}
				}
			}
		}

		private void DrawHorizonalLine(Graphics g)
		{
			int retInt;

			using (Pen pen = new Pen(g_lngSystemColor[(int)COLOR_NUM.GRID_MAIN]))
			{
				for (int i = g_disp.intStartMeasure; i <= g_disp.intEndMeasure; i++)
				{
					int itemdata = DataSource.DsListDispGridMain[frmMain.cboDispGridMain.SelectedIndex].Value;
					if (itemdata != 0)
					{
						retInt = 192 / itemdata;

						for (int j = 0; j <= g_Measure[i].intLen; j += retInt)
						{
							//g.DrawLine(pen, LEFT_SPACE, frmMain.picMain.Height - (g_Measure[i].lngY + j), g_disp.lngMaxX - FRAME_WIDTH, frmMain.picMain.Height - (g_Measure[i].lngY + j));
							//g.DrawLine(pen, LEFT_SPACE, frmMain.picMain.Height - (g_Measure[i].lngY + j), g_disp.lngMaxX, frmMain.picMain.Height - (g_Measure[i].lngY + j));
							//g.DrawLine(pen, LEFT_SPACE, frmMain.picMain.Height - (g_Measure[i].lngY + j), LEFT_SPACE + (g_disp.lngMaxX - RIGHT_SPACE), frmMain.picMain.Height - (g_Measure[i].lngY + j));
							PrintLine(g, pen, LEFT_SPACE, g_Measure[i].lngY + j, g_disp.lngMaxX - RIGHT_SPACE, 0);
						}
					}
				}
			}

			using (Pen pen = new Pen(g_lngSystemColor[(int)COLOR_NUM.GRID_SUB], 1.0f))
			{
				for (int i = g_disp.intStartMeasure; i <= g_disp.intEndMeasure; i++)
				{
					int itemdata = DataSource.DsListDispGridSub[frmMain.cboDispGridSub.SelectedIndex].Value;
					if (itemdata != 0)
					{
						retInt = 192 / itemdata;

						for (int j = 0; j <= g_Measure[i].intLen; j += retInt)
						{
							//g.DrawLine(pen, FRAME_WIDTH, frmMain.picMain.Height - (g_Measure[i].lngY + j), g_disp.lngMaxX - RIGHT_SPACE, frmMain.picMain.Height - (g_Measure[i].lngY + j));
							//g.DrawLine(pen, FRAME_WIDTH, frmMain.picMain.Height - (g_Measure[i].lngY + j), g_disp.lngMaxX, frmMain.picMain.Height - (g_Measure[i].lngY + j));
							PrintLine(g, pen, FRAME_WIDTH, g_Measure[i].lngY + j, g_disp.lngMaxX - FRAME_WIDTH, 0);
						}
					}
				}
			}
		}

		private void DrawVerticalWhiteLine(Graphics g)
		{
			using (Pen pen = new Pen(g_lngSystemColor[(int)COLOR_NUM.VERTICAL_MAIN], 1.0f))
			{
				for (int i = 0; i < g_VGrid.Length; i++)
				{
					if (g_VGrid[i].blnDraw)
					{
						if (g_VGrid[i].intCh == 0)
						{
							//g.DrawLine(pen, g_VGrid[i].lngLeft, g_disp.y, g_VGrid[i].lngLeft, g_disp.y + frmMain.picMain.Height);
							PrintLine(g, pen, g_VGrid[i].lngLeft, g_disp.y, 0, frmMain.picMain.Height);
							//g.DrawLine(pen, g_VGrid[i].lngLeft + g_VGrid[i].intWidth, g_disp.y, g_VGrid[i].lngLeft + g_VGrid[i].intWidth, g_disp.y + frmMain.picMain.Height);
							PrintLine(g, pen, g_VGrid[i].lngLeft + g_VGrid[i].intWidth, g_disp.y, 0, frmMain.picMain.Height);
						}
					}
				}
			}
		}

		private void DrawMeasureLine(Graphics g)
		{
			using (Pen pen = new Pen(g_lngSystemColor[(int)COLOR_NUM.MEASURE_LINE], 1.0f))
			{
				for (int i = g_disp.intStartMeasure; i <= g_disp.intEndMeasure; i++)
				{
					//g.DrawLine(pen, FRAME_WIDTH, frmMain.picMain.Height - g_Measure[i].lngY, g_disp.lngMaxX - FRAME_WIDTH, frmMain.picMain.Height - g_Measure[i].lngY);
					//g.DrawLine(pen, FRAME_WIDTH, frmMain.picMain.Height - g_Measure[i].lngY, g_disp.lngMaxX, frmMain.picMain.Height - g_Measure[i].lngY);
					PrintLine(g, pen, FRAME_WIDTH, g_Measure[i].lngY, g_disp.lngMaxX - FRAME_WIDTH, 0);
				}

				if (g_disp.intEndMeasure == 999)
				{
					//g.DrawLine(pen, FRAME_WIDTH, frmMain.picMain.Height - g_Measure[999].lngY + g_Measure[999].intLen, g_disp.lngMaxX - FRAME_WIDTH, frmMain.picMain.Height - g_Measure[999].lngY);
					//g.DrawLine(pen, FRAME_WIDTH, frmMain.picMain.Height - g_Measure[999].lngY + g_Measure[999].intLen, g_disp.lngMaxX, frmMain.picMain.Height - g_Measure[999].lngY);
					PrintLine(g, pen, FRAME_WIDTH, g_Measure[999].lngY + g_Measure[999].intLen, g_disp.lngMaxX - FRAME_WIDTH, 0);
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
				using (Brush brush = new SolidBrush(g_lngSystemColor[(int)COLOR_NUM.INFO]))
				{
					for (int i = 0; i < g_VGrid.Length; i++)
					{
						if (g_VGrid[i].blnDraw)
						{
							if (g_VGrid[i].intCh != 0)
							{
								if (frmMain.mnuOptionsVertical.Checked)
								{
									lngRet = (int)((g_VGrid[i].lngLeft + (g_VGrid[i].intWidth / 2) - g_disp.x) * g_disp.width);

									for (int j = 0; j < g_VGrid[i].strText.Length; j++)
									{
										strRet = StringUtil.Mid(g_VGrid[i].strText, j + 1, 1);
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
									intRet = g_VGrid[i].strText.Length;
									retSize = g.MeasureString(g_VGrid[i].strText, font);

									int x = (int)((g_VGrid[i].lngLeft + g_VGrid[i].intWidth / 2 - g_disp.x) * g_disp.width - (int)(retSize.Width / 2.0f) + 1);

									g.DrawString(g_VGrid[i].strText, font, Brushes.Black, x, 0);
									g.DrawString(g_VGrid[i].strText, font, Brushes.Black, x - 1, 1);
									g.DrawString(g_VGrid[i].strText, font, Brushes.Black, x + 1, 1);
									g.DrawString(g_VGrid[i].strText, font, Brushes.Black, x, 2);
									g.DrawString(g_VGrid[i].strText, font, brush, x, 1);
								}
							}
						}
					}
				}
			}
		}

		// 
		private void PrintLine(Graphics e, Pen pen, int x, int y, int w, int h)
		{
			w = w * (int)g_disp.width;

			if (x - g_disp.x < 0)
			{
				if (w != 0)
				{
					w += (x - g_disp.x) * (int)g_disp.width;
				}

				x = 0;
			}
			else
			{
				x = (x - g_disp.x) * (int)g_disp.width;
			}

			if (y + g_disp.y < 0)
			{
				if (h != 0)
				{
					h += y - g_disp.y;
				}

				y = 0;
			}
			else
			{
				y = (y - g_disp.y) * (int)g_disp.height;
			}

			// やっぱり使います
			e.DrawLine(pen, x, frmMain.picMain.Height - 1 - y, x + w, frmMain.picMain.Height - 1 - y - h);
		}

		//private static Font ObjFont = new Font(System.Windows.Forms.Control.DefaultFont.Name, 8.0f, FontStyle.Regular);
		public void DrawObj(ref g_udtObj retObj, Graphics g)
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
				if (g_intVGridNum[retObj.intCh] == 0)
				{
					return;
				}

				x = (g_VGrid[g_intVGridNum[retObj.intCh]].lngObjLeft - g_disp.x) * (int)g_disp.width + 1;
				y = frmMain.picMain.Height + OBJ_DIFF - (g_Measure[retObj.intMeasure].lngY + retObj.lngPosition - g_disp.y) * (int)g_disp.height;
				width = GRID_WIDTH * (int)g_disp.width - 1;

				switch (retObj.intCh)
				{
					case 3:
					case 8:
					case 9:
						text = retObj.sngValue.ToString();
						break;

					case 4:
					case 6:
					case 7:
						text = g_strBMP[retObj.sngValue];
						if (frmMain.mnuOptionsObjectFileName.Checked && text.Length > 0)
						{
							array = text.Split('.');
							text = StringUtil.Left(text, text.Length - (array[array.Length - 1].Length + 1));
						}
						else
						{
							text = strNumConv(retObj.sngValue);
						}
						break;

					default:
						text = g_strWAV[retObj.sngValue];

						if (frmMain.mnuOptionsObjectFileName.Checked && text.Length > 0)
						{
							array = text.Split('.');
							text = StringUtil.Left(text, text.Length - (array[array.Length - 1].Length + 1));
						}
						else
						{
							text = strNumConv(retObj.sngValue);
						}

						if (retObj.intAtt == 2 || (retObj.intCh >= 51 && retObj.intCh < 69))
						{
							x += 3;
							width -= 6;
						}

						if (retObj.intAtt == 2 && retObj.intCh >= 11 && retObj.intCh <= 29)
						{
							CopyObj(ref m_retObj[m_retObj.Length - 1], ref retObj);
							m_retObj[m_retObj.Length - 1].intCh = retObj.intCh + 40;

							Array.Resize(ref m_retObj, m_retObj.Length + 1);
						}

						break;
				}

				switch (retObj.intSelect)
				{
					case 0:
					case 4:
					case 5:
					case 6:
						if (retObj.intCh < 10 || retObj.intCh > 100)
						{
							lightNum = g_VGrid[g_intVGridNum[retObj.intCh]].intLightNum;
							shadowNum = g_VGrid[g_intVGridNum[retObj.intCh]].intShadowNum;
							brushNum = g_VGrid[g_intVGridNum[retObj.intCh]].intBrushNum;
						}
						else if (retObj.intCh > 50)
						{
							lightNum = (int)PEN_NUM.LONGNOTE_LIGHT;
							shadowNum = (int)PEN_NUM.LONGNOTE_SHADOW;
							brushNum = (int)BRUSH_NUM.LONGNOTE;
						}
						else
						{
							if (retObj.intAtt == 0)
							{
								lightNum = g_VGrid[g_intVGridNum[retObj.intCh]].intLightNum;
								shadowNum = g_VGrid[g_intVGridNum[retObj.intCh]].intShadowNum;
								brushNum = g_VGrid[g_intVGridNum[retObj.intCh]].intBrushNum;
							}
							else
							{
								intRet = retObj.intCh % 10;

								if (11 <= retObj.intCh && retObj.intCh <= 15)
								{
									lightNum = (int)PEN_NUM.INV_KEY01_LIGHT + intRet - 1;
									shadowNum = (int)PEN_NUM.INV_KEY01_SHADOW + intRet - 1;
									brushNum = (int)BRUSH_NUM.INV_KEY01 + intRet - 1;
								}
								else if (retObj.intCh == 18)
								{
									lightNum = (int)PEN_NUM.KEY06_LIGHT;
									shadowNum = (int)PEN_NUM.INV_KEY06_SHADOW;
									brushNum = (int)BRUSH_NUM.INV_KEY06;
								}
								else if (retObj.intCh == 19)
								{
									lightNum = (int)PEN_NUM.KEY07_LIGHT;
									shadowNum = (int)PEN_NUM.INV_KEY07_SHADOW;
									brushNum = (int)BRUSH_NUM.INV_KEY07;
								}
								else if (retObj.intCh == 16)
								{
									lightNum = (int)PEN_NUM.KEY08_LIGHT;
									shadowNum = (int)PEN_NUM.INV_KEY08_SHADOW;
									brushNum = (int)BRUSH_NUM.INV_KEY08;
								}
								else if (21 <= retObj.intCh && retObj.intCh <= 25)
								{
									lightNum = (int)PEN_NUM.INV_KEY11_LIGHT + intRet - 1;
									shadowNum = (int)PEN_NUM.INV_KEY11_SHADOW + intRet - 1;
									brushNum = (int)BRUSH_NUM.INV_KEY11 + intRet - 1;
								}
								else if (retObj.intCh == 28)
								{
									lightNum = (int)PEN_NUM.KEY16_LIGHT;
									shadowNum = (int)PEN_NUM.INV_KEY16_SHADOW;
									brushNum = (int)BRUSH_NUM.INV_KEY16;
								}
								else if (retObj.intCh == 29)
								{
									lightNum = (int)PEN_NUM.KEY17_LIGHT;
									shadowNum = (int)PEN_NUM.INV_KEY17_SHADOW;
									brushNum = (int)BRUSH_NUM.INV_KEY17;
								}
								else if (retObj.intCh == 26)
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
						if (retObj.intSelect == 2)
						{
							lightNum = (int)PEN_NUM.EDIT_FRAME;
						}
						else
						{
							lightNum = (int)PEN_NUM.DELETE_FRAME;
						}

						brushNum = m_hBrush.Length - 1;

						g.FillRectangle(m_hBrush[brushNum], x - 1, y - OBJ_HEIGHT - 1, width + 2, OBJ_HEIGHT + 3);	// 座標修正済み
						g.DrawRectangle(m_hPen[lightNum], x - 1, y - OBJ_HEIGHT - 1, width + 2, OBJ_HEIGHT + 3);		// 座標修正済み

						return;
				}

				g.FillRectangle(m_hBrush[brushNum], x, y - OBJ_HEIGHT, width, OBJ_HEIGHT + 1);	// 座標修正済み
				g.DrawRectangle(m_hPen[lightNum], x, y - OBJ_HEIGHT, width, OBJ_HEIGHT + 1);		// 座標修正済み

				g.DrawLine(m_hPen[shadowNum], x, y, x + width - 1, y);
				g.DrawLine(m_hPen[shadowNum], x + width - 1, y, x + width - 1, y - OBJ_HEIGHT);

				intRet = text.Length;

				FontFamily fontFamily = frmMain.picMain.Font.FontFamily;
				using (Font font = new Font(fontFamily, 8.0f))
				{
					retSize = g.MeasureString(text, font);

					y -= (OBJ_HEIGHT + (int)retSize.Height) / 2 + 1;

					if (retObj.intSelect == 1)
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
				if (g_intVGridNum[g_Obj[num].intCh] == 0)
				{
					return;
				}

				x = (g_VGrid[g_intVGridNum[g_Obj[num].intCh]].lngObjLeft - g_disp.x) * (int)g_disp.width + 1;
				y = frmMain.picMain.Height + OBJ_DIFF - (g_Measure[g_Obj[num].intMeasure].lngY + g_Obj[num].lngPosition - g_disp.y) * (int)g_disp.height;

				width = GRID_WIDTH * (int)g_disp.width - 1;

				if(g_Obj[num].intAtt == 2 || (g_Obj[num].intCh >= 51 && g_Obj[num].intCh <= 69))
				{
					x += 3;
					width -= 6;
				}

				using(Graphics gPicMain = frmMain.picMain.CreateGraphics())
				{
					using(Pen pen = new Pen(g_lngPenColor[(int)PEN_NUM.EDIT_FRAME]))
					{
						gPicMain.DrawRectangle(pen, x - 1, y - OBJ_HEIGHT - 1, width + 2, OBJ_HEIGHT + 3 );		// 座標修正済み
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
			g_udtObj retObj = new g_udtObj();

			try
			{
				if (g_blnIgnoreInput)
				{
					return;
				}

				SetObjData(ref retObj, x, y);

				if (frmMain.tlbMenuWrite.Checked)
				{
					if (retObj.intCh >= 11 && retObj.intCh <= 29)
					{
						if (EnvUtil.Control)
						{
							retObj.intAtt = 1;
						}
						else if (EnvUtil.Shift)
						{
							retObj.intCh += 40;
							retObj.intAtt = 2;
						}
					}

					if (DataSource.DsListDispGridMain[frmMain.cboDispGridMain.SelectedIndex].Value != 0)
					{
						lngRet = 192 / DataSource.DsListDispGridMain[frmMain.cboDispGridMain.SelectedIndex].Value;
						retObj.lngPosition = (retObj.lngPosition / lngRet) * lngRet;
					}
				}

				if (!frmMain.tlbMenuWrite.Checked)
				{
					lngRet = g_Measure[retObj.intMeasure].lngY + retObj.lngPosition;

					for (int i = g_Obj.Length - 1; i >= 0; i--)
					{
						if (g_Obj[i].intCh == retObj.intCh || (retObj.intAtt == 2
							&& g_Obj[i].intCh + 40 == retObj.intCh))
						{
							if (g_Measure[g_Obj[i].intMeasure].lngY + g_Obj[i].lngPosition + OBJ_HEIGHT / g_disp.height >= lngRet
								&& g_Measure[g_Obj[i].intMeasure].lngY + g_Obj[i].lngPosition <= lngRet)
							{
								if (frmMain.tlbMenuEdit.Checked)
								{
									retObj.intSelect = 2;
								}
								else if (frmMain.tlbMenuDelete.Checked)
								{
									retObj.intSelect = 3;
								}

								retObj.intAtt = g_Obj[i].intAtt;

								if (retObj.intAtt == 2)
								{
									retObj.intCh += 40;
								}

								retObj.sngValue = g_Obj[i].sngValue;
								retObj.lngPosition = g_Obj[i].lngPosition;
								retObj.intMeasure = g_Obj[i].intMeasure;
								retObj.lngHeight = i;
							}
						}
					}
				}

				DrawStatusBar(ref retObj);

				if (frmMain.tlbMenuWrite.Checked)
				{
					if (retObj.intCh != g_Obj[g_Obj.Length - 1].intCh
						|| retObj.intAtt != g_Obj[g_Obj.Length - 1].intAtt
						|| retObj.intMeasure != g_Obj[g_Obj.Length - 1].intMeasure
						|| retObj.lngPosition != g_Obj[g_Obj.Length - 1].lngPosition
						|| retObj.sngValue != g_Obj[g_Obj.Length - 1].sngValue)
					{
						CopyObj(ref g_Obj[g_Obj.Length - 1], ref retObj);
						g_lngObjID[g_Obj[g_Obj.Length - 1].lngID] = g_Obj.Length - 1;
					}
					else
					{
						g_Obj[g_Obj.Length - 1].lngHeight = retObj.lngHeight;
						return;
					}
				}
				else
				{
					if (retObj.intSelect != 2 && retObj.intSelect != 3)
					{
						retObj.intCh = 0;
						g_Obj[g_Obj.Length - 1].intCh = 0;
					}

					if (retObj.lngHeight != g_Obj[g_Obj.Length - 1].lngHeight)
					{
						if (g_Obj[retObj.lngHeight].intCh != 0)
						{
							retObj.lngPosition = g_Obj[retObj.lngHeight].lngPosition;
						}

						CopyObj(ref g_Obj[g_Obj.Length - 1], ref retObj);
						g_lngObjID[g_Obj[g_Obj.Length - 1].lngID] = g_Obj.Length - 1;
					}
					else
					{
						return;
					}
				}

				frmMain.picMain.Refresh();

				if (g_Obj[g_Obj.Length - 1].intCh != 0)
				{
					InitPen();

					using(Graphics gPicmain = frmMain.picMain.CreateGraphics())
					{
						DrawObj(ref g_Obj[g_Obj.Length - 1], gPicmain);
					}

					DeletePen();
				}

				if (g_disp.intEffect != 0)
				{
					//TODO: DrawEffect();
				}
			}
			catch (Exception e)
			{
				CleanUp(e.Message, "DrawObjMax");
			}
		}

		public void SetObjData(ref g_udtObj retObj, int x, int y)
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

			lngRet = x / (int)g_disp.width + g_disp.x;

			retObj.intCh = 8;

			for (int i = 0; i < g_VGrid.Length; i++)
			{
				if (g_VGrid[i].blnDraw && g_VGrid[i].intCh != 0)
				{
					if (g_VGrid[i].lngLeft <= lngRet)
					{
						retObj.intCh = g_VGrid[i].intCh;
					}
					else
					{
						break;
					}
				}
			}

			retObj.lngID = g_lngIDNum;
			retObj.lngHeight = g_Obj.Length - 1;

			if (y < 1)
			{
				y = 1;
			}
			else if (y > frmMain.picMain.Height + OBJ_DIFF)
			{
				y = frmMain.picMain.Height + OBJ_DIFF;
			}

			lngRet = (frmMain.picMain.Height - y + OBJ_DIFF) / (int)g_disp.height + g_disp.y;

			for (int i = 0; i < 1000; i++)
			{
				if (g_Measure[i].lngY <= lngRet)
				{
					retObj.intMeasure = i;
					retObj.lngPosition = lngRet - g_Measure[i].lngY;

					if (retObj.lngPosition > g_Measure[i].intLen)
					{
						retObj.lngPosition = g_Measure[i].intLen - 1;
					}
				}
				else
				{
					break;
				}
			}

			switch (retObj.intCh)
			{
				case 3:
				case 8:
				case 9:
					retObj.sngValue = 0;
					break;

				case 4:
				case 6:
				case 7:
					retObj.sngValue = frmMain.lngFromLong(frmMain.lstBMP.SelectedIndex + 1);
					break;

				default:
					retObj.sngValue = frmMain.lngFromLong(frmMain.lstWAV.SelectedIndex + 1);
					break;
			}
		}

		public void DrawStatusBar(ref g_udtObj retObj)
		{
			string strRet;
			int lngRet;
			string[] array;

			strRet = "Position:  " + retObj.intMeasure + g_strStatusBar[22] + "  ";

			int _cboData = DataSource.DsListDispGridMain[frmMain.cboDispGridMain.SelectedIndex].Value;
			lngRet = _cboData;

			if (lngRet != 0)
			{
				if (retObj.intSelect > 1 && retObj.lngPosition != 0)
				{
					lngRet = GCD(retObj.lngPosition, g_Measure[retObj.intMeasure].intLen);

					if (lngRet > 192 / _cboData)
					{
						lngRet = _cboData;
					}
					else
					{
						lngRet = 192 / lngRet;
					}
				}

				strRet = strRet + retObj.lngPosition * lngRet / 192 + "/" + g_Measure[retObj.intMeasure].intLen * lngRet / 192;
			}
			else
			{
				strRet = strRet + retObj.lngPosition + "/" + g_Measure[retObj.intMeasure].intLen;
			}

			strRet = strRet + "  ";

			if (retObj.intCh > 100)
			{
				strRet = strRet + g_strStatusBar[0] + " " + (retObj.intCh - 100).ToString("00");
			}
			else if (retObj.intCh < 10)
			{
				strRet = strRet + g_strStatusBar[retObj.intCh - 1];
			}
			else if (11 <= retObj.intCh && retObj.intCh <= 15)
			{
				strRet = strRet + g_strStatusBar[10] + (retObj.intCh - 10).ToString();
			}
			else if (retObj.intCh == 16)
			{
				strRet = strRet + g_strStatusBar[12];
			}
			else if (retObj.intCh == 18 || retObj.intCh == 19)
			{
				strRet = strRet + g_strStatusBar[10] + (retObj.intCh - 12).ToString();
			}
			else if (21 <= retObj.intCh && retObj.intCh <= 25)
			{
				strRet = strRet + g_strStatusBar[11].Length + (retObj.intCh - 20).ToString();
			}
			else if (retObj.intCh == 26)
			{
				strRet = strRet + g_strStatusBar[13];
			}
			else if (retObj.intCh == 28 || retObj.intCh == 29)
			{
				strRet = strRet + g_strStatusBar[11] + (retObj.intCh - 22).ToString();
			}
			else if (51 <= retObj.intCh && retObj.intCh <= 55)
			{
				strRet = strRet + g_strStatusBar[10] + (retObj.intCh - 50).ToString();
			}
			else if (retObj.intCh == 56)
			{
				strRet = strRet + g_strStatusBar[12];
			}
			else if (retObj.intCh == 58 || retObj.intCh == 59)
			{
				strRet = strRet + g_strStatusBar[10] + (retObj.intCh - 52).ToString();
			}
			else if (61 <= retObj.intCh && retObj.intCh <= 65)
			{
				strRet = strRet + g_strStatusBar[11] + (retObj.intCh - 60).ToString();
			}
			else if(retObj.intCh == 66)
			{
				strRet = strRet + g_strStatusBar[13];
			}
			else if(retObj.intCh == 68 || retObj.intCh == 69)
			{
				strRet = strRet + g_strStatusBar[11] + (retObj.intCh - 62).ToString();
			}

			if (retObj.intCh >= 11 && retObj.intCh <= 29)
			{
				if (retObj.intAtt == 1)
				{
					strRet = strRet + " " + g_strStatusBar[14];
				}
				else if (retObj.intAtt == 2)
				{
					strRet = strRet + " " + g_strStatusBar[15];
				}
			}
			else if (retObj.intCh >= 51 && retObj.intCh <= 69)
			{
				strRet = strRet + " " + g_strStatusBar[15];
			}

			frmMain.staMainPosition.Text = strRet;

			array = StringUtil.Mid(frmMain.lstMeasureLen.Items[retObj.intMeasure].ToString(), 6).Split('/');

			frmMain.staMainMeasure.Text = StringUtil.Right(" " + array[0], 2) + "/" + StringUtil.Left(array[1] + " ", 2);
		}

		public void DrawSelectArea(Graphics g)
		{
			int lngRet;
			RECT retRect;

			//frmMain.picMain.Refresh();

			retRect.top = (g_SelectArea.y1 - g_disp.y) * (- (int)g_disp.height) + frmMain.picMain.Height;
			retRect.left = (g_SelectArea.x1 - g_disp.x) * (int)g_disp.width;
			retRect.right = g_Mouse.x;
			retRect.bottom = g_Mouse.y;

			using (Graphics gPicMain = frmMain.picMain.CreateGraphics())
			{
				using(Pen pen = new Pen(g_lngPenColor[(int)PEN_NUM.EDIT_FRAME]))
				{
					//gPicMain.DrawRectangle(pen, retRect.left, retRect.top, retRect.right - retRect.left, retRect.bottom - retRect.top);		// 座標修正済み
					if (retRect.left < retRect.right)
					{
						if (retRect.top < retRect.bottom)
						{
							g.DrawRectangle(pen, retRect.left, retRect.top, retRect.right - retRect.left, retRect.bottom - retRect.top);
						}
						else
						{
							g.DrawRectangle(pen, retRect.left, retRect.bottom, retRect.right - retRect.left, retRect.top - retRect.bottom);
						}
					}
					else
					{
						if (retRect.top < retRect.bottom)
						{
							g.DrawRectangle(pen, retRect.right, retRect.top, retRect.left - retRect.right, retRect.bottom - retRect.top);
						}
						else
						{
							g.DrawRectangle(pen, retRect.right, retRect.bottom, retRect.left - retRect.right, retRect.top - retRect.bottom);
						}
					}
				}
			}

			for (int i = 0; i < g_Obj.Length - 1; i++)
			{
				if (g_Obj[i].intSelect == 4 || g_Obj[i].intSelect == 5)
				{
					lngRet = g_Measure[g_Obj[i].intMeasure].lngY + g_Obj[i].lngPosition;

					if (g_disp.lngStartPos <= lngRet && g_disp.lngEndPos >= lngRet)
					{
						DrawObjRect(i);
					}
				}
			}
		}

		public int ChangeMaxMeasure(int measure)
		{
			if (measure + 16 > g_disp.intMaxMeasure)
			{
				g_disp.intMaxMeasure = measure + 16;

				if (g_disp.intMaxMeasure > 999)
				{
					g_disp.intMaxMeasure = 999;
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

			retInt = g_disp.intResolution;

			for (int i = 0; i <= g_disp.intMaxMeasure; i++)
			{
				retLng = retLng + g_Measure[i].intLen;
			}

			retDouble = retLng / 32000;

			if (retDouble > 48)
			{
				g_disp.intResolution = 96;
			}
			else if (retDouble > 24)
			{
				g_disp.intResolution = 48;
			}
			else if (retDouble > 12)
			{
				g_disp.intResolution = 24;
			}
			else if (retDouble > 6)
			{
				g_disp.intResolution = 12;
			}
			else if (retDouble > 3)
			{
				g_disp.intResolution = 6;
			}
			else if (retDouble > 1)
			{
				g_disp.intResolution = 3;
			}
			else
			{
				g_disp.intResolution = 1;
			}

			if (retInt == g_disp.intResolution)
			{
				return;
			}

			// UNDONE: スクロールバー絡みの問題
			//frmMain.vsbMain.Value = (frmMain.vsbMain.Maximum - ((frmMain.vsbMain.Maximum - frmMain.vsbMain.Value) / g_disp.intResolution) * retInt);
			frmMain.vsbMain.Value = (frmMain.vsbMain.Value / g_disp.intResolution) * retInt;

			DataSource.DsListVScroll.Clear();
			retInt = 0;

			for (int i = 0; i < 6; i++)
			{
				retLng = (int)Math.Pow(2, i + 1) * 3;

				if (retLng >= g_disp.intResolution)
				{
					DataSource.DsListVScroll.Insert(retInt, new Ds(retLng / g_disp.intResolution, retLng.ToString()));

					retInt++;
				}
			}

			DataSource.Update(frmMain.cboVScroll);

			frmMain.cboVScroll.SelectedIndex = frmMain.cboVScroll.Items.Count - 2;
		}

		public void CopyObj(ref g_udtObj destObj, ref g_udtObj srcObj)
		{
			destObj.lngID = srcObj.lngID;
			destObj.intCh = srcObj.intCh;
			destObj.lngHeight = srcObj.lngHeight;
			destObj.intMeasure = srcObj.intMeasure;
			destObj.lngPosition = srcObj.lngPosition;
			destObj.intSelect = srcObj.intSelect;
			destObj.sngValue = srcObj.sngValue;
			destObj.intAtt = srcObj.intAtt;
		}

		public void RemoveObj(int num)
		{
			try
			{
				g_lngObjID[g_Obj[num].lngID] = -1;
				g_Obj[num].lngID = 0;
				g_Obj[num].intCh = 0;
				g_Obj[num].lngHeight = 0;
				g_Obj[num].intMeasure = 0;
				g_Obj[num].lngPosition = 0;
				g_Obj[num].intSelect = 0;
				g_Obj[num].sngValue = 0;
				g_Obj[num].intAtt = 0;
			}
			catch (Exception e)
			{
				CleanUp(e.Message, "RemoveObj");
			}
		}

		public void ArrangeObj()
		{
			int lngRet = 0;

			for (int i = 0; i < g_Obj.Length - 1; i++)
			{
				if (g_Obj[i].intCh != 0)
				{
					SwapObj(lngRet, i);

					if (i == g_Obj[g_Obj.Length - 1].lngHeight)
					{
						g_Obj[g_Obj.Length - 1].lngHeight = lngRet;
					}

					lngRet++;
				}
			}

			CopyObj(ref g_Obj[lngRet], ref g_Obj[g_Obj.Length - 1]);

			Array.Resize(ref g_Obj, lngRet + 1);
		}

		public void MoveSelectedObj()
		{
			int j;
			int lngRet = 0;

			try
			{
				for (int i = 0; i < g_Obj.Length - 1; i++)
				{
					if (g_Obj[i].intSelect != 0)
					{
						lngRet++;
					}
				}

				if (lngRet == 0)
				{
					return;
				}

				j = g_Obj.Length - 1;

				Array.Resize(ref g_Obj, j + lngRet + 1);

				SwapObj(g_Obj.Length - 1, j);

				lngRet = 0;

				for (int i = 0; i < j; i++)
				{
					if (g_Obj[i].intSelect != 0)
					{
						SwapObj(i, j + lngRet);

						if (i == g_Obj[g_Obj.Length - 1].lngHeight)
						{
							g_Obj[g_Obj.Length - 1].lngHeight = j + lngRet;
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
			for (int i = 0; i < g_Obj.Length - 1; i++)
			{
				g_Obj[i].intSelect = 0;
			}
		}

		public void InitPen()
		{
			// ペン生成
			for (int i = 0; i < m_hPen.Length; i++)
			{
				m_hPen[i] = new Pen(g_lngPenColor[i], 1.0f);
			}

			// ブラシ生成
			for (int i = 0; i < m_hBrush.Length - 1; i++)
			{
				m_hBrush[i] = new SolidBrush(g_lngBrushColor[i]);
			}

			m_hBrush[m_hBrush.Length - 1] = new SolidBrush(Color.FromArgb(0, Color.Black));
		}

		public void DeletePen()
		{
			// ペン削除
			for (int i = 0; i < m_hPen.Length; i++)
			{
				m_hPen[i].Dispose();
			}

			// ブラシ削除
			for (int i = 0; i < m_hBrush.Length; i++)
			{
				m_hBrush[i].Dispose();
			}
		}
	}
}
