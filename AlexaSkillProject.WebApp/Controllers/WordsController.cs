using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlexaSkillProject.Domain;
using AlexaSkillProject.Repository;
using AlexaSkillProject.WebApp.Models;
using System.Text.RegularExpressions;
using AlexaSkillProject.Services;

namespace AlexaSkillProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WordsController : Controller
    {
        
        private readonly IWordService _wordService;

        public WordsController(IWordService wordService)
        {
            _wordService = wordService;
        }
        
        // GET: Words
        public ActionResult Index()
        {
            return View(_wordService.Words());
        }


        [HttpGet]
        public ActionResult BatchInsert()
        {
            WordListViewModel viewModel = new WordListViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BatchInsert(WordListViewModel wordViewModel)
        {
            string[] wordList = wordViewModel.WordList.Split(
                new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<Exception> exceptionList = new List<Exception>();
            List<string> failedWords = new List<string>();

            foreach(string word in wordList)
            {
                if (word != null)
                {
                    try
                    {
                        _wordService.GetAndSaveWordInformation(word.ToLower());
                    }
                    catch (Exception e)
                    {
                        failedWords.Add(word);
                        exceptionList.Add(e);
                    }
                }
            }

            wordViewModel.WordList = string.Join(string.Empty, failedWords);

            return View(wordViewModel);
        }

        // GET: Words/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Word word = _wordService.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // GET: Words/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Words/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WordName,PartOfSpeech,Definition,Example,WordOfTheDay")] Word word)
        {
            if (ModelState.IsValid)
            {
                _wordService.AddWord(word);
                return RedirectToAction("Index");
            }

            return View(word);
        }

        // GET: Words/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Word word = _wordService.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // POST: Words/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WordName,PartOfSpeech,Definition,Example,WordOfTheDay")] Word word)
        {
            if (ModelState.IsValid)
            {

                _wordService.UpdateWord(word);
                return RedirectToAction("Index");
            }
            return View(word);
        }

        // GET: Words/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Word word = _wordService.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // POST: Words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Word word = _wordService.Find(id);
            _wordService.Remove(word);
            return RedirectToAction("Index");
        }


        // http://stackoverflow.com/questions/23765228/controllers-services-and-unit-of-work-should-i-really-dispose-database-contex
        // http://cpratt.co/idisposable-and-dependency-injection/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _wordService.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}
