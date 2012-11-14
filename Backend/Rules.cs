using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Backend
{
    public sealed class Rules
    {
        public string CharactersToOmit = "[qeyuiohgaxcvQEYUIOHGAXCV]*";
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

        public Rules()
        {

        }

        public Rules(string language)
        {
            SetConverterForLanguage(language);
        }

        public Rules(string language, string converterFilePath)
        {
            SetConverterForLanguage(language, converterFilePath);
        }

        public string GetPattern(string search)
        {
            return "^" + CharactersToOmit + string.Join(CharactersToOmit, search.Select(o => Converter[o.ToString()]).ToArray()) + CharactersToOmit + "$";
        }

        public void SetConverterForLanguage(string language)
        {
            SetConverterForLanguage(language, "Converters.xml");
        }

        public void SetConverterForLanguage(string language, string converterFilePath)
        {
            using (var reader = XmlReader.Create(converterFilePath))
            {
                while (reader.Read())
                {
                    if (LanguageSettingsExits(language, reader))
                    {
                        UpdateConverter(reader);
                    }
                }
            }
        }

        private static bool LanguageSettingsExits(string language, XmlReader reader)
        {
            return reader.NodeType == XmlNodeType.Element && reader.Name == language &&
                                    reader.Read();
        }

        private void UpdateConverter(XmlReader reader)
        {
            if (reader.Name == "Mapping")
            {
                SetConverterValues(reader);
            }
            else if (reader.Name == "CharactersToOmit")
            {
                CharactersToOmit = reader.Value;
            }
        }

        private void SetConverterValues(XmlReader reader)
        {
            var converterValues = reader.Value.Split(',');

            for (int i = 0; i < 10; i++)
            {
                Converter[i.ToString()] = converterValues[i];
            }
        }
    }
}
