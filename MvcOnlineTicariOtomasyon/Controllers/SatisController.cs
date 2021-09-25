using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.SatisHarekets.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult SatisEkle()
        {
            List<SelectListItem> cariler = (from x in c.Carilers.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.CariAd + " " + x.CariSoyad,
                                                 Value = x.Cariid.ToString()
                                             }).ToList();

            List<SelectListItem> urunler = (from x in c.Uruns.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.UrunAd,
                                                Value = x.Urunid.ToString()
                                            }).ToList();
            List<SelectListItem> personel = (from x in c.Personels.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                 Value = x.Personelid.ToString()
                                             }).ToList();
            ViewBag.dgr1 = cariler.ToList();
            ViewBag.dgr2 = urunler.ToList();
            ViewBag.dgr3 = personel.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult SatisEkle(SatisHareket s)
        {
            c.SatisHarekets.Add(s);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> cariler = (from x in c.Carilers.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.CariAd + " " + x.CariSoyad,
                                                Value = x.Cariid.ToString()
                                            }).ToList();

            List<SelectListItem> urunler = (from x in c.Uruns.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.UrunAd,
                                                Value = x.Urunid.ToString()
                                            }).ToList();
            List<SelectListItem> personel = (from x in c.Personels.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                 Value = x.Personelid.ToString()
                                             }).ToList();
            ViewBag.dgr1 = cariler.ToList();
            ViewBag.dgr2 = urunler.ToList();
            ViewBag.dgr3 = personel.ToList();
            var satislar = c.SatisHarekets.Find(id);
            return View(satislar);
        }

        public ActionResult SatisGuncelle(SatisHareket s)
        {
            var satislar = c.SatisHarekets.Find(s.Satisid);
            satislar.Tarih = s.Tarih;
            satislar.Urunid = s.Urunid;
            satislar.Personelid = s.Personelid;
            satislar.Adet = s.Adet;
            satislar.Fiyat = s.Fiyat;
            satislar.ToplamTutar = s.ToplamTutar;
            satislar.Cariid = s.Cariid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SatisDetay(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Satisid == id).ToList();
            return View(degerler);
        }
    }
}