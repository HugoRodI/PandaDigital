using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaDigital
{
    public class PointFinder
    {

        public List<Point> autoGetPoints()
        {
            //int stepX = 2;
            //int stepY = 2;
            //int rango = penSizeTrackBar.Value;
            List<Point> userPoints = new List<Point>();
            //Color pixelColor, curveColor;
            //Bitmap b = new Bitmap(imgBox.Image);
            //float factorX = (float)imgBox.Width / b.Width;
            //float factorY = (float)imgBox.Height / b.Height;
            //curveColor = Color.FromName((curvesColorCmbBox.SelectedItem).ToString());

            //if (curves.Count > 0 && curves[0].Count > 0)
            //{
            //    foreach (List<Point> curve in curves)
            //    {
            //        foreach (Point p in curve)
            //        {
            //            for (int i = p.X - rango; i < p.X + rango; i += stepX)
            //            {
            //                for (int j = p.Y - rango; j < p.Y + rango; j += stepY)
            //                {
            //                    pixelColor = b.GetPixel(i * b.Width / imgBox.Width, j * b.Height / imgBox.Height);
            //                    Point p = new Point(i, j);

            //                    if (ColorsAreEqual(pixelColor, curveColor))
            //                        userPoints.Add(p);
            //                }
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < b.Width; i += stepX)
            //    {
            //        for (int j = 0; j < b.Height; j += stepY)
            //        {
            //            pixelColor = b.GetPixel(i, j);
            //            Point p = new Point(i * factorX, j * factorY);

            //            if (ColorsAreEqual(pixelColor, curveColor))
            //                userPoints.Add(p);

            //        }
            //    }
            //}

            return userPoints;
        }

        private bool ColorsAreEqual(Color c1, Color c2)
        {
            if (c1.Name.Equals(c2.Name))
                return true;

            return false;
        }
    }
}
