using BlackJack.BLL.Repositories;
using BlackJack.DAL.Entities;
using BlackJack.VML;
using Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Services
{
    public class GameSetService
    {
        private Repository<Card> _cardRepository = new Repository<Card>(new DAL.BlackJackContext());
        private Repository<Player> _playerRepository = new Repository<Player>(new DAL.BlackJackContext());


        public GameSetService()
        {
        }


        public async Task SetBotCount(int playersCount)
        {
            //throw new Exception("MY");
            try
            {
                for (int i = 0; i < playersCount; i++)
                {

                    if (_playerRepository.IsExist($"Bot{i}") == false)
                    {
                        await _playerRepository.Insert(new Player { Name = $"Bot{i}", PlayerType="Bot"});
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<PlayerViewModel>> GetPlayers()
        {
            List<PlayerViewModel> gamePlayerViewModelList;
            List<Player> gamePlayer = new List<Player>();
            try
            {
                foreach (var player in await _playerRepository.Get())
                {
                    gamePlayer.Add(player);
                }

                gamePlayerViewModelList = Mapp.MappPlayer(gamePlayer);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return gamePlayerViewModelList;
        }


        public async Task<List<CardViewModel>> GetDeck()
        {
            List<Card> cardsList = new List<Card>();
            List<CardViewModel> cardsViewModel;
            int deckCount = 54;
            try
            {
                foreach (var card in await _cardRepository.Get())
                {

                    cardsList.Add(card);
                    if (cardsList.Count < deckCount)
                        continue;
                }
                cardsViewModel = Mapp.MappCard((cardsList.ToList()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cardsViewModel;
        }


        public async Task InitializePlayers()
        {
            var dealer = new Player();
            dealer.Name = "Dealer";
            dealer.PlayerType = "Dealer";
            var playerPerson = new Player();
            playerPerson.Name = "You";
            playerPerson.PlayerType = "Person";

            try
            {
                if (_playerRepository.IsExist(dealer.Name) == false && _playerRepository.IsExist(playerPerson.Name) == false)
                {
                    await _playerRepository.Insert(dealer);
                    await _playerRepository.Insert(playerPerson);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task SetDeck()
        {
            try
            {
                if (_cardRepository.IsExist() == false)
                {
                    var countOfDeckCards = 54;
                    for (int i = 0; i < countOfDeckCards; i++)
                    {
                        var cardValue = new Random().Next(1, 13);
                        if (cardValue == 11)
                        {
                            cardValue = 1;
                        }
                        if (cardValue == 12)
                        {
                            cardValue = 2;
                        }
                        if (cardValue == 13)
                        {
                            cardValue = 3;
                        }

                        await _cardRepository.Insert(new Card { CardValue = cardValue });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<CardViewModel>> ReSetDeck()
        {
            List<CardViewModel> cardsViewModelList;
            try
            {
                var playingCardList = await _cardRepository.Get();

                foreach (var card in playingCardList)
                {
                    await _cardRepository.Delete(card.Id);
                }

                await SetDeck();
                cardsViewModelList = await GetDeck();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return cardsViewModelList;
        }


        public async Task StartNewGame()
        {
            await ReSetDeck();
            foreach (var item in await _playerRepository.Get())
            {
                await _playerRepository.Delete(item.Id);
            }
        }
    }
}
