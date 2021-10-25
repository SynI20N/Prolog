using EleWise.ELMA;
using EleWise.ELMA.ComponentModel;
using EleWise.ELMA.Security;
using EleWise.ELMA.Security.Models;
using EleWise.ELMA.Web.Mvc.Portlets;
using System;
using System.Web.Mvc;

namespace Prolog.Web.Portlets
{
    [Component]
    public class ProductsPortlet : Portlet<ProductsPortletPersonalization>
    {
        public static string UID_S = "{f47f2c60-c15b-4b03-b3de-d1540a3c3041}";
        public static Guid UID = new Guid(UID_S);
        private readonly PortletProfile profile;

        public ProductsPortlet()
        {
            profile = base.Profile as PortletProfile;

            if (profile == null)
            {
                profile = PortletProfile.Default;
                profile.DefaultZone = "Right";
            }
            profile.Customizable = false;
            profile.DefaultOrder = 0;
            profile.ImageUrl = RouteProvider.ImagesFolder + "x24/ProductsPortlet.png";
        }

        public override IPortletProfile Profile
        {
            get { return profile; }
        }

        public override Guid Uid
        {
            get { return UID; }
        }

        public override string Name
        {
            get { return SR.T("Изделия"); }
        }

        public override string Description
        {
            get { return SR.T("Отображение информации об изделиях"); }
        }

        public override string GetNameUrl(UrlHelper urlHelper, ProductsPortletPersonalization data)
        {
            return urlHelper.Action("View", "Catalogs", new { area = "EleWise.ELMA.BPM.Common.Web" });
        }

        public override MvcHtmlString Content(HtmlHelper html, ProductsPortletPersonalization data)
        {
            var fNotFound = new Func<MvcHtmlString>(() =>
            {
                return MvcHtmlString.Create("У портлета не заданы настройки");
            });

            return data == null
                ? fNotFound()
                : RenderContentAction(html, "ProductsPortlet", "Products", RouteProvider.AreaName, data);
        }

        public override MvcHtmlString Settings(HtmlHelper html, ProductsPortletPersonalization data, string path)
        {
            return RenderSettingsPartialView(html, "Portlets/ProductsPortlet/Personalization", data);
        }

        protected override Permission PortletPermission()
        {
            return null;
        }

        public override bool AllowMultipleInstance
        {
            get
            {
                return true;
            }
        }

    }

    [Serializable]
    public class ProductsPortletPersonalization : PortletPersonalization
    {
        [Personalization(PersonalizationScope.User)]
        [HiddenInput(DisplayValue = false)]
        public User Responsible { get; set; }

    }
}