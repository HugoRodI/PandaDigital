using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaDigital
{
    public class LogarithmicScaler : Scaler
    {
        public LogarithmicScaler(List<Axe> axis) : base(axis)
        {

        }

        public override List<Point> Scale(List<Point> userPoints)
        {
            int xCoordinate, yCoordinate;
            Point realPoint;
            List<Point> realPoints = new List<Point>();

            foreach (Point userPoint in userPoints)
            {
                xCoordinate = (int)(Math.Log(axis[0].lowerRealBound) + (Math.Log(axis[0].higherRealBound) - Math.Log(axis[0].lowerRealBound)) * ((userPoint.X - axis[0].lowerPixelBound) / (axis[0].higherPixelBound - axis[0].lowerPixelBound)));
                yCoordinate = (int)(Math.Log(axis[1].lowerRealBound) + (Math.Log(axis[1].higherRealBound) - Math.Log(axis[1].lowerRealBound)) * ((userPoint.Y - axis[1].lowerPixelBound) / (axis[1].higherPixelBound - axis[1].lowerPixelBound)));

                realPoint = new Point(xCoordinate, yCoordinate);
                realPoints.Add(realPoint);
            }

            return realPoints;
        }
    }
}
