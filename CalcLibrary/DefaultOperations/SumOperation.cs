using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary.DefaultOperations
{
    class SumOperation : IOperationArgs
    {
        public string Name
        {
            get
            {
                return "sum";
            }
        }

        public double Calc(IEnumerable<double> args)
        {
            return args.Sum();
        }

        public double Calc(double x, double y)
        {
            return x + y;
        }
    }
}
