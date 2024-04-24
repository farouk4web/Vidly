namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "MoviePageOnImdb", c => c.String());
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Movies", "MoviePageOnImdb");
        }
    }
}
