namespace bLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            AlterColumn("dbo.Reviews", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Reviews", "User_Id");
            AddForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            AlterColumn("dbo.Reviews", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reviews", "User_Id");
            AddForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
