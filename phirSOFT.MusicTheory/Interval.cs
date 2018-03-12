using System;
using System.Diagnostics;
using System.Globalization;

namespace phirSOFT.MusicTheory
{

    public struct Interval : IFormattable, IEquatable<Interval>
    {
        public override bool Equals(object obj)
        {
            return obj is Interval interval && Equals(interval);
        }

        private static readonly IntervalFormatter Formatter = new IntervalFormatter();

        private Interval(sbyte semitones)
        {
            Semitones = semitones;
        }

        public static Interval FromSemitones(sbyte semitones)
        {
            return new Interval(semitones);
        }

        public static Interval operator +(Interval left, Interval right)
        {
            return new Interval((sbyte) (left.Semitones + right.Semitones));
        }

        public static Interval operator -(Interval left, Interval right)
        {
            return new Interval((sbyte) (left.Semitones - right.Semitones));
        }

        public static Interval operator ++(Interval other)
        {
            return new Interval((sbyte) (other.Semitones + 1));
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return Formatter.Format(format, this, formatProvider);
        }

        public sbyte Semitones { get; }

        public override string ToString()
        {
            return ToString("c", CultureInfo.InvariantCulture);
        }

     

        public override int GetHashCode()
        {
            return Semitones.GetHashCode();
        }

        public static bool operator ==(Interval left, Interval right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Interval left, Interval right)
        {
            return !(left == right);
        }

        public bool Equals(Interval other)
        {
            return Semitones == other.Semitones;
        }
    }
}