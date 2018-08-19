using System;

namespace TrekNepal.Models
{
    public class OnlineQuery
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public string TripName { get; set; }

        public string Query { get; set; }

        public DateTime PostedDate { get; set; }
    }
}