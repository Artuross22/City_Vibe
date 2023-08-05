using System;

namespace City_Vibe.ViewModels.ApiBulletinBoard
{
    public class BulletinBoard
    {
        public string BulletinBoardId { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public string Category { get; set; } = string.Empty;

        public string Experience { get; set; } = string.Empty;

    }
}
