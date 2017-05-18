using System;
using System.Collections.Generic;
using System.IO;
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
            Operations = new List<IOperation>();
            //var assm = Assembly.GetAssembly(typeof(IOperationArgs));

            //var types = assm.GetTypes().ToList();

            var types = new List<Type>();
            //найти длл рядом с ехе
            var dlls = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dll");
            foreach(var dll in dlls)
            {
                var assm = Assembly.LoadFrom(dll);

                types.AddRange(assm.GetTypes());
            }
            //загрузить её как сборку
            //добавить типы
            var iOper = typeof(IOperation);
            foreach(var type in types) {
                if (type.IsInterface)
                    continue;
                var interfaces = type.GetInterfaces();
                if (interfaces.Contains(iOper))
                {
                    var oper = Activator.CreateInstance(type) as IOperation;
                    if(oper != null)
                    {
                        Operations.Add(oper);
                    }
                }
            }
            var opers = types.Where(t => t.GetInterfaces().Contains(typeof(IOperationArgs)));
             
        }
        public Calc(string extendDllDirectory)
        {
            Operations = new List<IOperation>();

            //var assm = Assembly.GetAssembly(typeof(IOperation));
            //var types = assm.GetTypes().ToList();
            var types = new List<Type>();
            // найти длл рядом с нашим exe

            var path = string.IsNullOrWhiteSpace(extendDllDirectory)
                ? Directory.GetCurrentDirectory()
                : extendDllDirectory;

            var dlls = Directory.GetFiles(path, "*.dll");
            foreach (var dll in dlls)
            {
                // загрузить ее как сборку
                var assm = Assembly.LoadFrom(dll);
                // добавить типы
                types.AddRange(assm.GetTypes());
            }

            var ioper = typeof(IOperation);
            foreach (var type in types)
            {
                if (type.IsInterface)
                    continue;

                var interfaces = type.GetInterfaces();
                if (interfaces.Any(i => i.FullName == ioper.FullName))
                {
                    var oper = Activator.CreateInstance(type) as IOperation;
                    if (oper != null)
                    {
                        Operations.Add(oper);
                    }
                }
            }
        }
        /// <summary>
        /// Список доступных операций
        /// </summary>
        public IList<IOperation> Operations { get; private set; }

        [Obsolete("Не используйте")]
        /// <summary>
        /// Выполнить операцию
        /// </summary>
        /// <param name="operation">Название операции</param>
        /// <param name="args">Аргументы операции</param>
        /// <returns></returns>
        public object Execute(string operation, object[] args)
        {
            //находим операцию в списке доступных
            var oper = Operations.FirstOrDefault(it => it.Name == operation);

            //если не нашли - возвращаем ошибку
            if(oper == null)
            {
                return "Error";
            }
            //если нашли
            //разбираем аргументы
            

            double result = 0;
            var operArgs = oper as IOperationArgs;
            if(operArgs != null)
            {
                result = operArgs.Calc(args.Select(it => double.Parse(it.ToString())));
            }
            else
            {
                double x;
                double.TryParse(args[0].ToString(), out x);

                double y;
                double.TryParse(args[1].ToString(), out y);
            }

            //result = oper.Calc(x, y);

            return result;
        }

        /// <summary>
        /// Выполнить операцию
        /// </summary>
        /// <param name="operation">Название операции</param>
        /// <param name="args">Аргументы операции</param>
        /// <returns></returns>
        public object Execute(IOperation operation, object[] args)
        {
            if (operation == null)
                return null;

            double result = 0;
            var operArgs = operation as IOperationArgs;
            if (operArgs != null)
            {
                result = operArgs.Calc(args.Select(it => double.Parse(it.ToString())));
            }
            else
            {
                double x;
                double.TryParse(args[0].ToString(), out x);

                double y;
                double.TryParse(args[1].ToString(), out y);

                result = operation.Calc(x, y);
            }

            //result = oper.Calc(x, y);

            return result;
        }
        [Obsolete("Не используйте")]
        public double Sum(double x, double y)
        {
            return (double)Execute("sum", new object[] { x, y });
            //return int.Parse(t.ToString());
        }
        [Obsolete("Не используйте")]
        public double Divide(double x, double y)
        {
            return (double)Execute("divide", new object[] { x, y });
            //return y == 0 ? Double.NaN : x * 1D / y;
        }
    }
}
