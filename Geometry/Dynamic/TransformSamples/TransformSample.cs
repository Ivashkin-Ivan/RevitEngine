using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Creation;

namespace Tools.Geometry.Dynamic.TransformSamples
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    internal class TransformSample : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Transaction
            //======================================

            var doc = commandData.Application.ActiveUIDocument.Document;


            //Put your code here
            //======================================
            //FamilyInstance cube = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance))
            //                                                        .Cast<FamilyInstance>()
            //                                                        .Last(it => it.Symbol.FamilyName == "SampleFamily1.rvt" && it.Symbol.Name == "кубик");



            //Создаю экземпляр трансформаций

            var angle = Math.PI / 6; // Угол, на который будет происходить поворот
            
            Transform transform1 = Transform.CreateRotation(XYZ.BasisZ, angle);

            XYZ vector = new XYZ(0,1000, 0); //Пока не понятно это смещение или новые координаты //Это смещение

            Transform transform2 = Transform.CreateTranslation(vector);


            Transform compose1 = transform1 * transform2;
            Transform compose2 = transform2 * transform1;
            var basis0 = transform1.get_Basis(0);
            var basis1 = transform1.get_Basis(1);
            var basis2 = transform1.get_Basis(2);
            



            // Трансформ в минус первой стпени

            //var instance = cube as Instance;

           // Transform inverse = instance.GetTotalTransform().Inverse;

            var listid = new List<ElementId>();
           // listid.Add(cube.Id);

            // Задаётся конечное положение
            //======================================


            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("transaction");


                //ElementTransformUtils.CopyElements(doc, listid, doc, transform1, new CopyPasteOptions()); //Разве что копирование геометрии из документа в себя же

                //ElementTransformUtils.CopyElements(doc, listid, doc, transform2, new CopyPasteOptions()); //С углом тоже работает

               // ElementTransformUtils.CopyElements(doc, listid, doc, inverse, new CopyPasteOptions()); // Повороты образуют не коммутативную группу
                
                //ElementTransformUtils.CopyElements(doc, listid, doc, compose2, new CopyPasteOptions());
                
                //Пока для начала можно взять правило, что каждый новый transform строится от нового базиса.

               
                transaction.Commit();
            }
            //=======================================


            return Result.Succeeded;
        }
    }
}
