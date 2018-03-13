using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace phirSOFT.MusicTheory.Test
{
    [TestFixture]
    public class KeyTest
    {
        [Test]
        [TestCase(0, "C")]
        [TestCase(1, "D\u266D")]
        [TestCase(2, "D")]
        [TestCase(3, "E\u266D")]
        [TestCase(4, "E")]
        [TestCase(5, "F")]
        [TestCase(6, "F\u266F")]
        [TestCase(7, "G")]
        [TestCase(8, "A\u266D")]
        [TestCase(9, "A")]
        [TestCase(10, "B\u266D")]
        [TestCase(11, "B\u266E")]
        public void TestBase(int shift, string baseKey)
        {
            var key = new Key();
            key += Interval.FromSemitones((sbyte)shift);

            Assert.AreEqual(baseKey, key.BaseNote);
        }

        [Test]
        [TestCase(Pitch.C, Scale.Ionian, 0)]
        [TestCase(Pitch.A, Scale.Aeolian, 0)]
        [TestCase(Pitch.F, Scale.Ionian, 1)]
        [TestCase(Pitch.D, Scale.Aeolian, 1)]
        [TestCase(Pitch.G, Scale.Ionian, 11)]
        [TestCase(Pitch.E, Scale.Aeolian, 11)]
        [TestCase(Pitch.BFlat, Scale.Ionian, 2)]
        [TestCase(Pitch.G, Scale.Aeolian, 2)]
        [TestCase(Pitch.D, Scale.Ionian, 10)]
        [TestCase(Pitch.B, Scale.Aeolian, 10)]
        [TestCase(Pitch.EFlat, Scale.Ionian, 3)]
        [TestCase(Pitch.C, Scale.Aeolian, 3)]
        [TestCase(Pitch.A, Scale.Ionian, 9)]
        [TestCase(Pitch.FSharp, Scale.Aeolian, 9)]
        [TestCase(Pitch.AFlat, Scale.Ionian, 4)]
        [TestCase(Pitch.F, Scale.Aeolian, 4)]
        [TestCase(Pitch.E, Scale.Ionian, 8)]
        [TestCase(Pitch.CSharp, Scale.Aeolian, 8)]
        [TestCase(Pitch.DFlat, Scale.Ionian, 5)]
        [TestCase(Pitch.BFlat, Scale.Aeolian, 5)]
        [TestCase(Pitch.B, Scale.Ionian, 7)]
        [TestCase(Pitch.GSharp, Scale.Aeolian, 7)]
        [TestCase(Pitch.GFlat, Scale.Ionian, 6)]
        [TestCase(Pitch.EFlat, Scale.Aeolian, 6)]
        [TestCase(Pitch.FSharp, Scale.Ionian, 6)]
        [TestCase(Pitch.DSharp, Scale.Aeolian, 6)]
        [TestCase(Pitch.CFlat, Scale.Ionian, 7)]
        [TestCase(Pitch.AFlat, Scale.Aeolian, 7)]
        [TestCase(Pitch.CSharp, Scale.Ionian, 5)]
        [TestCase(Pitch.ASharp, Scale.Aeolian, 5)]
        [TestCase(Pitch.FFlat, Scale.Ionian, 8)]
        [TestCase(Pitch.DFlat, Scale.Aeolian, 8)]
        [TestCase(Pitch.GSharp, Scale.Ionian, 4)]
        [TestCase(Pitch.ESharp, Scale.Aeolian, 4)]
        public void TestSignature(Pitch pitch, Scale scale, int flats)
        {
            var key = Key.FromPitch(pitch, scale);
            Assert.AreEqual(flats, key.Signs.Flats());
            var sharps = (12 - flats) % 12;
            Assert.AreEqual(sharps, key.Signs.Sharps());
        }

        [Test]
        [TestCaseSource(typeof(KeyTest), nameof(ComparisonTestCases))]
        public Key TestComparison(Key left, Key right)
        {
            return Comparer<Key>.Default.Compare(left, right) < 0 ? right : left;
        }

        [Test]
        [TestCaseSource(typeof(KeyTest), nameof(ComparisonTestCases))]
        public Key TestComparisonNonGeneric(Key left, Key right)
        {
            return left.CompareTo((object) right) < 0 ? right : left;
        }

        [Test]
        [TestCaseSource(typeof(KeyTest), nameof(ComparisonTestCases))]
        public Key TestComparisonOperators_Less(Key left, Key right)
        {
            return left < right ? right : left;
        }

        [Test]
        [TestCaseSource(typeof(KeyTest), nameof(ComparisonTestCases))]
        public Key TestComparisonOperators_LessEq(Key left, Key right)
        {
            return left <= right ? right : left;
        }

        [Test]
        [TestCaseSource(typeof(KeyTest), nameof(ComparisonTestCases))]
        public Key TestComparisonOperators_Greater(Key left, Key right)
        {
            return left > right ? left : right;
        }

        [Test]
        [TestCaseSource(typeof(KeyTest), nameof(ComparisonTestCases))]
        public Key TestComparisonOperators_GreaterEq(Key left, Key right)
        {
            return left >= right ? left : right;
        }

        [Test]       
        public void TestComparisonNull([Range(0, 11)]int pitch, [Range(0, 7)] int scale)
        {
            var key = Key.FromPitch((Pitch) pitch, (Scale) scale);
            var cmp = Comparer.Default;
            Assert.Negative(cmp.Compare(null, key));
            Assert.Positive(cmp.Compare(key, null));
        }

        [Test]
        public void TestComparisonThrow([Values(typeof(object), typeof(int), typeof(Interval))] Type t ,[Range(0, 11)]int pitch, [Range(0, 7)] int scale)
        {
            var o = Activator.CreateInstance(t);
            var key = Key.FromPitch((Pitch) pitch, (Scale) scale);
            Assert.Throws<ArgumentException>(() => Comparer.Default.Compare(o, key));
        }

        public static IEnumerable ComparisonTestCases
        {
            get
            {

                // returns the higher pitch
                yield return new TestCaseData(Pitch.C.Major(), Pitch.D.Major()).Returns(Pitch.D.Major());
                yield return new TestCaseData(Pitch.C.Major(), Pitch.E.Major()).Returns(Pitch.E.Major());
                yield return new TestCaseData(Pitch.C.Major(), Pitch.F.Major()).Returns(Pitch.F.Major());
                yield return new TestCaseData(Pitch.C.Major(), Pitch.G.Major()).Returns(Pitch.C.Major());
                yield return new TestCaseData(Pitch.C.Major(), Pitch.A.Major()).Returns(Pitch.C.Major());
                yield return new TestCaseData(Pitch.C.Major(), Pitch.B.Major()).Returns(Pitch.C.Major());
               
                yield return new TestCaseData(Pitch.D.Major(), Pitch.C.Major()).Returns(Pitch.D.Major());
                yield return new TestCaseData(Pitch.D.Major(), Pitch.E.Major()).Returns(Pitch.E.Major());
                yield return new TestCaseData(Pitch.D.Major(), Pitch.F.Major()).Returns(Pitch.F.Major());
                yield return new TestCaseData(Pitch.D.Major(), Pitch.G.Major()).Returns(Pitch.G.Major());
                yield return new TestCaseData(Pitch.D.Major(), Pitch.A.Major()).Returns(Pitch.D.Major());
                yield return new TestCaseData(Pitch.D.Major(), Pitch.B.Major()).Returns(Pitch.D.Major());

                yield return new TestCaseData(Pitch.E.Major(), Pitch.C.Major()).Returns(Pitch.E.Major());
                yield return new TestCaseData(Pitch.E.Major(), Pitch.D.Major()).Returns(Pitch.E.Major());
                yield return new TestCaseData(Pitch.E.Major(), Pitch.F.Major()).Returns(Pitch.F.Major());
                yield return new TestCaseData(Pitch.E.Major(), Pitch.G.Major()).Returns(Pitch.G.Major());
                yield return new TestCaseData(Pitch.E.Major(), Pitch.A.Major()).Returns(Pitch.A.Major());
                yield return new TestCaseData(Pitch.E.Major(), Pitch.B.Major()).Returns(Pitch.E.Major());

                yield return new TestCaseData(Pitch.F.Major(), Pitch.C.Major()).Returns(Pitch.F.Major());
                yield return new TestCaseData(Pitch.F.Major(), Pitch.D.Major()).Returns(Pitch.F.Major());
                yield return new TestCaseData(Pitch.F.Major(), Pitch.E.Major()).Returns(Pitch.F.Major());
                yield return new TestCaseData(Pitch.F.Major(), Pitch.G.Major()).Returns(Pitch.G.Major());
                yield return new TestCaseData(Pitch.F.Major(), Pitch.A.Major()).Returns(Pitch.A.Major());
                yield return new TestCaseData(Pitch.F.Major(), Pitch.B.Major()).Returns(Pitch.B.Major());

                yield return new TestCaseData(Pitch.G.Major(), Pitch.C.Major()).Returns(Pitch.C.Major());
                yield return new TestCaseData(Pitch.G.Major(), Pitch.D.Major()).Returns(Pitch.G.Major());
                yield return new TestCaseData(Pitch.G.Major(), Pitch.E.Major()).Returns(Pitch.G.Major());
                yield return new TestCaseData(Pitch.G.Major(), Pitch.F.Major()).Returns(Pitch.G.Major());
                yield return new TestCaseData(Pitch.G.Major(), Pitch.A.Major()).Returns(Pitch.A.Major());
                yield return new TestCaseData(Pitch.G.Major(), Pitch.B.Major()).Returns(Pitch.B.Major());

                yield return new TestCaseData(Pitch.A.Major(), Pitch.C.Major()).Returns(Pitch.C.Major());
                yield return new TestCaseData(Pitch.A.Major(), Pitch.D.Major()).Returns(Pitch.D.Major());
                yield return new TestCaseData(Pitch.A.Major(), Pitch.E.Major()).Returns(Pitch.A.Major());
                yield return new TestCaseData(Pitch.A.Major(), Pitch.F.Major()).Returns(Pitch.A.Major());
                yield return new TestCaseData(Pitch.A.Major(), Pitch.G.Major()).Returns(Pitch.A.Major());
                yield return new TestCaseData(Pitch.A.Major(), Pitch.B.Major()).Returns(Pitch.B.Major());

                yield return new TestCaseData(Pitch.B.Major(), Pitch.C.Major()).Returns(Pitch.C.Major());
                yield return new TestCaseData(Pitch.B.Major(), Pitch.D.Major()).Returns(Pitch.D.Major());
                yield return new TestCaseData(Pitch.B.Major(), Pitch.E.Major()).Returns(Pitch.E.Major());
                yield return new TestCaseData(Pitch.B.Major(), Pitch.F.Major()).Returns(Pitch.B.Major());
                yield return new TestCaseData(Pitch.B.Major(), Pitch.G.Major()).Returns(Pitch.B.Major());
                yield return new TestCaseData(Pitch.B.Major(), Pitch.A.Major()).Returns(Pitch.B.Major());
            }
        }

     
    }
}
