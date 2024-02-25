using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_StokTakip.Models.Entity;

namespace MVC_StokTakip.Models.CustomModel
{
    public class SatisViewModel
    {
        public List<Tbl_Satislar> Satis { get; set; }
        public Tbl_Satislar yenisatis { get; set; }
       
    }
}