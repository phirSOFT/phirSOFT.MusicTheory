using System;
using System.Collections.Generic;
using System.Text;

namespace phirSOFT.MusicTheory
{
    public static class KeyExtensions
    {
        public static Key Major(this Pitch pitch)
        {
            return Key.FromPitch(pitch, Scale.Ionian);
        }

        public static Key Minor(this Pitch pitch)
        {
            return Key.FromPitch(pitch, Scale.Aeolian);
        }
    }
}
