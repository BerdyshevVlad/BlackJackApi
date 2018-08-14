﻿using BlackJack.BLL.Interfaces;
using BlackJack.BLL.Repositories;
using BlackJack.DAL.Entities;
using BlackJack.VML;
using Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlackJack.BLL.Services
{
    public class GameLogicService:IGamePlay
    {
        //Repository<Card> _cardRepository;
        Repository<Player> _playerRepository;
        Repository<Round> _roundRepository;
        IRepository<Card> _cardRepository;



        public GameLogicService(/*IRepository<Card> cardRepository*/)
        {
            //_cardRepository = cardRepository;
            _cardRepository= new Repository<Card>(new DAL.BlackJackContext());
            _playerRepository = new Repository<Player>(new DAL.BlackJackContext());
            _roundRepository=new Repository<Round>(new DAL.BlackJackContext());
        }


        private static int lastCard = 0;
        public async Task<CardViewModel> DrawCard()
        {
            int deckCount = 54;
            if (lastCard >= deckCount)
            {
                lastCard = 0;
            }
            var cardsList = await _cardRepository.Get();
            Card card = ((cardsList.ToList())[(cardsList.ToList()).Count - ++lastCard]);
            CardViewModel cardModel = new CardViewModel();
            cardModel = Mapp.MappCard(card);
            await _cardRepository.Delete(card.Id);
            await _cardRepository.Save();
            Thread.Sleep(100);
            return cardModel;
        }


        public async Task<PlayerViewModel> TakeCard(Player player, Card Card)
        {
            Player playerTmp = await _playerRepository.GetById(player.Id);
            playerTmp.Score += Card.CardValue;
            playerTmp.CardsList.Add(Card);

            PlayerViewModel playerModel = Mapp.MappPlayer(playerTmp);
            await _playerRepository.Save();
            return playerModel;
        }


        public async Task<List<PlayerViewModel>> HandOverCards()
        {
            List<PlayerViewModel> playerModelList = null;
            int handOutCardsFirstTime = 2;
            for (int i = 0; i < handOutCardsFirstTime; i++)
            {
                PlayerViewModel playerModel = null;
                playerModelList = new List<PlayerViewModel>();
                foreach (var item in await _playerRepository.Get())
                {
                    if (item.Score < 17)
                    {
                        CardViewModel drawnedCard = await DrawCard();
                        Card card = Mapp.MappCardModel(drawnedCard);
                        playerModel = await TakeCard(item, card);
                        playerModelList.Add(playerModel);
                        Thread.Sleep(200);
                    }
                }
            }

            var tmp = await _playerRepository.Get();

            return playerModelList;
        }


        public async Task<PlayerViewModel> ContinueOrDeny(Player player, Card card, string yesNo)
        {
            PlayerViewModel playerModel = new PlayerViewModel();
            if (player.Name == "You" && player.Score < 21 && player.Status != "Stop")
            {
                string yesOrNo = yesNo;
                if (yesOrNo == "y")
                {
                    playerModel = await TakeCard(player, card);
                }
                if (yesOrNo == "n")
                {
                    player.Status = "Stop";
                    await _playerRepository.Update(player);
                }
            }
            if (player.Name == "You" && player.Score >= 21)
            {
                var playerTmp = await _playerRepository.GetById(player.Id);
                playerTmp.Status = "Stop";
                await _playerRepository.Save();
            }
            if (player.Name != "You" && player.Score < 17)
            {
                playerModel = await TakeCard(player, card);

            }
            if (player.Name != "You" && player.Score >= 17)
            {
                var playerTmp = await _playerRepository.GetById(player.Id);
                playerTmp.Status = "Stop";

                await _playerRepository.Save();
            }

            return playerModel;
        }


        public async Task<List<PlayerViewModel>> PlayAgain(string yesOrNo)
        {
            List<PlayerViewModel> playerModelList = new List<PlayerViewModel>();
            if (yesOrNo == "n")
            {
                for (; ; )
                {
                    var playersList = (await _playerRepository.Get()).Where(x => x.Status != "Stop").ToList();
                    if (playersList.Count <= 0)
                    {
                        break;
                    }
                    for (int j = 0; j < playersList.Count; j++)
                    {
                        CardViewModel drawnedCard = await DrawCard();
                        Card card = Mapp.MappCardModel(drawnedCard);
                        PlayerViewModel playerModel = await ContinueOrDeny((playersList)[j], card, yesOrNo);
                    }
                }
            }
            if (yesOrNo == "y")
            {
                var playersList = (await _playerRepository.Get() as List<Player>).ToList().Where(x => x.Status != "Stop").ToList();
                for (int j = 0; j < playersList.Count; j++)
                {
                    CardViewModel drawnedCard = await DrawCard();
                    Card card = Mapp.MappCardModel(drawnedCard);
                    PlayerViewModel playerModel = await ContinueOrDeny((playersList)[j], card, yesOrNo);
                }
            }

            List<Player> players = await _playerRepository.Get() as List<Player>;
            playerModelList = Mapp.MappPlayer(players);

            var tmp = await _playerRepository.Get();

            return playerModelList;
        }


        public async Task<List<PlayerViewModel>> Winner()
        {
            var playerList = await _playerRepository.Get();
            var max = playerList.Where(x => x.Score <= 21).Max(x => x.Score);
            foreach (var item in playerList)
            {
                if (item.Score == max)
                {
                    item.WinsNumbers++;
                    await _playerRepository.Save();
                }
            }

            var tmp = playerList.Where(x => x.Score == max).ToList();
            List<PlayerViewModel> playerModel = Mapp.MappPlayer(tmp);
            return playerModel;
        }


        public async Task<List<PlayerViewModel>> RoundHistory()
        {
            Round round = new Round();
            var playerList = await _playerRepository.Get();
            foreach (var player in playerList)
            {
                round.PlayersList.Add(player);
            }

            foreach (var p in round.PlayersList)
            {
                foreach (var card in p.CardsList)
                {
                    var t = card;
                }
            }

            await _roundRepository.Insert(round);
            List<Round> roundsList = await _roundRepository.Get() as List<Round>;
            List<PlayerViewModel> playersModelList=null;

            foreach (var roundItem in roundsList)
            {
                playersModelList = Mapp.MappPlayer(roundItem.PlayersList.ToList());
            }

            return playersModelList;
        }
    }
}
