using System;
using System.Diagnostics;

namespace phirSOFT.MusicTheory
{

    public struct Interval
    {
        private readonly int _semitones;

        private Interval(int semitones)
        {
            _semitones = semitones;
        }

        public Interval FromSemitones(int semitones)
        {
            return new Interval(semitones);
        }

        public static Interval operator +(Interval left, Interval right)
        {
            return new Interval(left._semitones + right._semitones);
        }

        public static Interval operator -(Interval left, Interval right)
        {
            return new Interval(left._semitones - right._semitones);
        }

        public static Interval operator ++(Interval other)
        {
            return new Interval(other._semitones + 1);
        }
    }
}