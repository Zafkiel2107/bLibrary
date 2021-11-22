using bLibrary.Models;
using System.Data.Entity;

namespace bLibrary.DBContext
{
    public class BLibraryContext : DbContext
    {
        public BLibraryContext() : base("DefaultConnection") { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public static BLibraryContext CreateContext()
        {
            return new BLibraryContext();
        }
    }
}