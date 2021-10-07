using EleWise.ELMA;
using EleWise.ELMA.ComponentModel;
using EleWise.ELMA.Web.Content;
using EleWise.ELMA.Web.Content.Menu;
using System.Collections.Generic;

namespace Prolog.Web.MenuLeft
{
    [Component]
    public class MenuItemProvider : IMenuItemsProvider
    {
        public const string MyModule = "Prolog";
        public const string AbsenceGraph = "Absences";
        public const string NuclearWorks = "NuclearWorks";

        public const string PROJECT_ADMIN_MENU = "project-admin-menu";

        public void Items(MenuItemFactory factory)
        {

            #region left

            factory.Action(new ActionRoute("Index", "Home", new { area = RouteProvider.AreaName }))
                .Name(SR.M("Пролог"))
                .Code(MyModule)
                .Image32(RouteProvider.ImagesFolder + "code.png")
                .Order(100)
                .Container("left")
                .OnTop(true)
                .Stretch(true);

            factory.Action(new ActionRoute("Graph", "Absence", new { area = RouteProvider.AreaName }))
                .Name(SR.M("Отсутствия"))
                .Code(AbsenceGraph)
                .Parent(MyModule)
                .Order(10)
                .Container("left");

            factory.Action(new ActionRoute("Graph", "Works", new { area = RouteProvider.AreaName }))
                .Name(SR.M("Работы на АЭС"))
                .Code(NuclearWorks)
                .Parent(MyModule)
                .Order(20)
                .Container("left");

            #endregion
        }


        public List<string> LocalizedItemsNames
        {
            get { return null; }
        }

        public List<string> LocalizedItemsDescriptions
        {
            get { return null; }
        }
    }
}