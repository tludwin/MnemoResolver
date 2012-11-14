using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Backend
{
    public sealed class DictionarySource
    {
        private string _dictionaryPath;

        public DictionarySource()
        {
            _dictionaryPath = "dictionary";
        }

        public DictionarySource(string dictionaryPath)
        {
            _dictionaryPath = dictionaryPath;
        }

        public List<string> GetWords()
        {
            return File.ReadAllLines(_dictionaryPath).ToList();
        }

        public void SaveDictionary(List<string> words)
        {
            File.WriteAllLines(_dictionaryPath, words);
        }
    }
}
