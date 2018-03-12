using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using phirSOFT.ContextProperties;

namespace phirSOFT.MusicTheory
{
    public class KeyTable : ReadOnlyCollection<string>, IIndexer<int, string>
    {
        private static readonly ConcurrentDictionary<CultureInfo, KeyTable> Cache = new ConcurrentDictionary<CultureInfo, KeyTable>();
        public static readonly ContextProperty<IIndexer<int, string>> KeyProperty = new ContextProperty<IIndexer<int, string>>(HarmonyContext.Invariant);

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

    /// <summary>
    /// Redirects an index operation
    /// </summary>
    /// <typeparam name="TKey">The key of the index</typeparam>
    /// <typeparam name="TValue">The value of the index</typeparam>
    public interface IIndexer<in TKey, out TValue>
    {
        /// <summary>
        /// Gets the value associated with a given key.
        /// </summary>
        /// <param name="key">The key of the value to retrive.</param>
        /// <returns>The value associated with the key.</returns>
        TValue this[TKey key] { get; }
    }

    public class HarmonyContext : IContextProvider<IIndexer<int, string>, IIndexer<int, string>>
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

        public IIndexer<int, string> GetValue(object targetObject, ContextProperty<IIndexer<int, string>> targetProperty)
        {
            if (targetProperty != KeyTable.KeyProperty)
                throw new ArgumentException("Property does not match.", nameof(targetProperty));

            if (_key.HasValue)
                return new HarmonyDispatcher((KeyTable)targetObject, !_key.Value.Signs.RecommendSharp());
            return (KeyTable) targetObject;
        }

        public bool OverridesValue(object targetObject, ContextProperty<IIndexer<int, string>> targetProperty)
        {
            return targetProperty == KeyTable.KeyProperty && _key != null;
        }

        private struct HarmonyDispatcher : IIndexer<int, string>
        {
            private readonly bool _flat;
            private readonly KeyTable _keyTable;

            public HarmonyDispatcher(KeyTable keyTable, bool flat)
            {
                _keyTable = keyTable;
                this._flat = flat;
            }


            public string this[int key]
            {
                get
                {
                    var k = _keyTable[key];
                    var seperator = k.IndexOf('/');

                    if (seperator == -1)
                        return k;

                    return _flat ? k.Substring(0, seperator) : k.Substring(seperator);
                }
            }
        }
    }
}
