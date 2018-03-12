using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace phirSOFT.MusicTheory
{
    public class KeyTable : ReadOnlyCollection<string>
    {
        private static readonly ConcurrentDictionary<CultureInfo, KeyTable> Cache = new ConcurrentDictionary<CultureInfo, KeyTable>();

        protected KeyTable(IList<string> dictionary) : base(dictionary)
        {
            if(dictionary.Count != 12)
                throw new ArgumentException("Keymap must contain exact 12 entries.", nameof(dictionary));
        }

        public static KeyTable GetTableForCulture(CultureInfo culture)
        {
           return Cache.GetOrAdd(culture, Create);
        }

        private static KeyTable Create(CultureInfo culture)
        {
            var keyString = Strings.ResourceManager.GetString(nameof(Strings.Keymap), culture);
            return new KeyTable(keyString.Split(';').ToList());
        }


    }
}
