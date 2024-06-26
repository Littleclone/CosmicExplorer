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

        // Addition von zwei Zahlen
        public int Add(int a, int b)
        {
            return a + b;
        }

        // Subtraktion von zwei Zahlen
        public int Subtract(int a, int b)
        {
            return a - b;
        }

        // Multiplikation von zwei Zahlen
        public int Multiply(int a, int b)
        {
            return a * b;
        }

        // Division von zwei Zahlen
        public double Divide(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Teiler darf nicht null sein");
            }
            return (double)a / b;
        }
    }
}
