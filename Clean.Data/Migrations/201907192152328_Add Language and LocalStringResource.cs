namespace Clean.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLanguageandLocalStringResource : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LanguageCulture = c.String(nullable: false),
                        UniqueSeoCode = c.String(nullable: false),
                        Published = c.Boolean(nullable: false),
                        Default = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LocalStringResource",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LanguageId = c.Int(nullable: false),
                        ResourceName = c.String(nullable: false, maxLength: 250),
                        ResourceValue = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocalStringResource", "LanguageId", "dbo.Language");
            DropIndex("dbo.LocalStringResource", new[] { "LanguageId" });
            DropTable("dbo.LocalStringResource");
            DropTable("dbo.Language");
        }
    }
}
