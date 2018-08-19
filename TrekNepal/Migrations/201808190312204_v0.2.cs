namespace TrekNepal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "InstaLink", c => c.String());
            AddColumn("dbo.Settings", "Location", c => c.String());
            AddColumn("dbo.Settings", "DetailedLocation", c => c.String());
            DropColumn("dbo.Settings", "InstaLsink");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "InstaLsink", c => c.String());
            DropColumn("dbo.Settings", "DetailedLocation");
            DropColumn("dbo.Settings", "Location");
            DropColumn("dbo.Settings", "InstaLink");
        }
    }
}
