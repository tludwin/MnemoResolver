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
    }
}
