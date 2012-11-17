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
        public string LettersToOmit = "[qeyuiohgaxcvQEYUIOHGAXCV]*";
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
            return "^" + LettersToOmit + string.Join(LettersToOmit, search.Select(o => Converter[o.ToString()]).ToArray()) + LettersToOmit + "$";
        }

        public void SetConverterForLanguage(string language)
        {
            SetConverterForLanguage(language, "Converters.xml");
        }

        public void SetConverterForLanguage(string language, string converterFilePath)
        {
            using (var reader = XmlReader.Create(converterFilePath, new XmlReaderSettings
            {
                IgnoreWhitespace = true
            }))
            {
                reader.Read();
                while (reader.Read())
                {
                    if (LanguageSettingsExist(language, reader))
                    {
                        UpdateConverter(reader);
                        return;
                    }
                }
            }
        }

        private static bool LanguageSettingsExist(string language, XmlReader reader)
        {
            return reader.NodeType == XmlNodeType.Element && reader.Name == "Converter"
                && reader["language"] == language;
        }

        private void UpdateConverter(XmlReader reader)
        {
            SetConverterValues(reader);
            SetLettersToOmit(reader);
        }

        private void SetConverterValues(XmlReader reader)
        {
            string attribute = reader["mapping"];

            if (!string.IsNullOrEmpty(attribute))
            {
                var converterValues = attribute.Split(',');

                for (int i = 0; i < 10; i++)
                {
                    Converter[i.ToString()] = converterValues[i].Trim();
                }
            }
        }

        private void SetLettersToOmit(XmlReader reader)
        {
            string attribute = reader["lettersToOmit"];

            if (!string.IsNullOrEmpty(attribute))
            {
                LettersToOmit = attribute;
            }
        }

        //public void SetConverterForLanguage(string language, string converterFilePath)
        //{
        //    using (var reader = XmlReader.Create(converterFilePath, new XmlReaderSettings
        //                                                            {
        //                                                                IgnoreWhitespace = true
        //                                                            }))
        //    {
        //        reader.Read();
        //        while (reader.Read())
        //        {
        //            if (LanguageSettingsExits(language, reader))
        //            {
        //                UpdateConverter(reader);
        //            }
        //        }
        //    }
        //}

        //private static bool LanguageSettingsExits(string language, XmlReader reader)
        //{
        //    return reader.NodeType == XmlNodeType.Element && reader.Name == language &&
        //                            reader.Read();
        //}

        //private void UpdateConverter(XmlReader reader)
        //{
        //    if (reader.Name == "Mapping" && reader.Read())
        //    {
        //        SetConverterValues(reader);
        //    }
        //    else if (reader.Name == "LettersToOmit" && reader.Read())
        //    {
        //        LettersToOmit = reader.Value.Trim();
        //    }
        //}

        //private void SetConverterValues(XmlReader reader)
        //{
        //    var converterValues = reader.Value.Trim().Split(',');

        //    for (int i = 0; i < 10; i++)
        //    {
        //        Converter[i.ToString()] = converterValues[i].Trim();
        //    }
        //}
    }
}
