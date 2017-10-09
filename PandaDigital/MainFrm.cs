﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace PandaDigital
{
    public partial class MainFrm : Form
    {
        private PandaDigital pandaDigital;

        private List<PointF> userPoints = new List<PointF>();
        private List<AxisPoint> axisPoints = new List<AxisPoint>();
        private List<PointF> selectedPoints = new List<PointF>();
        private List<Point> currentLine = new List<Point>();
        private List<List<Point>> curves = new List<List<Point>>();
        
        private List<int> penSizes = new List<int>();
        private Point zoomedLocation;
        private int drawModePenSize = 30;
       
        private Color userPenColor = Color.Turquoise;
        private List<Color> colorList = new List<Color>(); 

        public MainFrm()
        {
            InitializeComponent();
            ResizeRedraw = true;
            DoubleBuffered = true;
           
        }

        /* Events */
        private void loadImageMainMenu_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Title = "Open image";
            fileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                pandaDigital = new PandaDigital(fileDialog.FileName);
                imgBox.Image = pandaDigital.PandaImage;
                FillBgBox();
                FillCurveBox();

            }
        }

        private void imgBox_MouseMove(object sender, MouseEventArgs e)
        {
            int size = 0;

            if (imgBox.Image != null)
            {
                if (tabControl1.SelectedIndex == 1 )
                {
                    if (drawModeCheckBox.Checked)
                    {
                        if (e.Button == System.Windows.Forms.MouseButtons.Left)
                        {
                            currentLine.Add(e.Location);
                            Invalidate(true);
                            Update();
                        }
                    }
                }
                else
                {
                    if ((e.X - size > imgBox.Location.X) && (e.X + size < imgBox.Location.X + imgBox.Size.Width) && (e.Y + size < imgBox.Location.Y + imgBox.Size.Height) && (e.Y - size > imgBox.Location.Y))
                    {
                        imgBox.Cursor = Cursors.Cross;
                        zoomedLocation = e.Location;
                        Invalidate(true);
                        Update();
                    }
                }
            }
        }

        private void imgBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (imgBox.Image != null & (tabControl1.SelectedIndex == 0))
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (userRadioBtn.Checked)
                        userPoints.Add(e.Location);
                    else if (x1RadioButton.Checked)
                        InitAxis(e.Location, 1);
                    else if (x2RadioButton.Checked)
                        InitAxis(e.Location, 2);
                    else if (y1RadioButton.Checked)
                        InitAxis(e.Location, 3);
                    else if (y2RadioButton.Checked)
                        InitAxis(e.Location, 4);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    SelectNearestPoint(e.Location);
                }
            }

            Invalidate(true);
            Update();
        }

        private void RemoveFromList(List<PointF> list, PointF p1)
        {
            foreach (PointF p2 in list.Reverse<PointF>())
            {
                if (p1 == p2)
                    list.Remove(p1);
            }
        }

        private void deletePrevBtn_Click(object sender, EventArgs e)
        {
            DeletePrevUserPoint();
        }
        
        private void deleteAllBtn_Click(object sender, EventArgs e)
        {
            DeleteAllUserPoints();
        }

        private void axisRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            EnableDefaultTextBox();
            EnableRadioButton(true);
            CheckDefaultRadioButton();
        }

        private void x1RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (axisRadioBtn.Checked)
            {
                EnableTextBox(false);
                x1TextBox.Enabled = true;
            }
        }

        private void x2RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (axisRadioBtn.Checked)
            {
                EnableTextBox(false);
                x2TextBox.Enabled = true;
            }
        }

        private void y1RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (axisRadioBtn.Checked)
            {
                EnableTextBox(false);
                y1TextBox.Enabled = true;
            }
        }

        private void y2RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (axisRadioBtn.Checked)
            {
                EnableTextBox(false);
                y2TextBox.Enabled = true;
            }
        }

        private void userRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            EnableTextBox(false);
            EnableRadioButton(false);
            CheckRadioButton(false);
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            if (imgBox.Image != null)
            {
                PointF realPoint;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.InitialDirectory = @"C:\";
                saveFileDialog1.Title = "Save text Files";
                saveFileDialog1.CheckPathExists = true;
                saveFileDialog1.DefaultExt = "pd";
                saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog1.FileName;
                    string extension = ".txt";
                    string path = fileName + extension;

                    using (StreamWriter sw = File.AppendText(path))
                    {
                        foreach (PointF pixelPoint in userPoints)
                        {
                            realPoint = PixelToReal(pixelPoint);
                            sw.WriteLine("{0},\t{1}\n", realPoint.X, realPoint.Y);
                        }
                        sw.Close();
                    }
                    MessageBox.Show("File correctly saved");
                }
            }
        }

        private void imgBox_Paint(object sender, PaintEventArgs e)
        {
            Draw(e);
        }

        private void zoomedImgBox_Paint(object sender, PaintEventArgs e)
        {
            ShowZoomedImage(zoomedLocation);
            DrawSight(e);
        }

        private void imgBox_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                imgBox.Image = null;
                string[] filename = (string[])e.Data.GetData(DataFormats.FileDrop);
                imgBox.Image = Image.FromFile(filename[0]);
                pandaDigital = new PandaDigital(filename[0]);
                FillBgBox();
                FillCurveBox();
            }
            catch (OutOfMemoryException ex)
            {
                MessageBox.Show(ex.Message + "\n\n\nCheck image file format.");
            }
        }

        private void imgBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            imgBox.AllowDrop = true;
            KeyPreview = true;
        }

        private void MainFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.D)
                    DeletePrevUserPoint();
                else if (e.KeyCode == Keys.A)
                    DeleteAllUserPoints();
                else if (e.KeyCode == Keys.S)
                {
                    Thread th = new Thread(new ThreadStart(ShowMessage));
                    th.Start();
                    th.Join();
                    //ExportUserPoints();
                }
                
            }
        }

        private void MainFrm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8 || e.KeyChar == 127)
            {
                selectedPoints.Clear();

            }
            else if (e.KeyChar == 27)
            {
                foreach (PointF p in selectedPoints)
                    userPoints.Add(p);
                
                selectedPoints.Clear();

            }
        }

        private void drawModePictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawModeLine(e);
        }

        private void penSizeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            drawModePenSize = penSizeTrackBar.Value;
            drawModePictureBox.Invalidate(true);
            drawModePictureBox.Update();
        }

        private void imgBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (imgBox.Image != null)
            {
                if (tabControl1.SelectedIndex == 1)
                {
                    if (drawModeCheckBox.Checked)
                    {
                        if (currentLine.Count > 1)
                        {
                            penSizes.Add(drawModePenSize);
                            curves.Add(currentLine.ToList());
                        }
                        currentLine.Clear();
                        Invalidate(true);
                        Update();
                    }
                }
            }
        }

        private void curvesColorCmbBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;

            if (e.Index >= 0)
            {
                string n = "0x" + ((ComboBox)sender).Items[e.Index].ToString();
                int i = Convert.ToInt32(n, 16);
                Color c = Color.FromArgb(i);
                Brush b = new SolidBrush(c);

                g.FillRectangle(b, rect.X + 2, rect.Y + 2, rect.Width - 2, rect.Height - 2);
            }
        }

        private void getPointsBtn_Click(object sender, EventArgs e)
        {
            AutoGetPoints();
            Invalidate(true);
            Update();
        }

        private void bgColorCmbBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;

            if (e.Index >= 0)
            {
                string n = "0x" + ((ComboBox)sender).Items[e.Index].ToString();
                int i = Convert.ToInt32(n, 16);
                Color c = Color.FromArgb(i);
                Brush b = new SolidBrush(c);

                g.FillRectangle(b, rect.X + 2, rect.Y + 2, rect.Width - 2, rect.Height - 2);
            }
        }

        private void clearPenBtn_Click(object sender, EventArgs e)
        {
            ClearPen();
        }

        private void selectPenColorBox_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(SelectPenColor));
            th.Start();
            th.Join();
            Invalidate(true);
            Update();


        }
        /* End of events */

        /* Drawing */
        private void DrawSight(PaintEventArgs e)
        {
            int sizeHorExt = 170;
            int sizeVertExt = 145;
            int radio = 1;

            int zoomedPboxWidth, zoomedPboxHeight;

            zoomedPboxWidth = zoomedImgBox.Size.Width;
            zoomedPboxHeight = zoomedImgBox.Size.Height;

            base.OnPaint(e);
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, zoomedImgBox.Width, zoomedImgBox.Height);
                zoomedImgBox.Region = new Region(gp);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            }
            
            e.Graphics.DrawLine(new Pen(Color.Red, 1), zoomedPboxWidth/2, zoomedPboxHeight/2 - sizeVertExt, zoomedPboxWidth/2, zoomedPboxHeight/2 + sizeVertExt);
            e.Graphics.DrawLine(new Pen(Color.Red, 1), zoomedPboxWidth / 2 - sizeHorExt, zoomedPboxHeight / 2, zoomedPboxWidth / 2 + sizeHorExt, zoomedPboxHeight / 2);
            e.Graphics.DrawEllipse(new Pen(Color.Red, 5), new Rectangle(zoomedPboxWidth/2 - radio, zoomedPboxHeight/2 - radio, 2*radio, 2*radio));
        }

        private void Draw(PaintEventArgs e)
        {
            DrawAxis(e);
            
            DrawUserAutoLine(e);
            DrawAutoPoints(e);
            DrawSelectedPoints(e);
            DrawUserPoints(e);
        }

        private void DrawSelectedPoints(PaintEventArgs e)
        {
            if (selectedPoints.Count != 0)
                foreach (PointF p in selectedPoints)
                    e.Graphics.DrawRectangle(new Pen(Color.Blue, 2), new Rectangle((int)p.X - 2, (int)p.Y - 2, 4, 4));
        }

        private void DrawAutoPoints(PaintEventArgs e)
        {
            if (autoPoints.Count != 0)
                foreach (PointF p in autoPoints)
                    e.Graphics.DrawRectangle(new Pen(Color.Red, 2), new Rectangle((int)p.X - 2, (int)p.Y - 2, 4, 4));
        }

        private void DrawUserAutoLine(PaintEventArgs e)
        {
            if (imgBox.Image != null)
            {
                if (curves.Count > 0)
                {
                    Pen pen = new Pen(userPenColor, drawModePenSize);

                    if (currentLine.Count > 1)
                    {
                        e.Graphics.DrawCurve(pen, currentLine.ToArray());
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    }
                    int cont = 0;
                    foreach (List<Point> curve in curves)
                    {
                        pen = new Pen(userPenColor, penSizes[cont]);
                        e.Graphics.DrawCurve(pen, curve.ToArray());
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        cont++;
                    }
                }
            }
        }

        private void DrawAxis(PaintEventArgs e)
        {
            int size = 8;
            if (axisPoints.Count != 0)
                foreach (AxisPoint axisPoint in axisPoints)
                {
                    e.Graphics.DrawLine(new Pen(axisPoint.color, 3), axisPoint.location.X - size, axisPoint.location.Y, axisPoint.location.X + size, axisPoint.location.Y);
                    e.Graphics.DrawLine(new Pen(axisPoint.color, 3), axisPoint.location.X, axisPoint.location.Y - size, axisPoint.location.X, axisPoint.location.Y + size);
                }
        }

        private void DrawUserPoints(PaintEventArgs e)
        {
            if (userPoints.Count != 0)
                foreach (PointF p in userPoints)
                    e.Graphics.DrawRectangle(new Pen(Color.Red, 2), new Rectangle((int)p.X - 2, (int)p.Y - 2, 4, 4));
        }
        /* End of drawing*/

        /* Methods for delete and export buttons events */
        private void DeletePrevUserPoint()
        {
            if (userPoints.Count != 0)
                userPoints.RemoveAt(userPoints.Count - 1);

            Invalidate(true);
            Update();
        }

        private void DeleteAllUserPoints()
        {
            if (userPoints.Count != 0)
                userPoints.RemoveRange(0, userPoints.Count);

            Invalidate(true);
            Update();
        }

        private void ExportUserPoints()
        {
            //MessageBox.Show("TODO: this");
        }

        private void ShowMessage()
        {
            MessageBox.Show("TODO: this");
        }
        /* End of methods for delete and export buttons events */

        /* Methods for handling axis UI */
        private void EnableDefaultRadioButton()
        {
            x1RadioButton.Enabled = true;
        }

        private void CheckDefaultRadioButton()
        {
            x1RadioButton.Checked = true;
        }

        private void EnableDefaultTextBox()
        {
            x1TextBox.Enabled = true;
        }

        private void CheckRadioButton(bool state)
        {
            x1RadioButton.Checked = state;
            x2RadioButton.Checked = state;
            y1RadioButton.Checked = state;
            y2RadioButton.Checked = state;
        }

        private void EnableRadioButton(bool state)
        {
            x1RadioButton.Enabled = state;
            x2RadioButton.Enabled = state;
            y1RadioButton.Enabled = state;
            y2RadioButton.Enabled = state;
        }

        private void EnableTextBox(bool state)
        {
            x1TextBox.Enabled = state;
            x2TextBox.Enabled = state;
            y1TextBox.Enabled = state;
            y2TextBox.Enabled = state;
        }
        /* End of methods for handling axis UI */

        /* Methods for getting real size points */
        private PointF PixelToReal(PointF pixelPoint)
        {
            float x, y;
            double x1, x2, y1, y2;
            float x1Axis, x2Axis, y1Axis, y2Axis;
            PointF realPoint = new Point();

            x1 = Convert.ToDouble(x1TextBox.Text);
            x2 = Convert.ToDouble(x2TextBox.Text);
            y1 = Convert.ToDouble(y1TextBox.Text);
            y2 = Convert.ToDouble(y2TextBox.Text);

            x1Axis = GetAxisPointByColor(Color.Blue).X;
            x2Axis = GetAxisPointByColor(Color.Red).X;
            y1Axis = GetAxisPointByColor(Color.Yellow).Y;
            y2Axis = GetAxisPointByColor(Color.Green).Y;

            if (linearRadioButtonX.Checked)
                x = (float)x1 + ((float)x2 - (float)x1) * ((pixelPoint.X - x1Axis) / (x2Axis - x1Axis));
            else
            {
                x = (float)Math.Log(x1) + ((float)Math.Log(x2) - (float)Math.Log(x1)) * ((pixelPoint.X - x1Axis) / (x2Axis - x1Axis));
                x = (float)Math.Pow(Math.E, Convert.ToDouble(x));
            }

            if (linearRadioButtonY.Checked)
                y = (float)y1 + ((float)y2 - (float)y1) * ((pixelPoint.Y - y1Axis) / (y2Axis - y1Axis));
            else
            {
                y = (float)Math.Log(y1) + ((float)Math.Log(y2) - (float)Math.Log(y1)) * ((pixelPoint.Y - y1Axis) / (y2Axis - y1Axis));
                y = (float)Math.Pow(Math.E, Convert.ToDouble(x));
            }

            realPoint.X = x;
            realPoint.Y = y;

            return realPoint;
        }

        private Point GetAxisPointByColor(Color color)
        {
            if (axisPoints.Count != 0)
                foreach (AxisPoint ap in axisPoints)
                    if (ap.color == color)
                        return ap.location;

            return new Point();
        }

        private void InitAxis(Point axisPointLocation, int axisCtr)
        {
            AxisPoint ap;

            if (axisCtr == 1) ap = new AxisPoint(axisPointLocation, Color.Blue);
            else if (axisCtr == 2) ap = new AxisPoint(axisPointLocation, Color.Red);
            else if (axisCtr == 3) ap = new AxisPoint(axisPointLocation, Color.Yellow);
            else if (axisCtr == 4) ap = new AxisPoint(axisPointLocation, Color.Green);
            else ap = null;


            if (axisPoints.Count != 0)
            {
                foreach (AxisPoint item in axisPoints)
                {
                    if (ap.color == item.color)
                    {
                        axisPoints.Remove(item);
                        break;
                    }
                }
            }

            axisPoints.Add(ap);
        }
        /* End of methods for getting real size points */

        /* Methods for handling zoomed image */
        private void ShowZoomedImage(Point zoomedLocation)
        {
            int radiusDisplayed = 10;
            Rectangle zoomedArea = new Rectangle(zoomedLocation.X - radiusDisplayed, zoomedLocation.Y - radiusDisplayed, 2 * radiusDisplayed, 2 * radiusDisplayed);
            if (imgBox.Image != null)
            {
                Bitmap myBitmap = new Bitmap(imgBox.Image, new Size(imgBox.Size.Width, imgBox.Size.Height));
                Bitmap CroppedImage = CropImage(myBitmap, zoomedArea);

                zoomedImgBox.Image = CroppedImage;
                zoomedImgBox.SizeMode = PictureBoxSizeMode.StretchImage;
                
            }
        }

        private Bitmap CropImage(Bitmap source, Rectangle section)
        {
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
            g.Dispose();

            return bmp;
        }

        private void DrawModeLine(PaintEventArgs e)
        {
            float x1, y1, x2, y2, dmPboxWidth, dmPboxHeight, margin;
            
            dmPboxWidth = drawModePictureBox.Size.Width;
            dmPboxHeight = drawModePictureBox.Size.Height;
            margin = 5;
            
            x1 = margin;
            y1 = dmPboxHeight / 2;
            x2 = dmPboxWidth - margin;
            y2 = dmPboxHeight / 2;
            
            e.Graphics.DrawLine(new Pen(userPenColor, drawModePenSize), x1, y1, x2, y2);

        }
        
        private void EmptyColorList()
        {
            colorList.Clear();
        }

        private void ClearCurvesComboBox()
        {
            curvesColorCmbBox.Items.Clear();
        }

        private void ClearBgComboBox()
        {
            bgColorCmbBox.Items.Clear();
        }
        
        
        
        private bool ColorsAreEqual(Color c1, Color c2)
        {
            if(c1.Name.Equals(c2.Name))//if ((c1.A == c2.A) && (c1.R == c2.R) && (c1.G == c2.G) && (c1.B == c2.B))
                return true;

            return false;
        }

        private void AutoGetPoints()
        {
            int stepX = 5;
            int stepY = 2;
            int rango = penSizeTrackBar.Value;
            Color pixelColor, curveColor;
            Graphics g = CreateGraphics();
            Bitmap b = new Bitmap(imgBox.Image);
            float factorX = (float)imgBox.Width / b.Width;
            float factorY = (float)imgBox.Height / b.Height;
            curveColor = Color.FromName((curvesColorCmbBox.SelectedItem).ToString());

            if (curves.Count > 0 && curves[0].Count > 0)
            {
                foreach (List<Point> curve in curves)
                {
                    foreach (Point p in curve)
                    {
                        for (int i = p.X - rango; i < p.X + rango; i += 1)
                        {
                            for (int j = p.Y - rango; j < p.Y + rango; j++)
                            {
                                pixelColor = b.GetPixel(i * b.Width / imgBox.Width, j * b.Height / imgBox.Height);
                                PointF pf = new PointF(i, j);

                                if (ColorsAreEqual(pixelColor, curveColor))
                                    userPoints.Add(pf);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < b.Width; i += stepX)
                {
                    for (int j = 0; j < b.Height; j += stepY)
                    {
                        pixelColor = b.GetPixel(i, j);
                        PointF p = new PointF(i * factorX, j * factorY);

                        if (ColorsAreEqual(pixelColor, curveColor))
                            userPoints.Add(p);

                    }
                }
            }

            
        }

      
        private void FillCurveBox()
        {
            ClearCurvesComboBox();
            
            foreach (PandaColor pandaColor in pandaDigital.PandaColors)
                curvesColorCmbBox.Items.Add(pandaColor.Color.Name);
        }

        private void FillBgBox()
        {
            ClearBgComboBox();

            foreach (PandaColor pandaColor in pandaDigital.PandaColors)
                bgColorCmbBox.Items.Add(pandaColor.Color.Name);
        }

        private void ClearPen()
        {
            curves.Clear();
            Invalidate(true);
            Update();
        }

        private void SelectPenColor()
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                userPenColor = colorDialog1.Color;
                selectPenColorBox.BackColor = userPenColor;
                
            }
        }

        private void SelectNearestPoint(PointF p)
        {
            float x, y;
            float difx, dify;
            double min = 1e12;
            double distanceBetweenPoints;

            x = p.X;
            y = p.Y;

            if (userPoints.Count != 0)
            {
                foreach (PointF up in userPoints)
                {
                    difx = x - up.X;
                    dify = y - up.Y;

                    distanceBetweenPoints = Math.Sqrt(Math.Pow(difx, 2) + Math.Pow(dify, 2));
                    if (distanceBetweenPoints < min)
                    {
                        p = up;
                        min = distanceBetweenPoints;
                    }
                }
            }

            if (selectedPoints.Count != 0)
            {
                foreach (PointF sp in selectedPoints)
                {
                    difx = x - sp.X;
                    dify = y - sp.Y;

                    distanceBetweenPoints = Math.Sqrt(Math.Pow(difx, 2) + Math.Pow(dify, 2));
                    if (distanceBetweenPoints < min)
                    {
                        p = sp;
                        min = distanceBetweenPoints;
                    }
                }
            }

            if (userPoints.Any(pt => pt == p))
            {
                RemoveFromList(userPoints, p);
                selectedPoints.Add(p);
            }
            else if (selectedPoints.Any(pt => pt == p))
            {
                RemoveFromList(selectedPoints, p);
                userPoints.Add(p);
            }
        }

        /* End of methods for handling zoomed image */
    }
}












//private void FindColors()
//{
//    Color pixelColor;
//    Bitmap b = new Bitmap(imgBox.Image);

//    for (int i = 0; i < b.Width; i++)
//    {
//        for (int j = 0; j < b.Height; j++)
//        {
//            pixelColor = b.GetPixel(i, j);

//            if (!imgColors.Any(c => c.Color == pixelColor))
//                imgColors.Add(new PandaColor(pixelColor));
//            else
//                imgColors.Where(c => c.Color == pixelColor).ToList()[0].Ocurrencies++;
//        }
//    }

//    pandaColors = imgColors.OrderByDescending(o => o.Ocurrencies).ToList().GetRange(0, 6);
//}
