using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PandaDigital
{
    public class Triangle:Shape
    {
        int triangleBase;
        int height;

        public Triangle(int thick, Color color, int triangleBase, int height) : base(thick, color)
        {
            this.triangleBase = triangleBase;
            this.height = height;
        }

        public override void drawShape(Point p, PaintEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
