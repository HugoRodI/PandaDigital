using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PandaDigital
{
    public class Cross:Shape
    {
        int horizontalSide;
        int verticalSide;

        public Cross(int thick, Color color, int horizontalSide, int verticalSide) : base(thick, color)
        {
            this.horizontalSide = horizontalSide;
            this.verticalSide = verticalSide;
        }

        public override void drawShape(Point p, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(color, thick), p.X - verticalSide/2, p.Y, p.X + verticalSide/2, p.Y);
            e.Graphics.DrawLine(new Pen(color, thick), p.X, p.Y - horizontalSide/2, p.X, p.Y + horizontalSide/2);
        }

    }
}
