using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary.DefaultOperations
{
    class DivideOperation : IOperationArgs
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

        public double Calc(double x, double y)
        {
            return y == 0 ? double.NaN : x / y;
        }
    }
}
