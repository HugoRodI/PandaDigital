using System;
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
        private ColorRecognizer colorRecognizer;
        private Sight sight;
        private List<Axe> axis = new List<Axe>();
        private int radiusDisplayed = 10;
        private UserPoints userPoints = new UserPoints(new Square(2, Color.Red, 4), new Square(4, Color.Red, 20));
        private UserPoints selectedPoints = new UserPoints(new Square(2, Color.Blue, 4), new Square(4, Color.Blue, 20));
        private UserPoints axisPoints = new UserPoints(new Cross(4, Color.Turquoise, 16, 16), new Cross(6, Color.Turquoise, 100, 100));
        private Point[] axisPointsArray = new Point[4];
        private List<Point> currentLine = new List<Point>();
        private List<List<Point>> curves = new List<List<Point>>();
        public List<Color> dominantColorsInImage = new List<Color>();
        private List<int> penSizes = new List<int>();
        private Point zoomedLocation;
        private int drawModePenSize = 30;
        private Color userPenColor = Color.Turquoise;

        public MainFrm()
        {
            InitializeComponent();
            ResizeRedraw = true;
            DoubleBuffered = true;
            horizontalStepBox.Text = "2";
            verticalStepBox.Text = "2";
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        /* Events */
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
                    ExportUserPoints();
            }
        }

        private void MainFrm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8 || e.KeyChar == 127)
                selectedPoints.RemoveAll();
            else if (e.KeyChar == 27)
                selectedPoints.RemoveAll();

            Invalidate(true);
            Update();
        }

        private void loadImageMainMenu_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Title = "Open image";
            fileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

            if (fileDialog.ShowDialog() == DialogResult.OK)
                imgBox.Image = new Bitmap(fileDialog.FileName);

            sight = new Sight(imgBox.Image, radiusDisplayed, imgBox.Size, zoomedImgBox.Size);
            userPoints.SetSight(sight);
            selectedPoints.SetSight(sight);
            axisPoints.SetSight(sight);
            ProcessImageColors();
        }

        private void imgBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void imgBox_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                imgBox.Image = null;
                string[] filename = (string[])e.Data.GetData(DataFormats.FileDrop);
                imgBox.Image = Image.FromFile(filename[0]);
                sight = new Sight(imgBox.Image, radiusDisplayed, imgBox.Size, zoomedImgBox.Size);
                userPoints.SetSight(sight);
                selectedPoints.SetSight(sight);
                axisPoints.SetSight(sight);


                ProcessImageColors();
            }
            catch (OutOfMemoryException ex)
            {
                MessageBox.Show(ex.Message + "\n\n\nCheck image file format.");
            }
        }
        
        private void imgBox_MouseMove(object sender, MouseEventArgs e)
        {
            int size = 0;

            if (imgBox.Image != null)
            {
                if (/*tabControl1.SelectedIndex == 1 && */drawModeCheckBox.Checked && (e.Button == System.Windows.Forms.MouseButtons.Left))
                    currentLine.Add(e.Location);
                else if ((e.X - size > imgBox.Location.X) && (e.X + size < imgBox.Location.X + imgBox.Size.Width) && (e.Y + size < imgBox.Location.Y + imgBox.Size.Height) && (e.Y - size > imgBox.Location.Y))
                {
                    sight.SetZoomedImage(zoomedLocation);
                    imgBox.Cursor = Cursors.Cross;
                    zoomedLocation = e.Location;
                }
                Invalidate(true);
                Update();
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
                        axisPointsArray[0] = e.Location;
                    else if (x2RadioButton.Checked)
                        axisPointsArray[1] = e.Location;
                    else if (y1RadioButton.Checked)
                        axisPointsArray[2] = e.Location;
                    else if (y2RadioButton.Checked)
                        axisPointsArray[3] = e.Location;

                    axisPoints.userPoints = axisPointsArray.ToList();
                }
                else if (e.Button == MouseButtons.Right)
                    SelectNearestPoint(e.Location);
            }

            Invalidate(true);
            Update();
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

        private void imgBox_Paint(object sender, PaintEventArgs e)
        {
            Draw(e);
        }

        private void zoomedImgBox_Paint(object sender, PaintEventArgs e)
        {
            DrawZoomedImage(zoomedLocation);
            DrawSight(e);
            DrawInZommedImage(e);
        }

        private void drawModePictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawModeLine(e);
        }

        private void deletePrevBtn_Click(object sender, EventArgs e)
        {
            DeletePrevUserPoint();
        }
        
        private void deleteAllBtn_Click(object sender, EventArgs e)
        {
            DeleteAllUserPoints();
        }
        
        private void exportBtn_Click(object sender, EventArgs e)
        {
            int numberOfAxisPoints = 4;

            if (axisPoints.GetSize() != numberOfAxisPoints)
                MessageBox.Show(numberOfAxisPoints - axisPoints.GetSize() + " axis points remaining");
            else if (imgBox.Image != null)
                ExportUserPoints();
        }

        private void getPointsBtn_Click(object sender, EventArgs e)
        {
            AutoGetPoints();
            Invalidate(true);
            Update();
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

        private void userRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            EnableTextBox(false);
            EnableRadioButton(false);
            CheckRadioButton(false);
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

        private void curvesColorCmbBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawRectangleColour(sender, e);
        }

        private void bgColorCmbBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawRectangleColour(sender, e);
        }

        private void penSizeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            drawModePenSize = penSizeTrackBar.Value;
            drawModePictureBox.Invalidate(true);
            drawModePictureBox.Update();
        }

        private void helpMainMenu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("See https://github.com/HugoRodI/PandaDigital/ for more info.");
        }
        /* End of events */

        /* Drawing */
        private void Draw(PaintEventArgs e)
        {
            DrawAxisPoints(e);
            DrawUserPoints(e);
            DrawSelectedPoints(e);
            DrawUserAutoLine(e);
        }

        private void DrawAxisPoints(PaintEventArgs e)
        {
            axisPoints.Draw(e);
        }

        private void DrawUserPoints(PaintEventArgs e)
        {
                userPoints.Draw(e);
        }

        private void DrawSelectedPoints(PaintEventArgs e)
        {
            selectedPoints.Draw(e);
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

        private void DrawZoomedImage(Point zoomedLocation)
        {
            if (imgBox.Image != null)
            {
                Image zoomedImage = sight.GetZoomedImage();
                zoomedImgBox.Image = zoomedImage;
                zoomedImgBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void DrawSight(PaintEventArgs e)
        {
            if (sight != null)
                sight.Draw(e);
        }

        private void DrawInZommedImage(PaintEventArgs e)
        {
            userPoints.DrawZoomed(e);
            selectedPoints.DrawZoomed(e);
            axisPoints.DrawZoomed(e);
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

        private void DrawRectangleColour(object sender, DrawItemEventArgs e)
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
        /* End of drawing*/

        /* Methods for events related with delete and export points */
        private void DeletePrevUserPoint()
        {
            userPoints.RemoveLast();

            Invalidate(true);
            Update();
        }

        private void DeleteAllUserPoints()
        {
            userPoints.RemoveAll();

            Invalidate(true);
            Update();
        }

        private void ExportUserPoints()
        {
            Axe xAxe = new Axe(Convert.ToInt32(x1TextBox.Text),
                                   Convert.ToInt32(x2TextBox.Text),
                                   axisPoints.GetUserPoints()[0].X,
                                   axisPoints.GetUserPoints()[1].X);

            Axe yAxe = new Axe(Convert.ToInt32(y1TextBox.Text),
                               Convert.ToInt32(y2TextBox.Text),
                               axisPoints.GetUserPoints()[2].Y,
                               axisPoints.GetUserPoints()[3].Y);

            axis.Add(xAxe);
            axis.Add(yAxe);

            Scaler scaler = new LinearScaler(axis);
            List<Point> scaledPoints = scaler.Scale(userPoints.GetUserPoints());
            PointExporter pointExporter = new PointExporter(scaledPoints);
        }        
        /* End of methods for events related with delete and export points */

        /* Methods for events related with axis UI */      
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
        /* End of methods for events related with axis UI */

        /* Methods for events related with image */
        private void ProcessImageColors()
        {
            colorRecognizer = new ColorRecognizer(imgBox.Image);
            colorRecognizer.RecognizeColors();
            FillCurveBox();
        }

        private void FillCurveBox()
        {
            ClearCurvesComboBox();
            
            foreach (Color dominantColor in colorRecognizer.getDominantColorsInImage())
                curvesColorCmbBox.Items.Add(dominantColor.Name);
        }

        private void ClearCurvesComboBox()
        {
            curvesColorCmbBox.Items.Clear();
        }
        
        private void AutoGetPoints()
        {
            List<Point> pointsAutomaticallyFound = new List<Point>();
            PointFinder pointFinder = new PointFinder(imgBox.Image, imgBox.Size, curvesColorCmbBox.SelectedItem, curves, penSizeTrackBar.Value, 
                                                      Convert.ToInt32(horizontalStepBox.Text), Convert.ToInt32(verticalStepBox.Text));

            pointsAutomaticallyFound = pointFinder.autoGetPoints();

            userPoints.AddRange(pointsAutomaticallyFound);
        }
        
        private void SelectPenColor()
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                userPenColor = colorDialog1.Color;
                selectPenColorBox.BackColor = userPenColor;
            }
        }
        
        private void ClearPen()
        {
            curves.Clear();
            Invalidate(true);
            Update();
        }

        private void SelectNearestPoint(Point point)
        {
            userPoints.FindNearestPoint(selectedPoints, point);
        }



        /* End of methods for events related with image */
    }
}













