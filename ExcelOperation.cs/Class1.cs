using CalcLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelOperation.cs
{
    public class Class1 : IOperation
    {
        public string Name => "excel";

        public double Calc(double x, double y)
        {
            return x * x + y * y + x - y;
        }
    }
}
