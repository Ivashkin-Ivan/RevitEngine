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
    class ComposeLocation : IExternalCommand
    {
        //Попробую построить композицию перемещений с использованим location
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            
            // Put your code here
            //========================================================
            





            //========================================================

            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("transaction");

                

                transaction.Commit();
            }
            //========================================================
            return Result.Succeeded;
        }
        
    }
    class ComposeLocaion
    {


    }
}
