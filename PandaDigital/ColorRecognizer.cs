using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaDigital
{
    public class ColorRecognizer
    {
        public Image imageToRecognize;
        public List<Color> colorsInImage;
        Dictionary<Color, int> ocurrenciesOfColorsInImage = new Dictionary<Color, int>();
        public List<Color> dominantColorsInImage;
        private int numberOfDominantColors = 6;
        private int pixelStep = 4;

        public ColorRecognizer(Image imageToRecognize)
        {
            this.imageToRecognize = imageToRecognize;
           /* colorsInImage = new List<Color>();
            dominantColorsInImage = new List<Color>();*/
        }

        public List<Color> getColorsInImage()
        {
            return colorsInImage;
        }

        public List<Color> getDominantColorsInImage()
        {
            return dominantColorsInImage;
        }

        public void RecognizeColors()
        {
            GetAllColorsInImage();
            GetDominantColorsInImage();
        }

        private void GetAllColorsInImage()
        {
            Color pixelColor;
            Bitmap b = new Bitmap(imageToRecognize);
            LockBitmap lockBitmap = new LockBitmap(b);

            lockBitmap.LockBits();

            for (int i = 0; i < lockBitmap.Width; i += pixelStep)
            {
                for (int j = 0; j < lockBitmap.Height; j += pixelStep)
                {
                    pixelColor = lockBitmap.GetPixel(i, j);

                    if (ocurrenciesOfColorsInImage.ContainsKey(pixelColor))
                        ocurrenciesOfColorsInImage[pixelColor]++;
                    else
                        ocurrenciesOfColorsInImage.Add(pixelColor, 1);
                }
            }

            lockBitmap.UnlockBits();

            colorsInImage = new List<Color>(ocurrenciesOfColorsInImage.Keys);
        }

        private void GetDominantColorsInImage()
        {
            if (colorsInImage.Count >= numberOfDominantColors)
                dominantColorsInImage = ocurrenciesOfColorsInImage.OrderBy(dict => dict.Value).Take(numberOfDominantColors).ToDictionary(k => k.Key, v => v.Value).Keys.ToList();
            else
                dominantColorsInImage = ocurrenciesOfColorsInImage.OrderBy(dict => dict.Value).ToDictionary(k => k.Key, v => v.Value).Keys.ToList();            
        }
        
    }
}
