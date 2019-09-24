using NUnit.Framework;
using DollarAddresses;
using System;

namespace Tests
{
    public class AddressValCompareTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EmptyTest()
        {
            Boolean a = Program.AddressValCompare(0, "");
            Assert.IsTrue(a);
        }

        [Test]
        public void FailTest()
        {
            Boolean a = Program.AddressValCompare(7, "Apples");
            Assert.IsFalse(a);
        }

        [Test]
        public void NormalTest()
        {
            Boolean a = Program.AddressValCompare(110, "Quitter");
            Assert.IsTrue(a);
        }

        [Test]
        public void NegativeTest()
        {
            Boolean a = Program.AddressValCompare(-9, "Quitter");
            Assert.IsFalse(a);
        }
    }

    public class ConfigBuilderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NullTest()
        {
            var a = Program.ConfigBuilder();
            Assert.IsTrue(a != null);
        }
    }
}