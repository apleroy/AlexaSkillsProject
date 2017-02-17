using AlexaSkillProject.Services;
using AlexaSkillProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearsonsApiWordCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            // enter a list of words
            List<string> possibleWords = WordList.GetWords();

            List<string> validatedWords = new List<string>();

            // send the request to pearson api
            PearsonsDictionaryApiService apiService = new PearsonsDictionaryApiService();

            // check if the response is the same part of speech and that a definition exists
            foreach (string word in possibleWords)
            {
                Console.WriteLine("Checking word: " + word);
                Dictionary<WordEnum, string> responseDictionary = apiService.GetWordDictionaryFromString(word);
                if (responseDictionary[WordEnum.Word]!= null && responseDictionary[WordEnum.Word].Equals(word)
                    && responseDictionary[WordEnum.WordDefinition] != null
                    && responseDictionary[WordEnum.WordExample] != null)
                {
                    validatedWords.Add(word);
                }
            }

            Console.WriteLine("Validated Words:");
            foreach(string word in validatedWords)
            {
                Console.WriteLine(word);
            }

            Console.ReadLine();

            // get this list of words (to add into application)
        }
    }
}
