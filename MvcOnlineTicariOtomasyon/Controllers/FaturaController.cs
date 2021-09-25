using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Faturalar
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Faturalars.ToList();
            return View(degerler);
        }   

        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
        {
            c.Faturalars.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaGetir(int id)
        {
            var fatura = c.Faturalars.Find(id);
            return View(fatura);
        }

        [HttpPost]
        public ActionResult FaturaGuncelle2(int? id)
        {
            //gelen id yi Veritabanımızda kontrol ediyoruz ve gelen employee partialviewimize gönderiyoruz
            return PartialView(c.Faturalars.FirstOrDefault(x => x.Faturaid == id));
        }

        [HttpPost]
        public ActionResult FaturaGuncelle(Faturalar f)
        {
            if (string.IsNullOrEmpty(f.FaturaSeriNo))
            {
                ModelState.AddModelError("Hata", "Hata");
            }

            if (ModelState.IsValid)
            {
                var fatura = c.Faturalars.Find(f.Faturaid);
                fatura.FaturaSeriNo = f.FaturaSeriNo;
                fatura.FaturaSiraNo = f.FaturaSiraNo;
                fatura.Tarih = f.Tarih;
                fatura.Saat = f.Saat;
                fatura.VergiDairesi = f.VergiDairesi;
                fatura.TeslimAlan = f.TeslimAlan;
                fatura.TeslimEden = f.TeslimEden;
                fatura.Toplam = f.Toplam;
                c.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }            
        }

        public ActionResult FaturaDetay(int id)
        {
            var degerler = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            TempData["FaturaID"] = id;
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem p)
        {
            c.FaturaKalems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Dinamik()
        {
            Class4 cs = new Class4();
            cs.deger1 = c.Faturalars.ToList();
            cs.deger2 = c.FaturaKalems.ToList();
            return View(cs);
        }

        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSiraNo,DateTime Tarih, string VergiDairesi, string Saat, string TeslimEden, string TeslimAlan, string Toplam, FaturaKalem[] kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSiraNo = FaturaSiraNo;
            f.Tarih = Tarih;
            f.Saat = Saat;
            f.VergiDairesi = VergiDairesi;
            f.Toplam = decimal.Parse(Toplam);
            f.TeslimAlan = TeslimAlan;
            f.TeslimEden = TeslimEden;
            c.Faturalars.Add(f);

            foreach (var x in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = x.Aciklama;
                fk.BirimFiyat = x.BirimFiyat;
                fk.Faturaid = x.FaturaKalemid;
                fk.Miktar = x.Miktar;
                fk.Tutar = x.Tutar;
                c.FaturaKalems.Add(fk);
            }

            c.SaveChanges();
            return Json("İşlem başarılı", JsonRequestBehavior.AllowGet);
        }
    }
}