using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Copyright 2024 Littleclone

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

namespace Cosmic_Explorer
{
    // Soweit ich weiß gerade nicht mehr in benutzung, war aber mal in Benutzung
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
