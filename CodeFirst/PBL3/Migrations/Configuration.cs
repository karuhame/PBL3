namespace PBL3.Migrations
{
    using PBL3.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PBL3.Models.DBGym>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PBL3.Models.DBGym context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //context.Database.ExecuteSqlCommand("DELETE FROM PhienTaps");
            //context.Database.ExecuteSqlCommand("DELETE FROM LoaiGois");
            //context.Database.ExecuteSqlCommand("DELETE FROM Lops");

            //context.PhienTaps.AddRange(new List<PhienTap>()
            //{
            //    new PhienTap() { PHIENTAP_DAY = DateTime.Today, PHIENTAP_START = 2, PHIENTAP_END =4},
            //    new PhienTap() { PHIENTAP_DAY = DateTime.Today, PHIENTAP_START = 4, PHIENTAP_END =6},
            //    new PhienTap() { PHIENTAP_DAY = DateTime.Today, PHIENTAP_START = 6, PHIENTAP_END =8},
            //});

            //context.LoaiGois.AddRange(new List<LoaiGoi>()
            //{
            //    new LoaiGoi() {GOI_TYPE = "Gym", GOI_FEE = 20000 },
            //    new LoaiGoi() {GOI_TYPE = "Yoga", GOI_FEE = 30000},
            //    new LoaiGoi() {GOI_TYPE = "Boxing", GOI_FEE = 300000 },
            //});

            //context.Lops.AddRange(new List<Lop>()
            //{
            //    new Lop() { LOP_START = DateTime.Today, LOP_END = DateTime.Today, LOP_NUMBERSESSION = 2,
            //                PhienTaps = new HashSet<PhienTap>(){
            //                    context.PhienTaps.OrderBy(p => p.PHIENTAP_ID).Skip(0).Take(1).FirstOrDefault(),
            //                    context.PhienTaps.OrderBy(p => p.PHIENTAP_ID).Skip(2).Take(1).FirstOrDefault() },
            //                LoaiGoi = context.LoaiGois.Where(p => p.GOI_TYPE == "Boxing").FirstOrDefault()
            //                },
            //     new Lop() { LOP_START = DateTime.Today, LOP_END = DateTime.Today, LOP_NUMBERSESSION = 3,
            //                PhienTaps = new HashSet<PhienTap>(){
            //                    context.PhienTaps.OrderBy(p => p.PHIENTAP_ID).Skip(0).Take(1).FirstOrDefault(),
            //                    context.PhienTaps.OrderBy(p => p.PHIENTAP_ID).Skip(1).Take(1).FirstOrDefault(),
            //                    context.PhienTaps.OrderBy(p => p.PHIENTAP_ID).Skip(2).Take(1).FirstOrDefault() },
            //                LoaiGoi = context.LoaiGois.Where(p => p.GOI_TYPE == "Gym").FirstOrDefault()

            //                }

            //});
            context.SaveChanges();
        }
    }
}
