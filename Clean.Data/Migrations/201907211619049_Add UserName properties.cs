namespace Clean.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserNameproperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("init.RegisterModel", "UserName", c => c.String(nullable: false));
            DropColumn("init.RegisterModel", "Email");
        }
        
        public override void Down()
        {
            AddColumn("init.RegisterModel", "Email", c => c.String(nullable: false));
            DropColumn("init.RegisterModel", "UserName");
        }
    }
}
