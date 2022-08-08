using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Application
{

    [Transaction(TransactionMode.Manual)]

    internal class TransactionRevit : IExternalCommand
    {   
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Transaction
            //=====================================
            
            var doc = commandData.Application.ActiveUIDocument.Document;
            //Put your code here
            //=====================================







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
