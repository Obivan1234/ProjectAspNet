namespace Clean.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adddescriptiontoprofile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Description", c => c.String());
            AddColumn("dbo.AspNetUsers", "imageData", c => c.Binary());
            DropColumn("dbo.AspNetUsers", "Year");
            DropColumn("dbo.AspNetUsers", "MyOwnId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "MyOwnId", c => c.String());
            AddColumn("dbo.AspNetUsers", "Year", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "imageData");
            DropColumn("dbo.AspNetUsers", "Description");
        }
    }
}
