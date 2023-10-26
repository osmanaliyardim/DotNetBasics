using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        DoublyLinkedList<T> LinkedList = new DoublyLinkedList<T>();

        public T Dequeue()
        {
            if (LinkedList.Length == 0)
            {
                throw new InvalidOperationException();
            }

            return LinkedList.RemoveAt(0);
        }

        public void Enqueue(T item)
        {
            LinkedList.AddAt(LinkedList.Length, item);
        }

        public T Pop()
        {
            if (LinkedList.Length == 0)
            {
                throw new InvalidOperationException();
            }

            return LinkedList.RemoveAt(LinkedList.Length - 1);
        }

        public void Push(T item)
        {
            LinkedList.Add(item);
        }
    }
}