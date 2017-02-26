using AlexaSkillProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexaSkillProject.WebApp.Models.ViewModels
{
    public class WordViewModel
    {
        public WordViewModel(Word word)
        {
            Word = word;
        }

        public Word Word { get; set; }
    }
}