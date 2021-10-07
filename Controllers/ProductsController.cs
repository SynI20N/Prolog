using EleWise.ELMA.API;
using EleWise.ELMA.BPM.Web.Common.Controllers;
using EleWise.ELMA.ConfigurationModel;
using EleWise.ELMA.Security.Models;
using EleWise.ELMA.Web.Mvc.Attributes;
using Prolog.Web.Models;
using Prolog.Web.Portlets;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using static EleWise.ELMA.API.PublicAPI.ObjectsApiRoot;

namespace Prolog.Web.Controllers
{
    public class ProductsController : FilterTreeBaseController<ITovar, long>
    {
        private UserObjectsApiRoot _portalHelper = PublicAPI.Objects.UserObjects;

        [HttpGet]
        public ActionResult ProductsPortlet(ProductsPortletPersonalization settings)
        {
            var model = new ProductsPortletModel { Responsible = settings.Responsible };
            return PartialView("Portlets/ProductsPortlet", model);
        }

        [CustomGridAction]
        public ActionResult PortletGrid(GridCommand command, long? filterId)
        {
            var filter = CreateFilter(filterId);
            IUser user = PublicAPI.Portal.Security.User.GetCurrentUser();
            ((TovarFilter)filter.Filter).Otvetstvennyy.Add(user);
            var list = CreateGridData(command, filter);
            return PartialView("Portlets/ProductsPortletGrid", list);
        }

        [HttpPost]
        public ActionResult UpdateStatus(long status, long id)
        {
            Tovar product = _portalHelper.UserTovar.Load(id);
            if (status != 0)
            {
                product.Status = _portalHelper.UserStatusPoziciiSpecifikacii.Load(status);
            }
            else
            {
                product.Status = null;
            }
            product.Save();

            return PartialView("Product/Status", product);
        }

        [HttpPost]
        public ActionResult UpdateDate(long id)
        {
            Tovar product = _portalHelper.UserTovar.Load(id);

            return PartialView("Product/Date", product);
        }

    }
}
