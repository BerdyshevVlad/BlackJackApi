using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.DAL.Entities
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int WinsNumbers { get; set; }
        public string Status { get; set; }
        public string PlayerType { get; set; }
        public virtual ICollection<Card> CardsList { get; set; }

        public Player()
        {
            CardsList = new HashSet<Card>();
        }
    }
}
