using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitePariuri.Models
{
    public class BiletVirtual
    {
        public Dictionary<Meci, decimal> bilet { get; set;}
        public decimal cotaFinala { get; set; }
        public decimal castigPotential { get; set; }

        public BiletVirtual()
        {
            bilet = new Dictionary<Meci, decimal>();
        }

        public void adaugaPeBilet(Meci meci, decimal cota)
        {
            bilet.Add(meci, cota);
        }

        public decimal calculeazaCotaFinala()
        {
            this.cotaFinala = 1.00M;
            foreach (KeyValuePair<Meci, decimal> b in bilet)
            {
                this.cotaFinala *= b.Value;
                //Console.WriteLine("Key = {0}, Value = {1}",
                //    b.Key.echipe, b.Value);
            }
            return this.cotaFinala;
        }

        public decimal calcueazaCastigPotential(decimal miza)
        {
            decimal impozit = 5.00M;
            miza -= miza * impozit / 100;
            this.castigPotential = miza * calculeazaCotaFinala();          
            return this.castigPotential;
        }
    }
}