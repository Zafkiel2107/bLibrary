using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public Genre Genre { get; set; }
        public int Part { get; set; }
        public int Pages { get; set; }
        public Language Language { get; set; }
        public string Description { get; set; }
        public List<Review> Reviews;
    }
    public enum Genre : byte
    {

    }
    public enum Language : byte
    {

    }
}