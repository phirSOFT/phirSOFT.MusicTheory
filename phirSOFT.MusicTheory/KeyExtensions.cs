using System;
using System.Collections.Generic;
using System.Text;

namespace phirSOFT.MusicTheory
{
    public static class KeyExtensions
    {
        public static Key Major(this Key.Pitch pitch)
        {
            return Key.FromPitch(pitch, Scale.Ionian);
        }

        public static Key Minor(this Key.Pitch pitch)
        {
            return Key.FromPitch(pitch, Scale.Aeolian);
        }
    }
}
