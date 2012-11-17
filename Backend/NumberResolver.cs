using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Backend
{
    public class NumberResolver
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
        }

        public Dictionary<string, IEnumerable<string>> Search(string textToSearch)
        {
            var results = new Dictionary<string, IEnumerable<string>>();
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

        public Dictionary<string, IEnumerable<string>> Search(string textToSearch, int i, Dictionary<string, IEnumerable<string>> previousResults)
        {
            string first;
            string last;

            while (i > 0)
            {
                first = textToSearch.Substring(0, i);
                last = textToSearch.Substring(i);

                if (previousResults.ContainsKey(first))
                {
                    return Search(last, last.Length, previousResults);
                }

                var result = SearchUsingNumbers(first);
                if (result.Count() > 0)
                {
                    previousResults.Add(first, result.ToList());
                    return Search(last, last.Length, previousResults);
                }

                i--;
            }

            return previousResults;
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
