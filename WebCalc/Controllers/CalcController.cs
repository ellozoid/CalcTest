using CalcLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalc.Helpers;
using WebCalc.Managers;
using WebCalc.Models;

namespace WebCalc.Controllers
{
    public class CalcController : Controller
    {
        #region Private Members

        private Calc Calc { get; set; }

        private IEnumerable<SelectListItem> OperationList { get; set; }

        private IOperationResultRepository OperationResultRepository { get; set; }

        #endregion

        public CalcController()
        {
            Calc = new Calc(@"C:\Users\Bogdan\Documents\Visual Studio 2015\Projects\CalcTest\WebCalc\bin");
            OperationList = Calc.Operations.Select(o => new SelectListItem() { Text = $"{o.GetType().Name}.{o.Name}", Value = $"{o.GetType().Name}.{o.Name}" });

            OperationResultRepository = new OperationManager();
        }
        // GET: Calc
        public ActionResult Index()
        {
            var model = new OperationViewModel(OperationList);

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(OperationViewModel model)
        {
            
            var operResults = OperationResultRepository.GetAll();
            //var calc = new Calc(@"C:\Users\Bogdan\Documents\Visual Studio 2015\Projects\CalcTest\WebCalc\bin");
            //var oldResult = operResults.FirstOrDefault(op => op.OperationName == model.Operation && op.Arguments == model.InputData);
            var oldResult = operResults.FirstOrDefault(
                op => op.OperationName == model.Operation && op.Arguments == model.InputData.Trim()
            );
            if (oldResult != null && Request.Params["updBtn"] != "Calc!")
            {
                model.Result = $"Это уже вычисляли {oldResult.Result}";
            }
            else
            {
                var stopWatch = new Stopwatch();
                var names = model.Operation.Split('.');
                var opers = Calc.Operations.Where(o => o.Name == names[1]);
                var oper = opers.FirstOrDefault(o => o.GetType().Name == names[0]);
                var result = Calc.Execute(oper, model.InputData.Trim().Split(' '));

                model.Result = $"{result}";
                var operResult = new OperationResult()
                {
                    OperationName = model.Operation,
                    Result = result as double?,
                    Arguments = model.InputData.Trim(),
                    ExecutionTime = stopWatch.ElapsedMilliseconds * 10,
                    ExecutionDate = DateTime.Now
                };
                if (Request.Params["updBtn"] == "Calc!")
                {
                    OperationResultRepository.Update(operResult);
                }
                else
                {
                    OperationResultRepository.Save(operResult);
                }
                
            }
            //var result = Calc.Execute(model.Operation, model.InputData.Split(' '));

            model.Operations = OperationList;

            return View(model);
        }
        public ActionResult Operations(OperationViewModel model)
        {
            model.Operations = OperationList;
            return View(model);
        }
    }
}