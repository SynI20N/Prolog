using EleWise.ELMA.Security.Models;
using EleWise.ELMA.Web.Mvc.Portlets;
using System;
using System.Web.Mvc;

namespace Prolog.Web.Portlets
{
    [Serializable]
    public class ProductsPortletPersonalization : PortletPersonalization
    {
        [Personalization(PersonalizationScope.User)]
        [HiddenInput(DisplayValue = false)]
        public User Responsible { get; set; }

    }
}