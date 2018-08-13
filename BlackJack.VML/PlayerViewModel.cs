using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.VML
{
    public class PlayerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int WinsNumbers { get; set; }
        public string Status { get; set; }
        public string PlayerType { get; set; }
        public List<CardViewModel> CardsViewModelList { get; set; }

        public PlayerViewModel()
        {
            CardsViewModelList = new List<CardViewModel>();
        }
    }
}
