using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>, IEnumerable<T>
    {
        public DoubleNode<T> head;
        public DoubleNode<T> tail;

        public int Length { get; private set; } = 0;

        public DoublyLinkedList()
        {
            head = null;
            tail = null;
            this.Length = 0;
        }

        public void Add(T e)
        {
            DoubleNode<T> newNode = new DoubleNode<T>(e);

            if (tail == null)
            {
                head = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
            }

            tail = newNode;
            Length++;
        }

        public void AddAt(int index, T e)
        {
            if (index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            if (index == Length)
            {
                Add(e);

                return;
            }

            DoubleNode<T> newNode = new DoubleNode<T>(e);
            DoubleNode<T> current = head;

            int i = 0;
            while (i < index)
            {
                current = current.Next;
                i++;
            }

            if (current.Prev == null)
            {
                head = newNode;
            }
            else
            {
                current.Prev.Next = newNode;
                newNode.Prev = current.Next;
            }

            newNode.Prev = current;
            current.Next = newNode;

            Length++;
        }

        public T ElementAt(int index)
        {
            DoubleNode<T> current = head;
            int i = 0;

            if (index < 0 || index >= Length || head == null)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            while (i < index)
            {
                current = current.Next;
                i++;
            }

            return current.Data;
        }

        public void Remove(T item)
        {
            DoubleNode<T> current = head;
            DoubleNode<T> Next;

            while (current != null)
            {
                Next = current.Next;
                if (current.Data.Equals(item))
                {
                    DeleteNode(current);
                    break;
                }
                current = Next;
            }
        }

        public T RemoveAt(int index)
        {
            DoubleNode<T> current = head;
            T deletedData;
            int i = 0;

            if (index < 0 || index >= Length || head == null)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            while (i < index)
            {
                current = current.Next;
                i++;
            }
            deletedData = current.Data;
            
            DeleteNode(current);
            
            return deletedData;
        }

        public void DeleteNode(DoubleNode<T> del)
        {
            if (head == del)
            {
                head = del.Next;
            }

            if (del.Next != null)
            {
                del.Next.Prev = del.Prev;
            }

            if (del.Prev != null)
            {
                del.Prev.Next = del.Next;
            }

            del = null;
            Length--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnumerator<T>(head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class DoubleNode<T>
    {
        public T Data { get; private set; }
        public DoubleNode<T> Next { get; set; }
        public DoubleNode<T> Prev { get; set; }

        public DoubleNode(T Data)
        {
            this.Data = Data;
            Next = null;
            Prev = null;
        }
    }

    public class DoublyLinkedListEnumerator<T> : IEnumerator<T>
    {
        private DoubleNode<T> current;

        public DoublyLinkedListEnumerator(DoubleNode<T> head)
        {
            current = new DoubleNode<T>(default(T))
            {
                Next = head
            };
        }

        public T Current
        {
            get { return current.Data; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            current = current.Next;

            return (current != null);
        }

        public void Reset()
        {
            current = null;
        }

        public void Dispose()
        {
            // Do Nothing..
        }
    }
}