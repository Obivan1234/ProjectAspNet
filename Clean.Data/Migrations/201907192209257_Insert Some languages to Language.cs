namespace Clean.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertSomelanguagestoLanguage : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Language Values('Ukraine', 'uk-UA', 'ua', 'true', 0);");
            Sql("insert into Language Values('English', 'en-GB', 'en', 'true', null);");
        }
        
        public override void Down()
        {
            Sql("delete from Language " +
                "where [Name] = 'Ukraine' and LanguageCulture = 'uk-UA' and UniqueSeoCode = 'ua' and Published = 'true' and[Default] = 0; ");

            Sql("delete from Language " +
                "where [Name] = 'English' and LanguageCulture = 'en-GB' and UniqueSeoCode = 'en' and Published = 'true'; ");

        }
    }
}
