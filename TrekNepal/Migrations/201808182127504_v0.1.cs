namespace TrekNepal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Currency = c.String(),
                        ViberNumber = c.String(),
                        MobileNumber = c.String(),
                        LandlineNumber = c.String(),
                        FbLink = c.String(),
                        TwitterLink = c.String(),
                        InstaLsink = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Settings");
        }
    }
}
