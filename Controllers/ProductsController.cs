using EleWise.ELMA.API;
using EleWise.ELMA.BPM.Web.Common.Controllers;
using EleWise.ELMA.BPM.Web.Common.Models;
using EleWise.ELMA.ConfigurationModel;
using EleWise.ELMA.Security.Models;
using EleWise.ELMA.Web.Mvc.Attributes;
using Prolog.Web.Models;
using Prolog.Web.Portlets;
using System;
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

        [CustomGridAction]
        public ActionResult PortletGrid(GridCommand command, long? filterId, string statusFilter)
        {
            var filter = CreateFilter(filterId);
            IUser user = _portalUser.GetCurrentUser();
            ((TovarFilter)filter.Filter).Otvetstvennyy.Add(user);
            var list = CreateGridData(command, filter);
            
            return PartialView("Portlets/ProductsPortlet/Grid", getFilteredList(ref list, statusFilter));
        }

        private GridDataFilter<ITovar> getFilteredList(ref GridDataFilter<ITovar> list, string filter)
        {
            if(filter == null)
            {
                return list;
            }
            for (int i = 0; i < filter.Length; i++)
            {
                if(filter[i] == 1)
                {
                    list.DataFilter.Filter.Query += "Status = " + getStatusId(i) + "AND";
                }
            }

            return list;
        }

        private string getStatusId(int i)
        {
            if(i == 5)
            {
                return "102";
            }
            else
            {
                return i.ToString();
            }
        }

        [HttpPost]
        public ActionResult UpdateStatus(long? status, long? id)
        {
            Tovar product = _portalUserObjects.UserTovar.Load(id.Value);
            product.Status = _portalUserObjects.UserStatusPoziciiSpecifikacii.Load(status.Value);
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
