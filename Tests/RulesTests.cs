using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Backend;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RulesTests
    {
        string _converterFilePath = "Converters.xml";

        List<string> _expectedMapping = new List<string> 
            {
                "(a|b)", "(c|d)", "e", "f", "g", "h", "i", "j", "(k|l)", "(m|n)"
            };
        string _charactersToOmit = "CharactersToOmit rule";

        [Test]
        public void WhenOnlyMappingIsProvidedOnlyConverterIsBeingChanged()
        {
            var rules = new Rules("only_mapping_test");

            TestWhenOnlyMappingIsProvidedOnlyConverterIsBeingChanged(rules);
        }

        [Test]
        public void WhenConverterFilePathAndOnlyMappingIsProvidedOnlyConverterIsBeingChanged()
        {
            var rules = new Rules("only_mapping_test", _converterFilePath);

            TestWhenOnlyMappingIsProvidedOnlyConverterIsBeingChanged(rules);
        }

        private void TestWhenOnlyMappingIsProvidedOnlyConverterIsBeingChanged(Rules rules)
        {
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(_expectedMapping[i], rules.Converter[i.ToString()]);
            }
            Assert.AreNotEqual(_charactersToOmit, rules.CharactersToOmit);
        }

        [Test]
        public void WhenOnlyCharactersToOmitAreProvidedOnlyCharactersToOmitAreBeingChanged()
        {
            var rules = new Rules("only_characters_to_omit_test");

            TestWhenOnlyCharactersToOmitAreProvidedOnlyCharactersToOmitAreBeingChanged(rules);
        }

        [Test]
        public void WhenConverterFilePathAndOnlyCharactersToOmitAreProvidedOnlyCharactersToOmitAreBeingChanged()
        {
            var rules = new Rules("only_characters_to_omit_test", _converterFilePath);

            TestWhenOnlyCharactersToOmitAreProvidedOnlyCharactersToOmitAreBeingChanged(rules);
        }

        private void TestWhenOnlyCharactersToOmitAreProvidedOnlyCharactersToOmitAreBeingChanged(Rules rules)
        {
            for (int i = 0; i < 10; i++)
            {
                Assert.AreNotEqual(_expectedMapping[i], rules.Converter[i.ToString()]);
            }
            Assert.AreEqual(_charactersToOmit, rules.CharactersToOmit);
        }

        [Test]
        public void WhenFullConverterIsProvidedConverterAndCharactersToOmitAreBeingChanged()
        {
            var rules = new Rules("mapping_and_characters_to_omit_test");

            TestWhenFullConverterIsProvidedConverterAndCharactersToOmitAreBeingChanged(rules);
        }

        [Test]
        public void WhenConverterFilePathAndFullConverterIsProvidedConverterAndCharactersToOmitAreBeingChanged()
        {
            var rules = new Rules("mapping_and_characters_to_omit_test", _converterFilePath);

            TestWhenFullConverterIsProvidedConverterAndCharactersToOmitAreBeingChanged(rules);
        }

        private void TestWhenFullConverterIsProvidedConverterAndCharactersToOmitAreBeingChanged(Rules rules)
        {
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(_expectedMapping[i], rules.Converter[i.ToString()]);
            }
            Assert.AreEqual(_charactersToOmit, rules.CharactersToOmit);
        }
    }
}
