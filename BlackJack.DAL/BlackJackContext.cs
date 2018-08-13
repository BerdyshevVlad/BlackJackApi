using BlackJack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.DAL
{
    public class BlackJackContext : DbContext
    {
        static BlackJackContext()
        {
            Database.SetInitializer<BlackJackContext>(new BlackJackDbInitializer());
        }

        public BlackJackContext() : base("BlackJackContext")
        { }
        public DbSet<Player> Players { get; set; }
        public DbSet<Card> PlayingCards { get; set; }
        public DbSet<ExceptionDetail> ExceptionDetails { get; set; }
    }

    public class BlackJackDbInitializer :DropCreateDatabaseAlways<BlackJackContext>
    {
        protected override void Seed(BlackJackContext context)
        {
        }
    }
}
