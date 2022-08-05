using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Tools.Geometry.Dynamic;
using Tools.Geometry.Dynamic.Location;

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




            locationPanel.AddItem(new PushButtonData(nameof(Down), "Вниз", assemblyLocation, typeof(Down).FullName)
                {
                    LargeImage = new BitmapImage(new Uri(iconsDirectoryPath + "icon5.png"))
                });
            
            locationPanel.AddItem(new PushButtonData(nameof(Up), "Вверх", assemblyLocation, typeof(Up).FullName)
            {
                LargeImage = new BitmapImage(new Uri(iconsDirectoryPath + "icon6.png"))
            });
            
            locationPanel.AddItem(new PushButtonData(nameof(Left), "Влево", assemblyLocation, typeof(Left).FullName)
            {
                LargeImage = new BitmapImage(new Uri(iconsDirectoryPath + "icon4.png"))
            });
            
            locationPanel.AddItem(new PushButtonData(nameof(Right), "Вправо", assemblyLocation, typeof(Right).FullName)
            {
                LargeImage = new BitmapImage(new Uri(iconsDirectoryPath + "icon3.png"))
            });

            locationPanel.AddItem(new PushButtonData(nameof(Direction), "Произвольное направление", assemblyLocation, typeof(Direction).FullName)
            {
                LargeImage = new BitmapImage(new Uri(iconsDirectoryPath + "blue.png"))
            });

            locationPanel.AddItem(new PushButtonData(nameof(Rotate), "Поворот на заданный угол", assemblyLocation, typeof(Rotate).FullName)
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
