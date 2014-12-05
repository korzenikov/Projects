namespace CourseraTasks.CSharp
{
    public class Entry<TKey, TValue>
    {
        public Entry(TKey key)
        {
            Key = key;
        }

        public TKey Key { get; private set; }

        public TValue Value { get; set; }

        public Entry<TKey, TValue> Next { get; set; }
    }
}
