namespace phirSOFT.MusicTheory
{
    public static class SignExtensions
    {
        public static int Flats(this sbyte signCode)
        {
            return (12 - signCode) % 12;
        }

        public static int Sharps(this sbyte signCode)
        {
            return signCode;
        }

        public static bool RecommendFlat(this sbyte signCode)
        {
            return signCode.Flats() < 7;
        }

        public static bool RecommendSharp(this sbyte signCode)
        {
            return signCode.Sharps() < 7;
        }
    }
}