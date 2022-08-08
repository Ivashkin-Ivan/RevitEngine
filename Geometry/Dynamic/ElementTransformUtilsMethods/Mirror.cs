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
    class Mirror : IExternalCommand
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

            //Отразим по горизонтали от BasisX

            Plane plane = Plane.CreateByNormalAndOrigin(XYZ.BasisX, new XYZ(20, 0, 0));



            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("transaction");

                ElementTransformUtils.MirrorElement(doc, cube.Id, plane);

                transaction.Commit();
            }
            //======================================

            return Result.Succeeded;
        }
    }
}
