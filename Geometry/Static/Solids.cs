using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Geometry.Static
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    class Solids : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            var doc = commandData.Application.ActiveUIDocument.Document; //Получение геометрии из FamilyInstance

            var sunPoint = doc.GetElement(new ElementId(316616));

            var options = new Options()
            {
                DetailLevel = ViewDetailLevel.Medium,
                ComputeReferences = true,
            };
            
            var solids = new List<Solid>();
            var geometryElement = sunPoint.get_Geometry(options);
            
            foreach (GeometryObject geometryObject in geometryElement)
            {
                if (geometryObject is GeometryInstance)
                {
                    var geometryInstance = (GeometryInstance)geometryObject;
                    GeometryElement instGeomElem = geometryInstance.GetInstanceGeometry();
                    foreach (GeometryObject instGeometryObject in instGeomElem)
                    {
                        if (instGeometryObject is Solid)
                        {
                            Solid solid = (Solid)instGeometryObject;
                            if(solid.Faces.Size > 0 && solid.Volume > 0)
                            {
                                solids.Add(solid);
                            }      
                        }
                    } 
                }
            }
            return Result.Succeeded;
        }
    }
}
