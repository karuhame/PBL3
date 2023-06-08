using PBL3_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBL3_2.Service
{
    public class QLThietBi
    {
        public List<ThietBi> GetTB()
        {
            DBGym db = new DBGym();
            return db.ThietBis.ToList();
        }
        public List<ThietBi> GetThietBiByName(string strSearchThietBi)
        {
            DBGym db = new DBGym();
            return db.ThietBis.Where(p => p.THIETBI_NAME.Contains(strSearchThietBi)).ToList();
        }
    }
}