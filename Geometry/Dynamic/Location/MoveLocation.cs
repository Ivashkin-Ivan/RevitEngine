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
    class MoveLocation : IExternalCommand
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

            XYZ down = new XYZ(200, 0, 0); // вектор задающий направление смещения

            
            //=====================================


            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("transaction");
                
                location.Move(down);

                transaction.Commit();
            }
            //======================================

            return Result.Succeeded;
        }

    }
}
