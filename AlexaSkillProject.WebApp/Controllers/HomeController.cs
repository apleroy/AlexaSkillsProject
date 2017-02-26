using AlexaSkillProject.Domain;
using AlexaSkillProject.Services;
using AlexaSkillProject.WebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlexaSkillProject.WebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly IWordService _wordService;

        public HomeController(IWordService wordService)
        {
            _wordService = wordService;
        }

        public ActionResult Index()
        {
            Word word = _wordService.GetWordOfTheDay();
            WordViewModel wordViewModel = new WordViewModel(word);
            return View(wordViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}