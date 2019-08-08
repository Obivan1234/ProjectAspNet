namespace Clean.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adddescriptiontoprofileandimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("init.RegisterModel", "Description", c => c.String());
            AddColumn("init.RegisterModel", "imageData", c => c.Binary());
            DropColumn("init.RegisterModel", "Year");
        }
        
        public override void Down()
        {
            AddColumn("init.RegisterModel", "Year", c => c.Int(nullable: false));
            DropColumn("init.RegisterModel", "imageData");
            DropColumn("init.RegisterModel", "Description");
        }
    }
}
