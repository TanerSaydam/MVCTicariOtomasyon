using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Carilers.Where(x=> x.Durum==true).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniCari()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniCari(Cariler cariler)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            c.Carilers.Add(cariler);            
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariSil(int id)
        {
            var sil = c.Carilers.Find(id);
            sil.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariGetir(int id)
        {
            var guncelle = c.Carilers.Find(id);
            return View("CariGetir", guncelle);
        }

        public ActionResult CariGuncelle(Cariler cariler)
        {
            var cr = c.Carilers.Find(cariler.Cariid);
            cr.CariAd = cariler.CariAd;
            cr.CariSoyad = cariler.CariSoyad;
            cr.CariSehir = cariler.CariSehir;
            cr.CariMail = cariler.CariMail;
            cr.Durum = cariler.Durum;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariSatisGecmisi(int id)
        {
            var deger = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            var cariadi = c.Carilers.Where(x => x.Cariid == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cadi = cariadi;
            return View(deger);
        }
    }
}