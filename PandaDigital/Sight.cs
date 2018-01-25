using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaDigital
{
    public class Sight
    {
        public Size UserImageBox { get; set; }
        public Image UserImageToBeZoomed { get; set; }
        public int SightRadius { get; set; }

        public Sight(Image userImageToBeZoomed, int sightRadius, Size userImageBox)
        {
            UserImageToBeZoomed = userImageToBeZoomed;
            SightRadius = sightRadius;
            UserImageBox = userImageBox;
        }

        public Image GetZoomedImage(Point zoomCenter)
        {
            Rectangle zoomedArea = new Rectangle(zoomCenter.X - SightRadius, zoomCenter.Y - SightRadius, 2 * SightRadius, 2 * SightRadius);
            
            Bitmap userImageToBeZoomed = new Bitmap(UserImageToBeZoomed, new Size(UserImageBox.Width, UserImageBox.Height));
            Bitmap zoomedImage = CropImage(userImageToBeZoomed, zoomedArea);
            
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
    }
}
