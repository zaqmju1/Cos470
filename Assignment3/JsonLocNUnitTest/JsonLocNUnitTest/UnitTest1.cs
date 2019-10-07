using NUnit.Framework;
using A3ClassLibrary;
using static A3ClassLibrary.JsonLoc;
using System;

namespace Tests
{
    //Template
    //DateTime a = new DateTime(1991, 2, 20, 1, 2, 3);
    //DateTime b = new DateTime(1993, 5, 21, 3, 3, 3);
    //DateTime c = new DateTime(1993, 6, 20, 6, 3, 28);
    //DateTime d = new DateTime(1993, 6, 20, 6, 29, 1);
    //DateTime e = new DateTime(1993, 6, 20, 7, 2, 22);
    //DateTime f = new DateTime(1993, 6, 20, 8, 2, 11);
    //DateTime g = new DateTime(1993, 6, 21, 1, 22, 49);
    //DateTime h = new DateTime(1993, 6, 21, 2, 23, 44);
    //DateTime i = new DateTime(1993, 6, 21, 8, 2, 10);
    //DateTime j = new DateTime(1993, 6, 28, 9, 9, 9);

    //Loc A = null;
    //Loc B = new Loc(a, 12, 13);
    //Loc C = new Loc(b, 13, 12);
    //Loc D = new Loc(c, 22, 14);
    //Loc E = new Loc(d, 21, 13);
    //Loc F = new Loc(e, 53, 12);
    //Loc G = new Loc(f, 52, 15);
    //Loc H = new Loc(g, 53, 53);
    //Loc I = new Loc(h, 52, 49);
    //Loc J = new Loc(i, 60, 59);
    //Loc K = new Loc(j, 57, 24);

    //Loc[] aa = null;

    //Loc[] ab = new Loc[4];
    //ab[0] = A;
    //ab[1] = B;
    //ab[2] = C;
    //ab[3] = D;

    //Loc[] ac = new Loc[4];
    //ac[0] = B;
    //ac[1] = C;
    //ac[2] = D;
    //ac[3] = E;

    //Loc[] ad = new Loc[3];
    //ad[0] = F;
    //ad[1] = G;
    //ad[2] = H;

    //Loc[] ae = new Loc[5];
    //ae[0] = B;
    //ae[1] = C;
    //ae[2] = D;
    //ae[3] = E;
    //ae[4] = F;

    //Loc[] af = new Loc[5];
    //af[0] = G;
    //af[1] = H;
    //af[2] = I;
    //af[3] = J;
    //af[4] = K;


    public class AlibiTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NullTest()
        {
            Loc[] aa = null;

            Loc[] q = Alibi(aa, 1993, 6, 20);

            Assert.IsTrue(q == null);
        }

        [Test]
        public void SuccessTest()
        {
            DateTime a = new DateTime(1991, 2, 20, 1, 2, 3);
            DateTime b = new DateTime(1993, 5, 21, 3, 3, 3);
            DateTime c = new DateTime(1993, 6, 20, 6, 3, 28);

            Loc A = null;
            Loc B = new Loc(a, 12, 13);
            Loc C = new Loc(b, 13, 12);
            Loc D = new Loc(c, 22, 14);

            Loc[] ab = new Loc[4];
            ab[0] = A;
            ab[1] = B;
            ab[2] = C;
            ab[3] = D;

            Loc[] q = Alibi(ab, 1993, 6, 20);

            Assert.IsTrue(q.Length == 1);
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
            Loc[] aa = null;

            Loc[] locations1 = aa;
            Loc[] locations2 = aa;

            Loc q = HaveMet(locations1, locations2, 60, 20);

            Assert.IsTrue(q == null);
        }

        [Test]
        public void SuccessTest()
        {
            DateTime a = new DateTime(1991, 2, 20, 1, 2, 3);
            DateTime b = new DateTime(1993, 5, 21, 3, 3, 3);
            DateTime c = new DateTime(1993, 6, 20, 6, 3, 28);
            DateTime d = new DateTime(1993, 6, 20, 6, 29, 1);

            Loc A = null;
            Loc B = new Loc(a, 12, 13);
            Loc C = new Loc(b, 13, 12);
            Loc D = new Loc(c, 22, 14);
            Loc E = new Loc(d, 21, 13);

            Loc[] ab = new Loc[4];
            ab[0] = A;
            ab[1] = B;
            ab[2] = C;
            ab[3] = D;

            Loc[] ac = new Loc[4];
            ac[0] = B;
            ac[1] = C;
            ac[2] = D;
            ac[3] = E;

            Loc[] locations1 = ab;
            Loc[] locations2 = ac;

            Loc q = HaveMet(locations1, locations2, 60, 20);

            Assert.IsTrue(q.time.Year == 1993 && q.time.Month == 6 && q.time.Day == 20);
        }

    }
}