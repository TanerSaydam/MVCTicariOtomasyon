using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaKalemController : Controller
    {
        // GET: FaturaKalem
        Context c = new Context();
        public ActionResult Index(int id)
        {            
            var degerler = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            var kid = c.Faturalars.Where(x => x.Faturaid == id).Select(y => y.Faturaid).FirstOrDefault();
            ViewBag.ki = kid;
            return View(degerler);
        }

        [HttpGet]
        public ActionResult KalemEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KalemEkle(FaturaKalem k, int id)
        {
            k.Faturaid = id;
            c.FaturaKalems.Add(k);            
            c.SaveChanges();
            return RedirectToAction("/Index/" + id.ToString());
        }
    }
}