using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaDigital
{
    public abstract class Scaler
    {
        public List<Axe> axis;

        public Scaler(List<Axe> axis)
        {
            this.axis = axis;
        }

        public abstract List<Point> Scale(List<Point> userPoints);
    }
}
