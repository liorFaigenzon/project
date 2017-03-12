namespace Exercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusTypeEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QueueNodeViewModels", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QueueNodeViewModels", "Status", c => c.String());
        }
    }
}
