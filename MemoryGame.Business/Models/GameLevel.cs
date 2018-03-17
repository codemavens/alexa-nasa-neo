using System;

namespace MemoryGame.Business.Models
{
    public class GameLevel
    {
        public int GameLevelId { get; set; }
        public string Name { get; set; }
        public int NumWords { get; set; }
        public string SuccessSoundUrl { get; set; }
        public string FailureSoundUrl { get; set; }

        // Foreign refs
        public int GameId { get; set; }
        public Game Game { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}
