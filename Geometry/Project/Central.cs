using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Geometry.Project
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    class Central : IExternalCommand
    {
        // 1) По каким объектам строить?
        // 2) Точка схода?
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            var redPoints = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).Cast<FamilyInstance>().Where(it => it.Symbol.Name == "Red").ToList();
            var sunPoint = doc.GetElement(new ElementId(316616)) as FamilyInstance;

            var planes = new List<Plane>();

            for(int i = 0; i < redPoints.Count; i++)
            {
                var point1 = redPoints[i].GetTotalTransform().Origin;
                var point2 = new XYZ();
                if (i < 3)
                {
                    point2 = redPoints[i+1].GetTotalTransform().Origin;
                }
                if (i == 3)  
                {
                    point2 = redPoints[0].GetTotalTransform().Origin;
                }

                var point3 = sunPoint.GetTotalTransform().Origin;
                var plane = Plane.CreateByThreePoints(point1, point2, point3);
                planes.Add(plane);
            }
            //====================================
            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("transaction");
                


                transaction.Commit();
            }
            //=====================================





            return Result.Succeeded;
        }
    }
}
