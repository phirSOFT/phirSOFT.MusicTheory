using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using phirSOFT.ContextProperties;

namespace phirSOFT.MusicTheory
{
    public class KeyTable : ReadOnlyCollection<string>, IIndexer<int, string>
    {
        private static readonly ConcurrentDictionary<CultureInfo, KeyTable> Cache = new ConcurrentDictionary<CultureInfo, KeyTable>();

        public static readonly ContextProperty<IIndexer<int, string>> KeyProperty =
            new ContextProperty<IIndexer<int, string>>(new IndependentContextPool<IIndexer<int, string>>());

        protected KeyTable(IList<string> dictionary) : base(dictionary)
        {
            if (dictionary.Count != 12)
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

        public IIndexer<int, string> this[HarmonyContext context] => KeyProperty[this, context];
    }

    public class HarmonyContext : IContextProvider<IContextProperty<IIndexer<int, string>>, IIndexer<int, string>>
    {
        private readonly Key? _key;

        public static readonly HarmonyContext Invariant = new HarmonyContext();

        private HarmonyContext()
        {
            _key = null;
        }

        public HarmonyContext(Key key)
        {
            _key =  key;
        }

        public IIndexer<int, string> GetValue(object targetObject, IContextProperty<IIndexer<int, string>> targetProperty)
        {
            if (targetProperty != KeyTable.KeyProperty)
                throw new ArgumentException("Property does not match.", nameof(targetProperty));

            if (_key.HasValue)
                return new HarmonyDispatcher((KeyTable)targetObject, !_key.Value.Signs.RecommendSharp());
            return (KeyTable) targetObject;
        }

        public bool OverridesValue(object targetObject, IContextProperty<IIndexer<int, string>> targetProperty)
        {
            return targetProperty == KeyTable.KeyProperty && _key != null;
        }

        public static implicit operator HarmonyContext(Key key)
        {
            return new HarmonyContext(key);
        }

        private struct HarmonyDispatcher : IIndexer<int, string>
        {
            private readonly bool _flat;
            private readonly KeyTable _keyTable;

            public HarmonyDispatcher(KeyTable keyTable, bool flat)
            {
                _keyTable = keyTable;
                _flat = flat;
            }


            public string this[int key]
            {
                get
                {
                    var k = _keyTable[key];
                    var seperator = k.IndexOf('/');

                    if (seperator == -1)
                        return k;

                    return _flat ? k.Substring(seperator +1) : k.Substring(0, seperator) ;
                }
            }
        }

    }
}
