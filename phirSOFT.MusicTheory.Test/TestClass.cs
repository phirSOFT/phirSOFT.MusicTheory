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
    }
}
