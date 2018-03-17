using System;

namespace MemoryGame.Business.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}
