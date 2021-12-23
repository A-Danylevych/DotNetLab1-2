using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTree
{
    public class NodeEnum<T> : IEnumerator<T> where T: IComparable<T>
    {
        private readonly Node<T>[]? _nodes;

        private int _position = -1;
        public T Current { get; private set; } = default!;

        object IEnumerator.Current => Current;


        public void Dispose() 
        {
            GC.SuppressFinalize(this);
        }


        public bool MoveNext()
        {
            _position++;
            if (_position >= _nodes.Length)
            {
                return false;
            }
            else
            {
                Current = _nodes[_position].Value;
                return true;
            }
        }

        public void Reset()
        {
            _position = -1;
        }
        public NodeEnum(Node<T>[]? nodes)
        {
            _nodes = nodes;
        }
    }
}