using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using IntoSport.Models;
using System.Web.Script.Serialization;



namespace IntoSport.Controllers
{
    public class MainController : Controller
    {
        public HttpCookie cookie;
        public JavaScriptSerializer jsonSerializer;
        /*
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            jsonSerializer = new JavaScriptSerializer();
            base.Initialize(requestContext);
            Cart.products = new List<Dictionary<string, object>>();
            createCookie("shoppingcart");
            fillCart();
            ViewData.Add("cart-amount", Cart.getAmount());
            ViewData.Add("categories", getCategories());
        }

        private void fillCart()
        {
            if (cookie.Value != null)
            {
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                Cart.products = jsonSerializer.Deserialize<List<Dictionary<string, object>>>(cookie.Value);
            }
        }*/

        public List<Dictionary<string, object>> getCategories()
        {
            Query query = new Query();
            query.Select("*");
            query.From("category");
            return query.Execute();
        }

        protected void createCookie(string name)
        {
            try
            {
                if (Request.Cookies[name] == null)
                {
                    this.cookie = new HttpCookie(name);
                }
                else
                {
                    this.cookie = Request.Cookies[name];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}