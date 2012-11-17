using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Backend
{
    public sealed class NumberResolver
    {
        public DictionarySource DictionarySource { get; set; }
        public Rules Rules { get; set; }

        private List<string> _words;
        public List<string> Words
        {
            get
            {
                if (_words == null)
                {
                    _words = DictionarySource.GetWords();
                }
                return _words;
            }
            set
            {
                _words = value;
            }
        }

        public NumberResolver()
        {
            DictionarySource = new DictionarySource();
            Rules = new Rules();
        }

        public NumberResolver(string dictionaryPath)
        {
            DictionarySource = new DictionarySource(dictionaryPath);
            Rules = new Rules();
        }

        public List<Result> Search(string textToSearch)
        {
            var results = new List<Result>();
            if (ContainsOnlyNumbers(textToSearch))
            {
                return Search(textToSearch, textToSearch.Length, results);
            }

            return results;
        }

        private bool ContainsOnlyNumbers(string textToSearch)
        {
            var numbersRule = new Regex(Rules.NumbersRule);
            return numbersRule.IsMatch(textToSearch);
        }

        public List<Result> Search(string textToSearch, int i, List<Result> previousResults)
        {
            string first;
            string last;

            while (i > 0)
            {
                first = textToSearch.Substring(0, i);
                last = textToSearch.Substring(i);

                if (AddIfExists(first, previousResults))
                {
                    return Search(last, last.Length, previousResults);
                }

                var result = SearchUsingNumbers(first);
                if (result.Count() > 0)
                {
                    previousResults.Add(new Result { Number = first, Words = result });
                    return Search(last, last.Length, previousResults);
                }

                i--;
            }

            return previousResults;
        }

        private bool AddIfExists(string resolvedNumber, List<Result> results)
        {
            var existing = results.Where(o => o.Number == resolvedNumber);

            if (existing.Count() > 0)
            {
                results.Add(existing.First());
                return true;
            }
            return false;
        }

        public IEnumerable<string> SearchUsingNumbers(string search)
        {
            var result = new List<string>();
            string pattern = Rules.GetPattern(search);
            var regex = new Regex(pattern);
            foreach (var word in Words)
            {
                if (regex.IsMatch(word))
                {
                    yield return word;
                }
            }
        }
    }
}
