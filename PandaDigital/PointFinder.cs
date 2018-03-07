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
        int range, horizontalStep, verticalStep;
        float factorX, factorY;
        Image userImage;
        Size userImageSize;
        Object selectedCurveColor;
        Bitmap b;
        Color pixelColor, curveColor;
        List<List<Point>> curves;

        public PointFinder(Image userImage, Size userImageSize, Object selectedCurveColor, List<List<Point>> curves, int range, int horizontalStep, int verticalStep)
        {
            this.userImage = userImage;
            this.userImageSize = userImageSize;
            this.selectedCurveColor = selectedCurveColor;
            this.curves = curves;
            this.range = range / 10;
            this.horizontalStep = horizontalStep;
            this.verticalStep = verticalStep;
            
            b = new Bitmap(userImage);
            curveColor = Color.FromName(selectedCurveColor.ToString());
            factorX = (float)userImageSize.Width / b.Width;
            factorY = (float)userImageSize.Height / b.Height;
        }

        public List<Point> autoGetPoints()
        {
            List<Point> userPoints;

            if (SearchRegionExists())
                userPoints = GetSearchRegionPoints();
            else
                userPoints = GetPointsByColor();

            return userPoints;
        }

        private bool SearchRegionExists()
        {
            if (curves.Count > 0 && curves[0].Count > 0)
                return true;

            return false;
        }

        private List<Point> GetPointsByColor()
        {
            Point p;
            List<Point> userPoints = new List<Point>();

            for (int i = 0; i < b.Width; i += horizontalStep)
            {
                for (int j = 0; j < b.Height; j += verticalStep)
                {
                    pixelColor = b.GetPixel(i, j);
                    p = new Point((int)(i * factorX), (int)(j * factorY));

                    if (ColorsAreEqual(pixelColor, curveColor))
                        userPoints.Add(p);
                }
            }

            return userPoints;
        }

        private List<Point> GetSearchRegionPoints()
        {
            Point newPoint;
            List<Point> userPoints = new List<Point>();

            foreach (List<Point> curve in curves)
            {
                foreach (Point p in curve)
                {
                    for (int i = p.X - range; i < p.X + range; i += horizontalStep)
                    {
                        for (int j = p.Y - range; j < p.Y + range; j += verticalStep)
                        {
                            pixelColor = b.GetPixel(i * b.Width / userImageSize.Width, j * b.Height / userImageSize.Height);
                            newPoint = new Point(i, j);

                            if (ColorsAreEqual(pixelColor, curveColor))
                                userPoints.Add(newPoint);
                        }
                    }
                }
            }

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
