using EleWise.ELMA.BPM.Mvc.Controllers;
using EleWise.ELMA.Content.Models;
using EleWise.ELMA.Web.Content;
using System.Web.Mvc;

namespace Prolog.Web.Controllers
{
    public class HomeController : BPMController<IMenu, int>
    {
        [ContentItem(Name = "Графики",
                    Image16 = RouteProvider.ImagesFolder + "code.png",
                    Image24 = RouteProvider.ImagesFolder + "code.png",
                    Image32 = RouteProvider.ImagesFolder + "code.png")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
