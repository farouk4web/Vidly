namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewPropsToMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "RunningTime", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "ImageSrc", c => c.String());
            AddColumn("dbo.Movies", "MovieFileSrc", c => c.String());
            AddColumn("dbo.Movies", "Language", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Language");
            DropColumn("dbo.Movies", "MovieFileSrc");
            DropColumn("dbo.Movies", "ImageSrc");
            DropColumn("dbo.Movies", "RunningTime");
        }
    }
}
