namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAnnotationsFromPictureSrcInCustomer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "PersonalPictureSrc", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "PersonalPictureSrc", c => c.String(nullable: false));
        }
    }
}
