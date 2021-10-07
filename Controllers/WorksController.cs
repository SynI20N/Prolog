using EleWise.ELMA.BPM.Mvc.Controllers;
using EleWise.ELMA.Web.Content;
using System.Web.Mvc;

namespace Prolog.Web.Controllers
{
    public class WorksController : BPMController
    {
        [ContentItem(Name = "SR.M('График работ на АЭС')",
                    Image16 = RouteProvider.ImagesFolder + "x32/nuclear_works.png",
                    Image24 = RouteProvider.ImagesFolder + "x32/nuclear_works.png",
                    Image32 = RouteProvider.ImagesFolder + "x32/nuclear_works.png")]
        public ActionResult Graph()
        {
            return View();
        }

    }
}
