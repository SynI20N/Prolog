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
using static EleWise.ELMA.API.PublicAPI.PortalApiRoot.SecurityPortalApi;

namespace Prolog.Web.Controllers
{
    public class ProductsController : FilterTreeBaseController<ITovar, long>
    {
        private UserObjectsApiRoot _portalUserObjects = PublicAPI.Objects.UserObjects;
        private UserSecurityApi _portalUser = PublicAPI.Portal.Security.User;

        [HttpGet]
        public ActionResult ProductsPortlet(ProductsPortletPersonalization settings)
        {
            var model = new ProductsPortletModel { Responsible = settings.Responsible };
            return PartialView("Portlets/ProductsPortlet/Index", model);
        }

        [HttpGet]
        public ActionResult ResponsibilityPortlet(ResponsibilityPortletPersonalization settings)
        {
            return PartialView("Portlets/ResponsibilityPortlet/Index");
        }

        [CustomGridAction]
        public ActionResult PortletGrid(GridCommand command, long? filterId, string statusFilter)
        {
            var filter = CreateFilter(filterId);
            IUser user = _portalUser.GetCurrentUser();
            ((TovarFilter)filter.Filter).Otvetstvennyy.Add(user);
            /*if (statusFilter != null)
            {
                for (int i = 0; i < statusFilter.Length; i++)
                {
                    if (statusFilter[i] == '1')
                    {
                        filter.Filter.Query = "StatusNew = 0";
                    }
                }
            }*/
            var list = CreateGridData(command, filter);

            return PartialView("Portlets/ProductsPortlet/Grid", list);
        }

        [HttpPost]
        public ActionResult UpdateStatus(long? status, long? id)
        {
            Tovar product = _portalUserObjects.UserTovar.Load(id.Value);
            product.StatusNew = (ProductStatus)status.Value;
            product.Save();

            return PartialView("Portlets/ProductsPortlet/Partial/Status", product);
        }

        [HttpPost]
        public ActionResult UpdateDate(long? id)
        {
            Tovar product = _portalUserObjects.UserTovar.Load(id.Value);

            return PartialView("Portlets/ProductsPortlet/Partial/Date", product);
        }

    }
}
