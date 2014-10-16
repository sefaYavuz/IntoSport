using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntoSport.Helpers
{
    public static class CartHelper
        //CartHelper class om de cart cookie makkelijker te veranderen.
    {
        public string addItem(string cart, int id, int quantity){

            string newCart = "";

            string[] items = cart.Split('.');
            bool found = false;
            for(int i=0; i < items.Length; i++)
            {
                string[] item = items[i].Split(',');
                if (Int32.Parse(item[0]) == id)
                {
                    item[1] = (quantity+1).ToString().ToString();
                    items[i] = item[0] + "," + item[1];
                    found = true;
                }
                newCart = newCart + items[i] + ".";
            }
            if (!found)
            {
                newCart = newCart + id.ToString() + "," + quantity.ToString() + ".";
            }

            return newCart;

        }

        public string addItem(int id, int quantity)
        {
            string newCart = id.ToString() + "," + quantity.ToString() + ".";
            return newCart;
        }

        public string removeItem(string cart, int id)
        {
            string newCart = "";

            string[] items = cart.Split('.');
            for (int i = 0; i < items.Length; i++)
            {
                string[] item = items[i].Split(',');
                if (Int32.Parse(item[0]) == id)
                {
                    item[0] = null;
                    item[1] = null;
                }
                newCart = newCart + items[i] + ".";
            }

            return newCart;
        }

        public string makeQuantity(string cart, int id, int quantity)
        {
            string newCart = "";

            string[] items = cart.Split('.');
            for (int i = 0; i < items.Length; i++)
            {
                string[] item = items[i].Split(',');
                if (Int32.Parse(item[0]) == id)
                {
                    item[1] = quantity.ToString();
                    items[i] = item[0] + "," + item[1];
                }
                newCart = newCart + items[i] + ".";
            }

            return newCart;
        }
    }
}