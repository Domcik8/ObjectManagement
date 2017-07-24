namespace ObjectManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validation2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Principals", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Principals", "Password", c => c.String(nullable: false, maxLength: 60));
        }
    }
}
