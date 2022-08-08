using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Geometry.Dynamic.ElementTransformUtilsMethods
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    class Rotate : IExternalCommand
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

            XYZ locationPoint = (cube.Location as LocationPoint).Point;




            // Задаётся угол и ось вращения
            //=====================================

            Line rotationAxis = Line.CreateBound(new XYZ(0 ,0 ,0),(new XYZ(0, 0, 0) + new XYZ(0, 0, 1)));

            var angle = Math.PI/4;            //Задайте угол в радианах
            
            // Вращение против часовой стрелки, в правой системе координат

            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("transaction");


                ElementTransformUtils.RotateElement(doc, cube.Id, rotationAxis, angle);

                transaction.Commit();
            }
            //======================================

            return Result.Succeeded;
        }
    }
}
