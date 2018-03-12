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
        [TestCase(Key.Pitch.C, Scale.Ionian, 0)]
        [TestCase(Key.Pitch.A, Scale.Aeolian, 0)]
        [TestCase(Key.Pitch.F, Scale.Ionian, 1)]
        [TestCase(Key.Pitch.D, Scale.Aeolian, 1)]
        [TestCase(Key.Pitch.G, Scale.Ionian, 11)]
        [TestCase(Key.Pitch.E, Scale.Aeolian, 11)]
        [TestCase(Key.Pitch.BFlat, Scale.Ionian, 2)]
        [TestCase(Key.Pitch.G, Scale.Aeolian, 2)]
        [TestCase(Key.Pitch.D, Scale.Ionian, 10)]
        [TestCase(Key.Pitch.B, Scale.Aeolian, 10)]
        [TestCase(Key.Pitch.EFlat, Scale.Ionian, 3)]
        [TestCase(Key.Pitch.C, Scale.Aeolian, 3)]
        [TestCase(Key.Pitch.A, Scale.Ionian, 9)]
        [TestCase(Key.Pitch.FSharp, Scale.Aeolian, 9)]
        [TestCase(Key.Pitch.AFlat, Scale.Ionian, 4)]
        [TestCase(Key.Pitch.F, Scale.Aeolian, 4)]
        [TestCase(Key.Pitch.E, Scale.Ionian, 8)]
        [TestCase(Key.Pitch.CSharp, Scale.Aeolian, 8)]
        [TestCase(Key.Pitch.DFlat, Scale.Ionian, 5)]
        [TestCase(Key.Pitch.BFlat, Scale.Aeolian, 5)]
        [TestCase(Key.Pitch.B, Scale.Ionian, 7)]
        [TestCase(Key.Pitch.GSharp, Scale.Aeolian, 7)]
        [TestCase(Key.Pitch.GFlat, Scale.Ionian, 6)]
        [TestCase(Key.Pitch.EFlat, Scale.Aeolian, 6)]
        [TestCase(Key.Pitch.FSharp, Scale.Ionian, 6)]
        [TestCase(Key.Pitch.DSharp, Scale.Aeolian, 6)]
        [TestCase(Key.Pitch.CFlat, Scale.Ionian, 7)]
        [TestCase(Key.Pitch.AFlat, Scale.Aeolian, 7)]
        [TestCase(Key.Pitch.CSharp, Scale.Ionian, 5)]
        [TestCase(Key.Pitch.ASharp, Scale.Aeolian, 5)]
        [TestCase(Key.Pitch.FFlat, Scale.Ionian, 8)]
        [TestCase(Key.Pitch.DFlat, Scale.Aeolian, 8)]
        [TestCase(Key.Pitch.GSharp, Scale.Ionian, 4)]
        [TestCase(Key.Pitch.ESharp, Scale.Aeolian, 4)]
        public void TestSignature(Key.Pitch pitch, Scale scale, int flats)
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

        public static IEnumerable ComparisonTestCases
        {
            get
            {
                yield return new TestCaseData(Key.Pitch.C.Major(), Key.Pitch.D.Major()).Returns(Key.Pitch.D.Major());
                yield return new TestCaseData(Key.Pitch.C.Major(), Key.Pitch.E.Major()).Returns(Key.Pitch.E.Major());
                yield return new TestCaseData(Key.Pitch.C.Major(), Key.Pitch.F.Major()).Returns(Key.Pitch.F.Major());
                yield return new TestCaseData(Key.Pitch.C.Major(), Key.Pitch.G.Major()).Returns(Key.Pitch.C.Major());
                yield return new TestCaseData(Key.Pitch.C.Major(), Key.Pitch.A.Major()).Returns(Key.Pitch.C.Major());
                yield return new TestCaseData(Key.Pitch.C.Major(), Key.Pitch.B.Major()).Returns(Key.Pitch.C.Major());
               
                yield return new TestCaseData(Key.Pitch.D.Major(), Key.Pitch.C.Major()).Returns(Key.Pitch.C.Major());
                yield return new TestCaseData(Key.Pitch.D.Major(), Key.Pitch.E.Major()).Returns(Key.Pitch.E.Major());
                yield return new TestCaseData(Key.Pitch.D.Major(), Key.Pitch.F.Major()).Returns(Key.Pitch.F.Major());
                yield return new TestCaseData(Key.Pitch.D.Major(), Key.Pitch.G.Major()).Returns(Key.Pitch.D.Major());
                yield return new TestCaseData(Key.Pitch.D.Major(), Key.Pitch.A.Major()).Returns(Key.Pitch.D.Major());
                yield return new TestCaseData(Key.Pitch.D.Major(), Key.Pitch.B.Major()).Returns(Key.Pitch.D.Major());

                yield return new TestCaseData(Key.Pitch.E.Major(), Key.Pitch.C.Major()).Returns(Key.Pitch.E.Major());
                yield return new TestCaseData(Key.Pitch.E.Major(), Key.Pitch.D.Major()).Returns(Key.Pitch.E.Major());
                yield return new TestCaseData(Key.Pitch.E.Major(), Key.Pitch.F.Major()).Returns(Key.Pitch.F.Major());
                yield return new TestCaseData(Key.Pitch.E.Major(), Key.Pitch.G.Major()).Returns(Key.Pitch.G.Major());
                yield return new TestCaseData(Key.Pitch.E.Major(), Key.Pitch.A.Major()).Returns(Key.Pitch.A.Major());
                yield return new TestCaseData(Key.Pitch.E.Major(), Key.Pitch.B.Major()).Returns(Key.Pitch.E.Major());

                yield return new TestCaseData(Key.Pitch.F.Major(), Key.Pitch.C.Major()).Returns(Key.Pitch.F.Major());
                yield return new TestCaseData(Key.Pitch.F.Major(), Key.Pitch.D.Major()).Returns(Key.Pitch.F.Major());
                yield return new TestCaseData(Key.Pitch.F.Major(), Key.Pitch.E.Major()).Returns(Key.Pitch.F.Major());
                yield return new TestCaseData(Key.Pitch.F.Major(), Key.Pitch.G.Major()).Returns(Key.Pitch.G.Major());
                yield return new TestCaseData(Key.Pitch.F.Major(), Key.Pitch.A.Major()).Returns(Key.Pitch.A.Major());
                yield return new TestCaseData(Key.Pitch.F.Major(), Key.Pitch.B.Major()).Returns(Key.Pitch.B.Major());

                yield return new TestCaseData(Key.Pitch.G.Major(), Key.Pitch.C.Major()).Returns(Key.Pitch.C.Major());
                yield return new TestCaseData(Key.Pitch.G.Major(), Key.Pitch.D.Major()).Returns(Key.Pitch.G.Major());
                yield return new TestCaseData(Key.Pitch.G.Major(), Key.Pitch.E.Major()).Returns(Key.Pitch.G.Major());
                yield return new TestCaseData(Key.Pitch.G.Major(), Key.Pitch.F.Major()).Returns(Key.Pitch.G.Major());
                yield return new TestCaseData(Key.Pitch.G.Major(), Key.Pitch.A.Major()).Returns(Key.Pitch.A.Major());
                yield return new TestCaseData(Key.Pitch.G.Major(), Key.Pitch.B.Major()).Returns(Key.Pitch.B.Major());

                yield return new TestCaseData(Key.Pitch.A.Major(), Key.Pitch.C.Major()).Returns(Key.Pitch.A.Major());
                yield return new TestCaseData(Key.Pitch.A.Major(), Key.Pitch.D.Major()).Returns(Key.Pitch.A.Major());
                yield return new TestCaseData(Key.Pitch.A.Major(), Key.Pitch.E.Major()).Returns(Key.Pitch.E.Major());
                yield return new TestCaseData(Key.Pitch.A.Major(), Key.Pitch.F.Major()).Returns(Key.Pitch.F.Major());
                yield return new TestCaseData(Key.Pitch.A.Major(), Key.Pitch.G.Major()).Returns(Key.Pitch.G.Major());
                yield return new TestCaseData(Key.Pitch.A.Major(), Key.Pitch.B.Major()).Returns(Key.Pitch.A.Major());

                yield return new TestCaseData(Key.Pitch.B.Major(), Key.Pitch.C.Major()).Returns(Key.Pitch.C.Major());
                yield return new TestCaseData(Key.Pitch.B.Major(), Key.Pitch.E.Major()).Returns(Key.Pitch.E.Major());
                yield return new TestCaseData(Key.Pitch.B.Major(), Key.Pitch.F.Major()).Returns(Key.Pitch.F.Major());
                yield return new TestCaseData(Key.Pitch.B.Major(), Key.Pitch.G.Major()).Returns(Key.Pitch.G.Major());
                yield return new TestCaseData(Key.Pitch.B.Major(), Key.Pitch.A.Major()).Returns(Key.Pitch.A.Major());
                yield return new TestCaseData(Key.Pitch.B.Major(), Key.Pitch.B.Major()).Returns(Key.Pitch.B.Major());
            }
        }
    }
}
