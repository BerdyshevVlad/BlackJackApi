using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.DAL.Entities
{
    public class Round
    {
        [Key]
        public int Id { get; set; }

        public virtual ICollection<Player> PlayersList { get; set; }

        public Round()
        {
            PlayersList = new HashSet<Player>();
        }
    }
}
