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
        string _lettersToOmit = "LettersToOmit rule";

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
            Assert.AreNotEqual(_lettersToOmit, rules.LettersToOmit);
        }

        [Test]
        public void WhenOnlyLettersToOmitAreProvidedOnlyLettersToOmitAreBeingChanged()
        {
            var rules = new Rules("only_letters_to_omit_test");

            TestWhenOnlyLettersToOmitAreProvidedOnlyLettersToOmitAreBeingChanged(rules);
        }

        [Test]
        public void WhenConverterFilePathAndOnlyLettersToOmitAreProvidedOnlyLettersToOmitAreBeingChanged()
        {
            var rules = new Rules("only_letters_to_omit_test", _converterFilePath);

            TestWhenOnlyLettersToOmitAreProvidedOnlyLettersToOmitAreBeingChanged(rules);
        }

        private void TestWhenOnlyLettersToOmitAreProvidedOnlyLettersToOmitAreBeingChanged(Rules rules)
        {
            for (int i = 0; i < 10; i++)
            {
                Assert.AreNotEqual(_expectedMapping[i], rules.Converter[i.ToString()]);
            }
            Assert.AreEqual(_lettersToOmit, rules.LettersToOmit);
        }

        [Test]
        public void WhenFullConverterIsProvidedConverterAndLettersToOmitAreBeingChanged()
        {
            var rules = new Rules("mapping_and_letters_to_omit_test");

            TestWhenFullConverterIsProvidedConverterAndLettersToOmitAreBeingChanged(rules);
        }

        [Test]
        public void WhenConverterFilePathAndFullConverterIsProvidedConverterAndLettersToOmitAreBeingChanged()
        {
            var rules = new Rules("mapping_and_letters_to_omit_test", _converterFilePath);

            TestWhenFullConverterIsProvidedConverterAndLettersToOmitAreBeingChanged(rules);
        }

        private void TestWhenFullConverterIsProvidedConverterAndLettersToOmitAreBeingChanged(Rules rules)
        {
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(_expectedMapping[i], rules.Converter[i.ToString()]);
            }
            Assert.AreEqual(_lettersToOmit, rules.LettersToOmit);
        }
    }
}
