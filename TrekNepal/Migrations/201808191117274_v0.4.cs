namespace TrekNepal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v04 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrekPackages", "Night", c => c.Int(nullable: false));
            AddColumn("dbo.TrekPackages", "Day", c => c.Int(nullable: false));
            DropColumn("dbo.TrekPackages", "PackageDuration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrekPackages", "PackageDuration", c => c.Int(nullable: false));
            DropColumn("dbo.TrekPackages", "Day");
            DropColumn("dbo.TrekPackages", "Night");
        }
    }
}
