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

       
    }
}
