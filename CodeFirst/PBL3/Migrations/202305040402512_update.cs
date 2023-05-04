namespace PBL3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoaiGois", "GOI_TYPE", c => c.String());
            AddColumn("dbo.LoaiGois", "GOI_FEE", c => c.Int(nullable: false));
            DropColumn("dbo.Lops", "LOP_TYPE");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lops", "LOP_TYPE", c => c.String());
            DropColumn("dbo.LoaiGois", "GOI_FEE");
            DropColumn("dbo.LoaiGois", "GOI_TYPE");
        }
    }
}
