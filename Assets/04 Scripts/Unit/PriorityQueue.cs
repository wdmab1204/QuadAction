using System;

public class PriorityQueue<T> where T : IComparable<T>
{
    private T[] heap;
    private int size;

    public int Count { get { return size; } }

    public PriorityQueue(int maxHeapSize)
    {
        heap = new T[maxHeapSize];
        size = 0;
    }

    public void Enqueue(T item)
    {
        if (size >= heap.Length)
            Array.Resize(ref heap, heap.Length * 2);

        heap[size] = item;
        size++;

        int childIndex = size - 1;
        while (childIndex > 0)
        {
            int parentIndex = (childIndex - 1) / 2;
            if (heap[childIndex].CompareTo(heap[parentIndex]) >= 0)
                break;

            SwapElements(childIndex, parentIndex);
            childIndex = parentIndex;
        }
    }

    public T Dequeue()
    {
        if (size == 0)
            throw new InvalidOperationException("PriorityQueue is empty");

        T firstItem = heap[0];
        size--;

        heap[0] = heap[size];
        heap[size] = default(T);

        int parentIndex = 0;
        while (true)
        {
            int childIndex = parentIndex * 2 + 1;
            if (childIndex >= size)
                break;

            int rightChildIndex = childIndex + 1;
            if (rightChildIndex < size && heap[rightChildIndex].CompareTo(heap[childIndex]) < 0)
                childIndex = rightChildIndex;

            if (heap[parentIndex].CompareTo(heap[childIndex]) <= 0)
                break;

            SwapElements(parentIndex, childIndex);
            parentIndex = childIndex;
        }

        if (size < heap.Length / 4 && size > 16)
            Array.Resize(ref heap, heap.Length / 2);

        return firstItem;
    }

    public bool Contains(T item)
    {
        for (int i = 0; i < size; i++)
        {
            if (heap[i].Equals(item))
                return true;
        }

        return false;
    }

    public void Clear()
    {
        Array.Clear(heap, 0, size);
        size = 0;
    }

    private void SwapElements(int i, int j)
    {
        T temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
}
