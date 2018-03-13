namespace phirSOFT.MusicTheory
{
    public static class SignExtensions
    {
        public static int Flats(this int signCode)
        {
            return (12 - signCode) % 12;
        }

        public static int Sharps(this int signCode)
        {
            return signCode;
        }

        public static bool RecommendFlat(this int signCode)
        {
            return signCode.Flats() < 7;
        }

        public static bool RecommendSharp(this int signCode)
        {
            return signCode.Sharps() < 7;
        }
    }
}