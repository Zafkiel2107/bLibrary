﻿using bLibrary.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace bLibrary.DBContext
{
    public class BLibraryContext : IdentityDbContext
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