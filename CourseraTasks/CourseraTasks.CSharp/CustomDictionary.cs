using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class CustomDictionary<TKey, TValue>
    {
        private int _itemsCount;

        private Entry<TKey, TValue>[] _buckets;

        public CustomDictionary()
        {
            _buckets = new Entry<TKey, TValue>[2];
        }

        public TValue this[TKey key]
        {
            get
            {
                var entry = GetEntry(key);
                if (entry == null)
                    throw new ArgumentException("Dictionary does not contain specified key", "key");
                return entry.Value;
            }

            set
            {
                Add(key, value, true);
            }
        }

        public void Add(TKey key, TValue value)
        {
            Add(key, value, false);
        }

        public void Remove(TKey key)
        {
            var bucketIndex = GetBucketIndex(key);
            var current = _buckets[bucketIndex];
            Entry<TKey, TValue> previous = null;

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    if (previous == null)
                    {
                        _buckets[bucketIndex] = null;
                    }
                    else
                    {
                        previous.Next = current.Next;
                    }

                    _itemsCount--;
                    return;
                }

                previous = current;
                current = current.Next;
            }
        }

        public bool Contains(TKey key)
        {
            return GetEntry(key) != null;
        }

        private void Add(TKey key, TValue value, bool overwrite)
        {
            if (_itemsCount == _buckets.Length)
            {
                Resize();
            }

            var bucketIndex = GetBucketIndex(key);
            var current = _buckets[bucketIndex];
            Entry<TKey, TValue> last = null;

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    if (!overwrite)
                    {
                        throw new ArgumentException("Key should be unique", "key");
                    }

                    current.Value = value;
                    return;
                }

                last = current;
                current = current.Next;
            }

            _itemsCount++;
            _buckets[bucketIndex] = new Entry<TKey, TValue>(key) { Value = value, Next = last };
        }

        private int GetBucketIndex(TKey key)
        {
            return (key.GetHashCode() & 0x7FFFFFFF) % _buckets.Length;
        }

        private void Resize()
        {
            var entries = GetEntries().ToArray();

            _buckets = new Entry<TKey, TValue>[_buckets.Length * 2 + 1];
            foreach (var entry in entries)
            {
                Add(entry.Key, entry.Value, false);
            }
        }

        private Entry<TKey, TValue> GetEntry(TKey key)
        {
            var bucketIndex = GetBucketIndex(key);
            var current = _buckets[bucketIndex];

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    return current;
                }

                current = current.Next;
            }

            return null;
        }

        private IEnumerable<Entry<TKey, TValue>> GetEntries()
        {
            foreach (var bucket in _buckets)
            {
                var current = bucket;
                while (current != null)
                {
                    yield return current;
                    current = current.Next;
                }
            }
        }
    }
}
