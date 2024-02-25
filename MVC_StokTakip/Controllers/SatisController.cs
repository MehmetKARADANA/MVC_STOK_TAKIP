using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_StokTakip.Models.Entity;
using MVC_StokTakip.Models.CustomModel;
using PagedList;
using PagedList.Mvc;


namespace MVC_StokTakip.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Db_MVC_StokTakipEntities db=new Db_MVC_StokTakipEntities();
        SatisViewModel model1 = new SatisViewModel();
        public ActionResult Index()
        {
            var degerler=db.Tbl_Satislar.ToList();
            model1.Satis = degerler;
            List<SelectListItem> degerler2 = (from i in db.Tbl_Urunler.ToList()//selctlistitem text ve valuesi olan bir değer
                                             select new SelectListItem//listeye yeni selectlistitem ekle linq sorgusu
                                             {
                                                 Text = i.UrunAd,
                                                 Value = i.Urunid.ToString()
                                             }
                        ).ToList();
            List<SelectListItem> degerler3 = (from i in db.Tbl_Musteri.ToList()//selctlistitem text ve valuesi olan bir değer
                                              select new SelectListItem//listeye yeni selectlistitem ekle linq sorgusu
                                              {
                                                  Text = i.MusteriAd+" "+ i.MusteriSoyad,
                                                  Value = i.Musteriid.ToString()
                                              }
                       ).ToList();
            ViewBag.deger = degerler2;//bu veriyi viewa taşıyoruz
            ViewBag.deger3=degerler3;
            return View(model1);
            //html.textbox for kullanamdım çünkü iki model yollayamdım
        }//bu custom model sınıfıı falan hatırla

        [HttpGet]
        public ActionResult YeniSatis()
        {//linq sorgusunu burada yapınca hata verdi zaten get yukarııs
           
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(SatisViewModel p1)
        {
            db.Tbl_Satislar.Add(p1.yenisatis);
            db.SaveChanges();
            return Redirect("Index");//redirect değil view olursa hata veriyor
        }


    }
}