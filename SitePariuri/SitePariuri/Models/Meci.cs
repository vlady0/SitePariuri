using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SitePariuri.Models
{
    public class Meci
    {
        public int ID { get; set; }
        public int codMeci { get; set; }
        public DateTime data { get; set; }
        public string echipe { get; set; }
        public decimal tip1 { get; set; }
        public decimal tip2 { get; set; }
        public decimal tipX { get; set; }
        public decimal tip1X { get; set; }
        public decimal tipX2 { get; set; }
        public decimal tip12 { get; set; }       

        //pentru detalii
        public decimal pauzaSauFinal1 { get; set; }
        public decimal pauzaSauFinal2 { get; set; }
        public decimal pauzaSauFinalX { get; set; }
        public decimal sub3Goluri { get; set; }
        public decimal peste3Goluri { get; set; }
        public decimal ambeleMarcheaza { get; set; }
        public decimal nuMarcheazaAmbele { get; set; }
        public decimal castigaAmbeleReprize1 { get; set; }
        public decimal castigaAmbeleReprize2 { get; set; }
    }

    public class MeciDBContext : DbContext
    {
        public DbSet<Meci> Meciuri { get; set; }
    }
}