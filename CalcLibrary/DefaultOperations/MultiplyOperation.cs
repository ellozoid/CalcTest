using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary.DefaultOperations
{
    class MultiplyOperation : IOperation
    {
        public string Name
        {
            get
            {
                return "multiply";
            }
        }

        public double Calc(double x, double y)
        {
            return x*y;
        }
    }
}
