using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class İstatistikController : Controller
    {
        // GET: İstatistik
        Context c = new Context();
        public ActionResult Index()
        {
            ViewBag.d1 = c.Carilers.Count().ToString();
            ViewBag.d2 = c.Uruns.Count().ToString();
            ViewBag.d3 = c.Personels.Count().ToString();
            ViewBag.d4 = c.Kategoris.Count().ToString();
            ViewBag.d5 = c.Uruns.Sum(x=> x.Stok).ToString();
            ViewBag.d6 = (from x in c.Uruns select x.Marka).Distinct().Count().ToString();
            ViewBag.d7 = c.Uruns.Count(x=> x.Stok<=20).ToString();
            ViewBag.d8 = c.Uruns.Max(x=> x.SatisFiyat).ToString();
            ViewBag.d9 = c.Uruns.Min(x=> x.SatisFiyat).ToString();
            ViewBag.d10 = c.Carilers.Count().ToString();
            ViewBag.d11 = c.Carilers.Count().ToString();
            ViewBag.d12 = c.Carilers.Count().ToString();
            ViewBag.d13 = c.Uruns.Where(u => u.Urunid == (c.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault();
            ViewBag.d14 = c.Carilers.Count().ToString();
            ViewBag.d15 = c.Carilers.Count().ToString();
            ViewBag.d16 = c.Carilers.Count().ToString();
            return View();
        }
    }
}