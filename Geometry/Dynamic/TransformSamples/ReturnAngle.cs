using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Geometry.Dynamic.TransformSamples
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    internal class ReturnAngle : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Transaction
            //======================================

            var doc = commandData.Application.ActiveUIDocument.Document;

            //Put your code here
            //======================================
            FamilyInstance cube = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance))

                                                                    .Cast<FamilyInstance>()
                                                                    .Last(it => it.Symbol.FamilyName == "SampleFamily1.rvt" && it.Symbol.Name == "кубик");


            var instance = cube as Instance;
            var transform = instance.GetTotalTransform();

            // Получаю угол по +-sin(fi) 0 < fi < PI/2 , через BasisX.Y

            var sin = transform.BasisX.Y;
            if (sin < -1E-10)
            {
                message = "Элемент повёрнут по часовой стрелке на угол " + (Math.Abs(Math.Asin(transform.BasisX.Y) * 180)) / Math.PI;
            }
            if (sin > 1E-10)
            {
                message = "Элемент повёрнут против часовой стрелки на угол " + (Math.Abs(Math.Asin(transform.BasisX.Y) * 180)) / Math.PI;
            }
            if (sin > -1E-10 && sin < 1E-10)
            {
                message = "Ориентация по базису";
            }

            TaskDialog.Show("Угол", message);




            // Задаётся конечное положение
            //=====================================


            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("transaction");





                transaction.Commit();
            }
            //======================================


            return Result.Succeeded;
        }
    }
}
