using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using BitirmeProjesi.Models.Entity;
using OfficeOpenXml;

namespace BitirmeProjesi.Controllers
{
    public class OgretmenController : Controller
    {
        BitirmeProjesiEntities db = new BitirmeProjesiEntities();
        // GET: Ogretmen
        public ActionResult OgretmenGiris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OgretmenGiris(TBLOgretmenler t1)
        {
            
            var bilgiler = db.TBLOgretmenler.FirstOrDefault(x => x.MAIL == t1.MAIL && x.SIFRE == t1.SIFRE);
            if (bilgiler != null)
            {
                TempData["ogretmenId"] = bilgiler.ID;
                return RedirectToAction("AnaSayfa", "Ogretmen");
            }
            else
            {
                return View();
            }
        }
        //sayfa
        public ActionResult AnaSayfa()
        {
            // int sayi = int.Parse(TempData["ogretmenId"].ToString());
            // var liste=db.TBLKonular.Where(x => x.ID == sayi);
            var liste = db.TBLKonular.ToList();
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            return View(liste);
        }
        [HttpPost]
        public ActionResult apEkle(String ap)
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var istek = new TBLKonular();
            istek.KONU = "ap";
            istek.ICERIK = ap;
            istek.OGRETMENID= int.Parse(TempData["ogretmenId"].ToString());
            db.TBLKonular.Add(istek);
            db.SaveChanges();

