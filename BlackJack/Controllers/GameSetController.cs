using BlackJack.BLL.Interfaces;
using BlackJack.BLL.Services;
using BlackJack.VML;
using ExceptionLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlackJack.Controllers
{
    public class GameSetController :Controller
    {
        IGamePlay _gamePlay;
        GameSetService _gameSetService;
        public GameSetController()
        {
            _gameSetService = new GameSetService();
        }

        public GameSetController(IGamePlay gamePlay)
        {
            _gamePlay = gamePlay;
        }


        public async Task<ActionResult> SetDeck()
        {
            await _gameSetService.SetDeck();
            List<CardViewModel> cards = await _gameSetService.GetDeck();
            return View(cards);
        }


        //public async Task<ActionResult> ReSetDeck()
        //{
        //    await _gameSetService.ReSetDeck();
        //    List<PlayingCardViewModel> cards = await _gameSetService.GetDeck();
        //    return View("SetDeck", cards);
        //}


        [ExceptionLogger]
        public async Task<ActionResult> SetBotCount()
        {
            try
            {
                await _gameSetService.InitializePlayers();
                await _gameSetService.SetBotCount(3);
            }
            catch (Exception ex)
            {
                return View("Error", new string[] { ex.Message });
            }
            return View();
        }


        public async Task<ActionResult> ShowPlayers()
        {
            List<PlayerViewModel> players = await _gameSetService.GetPlayers();

            return View(players);
        }


        public async Task<ActionResult> StartNewGame()
        {
            await _gameSetService.StartNewGame();
            List<CardViewModel> cards = await _gameSetService.GetDeck();
            return View("SetDeck", cards);
        }
    }
}
