using System;
using System.Collections;
using System.Collections.Generic;

namespace AoC
{
    public class DLList<T> : IList<T>
    {
        private int _count;
        private DLNode<T> _head;
        private DLNode<T> _tail;

        public DLList() { }

        public DLList(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                var n = new DLNode<T>(item);
                if (_head == null)
                {
                    _head = n;
                    _tail = n;
                }
                else
                {
                    _tail.next = n;
                    _tail = n;
                }
                _count++;
            }
        }


        public T this[int index]
        {
            get => NodeAt(index).data;
            set => NodeAt(index).data = value;
        }

        private DLNode<T> NodeAt(int index)
        {
            if (index > _count / 2)
                return ReverseTraverse(_count - 1 - index);
            return Traverse(index);
        }


        private DLNode<T> ReverseTraverse(int steps)
        {
            var n = _tail;
            for (int i = 0; i < steps; i++)
                n = n.prev;
            return n;
        }

        private DLNode<T> Traverse(int steps)
        {
            var n = _head;
            for (int i = 0; i < steps; i++)
                n = n.next;
            return n;
        }

        public int Count { get { return _count; } }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            var node = new DLNode<T>(item);
            if (_head == null)
            {
                _head = node;
                _tail = node;
            }
            else
            {
                _tail.next = node;
                node.prev = _tail;
                _tail = node;
            }
            _count++;
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            _count = 0;
        }

        public bool Contains(T item)
        {
            var p = _head;
            while (p != null)
            {
                if (p.data.Equals(item))
                    return true;
                p = p.next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var n = _head;
            while (n != null)
            {
                yield return n.data;
                n = n.next;
            }
        }

        public int IndexOf(T item)
        {
            var n = _head;
            for (int i = 0; n != null; i++, n = n.next)
                if (n.data.Equals(item))
                    return i;
            return -1;
        }

        public void Insert(int index, T item)
        {
            var newNode = new DLNode<T>(item);
            if (index == 0)
            {
                newNode.next = _head;
                _head = newNode;
            }
            else if (index == _count - 1)
            {
                newNode.prev = _tail;
                _tail = newNode;
            }
            else
            {
                var n = Traverse(index);
                var next = n.next;
                n.next = newNode;
                next.prev = newNode;
                newNode.next = next;
                newNode.prev = n;
            }
            _count++;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index < 0)
                return false;
            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            if (index == 0)
            {
                _head = _head.next;
                _head.prev = null;
            }
            else if (index == _count -1)
            {
                _tail = _tail.prev;
                _tail.next = null;
            }
            else
            {
                var n = NodeAt(index);
                var p = n.prev;
                n.next.prev = p;
                p.next = n.next;
            }
            _count--;
        }

        public T RemoveHead()
        {
            if (_count == 0) throw new IndexOutOfRangeException();
            var n = _head;
            if (_count == 1)
            {
                _head = null;
                _tail = null;
            }
            else
            {
                _head = _head.next;
                _head.prev = null;
            }
            _count--;
            return n.data;
        }

        public T Pop()
        {
            if (_count == 0) throw new IndexOutOfRangeException();
            var n = _tail;
            _tail = n.prev;
            if (_tail != null)
                _tail.next = null;
            _count--;
            if (_count == 0)
                _head = null;
            return n.data;
        }

        public T Peek()
        {
            return _tail.data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal class DLNode<T>
    {
        internal T data;
        internal DLNode<T> next = null;
        internal DLNode<T> prev = null;

        public DLNode(T item)
        {
            data = item;
        }
    }
}
