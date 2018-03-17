using System;

namespace MemoryGame.Business.Models
{
    public class WordList
    {
        public int WordListId { get; set; }
        public string Words { get; set; }
        public bool? Randomize { get; set; } = false;

        // Foreign refs
        public int GameId { get; set; }
        public Game Game { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}
