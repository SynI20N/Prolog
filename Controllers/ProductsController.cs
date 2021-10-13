﻿using EleWise.ELMA.API;
using EleWise.ELMA.BPM.Web.Common.Controllers;
using EleWise.ELMA.BPM.Web.Common.Models;
using EleWise.ELMA.ConfigurationModel;
using EleWise.ELMA.Model.Common;
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

        [CustomGridAction]
        public ActionResult PortletGrid(GridCommand command, long? filterId, string statusFilter)
        {
            var filter = CreateFilter(filterId);
            IUser user = _portalUser.GetCurrentUser();
            ((TovarFilter)filter.Filter).Otvetstvennyy.Add(user);
            if(statusFilter != null)
            {
                for (int i = 0; i < statusFilter.Length; i++)
                {
                    ((TovarFilter)filter.Filter).Status.Add(_portalUserObjects.UserStatusPoziciiSpecifikacii.Load(getStatusId(i)));
                }
            }
            var list = CreateGridData(command, filter);

            return PartialView("Portlets/ProductsPortlet/Grid", list);
        }

        private long getStatusId(int i)
        {
            if (i == 5)
            {
                return 102;
            }
            else
            {
                return i;
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
