using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitirmeProjesi.Models.Entity;
namespace BitirmeProjesi.Controllers
{
    public class OgrenciController : Controller
    {
        BitirmeProjesiEntities db = new BitirmeProjesiEntities();
        // GET: Ogrenci
        public ActionResult OgrenciGiris()
        {
            return View();
        }
       [HttpPost]
        public ActionResult OgrenciGiris(TBLOgrenciler p)
        {
            var bilgiler = db.TBLOgrenciler.FirstOrDefault(x => x.MAIL == p.MAIL && x.SIFRE == p.SIFRE);
            if (bilgiler != null)
            {
                TempData["ogrenciId"] = bilgiler.ID;
                return RedirectToAction("AnaSayfa", "Ogrenci");
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult OgrenciKaydol(TBLOgrenciler p1)
        {
            var bilgiler = db.TBLOgrenciler.FirstOrDefault(x => x.MAIL == p1.MAIL );
            if(bilgiler ==null && p1.MAIL!=null)
            {
                db.TBLOgrenciler.Add(p1);
                db.SaveChanges();
            }   
            return View("OgrenciGiris");
        }
        public ActionResult AnaSayfa()
        {
            int sayi = int.Parse(TempData["ogrenciId"].ToString());
            ViewBag.ogrenciId = TempData["ogrenciId"];
            TempData.Keep("ogrenciId");
            var degerler = db.TBLOgretmenler.ToList();
            var ogrenci = db.TBLIstekler.FirstOrDefault(x => x.OGRENCIID == sayi);
            if (ogrenci == null)
            {
                TBLIstekler istek = new TBLIstekler();
                istek.OGRENCIID = int.Parse(TempData["ogrenciId"].ToString());
                istek.BPID = 1;
                istek.APID = 1;
                istek.APONAY = -1;
                istek.BPONAY = -1;
                db.TBLIstekler.Add(istek);
                db.SaveChanges();
            }
            return View(degerler);
        }

       /* public ActionResult IstekAp(int id)
        {
            int sayi = int.Parse(TempData["ogrenciId"].ToString());
            var ogrenci = db.TBLIstekler.FirstOrDefault(x => x.OGRENCIID ==sayi);
           if(ogrenci!=null)
            {
                if(ogrenci.APID==-1)
                {
                    ogrenci.APID = id;
                    db.SaveChanges();
                }
            }
            ViewBag.ogrenciId = TempData["ogrenciId"];
            TempData.Keep("ogrenciId");
            return RedirectToAction("AnaSayfa", "Ogrenci");
        }*/
        public ActionResult IstekApcik(int id,string yazi)
        {
            int sayi = int.Parse(TempData["ogrenciId"].ToString());
            var ogrenci = db.TBLIstekler.FirstOrDefault(x => x.OGRENCIID == sayi);
            if (ogrenci != null)
            {
                if (ogrenci.APONAY <= -1)
                {
                    ogrenci.APID = id;
                    ogrenci.APONAY = 0;
                    ogrenci.APACIKLAMA = yazi;
                    db.SaveChanges();
                }
            }
            ViewBag.ogrenciId = TempData["ogrenciId"];
            TempData.Keep("ogrenciId");
            return RedirectToAction("AnaSayfa", "Ogrenci");
        }
       /* public ActionResult IstekBp(int id)
        {
            int sayi = int.Parse(TempData["ogrenciId"].ToString());
            var ogrenci = db.TBLIstekler.FirstOrDefault(x => x.OGRENCIID == sayi);
            if (ogrenci != null)
            {
                if (ogrenci.BPID == -1)
                {
                    ogrenci.BPID = id;
                    db.SaveChanges();
                }
            }
            ViewBag.ogrenciId = TempData["ogrenciId"];
            TempData.Keep("ogrenciId");
            return RedirectToAction("AnaSayfa", "Ogrenci");
        }*/
        public ActionResult IstekBpcik(int id,string yazi)
        {
            int sayi = int.Parse(TempData["ogrenciId"].ToString());
            var ogrenci = db.TBLIstekler.FirstOrDefault(x => x.OGRENCIID == sayi);
            if (ogrenci != null)
            {
                if (ogrenci.BPONAY <= -1)
                {
                    ogrenci.BPID = id;
                    ogrenci.BPONAY =0;
                    ogrenci.BPACIKLAMA = yazi;
                    db.SaveChanges();
                }
            }
            ViewBag.ogrenciId = TempData["ogrenciId"];
            TempData.Keep("ogrenciId");
            return RedirectToAction("AnaSayfa", "Ogrenci");
        }
        public ActionResult Profil()
        {
            ViewBag.ogrenciId = TempData["ogrenciId"];
            TempData.Keep("ogrenciId");
            var ogrenci = db.TBLOgrenciler.Find(TempData["ogrenciId"]);
            return View(ogrenci);
        }
        public ActionResult Guncelleme(TBLOgrenciler p1)
        {
            ViewBag.ogrenciId = TempData["ogrenciId"];
            TempData.Keep("ogrenciId");
            var guncelleme = db.TBLOgrenciler.Find(TempData["ogrenciId"]);
            guncelleme.ISIM = p1.ISIM;
            guncelleme.MAIL = p1.MAIL;
            guncelleme.SIFRE = p1.SIFRE;
            guncelleme.TELEFON = p1.TELEFON;
            guncelleme.BOLUM = p1.BOLUM;
            guncelleme.ADRES = p1.ADRES;
            guncelleme.NUMARA = p1.NUMARA;
            db.SaveChanges();
            return RedirectToAction("Profil", "Ogrenci");
        }
        public ActionResult Ogretmenlerim()
        {
            ViewBag.ogrenciId = TempData["ogrenciId"];
            TempData.Keep("ogrenciId");
            int sayi = int.Parse(TempData["ogrenciId"].ToString());
            var ogrenci = db.TBLIstekler.FirstOrDefault(x => x.OGRENCIID == sayi);
            return View(ogrenci);
        }
        public ActionResult Duyurular()
        {
            ViewBag.ogrenciId = TempData["ogrenciId"];
            TempData.Keep("ogrenciId");
            int sayi = int.Parse(TempData["ogrenciId"].ToString());
            var ogrenci = db.TBLIstekler.ToList();
            return View(ogrenci);
        }
        public ActionResult Mesaj()
        {
            ViewBag.ogrenciId = TempData["ogrenciId"];
            TempData.Keep("ogrenciId");
            var ogretmenler = db.TBLOgretmenler.ToList();
            ViewBag.ogr = ogretmenler;
            var yoneticiler = db.TBLYoneticiler.ToList();
            ViewBag.yoneticiler = yoneticiler;
            return View();
        }
        public ActionResult Mesajlar(int id,int tablo)
        {
            ViewBag.ogrenciId = TempData["ogrenciId"];
            TempData.Keep("ogrenciId");
            var ogretmenler = db.TBLOgretmenler.ToList();
            ViewBag.ogr = ogretmenler;
            var yoneticiler = db.TBLYoneticiler.ToList();
            ViewBag.yoneticiler = yoneticiler;
            ViewBag.id = id;
            ViewBag.tablo = tablo;
            var mesajlar = db.TBLMesajlar.ToList();
            return View(mesajlar);
        }
        public ActionResult MesajGonder(string mesaj,int id,int tablo)
        {
            ViewBag.ogrenciId = TempData["ogrenciId"];
            TempData.Keep("ogrenciId");
            int sayi = int.Parse(TempData["ogrenciId"].ToString());
            TBLMesajlar gidenMesaj = new TBLMesajlar();
            gidenMesaj.OGRENCID = sayi;
            if(tablo==1)
            {
                gidenMesaj.OGRETMENID = id;
            }
            else
            {
                gidenMesaj.YONETICIID = id;
            }
            gidenMesaj.MESAJ = mesaj;
            gidenMesaj.MESAJYOLU = 0;
            db.TBLMesajlar.Add(gidenMesaj);
            db.SaveChanges();
            // new RedirectResult(@"~\Ogrenci\Mesajlar\"+id+tablo);
           return RedirectToAction("Mesajlar", "Ogrenci",new {id,tablo });
        }

    }
}