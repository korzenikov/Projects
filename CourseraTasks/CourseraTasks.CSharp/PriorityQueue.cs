﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class PriorityQueue<TElement, TPriority> : PriorityQueueBase<TElement, TPriority> 
        where TPriority : IComparable<TPriority>
    {
        private List<KeyValuePair<TElement, TPriority>> _keyValuePairs;

        private IDictionary<TElement, int> _elementPositions;

        public PriorityQueue(IEnumerable<KeyValuePair<TElement, TPriority>> keyValuePairs)
        {
            Build(keyValuePairs);
        }

        public override int Count
        {
            get
            {
                return _keyValuePairs.Count;
            }
        }

        public void Add(TElement key, TPriority value)
        {
            _keyValuePairs.Add(new KeyValuePair<TElement, TPriority>(key, value));
            BubbleUp(_keyValuePairs.Count - 1);
        }

        public void ChangePriority(TElement key, TPriority priority)
        {
            var index = GetElementIndex(key);

            if (!IsHigherPriority(priority, GetPriority(index)))
                throw new ArgumentException("Cannot decrease priority of specified element", "priority");

            _keyValuePairs[index] = new KeyValuePair<TElement, TPriority>(key, priority);
            BubbleUp(index);
        }

        protected override void SwapElements(int i, int j)
        {
            var temp = _keyValuePairs[i];
            _keyValuePairs[i] = _keyValuePairs[j];
            _keyValuePairs[j] = temp;
            _elementPositions[_keyValuePairs[i].Key] = i;
            _elementPositions[_keyValuePairs[j].Key] = j;
        }

        protected override TPriority GetPriority(int index)
        {
            return _keyValuePairs[index].Value;
        }

        protected override TElement GetElement(int index)
        {
            return _keyValuePairs[index].Key;
        }

        protected override void RemoveAt(int index)
        {
            _keyValuePairs.RemoveAt(index);
        }

        protected override bool IsHigherPriority(TPriority priority1, TPriority priority2)
        {
            return priority1.CompareTo(priority2) < 0;
        }

        private int GetElementIndex(TElement key)
        {
            int index;
            if (_elementPositions.TryGetValue(key, out index))
            {
                return index;
            }
            return -1;
        }

        private void Build(IEnumerable<KeyValuePair<TElement, TPriority>> keyValuePairs)
        {
            _keyValuePairs = keyValuePairs.ToList();
            _elementPositions = new Dictionary<TElement, int>();
            for (int i = 0; i < _keyValuePairs.Count; i++)
            {
                _elementPositions.Add(_keyValuePairs[i].Key, i);
            }

            HeapifyAll();
        }
    }
}