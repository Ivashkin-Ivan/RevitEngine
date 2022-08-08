using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using Tools.Geometry.Dynamic;

namespace Tools.Application
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    class Application : IExternalApplication
    {
        private const string GEOMETRY_TAB = "Геометрия";
        private const string PARAMETERS_TAB = "Параметры";

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            //Путь к сборке
            string assemblyLocation = Assembly.GetExecutingAssembly().Location,
                   iconsDirectoryPath = Path.GetDirectoryName(assemblyLocation) + @"\icons\";

            //Создание вкладок
            application.CreateRibbonTab(GEOMETRY_TAB);
            application.CreateRibbonTab(PARAMETERS_TAB);

            //Создание панелей
            RibbonPanel locationPanel = application.CreateRibbonPanel(GEOMETRY_TAB, "Location");
            RibbonPanel elementTransformUtilsPanel = application.CreateRibbonPanel(GEOMETRY_TAB, "ElementTransformUtils");
            RibbonPanel projectPanel = application.CreateRibbonPanel(GEOMETRY_TAB, "Project");

            RibbonPanel parametersPanel = application.CreateRibbonPanel(PARAMETERS_TAB, PARAMETERS_TAB);




            locationPanel.AddItem(new PushButtonData(nameof(MoveLocation), "Вниз", assemblyLocation, typeof(MoveLocation).FullName)
                {
                    LargeImage = new BitmapImage(new Uri(iconsDirectoryPath + "icon5.png"))
                });
           
            locationPanel.AddItem(new PushButtonData(nameof(RotateETU), "Поворот на заданный угол", assemblyLocation, typeof(RotateETU).FullName)
            {
                LargeImage = new BitmapImage(new Uri(iconsDirectoryPath + "blue.png"))
            });
            
            locationPanel.AddItem(new PushButtonData(nameof(ComposeLocation), "Композиция команд", assemblyLocation, typeof(ComposeLocation).FullName)
            {
                LargeImage = new BitmapImage(new Uri(iconsDirectoryPath + "blue.png"))
            });








            return Result.Succeeded;
        }
    }
}
