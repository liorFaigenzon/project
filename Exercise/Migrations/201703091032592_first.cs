namespace Exercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QueueNodeViewModels", "CustomerId_Id", "dbo.CustomerViewModels");
            DropIndex("dbo.QueueNodeViewModels", new[] { "CustomerId_Id" });
            AddColumn("dbo.QueueNodeViewModels", "CustomerName", c => c.String());
            AlterColumn("dbo.QueueNodeViewModels", "QueueNumber", c => c.Int(nullable: false));
            DropColumn("dbo.QueueNodeViewModels", "CustomerId_Id");
            DropTable("dbo.CustomerViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CustomerViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.QueueNodeViewModels", "CustomerId_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.QueueNodeViewModels", "QueueNumber", c => c.String());
            DropColumn("dbo.QueueNodeViewModels", "CustomerName");
            CreateIndex("dbo.QueueNodeViewModels", "CustomerId_Id");
            AddForeignKey("dbo.QueueNodeViewModels", "CustomerId_Id", "dbo.CustomerViewModels", "Id");
        }
    }
}
