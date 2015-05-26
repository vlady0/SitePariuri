using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SitePariuri.Models;
using System.Linq.Expressions;

namespace SitePariuri.Controllers
{
    public class MeciController : Controller
    {
        private MeciDBContext db = new MeciDBContext();
        private BiletVirtual bv;


        #region CRUD
        //
        // GET: /Meci/

        public ActionResult Index()
        {
            return View(db.Meciuri.ToList());
        }

        //
        // GET: /Meci/Details/5

        public ActionResult Details(int id = 0)
        {
            Meci meci = db.Meciuri.Find(id);
            if (meci == null)
            {
                return HttpNotFound();
            }
            return View(meci);
        }

        //
        // GET: /Meci/Create
        [Authorize(Users = "vlad")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Meci/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Meci meci)
        {
            if (ModelState.IsValid)
            {
                db.Meciuri.Add(meci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meci);
        }

        //
        // GET: /Meci/Edit/5
        [Authorize(Users = "vlad")]
        public ActionResult Edit(int id = 0)
        {
            Meci meci = db.Meciuri.Find(id);
            if (meci == null)
            {
                return HttpNotFound();
            }
            return View(meci);
        }

        //
        // POST: /Meci/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Meci meci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meci);
        }

        //
        // GET: /Meci/Delete/5
        [Authorize(Users = "vlad")]
        public ActionResult Delete(int id = 0)
        {
            Meci meci = db.Meciuri.Find(id);
            if (meci == null)
            {
                return HttpNotFound();
            }
            return View(meci);
        }

        //
        // POST: /Meci/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meci meci = db.Meciuri.Find(id);
            db.Meciuri.Remove(meci);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region Adaugare pe bilet
        public ActionResult AdaugaPeBilet(int id, decimal cota)
        {
            string test = id + " " + cota; 

            if (Session["BiletVirtual"] == null)
            {
                bv = new BiletVirtual();
                Session["BiletVirtual"] = bv;
            }

            Meci meci = db.Meciuri.Find(id);
            if (meci == null)
            {
                return HttpNotFound();
            }


            var bilet = Session["BiletVirtual"] as BiletVirtual;
            bilet.adaugaPeBilet(meci, cota);
            Session["BiletVirtual"] = bilet;

            return  View();
        }
        #endregion

        #region Afiseaza bilet virtual
        public ActionResult AfiseazaBiletVirtual()
        {
            if (Session["BiletVirtual"] == null)
            {
                bv = new BiletVirtual();
                Session["BiletVirtual"] = bv;
            }
            var bilet = Session["BiletVirtual"] as BiletVirtual;
           
            string message;
            if (bilet.bilet.Count == 0)
            {
                ViewBag.Message = "Nici un meci adaugat";
            }           
            else
            {

                message = "\n Bilet virtual: \nNr.   Cod   Data      Meci           Cota";
                int contor = 1;
                foreach (KeyValuePair<Meci, decimal> b in bilet.bilet)
                {
                    message += contor + "   " + b.Key.codMeci + "   " + b.Key.data + "      " + b.Key.echipe + "            " + b.Value;
                }

                message += " \n Cota finala: " + bilet.calculeazaCotaFinala().ToString();                


                ViewBag.Message = message;
            }

            return View(); 
        }

        #endregion

        #region Genereaza bilet
        public ActionResult genereazaBilet(decimal miza)
        {
            if (Session["BiletVirtual"] == null)
            {
                bv = new BiletVirtual();
                Session["BiletVirtual"] = bv;
            }
            var bilet = Session["BiletVirtual"] as BiletVirtual;

            string message;
            if (bilet.bilet.Count == 0)
            {
                ViewBag.Message = "Nici un meci adaugat";
            }
            else
            {

                //message = "\n Bilet virtual: \nNr.   Cod   Data      Meci           Cota";
                //int contor = 1;
                //foreach (KeyValuePair<Meci, decimal> b in bilet.bilet)
                //{
                //    message += contor + "   " + b.Key.codMeci + "   " + b.Key.data + "      " + b.Key.echipe + "            " + b.Value;
                //}

                //message += " \n Cota finala: " + bilet.calculeazaCotaFinala().ToString();

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                var result = new string(
                    Enumerable.Repeat(chars, 8)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());


                message = "Bilet virtual: \n Cota finala: " + bilet.calculeazaCotaFinala().ToString();
                message += "\nMiza: " + miza.ToString() + "\n Castig potential: " + bilet.calcueazaCastigPotential(miza).ToString();
                message += "\nCod de mers la agentie: " + result.ToString();


                ViewBag.Message = message;
            }
            return View();
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}