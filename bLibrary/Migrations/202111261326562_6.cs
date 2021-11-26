namespace bLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GenreBooks", "Genre_GenreId", "dbo.Genres");
            DropForeignKey("dbo.GenreBooks", "Book_BookId", "dbo.Books");
            DropIndex("dbo.GenreBooks", new[] { "Genre_GenreId" });
            DropIndex("dbo.GenreBooks", new[] { "Book_BookId" });
            AddColumn("dbo.Books", "Genre_GenreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "Genre_GenreId");
            AddForeignKey("dbo.Books", "Genre_GenreId", "dbo.Genres", "GenreId", cascadeDelete: true);
            DropTable("dbo.GenreBooks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GenreBooks",
                c => new
                    {
                        Genre_GenreId = c.Int(nullable: false),
                        Book_BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_GenreId, t.Book_BookId });
            
            DropForeignKey("dbo.Books", "Genre_GenreId", "dbo.Genres");
            DropIndex("dbo.Books", new[] { "Genre_GenreId" });
            DropColumn("dbo.Books", "Genre_GenreId");
            CreateIndex("dbo.GenreBooks", "Book_BookId");
            CreateIndex("dbo.GenreBooks", "Genre_GenreId");
            AddForeignKey("dbo.GenreBooks", "Book_BookId", "dbo.Books", "BookId", cascadeDelete: true);
            AddForeignKey("dbo.GenreBooks", "Genre_GenreId", "dbo.Genres", "GenreId", cascadeDelete: true);
        }
    }
}
