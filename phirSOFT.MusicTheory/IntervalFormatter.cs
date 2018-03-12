using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace phirSOFT.MusicTheory
{
    public class IntervalFormatter : ICustomFormatter, IFormatProvider
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (!(arg is Interval interval))
                throw new ArgumentException($"{nameof(arg)} has to be an Interval.");

            var intervalFormat = ParseIntervalFormat(format);

            switch (intervalFormat)
            {
                case IntervalFormat.Common:
                    return FormatCommon(interval, false, formatProvider);
                case IntervalFormat.CommonShort:
                    return FormatCommon(interval, true, formatProvider);
                case IntervalFormat.MajorMinorPerfect:
                    return FormatMajorMinorPerfect(interval, false, formatProvider);
                case IntervalFormat.MajorMinorPerfectShort:
                    return FormatMajorMinorPerfect(interval, true, formatProvider);
                case IntervalFormat.AugmentedDiminished:
                    return FormatAugmendtedDiminished(interval, false, formatProvider);
                case IntervalFormat.AugmentedDiminishedShort:
                    return FormatAugmendtedDiminished(interval, true, formatProvider);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }



        private static string FormatCommon(Interval interval, bool shortFormat, IFormatProvider formatProvider)
        {
            if (interval.Semitones == 6)
                return GetFormatString(nameof(Strings.CommonIntervallTritone), shortFormat, formatProvider);

            var tones = Math.DivRem(interval.Semitones, 2, out var semitones);

            if (tones == 0)
                return string.Format(
                    GetFormatString(nameof(Strings.CommonIntervalFormatSemitones), shortFormat, formatProvider), semitones);

            if(semitones == 0)
                return string.Format(
                    GetFormatString(nameof(Strings.CommonIntervalFormatTones), shortFormat, formatProvider), semitones);

            return string.Format(
                GetFormatString(nameof(Strings.CommonIntervalFormatFull), shortFormat, formatProvider), semitones);
        }

        private static string GetFormatString(string name, bool shortFormat, IFormatProvider formatProvider)
        {
            return GetFormatString(shortFormat ? name + "Short" : name, formatProvider);
        }

        private string FormatMajorMinorPerfect(Interval interval, bool shortFormat, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        private string FormatAugmendtedDiminished(Interval interval, bool shortFormat, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        private static string GetFormatString(string name, IFormatProvider formatProvider)
        {
            if (formatProvider is CultureInfo culture)
            {
                return Strings.ResourceManager.GetString(name, culture);
            }

            return Strings.ResourceManager.GetString(name);
        }

        private static IntervalFormat ParseIntervalFormat(string format)
        {
            if (string.IsNullOrEmpty(format))
                return IntervalFormat.Common;

            if (format.Length > 2)
                throw new ArgumentException(string.Format(Strings.FormatterUnrecognizedFormat, format), nameof(format));

            var secondParameter = format.Length == 2 ? format[1] : (char?)null;

            if (secondParameter.HasValue && secondParameter.Value != 's')
                throw new ArgumentException(string.Format(Strings.FormatterUnrecognizedFormat, format), nameof(format));


            switch (format[0])
            {
                case 'm':
                    return secondParameter.HasValue
                        ? IntervalFormat.MajorMinorPerfectShort
                        : IntervalFormat.MajorMinorPerfect;
                case 'c':
                    return secondParameter.HasValue
                        ? IntervalFormat.CommonShort
                        : IntervalFormat.Common;
                case 'a':
                    return secondParameter.HasValue
                        ? IntervalFormat.AugmentedDiminishedShort
                        : IntervalFormat.AugmentedDiminished;
                default:
                    throw new ArgumentException(string.Format(Strings.FormatterUnrecognizedFormat, format), nameof(format));
            }

        }

        private enum IntervalFormat
        {
            Common,
            CommonShort,
            MajorMinorPerfect,
            MajorMinorPerfectShort,
            AugmentedDiminished,
            AugmentedDiminishedShort,
        }

        public object GetFormat(Type formatType)
        {
            return formatType == typeof(Interval) ? this : null;
        }
    }
}
