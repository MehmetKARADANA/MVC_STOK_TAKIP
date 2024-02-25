using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_StokTakip.Models.Entity;
using PagedList;//kullanacağım model

namespace MVC_StokTakip.Controllers
{
    public class MusteriController : Controller
    {
        Db_MVC_StokTakipEntities db = new Db_MVC_StokTakipEntities();//db ye bağlantı oluşturdum
        public ActionResult Index(string p)
        {
            //var degerler = db.Tbl_Musteri.ToList();
            //var degerler = db.Tbl_Musteri.ToList().ToPagedList(sayfa,10);
            var degerler = from d in db.Tbl_Musteri select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m=>m.MusteriAd.Contains(p));
            }
            return View(degerler.ToList());//Bu satır, Index eyleminin sonucunda döndürülecek olan görünümü belirtir. degerler değişkeni,
                                  //Index.cshtml görünümüne model olarak iletilir.
                                  //Bu, görünümde müşteri verilerini göstermek için kullanılabilir.
        }


        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]//inputlar ile alacağız p1 i
        public ActionResult YeniMusteri(Tbl_Musteri p1)
        {

            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.Tbl_Musteri.Add(p1);
            db.SaveChanges();
            return View();

        }


        public ActionResult Sil(int id)
        {
            var musteri=db.Tbl_Musteri.Find(id);
            db.Tbl_Musteri.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id) //href içinde getirildi id verisi
        {//indexten musterigetier viewine veri taşıyoruz
            var musteri = db.Tbl_Musteri.Find(id);
            return View("MusteriGetir",musteri);

        }

        //guncelle actionu musterigetirin kullandığı ve post ettiği modeli parametre alıyor
        public ActionResult Guncelle(Tbl_Musteri p1)
        {
           var musteri= db.Tbl_Musteri.Find(p1.Musteriid);
            musteri.MusteriAd = p1.MusteriAd;
            musteri.MusteriSoyad = p1.MusteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }  
}