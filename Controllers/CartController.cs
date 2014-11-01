using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntoSport.Models;
using IntoSport.Helpers;
using IntoSport.ViewModels;
using System.Web.Security;
namespace IntoSport.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/

        public ActionResult Index()
        {
            if (Request.Cookies["cart"] != null)
            {
                String[] cart = CartHelper.getItems(Request.Cookies["cart"].Value);
                List<Dictionary<string,Product>> IDlist = new List<Dictionary<string,Product>>();
                List<Dictionary<string,int>> QAList = new List<Dictionary<string,int>>();
                for (int i = 0; i < CartHelper.getItems(Request.Cookies["cart"].Value).Length-1; i++)
                {
                    /*Dictionary<string, object> data = new Dictionary<string, object>();
                    string productID = CartHelper.getItems(Request.Cookies["cart"].Value)[i].Split(',')[0];
                    int productIDInt = Int32.Parse(productID);
                    Product product = new Product(Int32.Parse(productID));
                    data.Add("product", product);
                    int quantity = Int32.Parse(CartHelper.getItems(Request.Cookies["cart"].Value)[i].Split(',')[1]);
                    data.Add("quantity", quantity);
                    list.Add(data);
                     * */

                    Dictionary<string, Product> productData = new Dictionary<string, Product>();
                    string productID = CartHelper.getItems(Request.Cookies["cart"].Value)[i].Split(',')[0];
                    int productIDInt = Int32.Parse(productID);
                    Product product = new Product(Int32.Parse(productID));
                    productData.Add("product", product);
                    IDlist.Add(productData);

                    Dictionary<string, int> quantityData = new Dictionary<string, int>();
                    int quantity = Int32.Parse(CartHelper.getItems(Request.Cookies["cart"].Value)[i].Split(',')[1]);
                    quantityData.Add("id", quantity);
                    QAList.Add(quantityData);
                    
                }
                ViewData["Productlist"] = IDlist;
                ViewData["QAList"] = QAList;

                return View();
            }
            else
            {
                return View();

            }
        }

        
        public ActionResult Bestel()
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];//.ASPXAUTH
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string id = authTicket.Name;
                OrderDBController ODBC = new OrderDBController();
                HttpCookie myCookie = new HttpCookie("UserSettings");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
                return RedirectToAction("Success");

            }
            else
            {
                return Redirect("/Login");
            }
        }

        public ActionResult addToCart(int ID, int Quantity)
        {
            if (Request.Cookies["cart"] == null)
            {
                HttpCookie cart = new HttpCookie("cart");
                cart.Value = CartHelper.addItem(ID, Quantity);
                Response.Cookies.Add(cart);
                return RedirectToAction("Index");

            }
            else
            {
                Response.Cookies["cart"].Value = CartHelper.addItem(Request.Cookies["cart"].Value, ID, Quantity);
                Response.Cookies["cart"].Expires = DateTime.Now.AddDays(1);
                return RedirectToAction("Index");
            }
        }

        /*public ActionResult remove(int ID, int Quantity)
        {
            
        }*/

        public ActionResult Success()
        {
            return View();
        }
    }
}