using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PandaDigital
{
    public class UserPoints
    {
        public Sight sight;
        public Shape shape;
        public Shape zoomedShape;
        public List<Point> userPoints;

        public UserPoints() { }

        public UserPoints(Shape shape, Shape zoomedShape)
        {
            userPoints = new List<Point>();
            this.shape = shape;
            this.zoomedShape = zoomedShape;
        }

        public int GetSize()
        {
            return userPoints.Count;
        }

        public void SetSight(Sight sight)
        {
            this.sight = sight;
        }

        public bool IsEmpty()
        {
            if (userPoints.Count == 0)
                return true;

            return false;
        }

        public void Add(Point point)
        {
            userPoints.Add(point);
        }

        public void AddRange(List<Point> points)
        {
            userPoints.AddRange(points);
        }
        
        public List<Point> GetUserPoints()
        {
            return userPoints;
        }

        public void SetShape(Shape shape)
        {
            this.shape = shape;
        }

        public void Remove(Point p)
        {
            if (!IsEmpty())
                userPoints.Remove(p);
        }

        public void RemoveLast()
        {
            if (!IsEmpty())
                userPoints.RemoveAt(userPoints.Count - 1);
        }

        public void RemoveAll()
        {
            if (!IsEmpty())
                userPoints.RemoveRange(0, userPoints.Count);
        }

        public void FindNearestPoint(UserPoints userPoints, Point point)
        {
            int difx, dify;
            int min = int.MaxValue;
            int distanceBetweenPoints;
            Point nearestPoint = point;
           
            if (!IsEmpty())
            {
                foreach (Point userPoint in this.userPoints)
                {
                    difx = point.X - userPoint.X;
                    dify = point.Y - userPoint.Y;

                    distanceBetweenPoints = (int)Math.Sqrt(Math.Pow(difx, 2) + Math.Pow(dify, 2));

                    if (distanceBetweenPoints < min)
                    {
                        nearestPoint = new Point(difx, dify);
                        min = distanceBetweenPoints;
                    }
                }
            }

            if (!userPoints.IsEmpty())
            {
                foreach (Point userPoint in userPoints.GetUserPoints())
                {
                    difx = point.X - userPoint.X;
                    dify = point.Y - userPoint.Y;

                    distanceBetweenPoints = (int)Math.Sqrt(Math.Pow(difx, 2) + Math.Pow(dify, 2));

                    if (distanceBetweenPoints < min)
                    {
                        nearestPoint = new Point(difx, dify);
                        min = distanceBetweenPoints;
                    }
                }
            }
            
            if (this.userPoints.Any(pt => pt == nearestPoint))
            {
                userPoints.Remove(nearestPoint);
                userPoints.GetUserPoints().Add(nearestPoint);
            }
            else if (userPoints.GetUserPoints().Any(pt => pt == nearestPoint))
            {
                userPoints.GetUserPoints().Remove(nearestPoint);
                userPoints.Add(nearestPoint);
            }
            
        }

        public void Draw(PaintEventArgs e)
        {
            if (!IsEmpty())
                foreach (Point userPoint in userPoints)
                    if (userPoint != Point.Empty)
                        shape.drawShape(userPoint, e);
        }

        public void DrawZoomed(PaintEventArgs e)
        {
            Point zoomedUserPoint;

            if (!IsEmpty())
                foreach (Point userPoint in userPoints)
                {
                    if (IsInsideZoomedImage(userPoint))
                    {
                        zoomedUserPoint = TransformCoordinates(userPoint);
                        zoomedShape.drawShape(zoomedUserPoint, e);
                    }
                }
        }

        private bool IsInsideZoomedImage(Point p)
        {
            if (sight != null)
                if (Math.Abs(sight.zoomCenter.X - p.X) < sight.SightRadius && Math.Abs(sight.zoomCenter.Y - p.Y) < sight.SightRadius)
                    return true;
            return false;
        }

        public Point TransformCoordinates(Point point)
        {
            int x, y, zoomedPboxWidth, zoomedPboxHeight;
            float xDiff, yDiff, scale;

            xDiff = point.X - sight.zoomCenter.X;
            yDiff = point.Y - sight.zoomCenter.Y;

            zoomedPboxWidth = sight.zoomedImageBox.Width;
            zoomedPboxHeight = sight.zoomedImageBox.Height;

            scale = (float)zoomedPboxWidth / (2 * sight.SightRadius);

            x = (int)(zoomedPboxWidth / 2 + xDiff * scale);
            y = (int)(zoomedPboxHeight / 2 + yDiff * scale);

            return new Point(x, y);
        }
    }
}
