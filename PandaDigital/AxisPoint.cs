using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PandaDigital
{
    public class AxisPoint
    {
        public Color color;
        public PointF location;

        public AxisPoint(PointF location, Color color)
        {
            this.location = location;
            this.color = color;
        }
    }
}
