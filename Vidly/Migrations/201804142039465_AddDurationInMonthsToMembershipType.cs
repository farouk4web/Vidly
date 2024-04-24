namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDurationInMonthsToMembershipType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "DurationInMonths", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "DurationInMonths");
        }
    }
}
