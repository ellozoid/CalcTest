using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary.DefaultOperations
{
    class DivideOneOperation : IOperation
    {
        public string Name
        {
            get
            {
                return "divide";
            }
        }

        public double Calc(IEnumerable<double> args)
        {
            throw new NotImplementedException();
        }

        double IOperation.Calc(double x, double y)
        {
            return x;
        }

    }
}
