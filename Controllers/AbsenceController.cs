using EleWise.ELMA.BPM.Mvc.Controllers;
using EleWise.ELMA.Web.Content;
using System.Web.Mvc;

namespace Prolog.Web.Controllers
{
    public class AbsenceController : BPMController
    {
        [ContentItem(Name = "График Отсутствия",
                    Image16 = RouteProvider.ImagesFolder + "x32/absence_graph.png",
                    Image24 = RouteProvider.ImagesFolder + "x32/absence_graph.png",
                    Image32 = RouteProvider.ImagesFolder + "x32/absence_graph.png")]
        public ActionResult Graph()
        {
            return View();
        }

    }
}
