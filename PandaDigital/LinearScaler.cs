using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaDigital
{
    public class LinearScaler : Scaler
    {
        public LinearScaler(List<Axe> axis) : base(axis)
        {

        }

        public override List<Point> Scale(List<Point> userPoints)
        {
            int xCoordinate, yCoordinate;
            Point realPoint;
            List<Point> realPoints = new List<Point>();

            foreach (Point userPoint in userPoints)
            {
                xCoordinate = axis[0].lowerRealBound + (axis[0].higherRealBound - axis[0].lowerRealBound) * ((userPoint.X - axis[0].lowerPixelBound) / (axis[0].higherPixelBound - axis[0].lowerPixelBound));
                yCoordinate = axis[1].lowerRealBound + (axis[1].higherRealBound - axis[1].lowerRealBound) * ((userPoint.Y - axis[1].lowerPixelBound) / (axis[1].higherPixelBound - axis[1].lowerPixelBound));

                realPoint = new Point(xCoordinate, yCoordinate);
                realPoints.Add(realPoint);
            }
            
            return realPoints;
        }
    }
}
