using CalcLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Output = System.Console;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Calc();

            int x;
            int.TryParse(args[0], out x);

            int y;
            int.TryParse(args[1], out y);

            double result = 0; // = test.Sum(x, y);

            var operation = args[2];

            if(operation == "sum")
            {
                result = test.Sum(x, y);

            }else if(operation == "divide")
            {
                result = test.Divide(x, y);
            }

            Output.WriteLine("{0} {1} {2} = {3}", x, operation, y, result);
            Output.ReadKey();
        }
    }
}
