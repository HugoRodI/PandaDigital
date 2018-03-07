using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PandaDigital
{
    public class Square:Shape
    {
        int side;

        public Square(int thick, Color color, int side) : base(thick, color)
        {
            this.side = side;
        }

        public override void drawShape(Point point, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(color, thick), new Rectangle(point.X - side / 2, point.Y - side / 2, side, side));
        }
    }
}
