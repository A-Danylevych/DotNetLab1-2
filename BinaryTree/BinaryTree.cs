using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTree
{
    public class BinaryTree<T> : ICollection, ICollection<T> where T : IComparable<T>
    {
        public delegate void TreeStateHandler(object? sender, TreeEventArgs<T> e);
        public TreeStateHandler? Notify;
        private Node<T>? Head { get; set; }
        public void CopyTo(Array array, int index)
        {
            CopyTo((T[])array, index);
        }
        public int Count => Head?.Count() ?? 0;
        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => false;
        public bool IsReadOnly => false;
        public BinaryTree()
        {

        }

        public BinaryTree(T head)
        {
            Head = new Node<T>(head);
        }

        public void Add(T value)
        {
            Node<T> node = new(value);
            if (Head == null)
            {
                Head = node;
                Notify?.Invoke(this,new TreeEventArgs<T>("Add to the tree",
                    "Value was added to the tree", value));
                return;
            }

            if (Head.Add(node))
            {
                Notify?.Invoke(this,new TreeEventArgs<T>("Add to the tree",
                    "Value has already been added", value));
                return;
            }

            Notify?.Invoke(this,new TreeEventArgs<T>("Add to the tree",
                "Value was added to the tree", value));
        }

        public void Clear()
        {
            Head?.Clear();
            Head = null!;
            Notify?.Invoke(this ,new TreeEventArgs<T>("Clear the tree",
                "Tree is empty", default!));
        }

        public bool Contains(T item)
        {
            if (Head == null)
            {
                Notify?.Invoke(this, new TreeEventArgs<T>( "Tree contains",
                    "There is no item in the tree", item));
                return false;
            }
            if (Head.Contains(item))
            {
                Notify?.Invoke(this, new TreeEventArgs<T>( "Tree contains",
                    "Tree contains value", item));
                return true;
            }
            else
            {
                Notify?.Invoke(this, new TreeEventArgs<T>("Tree contains",
                    "Tree doesn't contain value", item));
                return false;
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length <= arrayIndex|| arrayIndex + Count > array.Length)
            {
                throw new IndexOutOfRangeException();
            }
            

            foreach (var item in this)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        public bool Remove(T item)
        {
            if (Head == null)
            {
                Notify?.Invoke(this, new TreeEventArgs<T>("Remove from the tree",
                    "There is no item in the tree", item));
                return false;
            }
            
            if (!Head.Contains(item))
            {
                Notify?.Invoke(this, new TreeEventArgs<T>("Remove from the tree",
                    "Value is not in the tree", item));
                return false;
            }

            if(!Head.Remove(item)){
                Clear();
                return true;
            }

            Notify?.Invoke(this,new TreeEventArgs<T>("Remove from the tree",
                "Value was removed from the tree", item));
            return true;
        }

        public Node<T>? Find(T item)
        {
            return Head?.Find(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var array = Head?.GetArray();
            return new NodeEnum<T>(array);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

    public class TreeEventArgs<T>: EventArgs
    {
        public string Operation { get; }
        public string Message { get; }
        public T Value { get; }

        public TreeEventArgs(string operation, string message, T value)
        {
            Operation = operation;
            Message = message;
            Value = value;
        }
    }
}
