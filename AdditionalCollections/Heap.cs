using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdditionalCollections
{
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    public class Heap<T> : ICollection<T>
    {
        public const int DefaultCapacity = 4;
        public const HeapType DefaultHeapType = HeapType.MaxHeap;

        private readonly List<T> _container;
        private readonly HeapType _heapType;
        private readonly IComparer<T> _comparer;

        #region Properties

        public int Capacity
        {
            get { return _container.Capacity; }
        }

        public HeapType HeapType
        {
            get { return _heapType; }
        }

        public IComparer<T> Comparer
        {
            get { return _comparer; }
        }

        #endregion

        #region Constructors

        public Heap()
            : this(DefaultCapacity, DefaultHeapType)
        { }

        public Heap(int capacity)
            : this(capacity, DefaultHeapType, Comparer<T>.Default)
        { }

        public Heap(HeapType heapType)
            : this(DefaultCapacity, heapType, Comparer<T>.Default)
        { }

        public Heap(IComparer<T> comparer) :
            this(DefaultCapacity, DefaultHeapType, comparer)
        { }

        public Heap(int capacity, HeapType heapType)
            : this(capacity, heapType, Comparer<T>.Default)
        { }

        public Heap(int capacity, IComparer<T> comparer)
            : this(capacity, DefaultHeapType, comparer)
        { }

        public Heap(HeapType heapType, IComparer<T> comparer)
            : this(DefaultCapacity, heapType, comparer)
        { }

        public Heap(int capacity, HeapType heapType, IComparer<T> comparer)
        {
            _container = new List<T>(capacity);
            _heapType = heapType;
            _comparer = _heapType == HeapType.MaxHeap ? comparer : Comparer<T>.Create((x, y) => comparer.Compare(y, x));
        }

        #endregion

        #region Core methods

        public T Peek()
        {
            if (_container.Count == 0)
            {
                throw new Exception("Cannot peak an empty heap.");
            }
            return _container[0];
        }

        public T Pop()
        {
            if (_container.Count == 0)
            {
                throw new Exception("Cannot pop an empty heap.");
            }

            return RemoveAt(0);
        }

        public void Push(T item)
        {
            _container.Add(item);
            BubbleUp(_container.Count - 1);
        }

        #endregion

        #region Private helper functions

        private void BubbleUp(int index)
        {
            int parentIndex = (index - 1) / 2;
            while (index > 0 && Greater(index, parentIndex))
            {
                Swap(index, parentIndex);

                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }

        private void BubbleDown(int index)
        {
            int childIndex = GetGreaterChild(index);
            while (childIndex != -1 && Greater(childIndex, index))
            {
                Swap(index, childIndex);

                index = childIndex;
                childIndex = GetGreaterChild(index);
            }
        }

        private int GetGreaterChild(int index)
        {
            int childIndex1 = index * 2 + 1;
            int childIndex2 = index * 2 + 2;

            if (childIndex1 < _container.Count && childIndex2 < _container.Count && Less(childIndex1, childIndex2))
            {
                return childIndex2;
            }

            if (childIndex1 < _container.Count)
            {
                return childIndex1;
            }
            return -1;
        }

        private int IndexOf(T item)
        {
            for (int i = 0; i < _container.Count; i++)
            {
                if (Equal(item, _container[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        private void Swap(int index, int parentIndex)
        {
            T temp = _container[index];
            _container[index] = _container[parentIndex];
            _container[parentIndex] = temp;
        }

        private T RemoveAt(int index)
        {
            T item = _container[index];
            Swap(index, _container.Count - 1);
            _container.RemoveAt(_container.Count - 1);
            BubbleDown(index);
            return item;
        }

        #endregion

        #region Comparer helper functions

        internal bool Equal(int index1, int index2)
        {
            return Equal(_container[index1], _container[index2]);
        }

        internal bool Equal(T item1, T item2)
        {
            return _comparer.Compare(item1, item2) == 0;
        }

        internal bool Greater(int index1, int index2)
        {
            return Greater(_container[index1], _container[index2]);
        }

        internal bool Greater(T item1, T item2)
        {
            return _comparer.Compare(item1, item2) > 0;
        }

        internal bool Less(int index1, int index2)
        {
            return Less(_container[index1], _container[index2]);
        }

        internal bool Less(T item1, T item2)
        {
            return _comparer.Compare(item1, item2) < 0;
        }

        #endregion

        #region ICollection methods

        public IEnumerator<T> GetEnumerator()
        {
            return _container.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            Push(item);
        }


        public void Clear()
        {
            _container.Clear();
        }

        public bool Contains(T item)
        {
            return _container.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _container.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public int Count
        {
            get { return _container.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion
    }
}
