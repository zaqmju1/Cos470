using NUnit.Framework;
using A3ClassLibrary;
using static A3ClassLibrary.JsonLoc;
using System;

namespace Tests
{
    public class BuildLocArray
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InvalidPathTest()
        {
            Location[] a = BuildLocArray("Hello");

            Assert.IsNull(a);
        }

        [Test]
        public void SuccessTest()
        {
            String dir = System.IO.Directory.GetCurrentDirectory();
            dir = dir.Substring(0, dir.LastIndexOf("bin\\Debug\\netcoreapp2.1")) + "Sample.json";

            Location[] locations1 = BuildLocArray(dir);

            Assert.IsTrue(locations1[1].timestampMs == "1548894299192");
        }

        [Test]
        public void FailTest()
        {
            String dir = System.IO.Directory.GetCurrentDirectory();
            dir = dir.Substring(0, dir.LastIndexOf("bin\\Debug\\netcoreapp2.1")) + "Sample.json";

            Location[] locations1 = BuildLocArray(dir);

            Assert.IsFalse(locations1[1].timestampMs == "1548895839283");
        }
    }

    public class AlibiTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NullTest()
        {
            Location[] aa = null;

            Location[] q = Alibi(aa, ConvertL("1548895149104"), ConvertL("1548895390341"));

            Assert.IsNull(q);
        }

        [Test]
        public void SuccessTest()
        {
            String dir = System.IO.Directory.GetCurrentDirectory();
            dir = dir.Substring(0, dir.LastIndexOf("bin\\Debug\\netcoreapp2.1")) + "Sample.json";

            Location[] locations1 = BuildLocArray(dir);

            Location[] q = Alibi(locations1, 1548895149104, 1548895390341);

            Assert.IsTrue(q.Length == 3 && q[2].timestampMs != null);
        }

        [Test]
        public void FailTest()
        {
            String dir = System.IO.Directory.GetCurrentDirectory();
            dir = dir.Substring(0, dir.LastIndexOf("bin\\Debug\\netcoreapp2.1")) + "Sample.json";

            Location[] locations1 = BuildLocArray(dir);

            Location[] q = Alibi(locations1, ConvertL("1548895149104"), ConvertL("1548895390341"));

            Assert.IsFalse(q.Length == 6);
        }

    }

    public class HaveMetTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NullTest()
        {
            Location[] aa = null;

            Location[] locations1 = aa;
            Location[] locations2 = aa;

            Location q = HaveMet(locations1, locations2, 3600000, 20);

            Assert.IsNull(q);
        }

        [Test]
        public void SuccessTest()
        {
            String dir = System.IO.Directory.GetCurrentDirectory();
            dir = dir.Substring(0, dir.LastIndexOf("bin\\Debug\\netcoreapp2.1")) + "Sample.json";


            Location[] locations1 = BuildLocArray(dir);
            Location[] locations2 = BuildLocArray(dir);

            Location q = HaveMet(locations1, locations2, 3600000, 20);

            Assert.IsTrue(q.timestampMs == "1548895511286");
        }

        [Test]
        public void FailTest()
        {
            String dir = System.IO.Directory.GetCurrentDirectory();
            dir = dir.Substring(0, dir.LastIndexOf("bin\\Debug\\netcoreapp2.1")) + "Sample.json";


            Location[] locations1 = BuildLocArray(dir);
            Location[] locations2 = BuildLocArray(dir);

            Location q = HaveMet(locations1, locations2, 3600000, 20);

            Assert.IsFalse(q.timestampMs == "1548881511286");
        }

    }
}