            return RedirectToAction("AnaSayfa", "Ogretmen");
        }
        [HttpPost]
        public ActionResult bpEkle(String bp)
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var istek = new TBLKonular();
            istek.KONU = "bp";
            istek.ICERIK = bp;
            istek.OGRETMENID = int.Parse(TempData["ogretmenId"].ToString());
            db.TBLKonular.Add(istek);
            db.SaveChanges();
            return RedirectToAction("AnaSayfa", "Ogretmen");
        }
        
        public ActionResult Istekler()
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var liste = db.TBLIstekler.ToList();
            return View(liste);
        }
        //public ActionResult ApOnay(int id)
        //{
        //    ViewBag.ogretmenId = TempData["ogretmenId"];
        //    TempData.Keep("ogretmenId");
        //    var degis = db.TBLIstekler.FirstOrDefault(x=> x.OGRENCIID==id);
        //    if(degis!=null)
        //    {
        //        degis.APONAY = 1;
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Istekler", "Ogretmen");
        //}
        public ActionResult ApOnaycik(int id,string yazi)
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var degis = db.TBLIstekler.FirstOrDefault(x => x.OGRENCIID == id);
            if (degis != null)
            {
                degis.APONAY = 1;
                degis.APCEVAP = yazi;
                db.SaveChanges();
            }
            return RedirectToAction("Istekler", "Ogretmen");
        }
        //public ActionResult ApRed(int id)
        //{
        //    ViewBag.ogretmenId = TempData["ogretmenId"];
        //    TempData.Keep("ogretmenId");
        //    var degis = db.TBLIstekler.FirstOrDefault(x => x.OGRENCIID == id);
        //    if (degis != null)
        //    {
        //        degis.APID = -1;
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Istekler", "Ogretmen");
        //}
        public ActionResult ApRedcik(int id,string yazi)
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var degis = db.TBLIstekler.FirstOrDefault(x => x.OGRENCIID == id);
            if (degis != null)
            {
               // degis.APID = 1;
                degis.APONAY = -2;
                degis.APCEVAP = yazi;
                db.SaveChanges();
            }
            return RedirectToAction("Istekler", "Ogretmen");
        }
        //public ActionResult BpOnay(int id)
        //{
        //    ViewBag.ogretmenId = TempData["ogretmenId"];
        //    TempData.Keep("ogretmenId");
        //    var degis = db.TBLIstekler.FirstOrDefault(x => x.OGRENCIID == id);
        //    if (degis != null)
        //    {
        //        degis.BPONAY = 1;
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Istekler", "Ogretmen");
        //}
        public ActionResult BpOnaycik(int id,string yazi)
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var degis = db.TBLIstekler.FirstOrDefault(x => x.OGRENCIID == id);
            if (degis != null)
            {
                degis.BPONAY = 1;
                degis.BPCEVAP = yazi;
                db.SaveChanges();
            }
            return RedirectToAction("Istekler", "Ogretmen");
        }
        //public ActionResult BpRed(int id)
        //{
        //    ViewBag.ogretmenId = TempData["ogretmenId"];
        //    TempData.Keep("ogretmenId");
        //    var degis = db.TBLIstekler.FirstOrDefault(x => x.OGRENCIID == id);
        //    if (degis != null)
        //    {
        //        degis.BPID = -1;
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Istekler", "Ogretmen");
        //}
        public ActionResult BpRedcik(int id, string yazi)
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var degis = db.TBLIstekler.FirstOrDefault(x => x.OGRENCIID == id);
            if (degis != null)
            {
            //    degis.BPID = 1;
                degis.BPONAY = -2;
                degis.BPCEVAP = yazi;
                db.SaveChanges();
            }
            return RedirectToAction("Istekler", "Ogretmen");
        }
        public ActionResult Profil()
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var ogrenci = db.TBLOgretmenler.Find(TempData["ogretmenId"]);
            return View(ogrenci);
        }
        public ActionResult Guncelleme(TBLOgretmenler p1)
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var guncelleme = db.TBLOgretmenler.Find(TempData["ogretmenId"]);
            guncelleme.ISIM = p1.ISIM;
            guncelleme.MAIL = p1.MAIL;
            guncelleme.SIFRE = p1.SIFRE;
            db.SaveChanges();
            return RedirectToAction("Profil", "Ogretmen");
        }
        public ActionResult KonuSil(int id)
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var degis = db.TBLKonular.Find(id);
            db.TBLKonular.Remove(degis);
            db.SaveChanges();
            return RedirectToAction("AnaSayfa", "Ogretmen");
        }
        public ActionResult KabulIstekler()
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var liste = db.TBLIstekler.ToList();
            return View(liste);
        }
        public ActionResult ExcelExport(int i)
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            ws.Cells["A1"].Value = "";
            ws.Cells["B1"].Value = "İsim";
            ws.Cells["C1"].Value = "Telefon";
            ws.Cells["D1"].Value = "Mail";
            ws.Cells["E1"].Value = "Bölüm";
            ws.Cells["F1"].Value = "Numara";
            int sayi = int.Parse(TempData["ogretmenId"].ToString());
            if(i==1)
            {
                var liste = db.TBLIstekler.Where(x => x.APID == sayi && x.APONAY==1);
                int row = 2;
                foreach (var item in liste)
                {
                    ws.Cells[string.Format("A{0}", row)].Value = row - 1;
                    ws.Cells[string.Format("B{0}", row)].Value = item.TBLOgrenciler.ISIM;
                    ws.Cells[string.Format("C{0}", row)].Value = item.TBLOgrenciler.TELEFON;
                    ws.Cells[string.Format("D{0}", row)].Value = item.TBLOgrenciler.MAIL;
                    ws.Cells[string.Format("E{0}", row)].Value = item.TBLOgrenciler.BOLUM;
                    ws.Cells[string.Format("F{0}", row)].Value = item.TBLOgrenciler.NUMARA;
                    row++;
                }
            }
            else
            {
                var liste = db.TBLIstekler.Where(x => x.BPID == sayi && x.BPONAY == 1);
                int row = 2;
                foreach (var item in liste)
                {
                    ws.Cells[string.Format("A{0}", row)].Value = row - 1;
                    ws.Cells[string.Format("B{0}", row)].Value = item.TBLOgrenciler.ISIM;
                    ws.Cells[string.Format("C{0}", row)].Value = item.TBLOgrenciler.TELEFON;
                    ws.Cells[string.Format("D{0}", row)].Value = item.TBLOgrenciler.MAIL;
                    ws.Cells[string.Format("E{0}", row)].Value = item.TBLOgrenciler.BOLUM;
                    ws.Cells[string.Format("F{0}", row)].Value = item.TBLOgrenciler.NUMARA;
                    row++;
                }
            }
            
            
            
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
            return RedirectToAction("KabulIstekler", "Ogretmen");
        }
        public ActionResult Duyurular()
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            int sayi= int.Parse(TempData["ogretmenId"].ToString());
            var liste = db.TBLDuyurular.ToList();
            return View(liste);
            
        }
        public ActionResult Duyuru(string duyuru)
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var istek = new TBLDuyurular();
            istek.DUYURU = duyuru;
            istek.OGRETMENID = int.Parse(TempData["ogretmenId"].ToString());
            db.TBLDuyurular.Add(istek);
            db.SaveChanges();       
            return RedirectToAction("Duyurular", "Ogretmen");
        }
        public ActionResult DuyuruKaldir(int id)
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var duyuru = db.TBLDuyurular.FirstOrDefault(x => x.ID == id);
            db.TBLDuyurular.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Duyurular", "Ogretmen");
        }
        public ActionResult Mesaj()
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var ogrenciler = db.TBLOgrenciler.ToList();
            ViewBag.ogr = ogrenciler;
            var yoneticiler = db.TBLYoneticiler.ToList();
            ViewBag.yoneticiler = yoneticiler;
            var liste = db.TBLMesajlar.ToList();
            return View(liste);
        }
        public ActionResult Mesajlar(int id,int tablo)
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            var ogrenciler = db.TBLOgrenciler.ToList();
            ViewBag.ogr = ogrenciler;
            var yoneticiler = db.TBLYoneticiler.ToList();
            ViewBag.yoneticiler = yoneticiler;
            ViewBag.id = id;
            ViewBag.tablo = tablo;
            var mesajlar = db.TBLMesajlar.ToList();
            return View(mesajlar);
        }
        public ActionResult MesajGonder(string mesaj, int id, int tablo)
        {
            ViewBag.ogretmenId = TempData["ogretmenId"];
            TempData.Keep("ogretmenId");
            int sayi = int.Parse(TempData["ogretmenId"].ToString());
            TBLMesajlar gidenMesaj = new TBLMesajlar();
            gidenMesaj.OGRETMENID = sayi;
            if (tablo == 0)
            {
                gidenMesaj.OGRENCID = id;
            }
            else
            {
                gidenMesaj.YONETICIID = id;
            }
            gidenMesaj.MESAJ = mesaj;
            gidenMesaj.MESAJYOLU = 1;
            db.TBLMesajlar.Add(gidenMesaj);
            db.SaveChanges();
            //return new RedirectResult(@"~\Ogretmen\Mesajlar\" + id);
            return RedirectToAction("Mesajlar", "Ogretmen", new { id, tablo });
        }
    }
}