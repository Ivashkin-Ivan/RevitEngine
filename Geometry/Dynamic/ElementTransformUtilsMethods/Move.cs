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
    class Move : IExternalCommand
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

            XYZ translation = new XYZ(250, 250, 0);

          

            // Задаётся ось вращения и угол в радианах, дополнительно, для понимания изобразим дугу
            //=====================================


            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("transaction");

                ElementTransformUtils.MoveElement(doc, cube.Id, translation);

                //doc.Create.NewDetailCurveArray(activeView, curves); //закомментил кривую

                transaction.Commit();
            }
            //======================================

            return Result.Succeeded;
        }

    }
}
