using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdditionalCollections.Tests
{
    [TestClass]
    public class HeapTests
    {
        [TestMethod]
        public void HeapTest()
        {
            Heap<int> heap = new Heap<int>();
            Assert.AreEqual(Heap<int>.DefaultCapacity, heap.Capacity);
            Assert.AreEqual(Heap<int>.DefaultHeapType, heap.HeapType);
        }

        [TestMethod]
        public void HeapTest_WithCapacity()
        {
            const int capacity = 123;
            Heap<int> heap = new Heap<int>(capacity);
            Assert.AreEqual(capacity, heap.Capacity);
            Assert.AreEqual(Heap<int>.DefaultHeapType, heap.HeapType);
        }

        [TestMethod]
        public void HeapTest_WithHeapType()
        {
            Heap<int> heap = new Heap<int>(HeapType.MinHeap);
            Assert.AreEqual(Heap<int>.DefaultCapacity, heap.Capacity);
            Assert.AreEqual(HeapType.MinHeap, heap.HeapType);

            heap = new Heap<int>(HeapType.MaxHeap);
            Assert.AreEqual(Heap<int>.DefaultCapacity, heap.Capacity);
            Assert.AreEqual(HeapType.MaxHeap, heap.HeapType);
        }

        [TestMethod]
        public void HeapTest_WithComparator()
        {
            IComparer<int> comparer = Comparer<int>.Create((i1, i2) => i1 * 10 - i2);

            Heap<int> heap = new Heap<int>(comparer);
            Assert.AreEqual(Heap<int>.DefaultCapacity, heap.Capacity);
            Assert.AreEqual(Heap<int>.DefaultHeapType, heap.HeapType);
            Assert.AreEqual(comparer, heap.Comparer);
        }

        [TestMethod]
        public void HeapTest_WithCapacityAndHeapType()
        {
            const int capacity = 123;

            Heap<int> heap = new Heap<int>(capacity, HeapType.MinHeap);
            Assert.AreEqual(capacity, heap.Capacity);
            Assert.AreEqual(HeapType.MinHeap, heap.HeapType);

            heap = new Heap<int>(capacity, HeapType.MaxHeap);
            Assert.AreEqual(capacity, heap.Capacity);
            Assert.AreEqual(HeapType.MaxHeap, heap.HeapType);
        }

        [TestMethod]
        public void HeapTest_WithCapacityAndComparator()
        {
            const int capacity = 123;
            IComparer<int> comparer = Comparer<int>.Create((i1, i2) => i1 * 10 - i2);

            Heap<int> heap = new Heap<int>(capacity, comparer);
            Assert.AreEqual(capacity, heap.Capacity);
            Assert.AreEqual(Heap<int>.DefaultHeapType, heap.HeapType);
            Assert.AreEqual(comparer, heap.Comparer);
        }

        [TestMethod]
        public void HeapTest_WithHeapTypeAndComparator()
        {
            IComparer<int> comparer = Comparer<int>.Create((i1, i2) => i1 * 10 - i2);

            Heap<int> heap = new Heap<int>(HeapType.MinHeap, comparer);
            Assert.AreEqual(Heap<int>.DefaultCapacity, heap.Capacity);
            Assert.AreEqual(HeapType.MinHeap, heap.HeapType);
            Assert.AreNotEqual(comparer, heap.Comparer);

            heap = new Heap<int>(HeapType.MaxHeap, comparer);
            Assert.AreEqual(Heap<int>.DefaultCapacity, heap.Capacity);
            Assert.AreEqual(HeapType.MaxHeap, heap.HeapType);
            Assert.AreEqual(comparer, heap.Comparer);
        }

        [TestMethod]
        public void HeapTest_WithCapacityHeapTypeAndComparator()
        {
            const int capacity = 123;
            IComparer<int> comparer = Comparer<int>.Create((i1, i2) => i1 * 10 - i2);

            Heap<int> heap = new Heap<int>(capacity, HeapType.MinHeap, comparer);
            Assert.AreEqual(capacity, heap.Capacity);
            Assert.AreEqual(HeapType.MinHeap, heap.HeapType);
            Assert.AreNotEqual(comparer, heap.Comparer);

            heap = new Heap<int>(capacity, HeapType.MaxHeap, comparer);
            Assert.AreEqual(capacity, heap.Capacity);
            Assert.AreEqual(HeapType.MaxHeap, heap.HeapType);
            Assert.AreEqual(comparer, heap.Comparer);

        }

        [TestMethod]
        public void PeekTest()
        {
            Heap<int> heap = new Heap<int>(HeapType.MinHeap);
            heap.Push(5);
            Assert.AreEqual(5, heap.Peek());
            heap.Push(1);
            Assert.AreEqual(1, heap.Peek());
            heap.Push(2);
            Assert.AreEqual(1, heap.Peek());
            heap.Pop();
            Assert.AreEqual(2, heap.Peek());
            heap.Push(0);
            Assert.AreEqual(0, heap.Peek());
            heap.Pop();
            Assert.AreEqual(2, heap.Peek());
            heap.Pop();
            Assert.AreEqual(5, heap.Peek());
        }

        [TestMethod]
        public void PopTest()
        {
            Heap<int> heap = new Heap<int>(HeapType.MaxHeap);
            heap.Push(5);
            heap.Push(1);
            heap.Push(2);
            heap.Push(0);
            heap.Push(6);

            Assert.AreEqual(6, heap.Pop());
            Assert.AreEqual(5, heap.Pop());
            Assert.AreEqual(2, heap.Pop());
            Assert.AreEqual(1, heap.Pop());
            Assert.AreEqual(0, heap.Pop());
            Assert.AreEqual(0, heap.Count);
        }

        [TestMethod]
        public void PushTest()
        {
            Heap<int> heap = new Heap<int>(HeapType.MaxHeap);
            heap.Push(5);
            heap.Push(1);
            heap.Push(2);

            Assert.AreEqual(5, heap.Peek());
            Assert.AreEqual(3, heap.Count);
        }

        [TestMethod]
        public void GetEnumeratorTest()
        {
            Heap<int> heap = new Heap<int>();
            heap.Push(5);
            heap.Push(1);
            heap.Push(2);

            HashSet<int> values = new HashSet<int> { 1, 2, 5 };

            IEnumerator<int> heapEnumerator = heap.GetEnumerator();
            while (heapEnumerator.MoveNext())
            {
                Assert.IsTrue(values.Contains(heapEnumerator.Current));
                values.Remove(heapEnumerator.Current);
            }
        }

        [TestMethod]
        public void AddTest()
        {
            Heap<int> heap = new Heap<int>();
            heap.Add(5);
            heap.Add(1);
            heap.Add(2);

            Assert.AreEqual(5, heap.Peek());
            Assert.AreEqual(3, heap.Count);
        }

        [TestMethod]
        public void ClearTest()
        {
            Heap<int> heap = new Heap<int>();
            heap.Add(5);
            heap.Add(1);
            heap.Add(2);

            heap.Clear();
            Assert.AreEqual(0, heap.Count);
        }

        [TestMethod]
        public void ContainsTest()
        {
            Heap<int> heap = new Heap<int>();
            heap.Add(5);
            heap.Add(1);
            heap.Add(2);

            Assert.IsTrue(heap.Contains(1));
            Assert.IsTrue(heap.Contains(2));
            Assert.IsTrue(heap.Contains(5));
            Assert.IsFalse(heap.Contains(0));
            Assert.IsFalse(heap.Contains(3));
            Assert.IsFalse(heap.Contains(4));
            Assert.IsFalse(heap.Contains(6));
        }

        [TestMethod]
        public void CopyToTest()
        {
            Heap<int> heap = new Heap<int>();
            heap.Add(5);
            heap.Add(1);
            heap.Add(2);

            int[] array = new int[3];
            heap.CopyTo(array, 0);
            Assert.IsTrue(array.Contains(1));
            Assert.IsTrue(array.Contains(2));
            Assert.IsTrue(array.Contains(5));
            Assert.IsFalse(array.Contains(0));
        }

        [TestMethod]
        public void RemoveTest()
        {
            Heap<int> heap = new Heap<int>();
            heap.Add(5);
            heap.Add(1);
            heap.Add(-1);
            heap.Add(0);
            heap.Add(6);

            Assert.AreEqual(6, heap.Peek());
            heap.Remove(6);
            Assert.AreEqual(5, heap.Peek());
            heap.Remove(1);
            Assert.AreEqual(5, heap.Peek());
            heap.Remove(5);
            Assert.AreEqual(0, heap.Peek());
            heap.Remove(-1);
            Assert.AreEqual(0, heap.Peek());
            heap.Remove(0);
            Assert.AreEqual(0, heap.Count);
        }
    }
}
