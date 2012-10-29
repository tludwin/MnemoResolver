using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Backend;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class BackendTests
    {
        [Test]
        public void WhenNumberResolverIsCreatedWordsAreNotNull()
        {
            var backend = new NumberResolver();
            Assert.IsNotNull(backend.Words);
        }
    }
}
