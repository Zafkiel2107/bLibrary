using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bLibrary.Models
{
    public class Review
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public string UserReview { get; set; }
        public Book Book { get; set; }
    }
    public enum Status : byte
    {
        Буду_читать,
        Читаю,
        Прочитана,
        Не_дочитана
    }
}