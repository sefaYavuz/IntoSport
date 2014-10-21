using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntoSport.Models
{
    public class Omzet
    {
        public readonly string product_naam;
        public readonly double omzet;
        public readonly int aantal_verkochte;

        public Omzet(string product_naam, int aantal_verkochte, double omzet)
        {
            this.product_naam = product_naam;
            this.omzet = omzet;
            this.aantal_verkochte = aantal_verkochte;
        }
    }
}