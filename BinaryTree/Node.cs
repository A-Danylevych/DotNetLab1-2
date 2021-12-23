using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTree
{
    public class Node<T> : IComparable<Node<T>> where T : IComparable<T>
    {
        private Node<T>? Right { get; set; } 
        private Node<T>? Left { get; set; } 
        private Node<T>? Parent { get; set; }
        public T Value { get; set; }
        public bool Add(Node<T> node)
        {
            if (Value.CompareTo(node.Value) < 0)
            {
                if (Right != null)
                {
                    Right.Add(node);
                }
                else
                {
                    node.Parent = this;
                    Right = node;
                    return true;
                }
            }
            if (Value.CompareTo(node.Value) > 0)
            {
                if (Left != null)
                {
                    Left.Add(node);
                }
                else
                {
                    node.Parent = this;
                    Left = node;
                    return true;
                }
            }
            return false;
            
        }
        public void Clear()
        {
            Right?.Clear();
            Left?.Clear();
            Right = null;
            Left = null;
        }
        public bool Contains(T item)
        {
            if (Value.Equals(item))
            {
                return true;
            }
            if (Right != null && Value.CompareTo(item) < 0)
            {
                return Right.Contains(item);
            }
            if (Left != null && Value.CompareTo(item) > 0)
            {
                return Left.Contains(item);
            }
            return false;
        }
        public bool Remove(T item)
        {
            switch (Value.CompareTo(item))
            {
                case > 0 when Left != null:
                    return Left.Remove(item);
                case < 0 when Right != null:
                    return Right.Remove(item);
            }

            if (!Value.Equals(item)) return false;
            if (Right == null && Left == null)
            {
                return RemoveLeaf();
            }

            if (Left == null || Right == null)
            {
                Remove();
                return true;
            }

            Node<T> successor = Next();
            return Remove(successor);

        }
        private bool RemoveLeaf()
        {
            if (Parent == null)
            {
                return false;
            }

            if (Parent.Right != null && Parent.Right.Equals(this))
            {
                Parent.Right = null;
                return true;
            }

            Parent.Left = null;
            return true;
        }

        private void Remove()
        {
            if (Parent == null)
            {
                if (Right != null)
                {
                    Value = Right.Value;
                    Right = null;
                }
                else if (Left != null)
                {
                    Value = Left.Value;
                    Left = null;
                }
            }
            else if (Parent.Right != null)
            {
                Parent.Right = Right ?? Left;
            }
            else if (Parent.Left != null)
            {
                Parent.Left = Left ?? Right;
            }
        }

        private bool Remove(Node<T> node)
        {
            Value = node.Value;
            if (node.Parent == null)
            {
                return false;
            }
            if (node.Parent.Left != null && node.Parent.Left.Equals(node))
            {
                node.Parent.Left = node.Right;
                if (node.Right != null)
                {
                    node.Right.Parent = node.Parent;
                }
            }
            else
            {
                node.Parent.Right = node.Right;
                if (node.Right != null)
                {
                    node.Right.Parent = node.Parent;
                }
            }

            return true;
        }
        public Node<T>? Find(T item)
        {
            if (Value.Equals(item))
            {
                return this;
            }
            if (Right != null && Value.CompareTo(item) < 0)
            {
                return Right.Find(item);
            }
            if (Left != null && Value.CompareTo(item) > 0)
            {
                return Left.Find(item);
            }
            return null;
        }

        private Node<T> Minimum()
        {
            if (Left == null)
            {
                return this;
            }
            return Left.Minimum();
        }

        private Node<T> Next()
        {
            if (Right != null)
            {
                return Right.Minimum();
            }
            Node<T> x = this;
            var y = Parent;
            while (y != null && x == y.Right)
            {

                x = y;
                y = y.Parent;
            }
            return y!;
        }
        public int Count()
        {
            if (Right != null && Left != null)
            {
                return 1 + Left.Count() + Right.Count();
            }
            else if (Right != null)
            {
                return 1 + Right.Count();
            }
            else if (Left != null)
            {
                return 1 + Left.Count();
            }
            else
            {
                return 1;
            }
        }
        public Node<T>[] GetArray()
        {
            List<Node<T>> list = new();
            list = GetList(list);
            Node<T>[] nodes = list.ToArray();
            return nodes;
            
        }

        private List<Node<T>> GetList(List<Node<T>> nodes)
        {
            nodes.Add(this);
            if(Left != null)
            {
                Left.GetList(nodes);
            }
            if(Right != null)
            {
                Right.GetList(nodes);
            }
            return nodes;
        }

        public Node(T value)
        {
            Value = value;
        }

        public int CompareTo(Node<T>? other)
        {
            return other == null ? 1 : Value.CompareTo(other.Value);
        }
    }

    public class KeyValuePairs<TK, V> : ICollection<KeyValue<TK, V>>
    {
        private KeyValue<TK, V> Head;
        public IEnumerator<KeyValue<TK, V>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValue<TK, V> item)
        {
            var temp = Head;
            while (true)
            {
                if (temp == null)
                {
                    Head.Next = item;
                    break;
                }

                temp = Head.Next;
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValue<TK, V> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValue<TK, V>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValue<TK, V> item)
        {
            throw new NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
    }

    public class KeyValue<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
        public KeyValue<TKey, TValue> Next;
    }
}

