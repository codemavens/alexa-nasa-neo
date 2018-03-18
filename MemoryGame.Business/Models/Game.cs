using System;
using System.Collections.Generic;

namespace MemoryGame.Business.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Descr { get; set; }
        public int SortOrder { get; set; }

        public List<GameLevel> Levels { get; set; }
        public WordList WordList { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
