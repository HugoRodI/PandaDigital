using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PandaDigital
{
    public class Circle:Shape
    {
        int radius;

        public Circle(int thick, Color color, int radius) : base(thick, color)
        {
            this.radius = radius;
        }
        
        public override void drawShape(Point p, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(new Pen(color, thick), new Rectangle(p.X, p.Y, radius, radius));
        }
    }
}
