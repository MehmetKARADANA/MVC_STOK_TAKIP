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
    public class UrunController : Controller
    {
        // GET: Urun
        Db_MVC_StokTakipEntities db = new Db_MVC_StokTakipEntities();
        public ActionResult Index()
        {
            //var degerler = db.Tbl_Urunler.ToList();
            var degerler = db.Tbl_Urunler.ToList();
            return View(degerler);//öbür tarafta yakalanıp list yapılacak
        }

        [HttpGet]
        public ActionResult YeniUrun() {
            List<SelectListItem> degerler = (from i in db.Tbl_Kategoriler.ToList()//selctlistitem text ve valuesi olan bir değer
                                            select new SelectListItem//listeye yeni selectlistitem ekle linq sorgusu
                                            {
                                                Text=i.KategoriAd,
                                                Value=i.Kategoriid.ToString()
                                            }
                           ).ToList();
            ViewBag.deger=degerler;//bu veriyi viewa taşıyoruz
            return View(); 
        }

        [HttpPost]
        public ActionResult YeniUrun(Tbl_Urunler p1) {
            //var kategori=db.Tbl_Kategoriler.Where(i=>i.Kategoriid==p1.Tbl_Kategoriler.Kategoriid).FirstOrDefault();//i kategori
            //p1.Tbl_Kategoriler = kategori;//diğer veriler zaten input name ile eşlendi
            db.Tbl_Urunler.Add(p1);//DİREKT URUNKATEGORİYİ DÖNDERDİM 
            db.SaveChanges();
            return RedirectToAction("Index");//olmazsa eklemeden sonra hata alıyorum çöz
        }

        public ActionResult Sil(int id) //id buttondan iletildi
        {
            var urun = db.Tbl_Urunler.Find(id);
            db.Tbl_Urunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }

        public ActionResult UrunGetir(int id) {

            List<SelectListItem> degerler = (from i in db.Tbl_Kategoriler.ToList()//selctlistitem text ve valuesi olan bir değer
                                             select new SelectListItem//listeye yeni selectlistitem ekle linq sorgusu
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.Kategoriid.ToString()
                                             }
                          ).ToList();
            ViewBag.deger = degerler;//bu veriyi viewa taşıyoruz
            var urun = db.Tbl_Urunler.Find(id);
            return View("urunGetir",urun);
        }

        public ActionResult Guncelle(Tbl_Urunler p1) {

            var urun = db.Tbl_Urunler.Find(p1.Urunid);//[getpost]la değilde direkt başka bir aciona(buna post ettik viewdan)
            urun.UrunAd = p1.UrunAd;
            urun.Fiyat = p1.Fiyat;
            urun.Marka = p1.Marka;
            urun.Model= p1.Model;
            urun.Stok = p1.Stok;
            var kategori = db.Tbl_Kategoriler.Where(i=>i.Kategoriid==p1.Tbl_Kategoriler.Kategoriid).FirstOrDefault();
            urun.UrunKategori = kategori.Kategoriid;//inin kategoriid si p1 in kategoridisine eşit olan ilk değer
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }

    }
}