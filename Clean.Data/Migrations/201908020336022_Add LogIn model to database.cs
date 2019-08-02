namespace Clean.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogInmodeltodatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Login",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "MyOwnId", c => c.String());
            AddColumn("dbo.Picture", "ApplicationUserMyId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Picture", "ApplicationUserMyId");
            AddForeignKey("dbo.Picture", "ApplicationUserMyId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Picture", "ApplicationUserMyId", "dbo.AspNetUsers");
            DropIndex("dbo.Picture", new[] { "ApplicationUserMyId" });
            DropColumn("dbo.Picture", "ApplicationUserMyId");
            DropColumn("dbo.AspNetUsers", "MyOwnId");
            DropTable("dbo.Login");
        }
    }
}
