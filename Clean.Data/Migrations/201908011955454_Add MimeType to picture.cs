namespace Clean.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMimeTypetopicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Picture", "MimeType", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Picture", "MimeType");
        }
    }
}
