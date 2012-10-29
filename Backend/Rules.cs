using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Backend
{
    public sealed class Rules
    {
        public readonly string CharactersToOmit = "[qeyuiohgaxcvQEYUIOHGAXCV]*";
        public readonly string NumbersRule = "^[0123456789]*$";

        public Dictionary<string, string> Converter = new Dictionary<string, string> 
        {
            { "0", "(s|z)" }, 
            { "1", "(t|d)" },
            { "2", "n" }, 
            { "3", "m" },
            { "4", "r" }, 
            { "5", "l" }, 
            { "6", "j" }, 
            { "7", "k" }, 
            { "8", "(f|w)" }, 
            { "9", "(p|b)" } 
        };

        public string GetPattern(string search)
        {
            return "^" + CharactersToOmit + string.Join(CharactersToOmit, search.Select(o => Converter[o.ToString()]).ToArray()) + CharactersToOmit + "$";
        }
    }
}
