using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Geometry.Dynamic
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    class RotateLocation : IExternalCommand
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

            View activeView = doc.ActiveView;

            LocationPoint location = cube.Location as LocationPoint;

            XYZ locationPoint = location.Point;




            // Задаётся угол и ось вращения
            //=====================================

            Line rotationAxis = Line.CreateBound(new XYZ(0, 0, 0), (new XYZ(0, 0, 0) + new XYZ(0, 0, 1)));

            var angle = Math.PI / 4;            //Задайте угол в радианах

            // Вращение против часовой стрелки, в правой системе координат

            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("transaction");

                location.Rotate(rotationAxis, angle);

                transaction.Commit();
            }
            //======================================

            return Result.Succeeded;
        }
    }
}
