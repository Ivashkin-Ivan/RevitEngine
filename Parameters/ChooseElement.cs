using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;

namespace Tools.Parameters
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    internal class ChooseElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;

            //var list = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).Cast<FamilySymbol>().Where(it => it.FamilyName == "Колонна прямоугольного сечения").ToList();

            var list2 = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol))
                                                                                            .Cast<FamilySymbol>()
                                                                                            .Where(it => it.FamilyName == "Стол")
                                                                                            .ToList();
            var listId = new List<ElementId>();

            foreach (var element in list2)
            {
                listId.Add(element.Id);
            }

            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("transaction");

                doc.Delete(listId);

                transaction.Commit();
            }

            return Result.Succeeded;
        }
    }
}
