using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PBL3_2.Models
{
    public class CreateDB : DropCreateDatabaseIfModelChanges<DBGym>
    {
        protected override void Seed(DBGym context)
        {

            context.ThietBis.Add(new ThietBi()
            {
                THIETBI_NAME = "Dumbell 10kg",
                THIETBI_NUM = 10,
                THIETBI_STATUS = 1
            });
            context.ThietBis.Add(new ThietBi()
            {
                THIETBI_NAME = "Dumbell 20kg",
                THIETBI_NUM = 10,
                THIETBI_STATUS = 1
            });
            context.SaveChanges();

        }
    }
}
