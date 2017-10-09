using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PandaDigital
{
    public class PandaColor
    {
        public Color Color { get; set; }
        public int Ocurrencies { get; set; }

        public PandaColor(Color color)
        {
            Color = color;
            Ocurrencies = 1;
        }
    }
}
