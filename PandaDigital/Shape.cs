using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PandaDigital
{
    public abstract class Shape
    {
        public int thick;
        public Color color;

        public Shape(int thick, Color color)
        {
            this.thick = thick;
            this.color = color;
        }

        public abstract void drawShape(Point p, PaintEventArgs e);        
    }
}
