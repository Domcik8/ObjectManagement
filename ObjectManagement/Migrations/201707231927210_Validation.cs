namespace ObjectManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        Due = c.DateTime(nullable: false),
                        Comment = c.String(maxLength: 255),
                        PrincipalID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.Principals", t => t.PrincipalID, cascadeDelete: true)
                .Index(t => t.PrincipalID);
            
            CreateTable(
                "dbo.Principals",
                c => new
                    {
                        PrincipalID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 60),
                        Password = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.PrincipalID)
                .Index(t => t.Username, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "PrincipalID", "dbo.Principals");
            DropIndex("dbo.Principals", new[] { "Username" });
            DropIndex("dbo.Items", new[] { "PrincipalID" });
            DropTable("dbo.Principals");
            DropTable("dbo.Items");
        }
    }
}
