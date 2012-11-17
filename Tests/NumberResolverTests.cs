using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Backend;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class NumberResolverTests
    {
        [Test]
        public void WhenNumberResolverIsCreatedWordsAreNotNull()
        {
            var backend = new NumberResolver();
            Assert.IsNotNull(backend.Words);
        }

        [Test]
        public void ResolverFindsSingleDigitValues()
        {
            var backend = new NumberResolver
            {
                Rules = new Rules("NumberResolverTests")
            };

            var dictionary = backend.Search("0123456789");

            for (int i = 0; i < 10; i++)
            {
                Assert.IsTrue(dictionary.Any(o => o.Number == i.ToString()));
            }
        }

        [Test]
        public void ResolverFindsMultipleDigitValues()
        {
            var backend = new NumberResolver
            {
                Rules = new Rules("NumberResolverTests")
            };

            var dictionary = backend.Search("0019928837465");

            Assert.IsTrue(dictionary.Any(o => o.Number == "0019928837465"));
            Assert.IsTrue(dictionary.Where(o=> o.Number=="0019928837465").Any(o => o.Words.Contains("zsdpbnwfmkrjl")));
        }
    }
}
