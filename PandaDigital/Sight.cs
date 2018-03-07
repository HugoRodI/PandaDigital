using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PandaDigital
{
    public class Sight
    {
        public Size UserImageBox { get; set; }
        public Image UserImageToBeZoomed { get; set; }
        public Size zoomedImageBox { get; set; }
        private Image zoomedImage;
        public int SightRadius { get; set; }
        private int ellipseRadius = 20;
        public Point zoomCenter;


        public Sight(Image userImageToBeZoomed, int sightRadius, Size userImageBox, Size zoomedImageBox)
        {
            UserImageToBeZoomed = userImageToBeZoomed;
            SightRadius = sightRadius;
            UserImageBox = userImageBox;
            this.zoomedImageBox = zoomedImageBox;
        }

        public void SetZoomedImage(Point zoomCenter)
        {
            
            Rectangle zoomedArea = new Rectangle(zoomCenter.X - SightRadius, zoomCenter.Y - SightRadius, 2 * SightRadius, 2 * SightRadius);

            Bitmap userImageToBeZoomed = new Bitmap(UserImageToBeZoomed, new Size(UserImageBox.Width, UserImageBox.Height));
            Bitmap zoomedImage = CropImage(userImageToBeZoomed, zoomedArea);

            this.zoomedImage = zoomedImage;
            this.zoomCenter = zoomCenter;
        }

        public Image GetZoomedImage()
        {
            return zoomedImage;
        }

        private Bitmap CropImage(Bitmap userImageToBeZoomed, Rectangle zoomedArea)
        {
            Bitmap zoomedImage = new Bitmap(zoomedArea.Width, zoomedArea.Height);
            Graphics g = Graphics.FromImage(zoomedImage);

            g.DrawImage(userImageToBeZoomed, 0, 0, zoomedArea, GraphicsUnit.Pixel);
            g.Dispose();

            return zoomedImage;
        }

        public void Draw(PaintEventArgs e)
        {
            int zoomedPboxWidth, zoomedPboxHeight;
            
            zoomedPboxWidth = zoomedImageBox.Width;
            zoomedPboxHeight = zoomedImageBox.Height;

            e.Graphics.DrawLine(new Pen(Color.Red, 1), zoomedPboxWidth / 2, 0, zoomedPboxWidth / 2, zoomedPboxHeight);
            e.Graphics.DrawLine(new Pen(Color.Red, 1), 0, zoomedPboxHeight / 2, zoomedPboxWidth, zoomedPboxHeight / 2);
            e.Graphics.DrawEllipse(new Pen(Color.Red, 2), new Rectangle(zoomedPboxWidth / 2 - ellipseRadius, zoomedPboxHeight / 2 - ellipseRadius, 2 * ellipseRadius, 2 * ellipseRadius));
        }
    }
}
