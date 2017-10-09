using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PandaDigital
{
    public class PandaDigital
    {
        private const int NUMBER_OF_PANDA_COLORS = 6;
        public Image PandaImage { get; set; }
        public List<PointF> ManualPoints { get; set; }
        public List<PointF> AutoPoints { get; set; }
        public List<PointF> SelectedPoints { get; set; }
        public List<PandaColor> ImgColors { get; set; }
        public List<PandaColor> PandaColors { get; set; }

        public PandaDigital(string fileName)
        {
            PandaImage = ImageFromFileName(fileName);
            FindPandaColors(PandaImage);
        }

        private Image ImageFromFileName(string fileName)
        {
            Image imageFromFileName;

            imageFromFileName = new Bitmap(fileName);
            return imageFromFileName;
        }

        private void FindPandaColors(Image img)
        {
            Color pixelColor;
            Bitmap b = new Bitmap(img);

            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    pixelColor = b.GetPixel(i, j);

                    if (!ImgColors.Any(c => c.Color == pixelColor))
                        ImgColors.Add(new PandaColor(pixelColor));
                    else
                        ImgColors.Where(c => c.Color == pixelColor).ToList()[0].Ocurrencies++;
                }
            }

            PandaColors = ImgColors.OrderByDescending(o => o.Ocurrencies).ToList().GetRange(0, NUMBER_OF_PANDA_COLORS);
        }


    }
}
