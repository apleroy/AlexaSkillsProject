using AlexaSkillProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Services
{
    // The IWordService is responsible for extracting and persisting word objects
    public interface IWordService
    {
        // CRUD methods
        List<Word> Words();
        void AddWord(Word word);
        Word Find(int? id);
        void Remove(Word word);
        void UpdateWord(Word word);

        // function specific
        Word FindWordByName(string word);

        // get a prefixed word from the repo
        Word GetWordOfTheDay();

        // get a random word from the repo
        Word GetRandomWord();

        // used with third party api to make and save request for a word
        // example is to go find a new word, pass it in and hit an api, save the response as a Word record
        void GetAndSaveWordInformation(string word);

        void Dispose();
    }
}
