﻿
using bandienthoai.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bandienthoai.Models.DAO
{
    public class BannerDAO
    {
        QlBanHangDbContext db = null;
        public BannerDAO()
        {
            db = new QlBanHangDbContext();
        }
        public List<SLIDE> GetListBanner(int index)
        {
            return db.SLIDEs.Where(x => x.STATUS == 1).OrderBy(y => y.DISPLAYORDER).ToList();
        
           
        }
        public BANNER GetLogo()
        {
            var kq = new BANNER();
           kq=   db.BANNERs.SingleOrDefault(x => x.IS_DELETE == false && x.VITRI == 1);
            //if (kq == null)
            //    kq.HINH = "\\Data\\slides\\logo-mobile.png";
            return kq;


        }
        public List<BANNER> GetBanner(int index)
        {
            return db.BANNERs.Where(x => x.IS_DELETE == false&&x.VITRI==index).ToList();


        }
    }
}