using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.MusicTheory
{
    public struct Key : ITopologicalComparable<Key>, IEquatable<Key>
    {
        private readonly sbyte _transposition;
        private readonly sbyte _scale;

        public enum Pitch
        {
            CFlat = B,
            C = 0,
            CSharp = 1,
            DFlat = CSharp,
            D = 2,
            DSharp = 3,
            EFlat = DSharp,
            E = 4,
            ESharp = F,
            FFlat = E,
            F = 5,
            FSharp = 6,
            GFlat = FSharp,
            G = 7,
            GSharp = 8,
            AFlat = GSharp,
            A = 9,
            ASharp = 10,
            BFlat = ASharp,
            B = 11,
            BSharp = C,
        }

        // Calculate the number of sharps for a given transposition:
        // for(int i = 0; i < 12; ++i) => (i * 7) % 12
        private static readonly sbyte[] SignTable = { 0, 7, 2, 9, 4, 11, 6, 1, 8, 3, 10, 5 };

        private Key(int transposition, sbyte scale)
        {
            _scale = scale;
            _transposition = (sbyte)Mod(transposition, 12);
        }

        public static Key FromPitch(Pitch pitch, Scale scale)
        {
            return new Key((int)pitch - (int)scale, (sbyte)scale);
        }

        public static Key ChangeScale(Key pitch, Scale scale)
        {
            return new Key(pitch._transposition + pitch._scale - (int)scale, (sbyte)scale);
        }

        private static int Mod(int left, int right)
        {
            // To perform a mod operation on signed values we have to perform a little trick.
            // 1. Calculate the modolus. The result will be in the range -($right -1) .. ($right - 1).
            // 2. Add $right. This is equivalent to add 0 mod $right. But it will result in a positive number
            // 3. Perform the modulus operaion again. This will result in an desired representetive.
            left %= right;
            left += right;
            left %= right;
            return left;
        }


        public static Key operator +(Key key, Interval transpose)
        {
            return new Key(key._transposition + transpose.Semitones, key._scale);
        }

        public static Key operator -(Key key, Interval transpose)
        {
            return new Key(key._transposition - transpose.Semitones, key._scale);
        }

        public static Interval operator -(Key left, Key right)
        {
            if (left._scale != right._scale)
                throw new ArgumentException("Only keys of the same scale can be subtracted");

            return Interval.FromSemitones((sbyte)(left._transposition - right._transposition));
        }

        public static Key operator ++(Key key)
        {
            return new Key(key._transposition + 1, key._scale);
        }

        public static Key operator --(Key key)
        {
            return new Key(key._transposition - 1, key._scale);
        }

        public int CompareTo(Key other)
        {
            var difference = _transposition - other._transposition;
            if (Math.Abs(difference) <= 6)
                return difference;
            return -difference;
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (!(obj is Key)) throw new ArgumentException($"Object must be of type {nameof(Key)}");
            return CompareTo((Key)obj);
        }

        public static bool operator <(Key left, Key right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(Key left, Key right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(Key left, Key right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(Key left, Key right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Sharp signs
        /// </summary>
        public sbyte Signs => SignTable[_transposition];

        public string BaseNote => KeyTable.GetTableForCulture(CultureInfo.CurrentCulture)[this][_transposition + _scale];

        public bool CanCompareTo(Key other)
        {
            return _scale == other._scale;
        }

        public override bool Equals(object obj)
        {
            return obj is Key other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (_transposition << 16) ^ _scale;
        }

        public static bool operator ==(Key left, Key right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Key left, Key right)
        {
            return !(left == right);
        }

        public bool Equals(Key other)
        {
            return _transposition == other._transposition && _scale == other._scale;
        }

        public override string ToString()
        {
            return $"{KeyTable.GetTableForCulture(CultureInfo.InvariantCulture)[_transposition + _scale]} ({(Scale)_scale})";
        }
    }
}