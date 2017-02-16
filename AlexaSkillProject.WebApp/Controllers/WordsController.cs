﻿using System;
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
        //private AlexaSkillProjectDataContext db = new AlexaSkillProjectDataContext();

        private readonly IWordService _wordService;

        public WordsController(IWordService wordService)
        {
            _wordService = wordService;
        }
        
        // GET: Words
        public ActionResult Index()
        {
            //return View(db.Words.ToList());
            return View(_wordService.Words());
        }


        [HttpGet]
        public ActionResult BatchInsert()
        {
            WordViewModel viewModel = new WordViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BatchInsert(WordViewModel wordViewModel)
        {
            string[] wordList = wordViewModel.WordList.Split(
                new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach(string word in wordList)
            {
                if (word != null)
                {
                    try
                    {
                        _wordService.GetAndSaveWordInformation(word);
                    }
                    catch
                    {
                        // no need to do anything just ignore and continue (one off for admin only)
                    }
                }
            }

            return View();
        }

        // GET: Words/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Word word = db.Words.Find(id);
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
        public ActionResult Create([Bind(Include = "Id,WordName,PartOfSpeech,Definition,Example")] Word word)
        {
            if (ModelState.IsValid)
            {
                //db.Words.Add(word);
                //db.SaveChanges();
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
            //Word word = db.Words.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,WordName,PartOfSpeech,Definition,Example")] Word word)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(word).State = EntityState.Modified;
                //db.SaveChanges();
                
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
            //Word word = db.Words.Find(id);
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
            //Word word = db.Words.Find(id);
            //db.Words.Remove(word);
            //db.SaveChanges();
            Word word = _wordService.Find(id);
            _wordService.Remove(word);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
