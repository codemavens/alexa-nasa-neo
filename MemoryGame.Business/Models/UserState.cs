using System;

namespace MemoryGame.Business.Models
{
    public class UserState
    {
        public int UserStateId { get; set; }
        public int CurrentScore { get; set; }

        //Foreign refs
        public int UserId { get; set; }
        public User User { get; set; }
        public int GameLevelId { get; set; }
        public GameLevel GameLevel { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}
