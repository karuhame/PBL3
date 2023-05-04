namespace PBL3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LopAccounts",
                c => new
                {
                    Lop_LOP_ID = c.Int(nullable: false),
                    Account_ACCOUNT_ID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Lop_LOP_ID, t.Account_ACCOUNT_ID });

            AddForeignKey("dbo.LopAccounts", "Lop_LOP_ID", "dbo.Lops", cascadeDelete: true);
            AddForeignKey("dbo.LopAccounts", "Account_ACCOUNT_ID", "dbo.Accounts", cascadeDelete: true);
            CreateIndex("dbo.LopAccounts", new[] { "Lop_LOP_ID" });
            CreateIndex("dbo.LopAccounts", new[] { "Account_ACCOUNT_ID" });
            AddColumn("dbo.Accounts", "ACCOUNT_CONFIRMEDPASSWORD", c => c.String());
            AddColumn("dbo.Accounts", "Lop_LOP_ID", c => c.Int());
            AddColumn("dbo.Lops", "LOP_TYPE", c => c.String());
            AddColumn("dbo.Lops", "Staff_ACCOUNT_ID", c => c.Int());
            AddColumn("dbo.Lops", "Account_ACCOUNT_ID", c => c.Int());
            CreateIndex("dbo.Accounts", "Lop_LOP_ID");
            CreateIndex("dbo.Lops", "Staff_ACCOUNT_ID");
            CreateIndex("dbo.Lops", "Account_ACCOUNT_ID");
            AddForeignKey("dbo.Accounts", "Lop_LOP_ID", "dbo.Lops", "LOP_ID");
            AddForeignKey("dbo.Lops", "Staff_ACCOUNT_ID", "dbo.Accounts", "ACCOUNT_ID");
            AddForeignKey("dbo.Lops", "Account_ACCOUNT_ID", "dbo.Accounts", "ACCOUNT_ID");
        }
        
        public override void Down()
        {
            DropTable("dbo.LopAccounts");
            DropForeignKey("dbo.Lops", "Account_ACCOUNT_ID", "dbo.Accounts");
            DropForeignKey("dbo.Lops", "Staff_ACCOUNT_ID", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "Lop_LOP_ID", "dbo.Lops");
            DropIndex("dbo.Lops", new[] { "Account_ACCOUNT_ID" });
            DropIndex("dbo.Lops", new[] { "Staff_ACCOUNT_ID" });
            DropIndex("dbo.Accounts", new[] { "Lop_LOP_ID" });
            DropColumn("dbo.Lops", "Account_ACCOUNT_ID");
            DropColumn("dbo.Lops", "Staff_ACCOUNT_ID");
            DropColumn("dbo.Lops", "LOP_TYPE");
            DropColumn("dbo.Accounts", "Lop_LOP_ID");
            DropColumn("dbo.Accounts", "ACCOUNT_CONFIRMEDPASSWORD");
            DropIndex("dbo.LopAccounts", "Account_ACCOUNT_ID");
            DropIndex("dbo.LopAccounts", "Lop_LOP_ID");
            DropForeignKey("dbo.LopAccounts", "Account_ACCOUNT_ID");
            DropForeignKey("dbo.Accounts", "ACCOUNT_ID");
            DropForeignKey("dbo.LopAccounts", "Lop_LOP_ID");
            DropForeignKey("dbo.Lops", "LOP_ID");
        }
    }
}
