﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary
{
    public class Calc
    {
        public Calc()
        {
            operations = new List<IOperation>();
            var assm = Assembly.GetAssembly(typeof(IOperation));

            var types = assm.GetTypes();

            foreach(var type in types) {
                var interfaces = type.GetInterfaces();
                if (interfaces.Contains(typeof(IOperation)))
                {
                    var oper = Activator.CreateInstance(type) as IOperation;
                    if(oper != null)
                    {
                        operations.Add(oper);
                    }
                }
            }
            var opers = assm.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IOperation)));
            //var oper
             
        }
        /// <summary>
        /// Список доступных операций
        /// </summary>
        private IList<IOperation> operations { get; set; }

        /// <summary>
        /// Выполнить операцию
        /// </summary>
        /// <param name="operation">Название операции</param>
        /// <param name="args">Аргументы операции</param>
        /// <returns></returns>
        public object Execute(string operation, object[] args)
        {
            //находим операцию в списке доступных
            var oper = operations.FirstOrDefault(it => it.Name == operation);

            //если не нашли - возвращаем ошибку
            if(oper == null)
            {
                return "Error";
            }
            //если нашли
            //разбираем аргументы
            double x;
            double.TryParse(args[0].ToString(), out x);

            double y;
            double.TryParse(args[1].ToString(), out y);

            var result = oper.Calc(x, y);

            return result;
        }

        [Obsolete("Не используйте")]
        public double Sum(double x, double y)
        {
            return (double)Execute("sum", new object[] { x, y });
            //return int.Parse(t.ToString());
        }

        public double Divide(double x, double y)
        {
            return (double)Execute("divide", new object[] { x, y });
            //return y == 0 ? Double.NaN : x * 1D / y;
        }
    }
}
