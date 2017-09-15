using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace PandaDigital
{
    public partial class MainFrm : Form
    {
        private Point oldPoint = Point.Empty;
        private List<Point> userPoints = new List<Point>();
        private List<AxisPoint> axisPoints = new List<AxisPoint>();
        private int axisCtr;
        private Point zoomedLocation;

        public MainFrm()
        {
            InitializeComponent();
            
            DoubleBuffered = true;

            axisCtr = 0;
        }
        
        /* Events */
        private void loadImageMainMenu_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Title = "Open image";
            fileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

            if (fileDialog.ShowDialog() == DialogResult.OK)
                imgBox.Image = new Bitmap(fileDialog.FileName);
        }

        private void imgBox_MouseMove(object sender, MouseEventArgs e)
        {
            int size = 30;

            if (imgBox.Image != null)
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
       
        private void imgBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (imgBox.Image != null)
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

                Invalidate(true);
                Update();
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
                        foreach (Point pixelPoint in userPoints)
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
        /* End of events */

        /* Drawing */
        private void DrawSight(PaintEventArgs e)
        {
            int size = 2;

            Rectangle sight = new Rectangle(zoomedImgBox.Size.Width / 2 - size, zoomedImgBox.Size.Height / 2 - size, 2 * size, 2 * size);
            e.Graphics.DrawRectangle(new Pen(Color.Red, size), sight);
        }

        private void Draw(PaintEventArgs e)
        {
            DrawAxis(e);
            DrawUserPoints(e);
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
                foreach (Point p in userPoints)
                    e.Graphics.DrawRectangle(new Pen(Color.Red, 2), new Rectangle(p.X - 2, p.Y - 2, 4, 4));
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
        private PointF PixelToReal(Point pixelPoint)
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
            int size = 30;
            Rectangle zoomedArea = new Rectangle(zoomedLocation.X - size, zoomedLocation.Y - size, 2 * size, 2 * size);
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
        /* End of methods for handling zoomed image */








    }
}
