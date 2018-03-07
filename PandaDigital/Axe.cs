using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaDigital
{
    public class Axe
    {
        public int lowerRealBound, higherRealBound;
        public int lowerPixelBound, higherPixelBound;

        public Axe(int lowerRealBound, int higherRealBound, int lowerPixelBound, int higherPixelBound)
        {
            this.lowerRealBound = lowerRealBound;
            this.higherRealBound = higherRealBound;
            this.lowerPixelBound = lowerPixelBound;
            this.higherPixelBound = higherPixelBound;
        }

        

    }
}
