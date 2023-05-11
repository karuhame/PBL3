using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web.Services.Description;

namespace PBL3_2.Models
{
    public class DBGym : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'PBL3_2.Models.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public DBGym()
            : base("name=Model1")
        {
            Database.SetInitializer<DBGym>(new CreateDB());
        }


        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountInfo> AccountInfos { get; set; }
        public virtual DbSet<BienLai> BienLais { get; set; }
        public virtual DbSet<LoaiGoi> LoaiGois { get; set; }
        public virtual DbSet<Lop> Lops { get; set; }
        public virtual DbSet<PhienTap> PhienTaps { get; set; }
        public virtual DbSet<ThietBi> ThietBis { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.Lops)
                .WithMany(e => e.Accounts);
        }


        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}