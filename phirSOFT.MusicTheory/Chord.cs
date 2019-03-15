using System;
using System.Collections;
using System.Collections.Generic;

namespace phirSOFT.MusicTheory
{
    public struct Chord : IEnumerable<Pitch>
    {
        private readonly Pitch chordPitch;
        private readonly Pitch bassPitch;
        private readonly 

        public static Chord FromRomanNumeral(Key key, int romanNumeral)
        {
            if(romanNumeral < 1 || romanNumeral > 7)
                throw new ArgumentOutOfRangeException("Roman numeral has to be a value between one and seven (inclusive).", nameof(romanNumeral));
            return new Chord();
        }

        public static Chord FromDiatonicFunction(Key key, DiatonicFunction diatonic)
        {
            if(key.Scale != Scale.Ionian && key.Scale != Scale.Aeolian)
                throw new ArgumentException("Key has to be a diatonic key. (Ionian = Major; Aeolian = minor)", nameof(key));

            return FromRomanNumeral(key, (int) diatonic);
        }
    }

    public enum DiatonicFunction
    {
    }
}