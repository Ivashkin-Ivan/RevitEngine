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
    internal class LocalCoordinateSystem: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Попытаюсь написать подвижную систему координат через класс Transform

            //Transaction
            //======================================

            var doc = commandData.Application.ActiveUIDocument.Document;


            //Put your code here
            //======================================
            FamilyInstance cube = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance))
                                                                    .Cast<FamilyInstance>()
                                                                    .Last(it => it.Symbol.FamilyName == "SampleFamily1.rvt" && it.Symbol.Name == "кубик");



            //Создаю экземпляр трансформаций

            var angle = Math.PI / 4; // Угол, на который будет происходить поворот

            Transform transform1 = Transform.CreateRotation(XYZ.BasisZ, angle);

            XYZ vector = new XYZ(0, 1000, 0); //Пока не понятно это смещение или новые координаты

            Transform transform2 = Transform.CreateTranslation(vector);

            Transform compose1 = transform1 * transform2;
            Transform compose2 = transform2 * transform1;

            var listid = new List<ElementId>();
            listid.Add(cube.Id);

            // Задаётся конечное положение
            //======================================


            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("transaction");





                transaction.Commit();
            }
            //=======================================


            return Result.Succeeded;
        }
    }
}
