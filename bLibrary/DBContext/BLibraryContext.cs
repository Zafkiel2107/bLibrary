using bLibrary.Models;
using System.Data.Entity;

namespace bLibrary.DBContext
{
    public class BLibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}