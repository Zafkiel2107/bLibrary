namespace bLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Author = c.String(nullable: false, maxLength: 128),
                        Part = c.Int(nullable: false),
                        Pages = c.Int(nullable: false),
                        Language = c.Byte(nullable: false),
                        Description = c.String(nullable: false, maxLength: 1024),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Status = c.Byte(nullable: false),
                        UserReview = c.String(nullable: false, maxLength: 1024),
                        IsRecommended = c.Boolean(nullable: false),
                        Book_BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Books", t => t.Book_BookId, cascadeDelete: true)
                .Index(t => t.Book_BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Book_BookId", "dbo.Books");
            DropIndex("dbo.Reviews", new[] { "Book_BookId" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Books");
        }
    }
}
