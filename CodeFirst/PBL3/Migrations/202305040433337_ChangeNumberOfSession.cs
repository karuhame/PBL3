namespace PBL3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNumberOfSession : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lops", "LOP_NUMBERSESSION", c => c.Int());
            DropColumn("dbo.LoaiGois", "GOI_NUMBERSESSION");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoaiGois", "GOI_NUMBERSESSION", c => c.Int());
            DropColumn("dbo.Lops", "LOP_NUMBERSESSION");
        }
    }
}
