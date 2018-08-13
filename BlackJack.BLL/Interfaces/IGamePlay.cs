using BlackJack.DAL.Entities;
using BlackJack.VML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Interfaces
{
    public interface IGamePlay
    {
        Task<CardViewModel> DrawCard();
        Task<PlayerViewModel> TakeCard(Player player, Card playingCard);
        Task<List<PlayerViewModel>> HandOverCards();
        Task<PlayerViewModel> ContinueOrDeny(Player player, Card card, string yesOrNo);
        Task<List<PlayerViewModel>> PlayAgain(string yesOrNo);
        Task<List<PlayerViewModel>> Winner();
    }
}
