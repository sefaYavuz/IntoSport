using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntoSport.Helpers
{
    public static class CartHelper
    {
        public string addItem(string cart, int id, int quantity){

            string[] items = cart.Split('.');
            for(int i=0; i < items.Length; i++)
            {
                string[] item = items[i].Split(',');
                if (j == 0 && Int16.Parse(item[0]) == id)
                {
                    
                }
            }

        }

        public string addItem(int id, int quanity)
        {

        }

        public string removeItem(string cart, int id)
        {

        }

        public string makeQuantity(string cart, int id, int quanity)
        {

        }
    }
}