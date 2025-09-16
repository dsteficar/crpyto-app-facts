using Domain.Entity.Trading.Bots;
using Domain.Entity.Trading.Graphs;
using Domain.Entity.Users;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entity.Authentication
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<ParallelChannel> ParallelChannels { get; set; } = new List<ParallelChannel>();

        public virtual UserSettings UserSettings { get; set; }

        public virtual ICollection<TradingBotTask> TradeBotTasks { get; set; } = new List<TradingBotTask>();
    }
}
