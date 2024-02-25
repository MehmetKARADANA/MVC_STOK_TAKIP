using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_StokTakip.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MVC_StokTakip.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Db_MVC_StokTakipEntities db=new Db_MVC_StokTakipEntities();
        public ActionResult Index()
        {
            //var degerler = db.Tbl_Kategoriler.ToList();
            var degerler = db.Tbl_Kategoriler.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniKategori() { 
        return View();
        }


        [HttpPost]
        public ActionResult YeniKategori(Tbl_Kategoriler p1) {

            if (!ModelState.IsValid) {
                return View("YeniKategori");
            }
            db.Tbl_Kategoriler.Add(p1);//ado.net te insert into gibi add
            db.SaveChanges();
            return View();

        }

        public ActionResult Sil(int id) { 
            var kategori=db.Tbl_Kategoriler.Find(id);//ilişkisi olan kategorileri silmeye kalkma hata çıkar
            db.Tbl_Kategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }

        public ActionResult KategoriGetir(int id) {//href içinde getirildi id verisi

            var kategori = db.Tbl_Kategoriler.Find(id);
            return View("KategoriGetir",kategori);//sayfalar arası veri taşıma
        }
        //get post metodu yerine guncelle action resultında yaptık
        public ActionResult Guncelle(Tbl_Kategoriler p1) {//her action resul bir viewa bağlı olmak zorunda değil
           //p1 diğer viewdaki model post edildi
            var kategori = db.Tbl_Kategoriler.Find(p1.Kategoriid);
            kategori.KategoriAd=p1.KategoriAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }

    }
}