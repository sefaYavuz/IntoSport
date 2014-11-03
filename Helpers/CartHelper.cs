using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntoSport.Helpers
{
    public static class CartHelper
        //CartHelper class om de cart cookie makkelijker te veranderen.
    {
        public static String[] getItems(string cart)
        {
            
            string[] items = cart.Split('.');
            return items;
        }

        public static string add(string cartString, int product, int quantity)
        {
            string newCart= "";
            string[] cartSplit = cartString.Split('.');
            bool found = false;
            for(int i = 0; i<cartSplit.Length-1; i++){
                string[] itemSplit = cartSplit[i].Split(',');
                if (itemSplit[0] == product.ToString())
                {
                    itemSplit[1] = (Int32.Parse(itemSplit[1]) + quantity).ToString();
                    found = true;
                    
                }
                newCart += itemSplit[0] + "," +itemSplit[1] + ".";
            }
            if (found == false)
            {
                newCart += product.ToString() + "," + quantity.ToString() + ".";
            }
            return newCart;

        }

        public static string add(int product, int quanity)
        {
            return (product.ToString() + "," + quanity.ToString() + ".");
        }

        public static string update(string cartString, int product, int quantity)
            //Had eigenlijk een andere methode moeten zijn zodat add die ook kon gebruiken, nu heb ik 2 keer dezelfde code eigenlijk
            //wat niet mooi is.
            //
            //Veranderd een quantity van een item met een andere hoeveelheid
        {
            string newCart = "";
            string[] cartSplit = cartString.Split('.');
            for(int i = 0; i<cartSplit.Length-1; i++){
                string[] itemSplit = cartSplit[i].Split(',');
                if (itemSplit[0] == product.ToString())
                {
                    itemSplit[1] = (Int32.Parse(itemSplit[1]) + quantity).ToString();
                    
                }
                newCart += itemSplit[0] + "," +itemSplit[1] + ".";
            }
            return newCart;

        }

        public static string remove(string cartString, int product)
        {
            string newCart = "";
            string[] cartSplit = cartString.Split('.');
            for (int i = 0; i < cartSplit.Length-1; i++)
            {
                bool found = false;
                string[] itemSplit = cartSplit[i].Split(',');
                if (itemSplit[0] == product.ToString())
                {
                    found = true;
                }
                if (!found)
                {
                    newCart += itemSplit[0] + "," + itemSplit[1] + ".";
                }
            }
            return newCart;
        }
        
    }
}