using BitirmeProjesi.Models.Entity;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitirmeProjesi.Controllers
{

    public class YoneticiController : Controller
    {
        BitirmeProjesiEntities db = new BitirmeProjesiEntities();
        // GET: Yonetici
        public ActionResult YoneticiGiris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YoneticiGiris(TBLYoneticiler yon)
        {
            var bilgiler = db.TBLYoneticiler.FirstOrDefault(x => x.MAIL == yon.MAIL && x.SIFRE == yon.SIFRE);
            if (bilgiler != null)
            {
                TempData["yoneticiId"] = bilgiler.ID;
                return RedirectToAction("AnaSayfa", "Yonetici");
            }
            else
            {
                return View();
            }
        }
        public ActionResult AnaSayfa()
        {
            ViewBag.yoneticiId = TempData["yoneticiId"];
            TempData.Keep("yoneticiId");
            ViewBag.ogr = db.TBLOgretmenler.ToList();
            return View();
        }
        public ActionResult ExcelExport(int i,int ogretmenid)
        {
            ViewBag.ogretmenId = TempData["yoneticiId"];
            TempData.Keep("yoneticiId");
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            //ws.Cells["A1"].Value = "";
            if(i==0)
            {
                ws.Cells["A1"].Value = "Ogretmen";
                ws.Cells["G1"].Value = "Proje";
            }
            ws.Cells["B1"].Value = "İsim";
            ws.Cells["C1"].Value = "Telefon";
            ws.Cells["D1"].Value = "Mail";
            ws.Cells["E1"].Value = "Bölüm";
            ws.Cells["F1"].Value = "Numara";
            int sayi = ogretmenid;
            if (i == 1)
            {
                var liste = db.TBLIstekler.Where(x => x.APID == sayi && x.APONAY == 1);
                int row = 2;
                foreach (var item in liste)
                {
                  //  ws.Cells[string.Format("A{0}", row)].Value = row - 1;
                    ws.Cells[string.Format("B{0}", row)].Value = item.TBLOgrenciler.ISIM;
                    ws.Cells[string.Format("C{0}", row)].Value = item.TBLOgrenciler.TELEFON;
                    ws.Cells[string.Format("D{0}", row)].Value = item.TBLOgrenciler.MAIL;
                    ws.Cells[string.Format("E{0}", row)].Value = item.TBLOgrenciler.BOLUM;
                    ws.Cells[string.Format("F{0}", row)].Value = item.TBLOgrenciler.NUMARA;
                    row++;
                }
            }
            else if (i == 0 && sayi==0)
            {
                var ogr = db.TBLOgretmenler.ToList();
                int row = 2;
                
                foreach(var sira in ogr)
                {
                    var liste = db.TBLIstekler.Where(x => x.APID == sira.ID && x.APONAY == 1);
                    foreach (var item in liste)
                    {
                        //  ws.Cells[string.Format("A{0}", row)].Value = row - 1;
                        ws.Cells[string.Format("A{0}", row)].Value = sira.ISIM;
                        ws.Cells[string.Format("B{0}", row)].Value = item.TBLOgrenciler.ISIM;
                        ws.Cells[string.Format("C{0}", row)].Value = item.TBLOgrenciler.TELEFON;
                        ws.Cells[string.Format("D{0}", row)].Value = item.TBLOgrenciler.MAIL;
                        ws.Cells[string.Format("E{0}", row)].Value = item.TBLOgrenciler.BOLUM;
                        ws.Cells[string.Format("F{0}", row)].Value = item.TBLOgrenciler.NUMARA;
                        ws.Cells[string.Format("G{0}", row)].Value = "Araştırma Projesi";
                        row++;
                    }
                    var liste1 = db.TBLIstekler.Where(x => x.BPID == sira.ID && x.BPONAY == 1);
                    foreach (var item in liste1)
                    {
                        //  ws.Cells[string.Format("A{0}", row)].Value = row - 1;
                        ws.Cells[string.Format("A{0}", row)].Value = sira.ISIM;
                        ws.Cells[string.Format("B{0}", row)].Value = item.TBLOgrenciler.ISIM;
                        ws.Cells[string.Format("C{0}", row)].Value = item.TBLOgrenciler.TELEFON;
                        ws.Cells[string.Format("D{0}", row)].Value = item.TBLOgrenciler.MAIL;
                        ws.Cells[string.Format("E{0}", row)].Value = item.TBLOgrenciler.BOLUM;
                        ws.Cells[string.Format("F{0}", row)].Value = item.TBLOgrenciler.NUMARA;
                        ws.Cells[string.Format("G{0}", row)].Value = "Bitirme Projesi";
                        row++;
                    }
                }
                
            }
            else
            {
                var liste = db.TBLIstekler.Where(x => x.BPID == sayi && x.BPONAY == 1);
                int row = 2;
                foreach (var item in liste)
                {
                    //  ws.Cells[string.Format("A{0}", row)].Value = row - 1;
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
            return RedirectToAction("Anasayfa", "Yonetici");
        }

        public ActionResult OgretmenEkle()
        {
            ViewBag.ogretmenId = TempData["yoneticiId"];
            TempData.Keep("yoneticiId");
            return View();
        }
        [HttpPost]
        public ActionResult OgretmenEkle(TBLOgretmenler ogr)
        {
            ViewBag.ogretmenId = TempData["yoneticiId"];
            TempData.Keep("yoneticiId");
            var bilgiler = db.TBLOgretmenler.FirstOrDefault(x => x.MAIL == ogr.MAIL && x.SIFRE == ogr.SIFRE);
            if (bilgiler == null && ogr.MAIL != null)
            {
                db.TBLOgretmenler.Add(ogr);
                db.SaveChanges();
            }
            return RedirectToAction("Anasayfa", "Yonetici");
        }
        public ActionResult Mesaj()
        {
            ViewBag.yoneticiId = TempData["yoneticiId"];
            TempData.Keep("yoneticiId");
            var ogrenciler = db.TBLOgrenciler.ToList();
            ViewBag.ogrenciler = ogrenciler;
            var ogretmenler = db.TBLOgretmenler.ToList();
            ViewBag.ogretmenler = ogretmenler;
            var liste = db.TBLMesajlar.ToList();
            return View(liste);
        }
        public ActionResult Mesajlar(int id, int tablo)
        {
            ViewBag.yoneticiId = TempData["yoneticiId"];
            TempData.Keep("yoneticiId");
            var ogrenciler = db.TBLOgrenciler.ToList();
            ViewBag.ogrenciler = ogrenciler;
            var ogretmenler = db.TBLOgretmenler.ToList();
            ViewBag.ogretmenler = ogretmenler;
            ViewBag.id = id;
            ViewBag.tablo = tablo;
            var mesajlar = db.TBLMesajlar.ToList();
            return View(mesajlar);
        }
        public ActionResult MesajGonder(string mesaj, int id, int tablo)
        {
            ViewBag.yoneticiId = TempData["yoneticiId"];
            TempData.Keep("yoneticiId");
            int sayi = int.Parse(TempData["yoneticiId"].ToString());
            TBLMesajlar gidenMesaj = new TBLMesajlar();
            gidenMesaj.YONETICIID = sayi;
            if (tablo == 0)
            {
                gidenMesaj.OGRENCID = id;
            }
            else
            {
                gidenMesaj.OGRETMENID = id;
            }
            gidenMesaj.MESAJ = mesaj;
            gidenMesaj.MESAJYOLU = 2;
            db.TBLMesajlar.Add(gidenMesaj);
            db.SaveChanges();
            //return new RedirectResult(@"~\Ogretmen\Mesajlar\" + id);
            return RedirectToAction("Mesajlar", "Yonetici", new { id, tablo });
        }
        public ActionResult Profil()
        {
            ViewBag.yoneticiId = TempData["yoneticiId"];
            TempData.Keep("yoneticiId");
            var yonetici = db.TBLYoneticiler.Find(TempData["yoneticiID"]);
            return View(yonetici);
        }
        [HttpPost]
        public ActionResult Profil(TBLYoneticiler yonetici)
        {
            ViewBag.yoneticiId = TempData["yoneticiId"];
            TempData.Keep("yoneticiId");
            var degisme = db.TBLYoneticiler.Find(TempData["yoneticiID"]);
            degisme.ISIM = yonetici.ISIM;
            degisme.MAIL = yonetici.MAIL;
            degisme.SIFRE = yonetici.SIFRE;
            db.SaveChanges();
            return View(yonetici);
        }
        public ActionResult Ogrenciler()
        {
            ViewBag.yoneticiId = TempData["yoneticiId"];
            TempData.Keep("yoneticiId");
            var liste = db.TBLOgrenciler.ToList();
            return View(liste);
        }
        public ActionResult OgrenciEkle()
        {
            ViewBag.ogretmenId = TempData["yoneticiId"];
            TempData.Keep("yoneticiId");
            return View();
        }
        [HttpPost]
        public ActionResult OgrenciEkle(TBLOgrenciler ogr)
        {
            ViewBag.ogretmenId = TempData["yoneticiId"];
            TempData.Keep("yoneticiId");
            var bilgiler = db.TBLOgrenciler.FirstOrDefault(x => x.MAIL == ogr.MAIL && x.SIFRE == ogr.SIFRE);
            if (bilgiler == null && ogr.MAIL != null)
            {
                db.TBLOgrenciler.Add(ogr);
                db.SaveChanges();
            }
            return RedirectToAction("Ogrenciler", "Yonetici");
        }
        public ActionResult ProfilKontrol(int id,int deger)
        {
            ViewBag.ogretmenId = TempData["yoneticiId"];
            TempData.Keep("yoneticiId");
            ViewBag.deger = deger;
            if(deger==0)
            {
                var kim = db.TBLOgrenciler.Find(id);
                ViewBag.kim = kim;
                return View();
            }
            else
            {
                var kim = db.TBLOgretmenler.Find(id);
                ViewBag.kim = kim;
                return View();
            }
        }
    }
}