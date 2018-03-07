using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PandaDigital
{
    public class PointExporter
    {
        List<Point> pointsToExport = new List<Point>();

        public PointExporter(List<Point> pointsToExport)
        {
            this.pointsToExport = pointsToExport;
        }

        public void export()
        {
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
                    foreach (Point pointToExport in pointsToExport)
                        sw.WriteLine("{0},{1}\n", pointToExport.X, pointToExport.Y);
                    sw.Close();
                }
                MessageBox.Show("File correctly saved");
            }
        }
        
    }
}
