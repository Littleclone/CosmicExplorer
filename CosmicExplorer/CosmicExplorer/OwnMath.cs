using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public class OwnMath
    {
        public bool result;
        public int x;
        //Subtraction and look if its bigger then 0
        public void EnergyController(int need, int has)
        {
            x = has;
            has -= need;
            if (has < 0)
            {
                result = false;
            }
            else
            {
                result = true;
                x = has;
            }
        }
    }
}
