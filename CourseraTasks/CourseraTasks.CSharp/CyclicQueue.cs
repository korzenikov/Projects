using System;
using System.Diagnostics.Contracts;

namespace CourseraTasks.CSharp
{
    public class CyclicQueue
    {
        private readonly int _capacity;

        private readonly int[] _storage;

        private int _head = -1;

        private int _tail;

        public CyclicQueue(int capacity)
        {
            Contract.Requires(capacity > 0);
            _capacity = capacity;
            _storage = new int[_capacity];
        }

        public void Enqueue(int element)
        {
            if (_tail == _head)
            {
                throw new InvalidOperationException("The queue is full");
            }

            _storage[_tail] = element;
            
            if (_head == -1)
            {
                _head = _tail;
            }

            _tail++;

            if (_tail == _capacity)
            {
                _tail = 0;
            }
        }

        public int Dequeue()
        {
            if (_head == -1)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            var element = _storage[_head++];
            if (_head == _capacity)
            {
                _head = 0;
            }

            if (_head == _tail)
            {
                _head = -1;
            }

            return element;
        }
    }
}
