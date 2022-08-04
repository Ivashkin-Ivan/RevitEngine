// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* PulldownButton_Work.cs
 * https://revit-addins.blogspot.ru
 * © Inpad, 2021
 *
 * This file contains the methods which are used by the 
 * command.
 */
#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using WPF = System.Windows;

#endregion


namespace InpadPlugins.RevitAddin
{
    public sealed partial class StackedPulldown1
    {

        private bool DoWork(ExternalCommandData commandData, ref String message, ElementSet elements)
        {

            if (null == commandData)
            {

                throw new ArgumentNullException(nameof(commandData));
            }

            if (null == message)
            {

                throw new ArgumentNullException(nameof(message));
            }

            if (null == elements)
            {

                throw new ArgumentNullException(nameof(elements));
            }

            ResourceManager res_mng = new ResourceManager(GetType());
            ResourceManager def_res_mng = new ResourceManager(typeof(Properties.Resources));

            UIApplication ui_app = commandData.Application;
            UIDocument ui_doc = ui_app?.ActiveUIDocument;
            Application app = ui_app?.Application;
            Document doc = ui_doc?.Document;

            var tr_name = res_mng.GetString("_transaction_name");

            try
            {
                using (var tr = new Transaction(doc, tr_name))
                {

                    if (TransactionStatus.Started == tr.Start())
                    {

                        // ====================================
                        // TODO: delete these code rows and put
                        // your code here.

                        // ====================================

                        return TransactionStatus.Committed ==
                            tr.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                /* TODO: Handle the exception here if you need 
                 * or throw the exception again if you need. */
                throw ex;
            }
            finally
            {

                res_mng.ReleaseAllResources();
                def_res_mng.ReleaseAllResources();
            }

            return false;
        }
    }
}
