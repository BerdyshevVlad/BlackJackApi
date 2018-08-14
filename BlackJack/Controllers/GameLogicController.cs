﻿using BlackJack.BLL.Interfaces;
using BlackJack.BLL.Services;
using BlackJack.DAL.Entities;
using BlackJack.VML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlackJack.Controllers
{
    public class GameLogicController : Controller
    {
        GameLogicService _gameLogic;
        IRepository<Card> _cardRepository;
        public GameLogicController()
        {
            //_gameLogic = new GameLogicService(_cardRepository);
            _gameLogic = new GameLogicService();
        }


        [HttpGet]
        //public async Task<JsonResult> HandOverCards()
        public async Task<ActionResult> HandOverCards()
        {
            List<PlayerViewModel> playersModelList = await _gameLogic.HandOverCards();
            return View(playersModelList);
            //return Json(playersModel, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<ActionResult> PlayAgain(string yesOrNo)
        {
            List<PlayerViewModel> playersModelList = await _gameLogic.PlayAgain(yesOrNo);
            return View(playersModelList);
        }

        public async Task<ActionResult> Winner()
        {
            List<PlayerViewModel> playersModelList = await _gameLogic.Winner();
            return View(playersModelList);
        }

        public async Task<ActionResult> RoundHistory()
        {
            List<PlayerViewModel> playersModelList = await _gameLogic.RoundHistory();
            return View(playersModelList);
        }
    }
}