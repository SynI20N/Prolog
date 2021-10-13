using EleWise.ELMA;
using EleWise.ELMA.ComponentModel;
using EleWise.ELMA.Security;
using EleWise.ELMA.Web.Mvc.Portlets;
using System;
using System.Web.Mvc;

namespace Prolog.Web.Portlets
{
    [Component]
    public class ResponsibilityPortlet : Portlet<ResponsibilityPortletPersonalization>
    {
        public static string UID_S = "{cb7fcef0-f1df-4aea-bb2b-f7f05adf35c1}";
        public static Guid UID = new Guid(UID_S);
        private readonly PortletProfile profile;

        public ResponsibilityPortlet()
        {
            profile = base.Profile as PortletProfile;

            if (profile == null)
            {
                profile = PortletProfile.Default;
                profile.DefaultZone = "Right";
            }

            profile.Customizable = false;
            profile.DefaultOrder = 0;
            profile.ImageUrl = RouteProvider.ImagesFolder + "x24/ResponsibilityPortlet.png";
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
            get { return SR.T("Ответственность отдела"); }
        }

        public override string Description
        {
            get { return SR.T("Какими изделиями занимается мой отдел?"); }
        }

        public override MvcHtmlString Content(HtmlHelper html, ResponsibilityPortletPersonalization data)
        {
            var fNotFound = new Func<MvcHtmlString>(() =>
            {
                return MvcHtmlString.Create("У портлета не заданы настройки");
            });

            return data == null
                ? fNotFound()
                : RenderContentAction(html, "ResponsibilityPortlet", "Products", RouteProvider.AreaName, data);
        }

        public override MvcHtmlString Settings(HtmlHelper html, ResponsibilityPortletPersonalization data, string path)
        {
            return RenderSettingsPartialView(html, "Portlets/ResponsibilityPortlet/Personalization", data);
        }

        protected override Permission PortletPermission()
        {
            return null;
        }

        public override bool AllowMultipleInstance
        {
            get
            {
                return false;
            }
        }

    }

    [Serializable]
    public class ResponsibilityPortletPersonalization : PortletPersonalization
    {

    }
}