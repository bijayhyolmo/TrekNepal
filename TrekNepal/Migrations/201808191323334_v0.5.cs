namespace TrekNepal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v05 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutUsContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Heading = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OnlineQueries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Email = c.String(),
                        ContactNumber = c.String(),
                        TripName = c.String(),
                        Query = c.String(),
                        PostedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TrekPackages", "FeaturedImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrekPackages", "FeaturedImage");
            DropTable("dbo.OnlineQueries");
            DropTable("dbo.AboutUsContents");
        }
    }
}
