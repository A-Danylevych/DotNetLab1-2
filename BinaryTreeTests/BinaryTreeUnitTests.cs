using System;
using System.Collections.Immutable;
using System.Linq;
using NUnit.Framework;
using BinaryTree;

namespace BinaryTreeTests
{

    public class Tests
    {
        #region TestAdding
        [Test]
        [TestCase(4)]
        [TestCase(-25)]
        [TestCase(0)]
        public void AddingToTheTreeHeadNull(int num)
        {
            var tree = new BinaryTree<int>();
            tree.Add(num);
            Assert.Contains(num, tree);
        }

        [Test]
        [TestCase(4)]
        [TestCase(-25)]
        [TestCase(0)]
        public void AddingToTheTreeHeadNotNull(int num)
        {
            var tree = new BinaryTree<int>(3);
            tree.Add(num);
            Assert.Contains(num, tree);
        }

        [Test]
        [TestCase(4)]
        [TestCase(-25)]
        [TestCase(0)]
        public void AddingToTheTreeAlreadyAdded(int num)
        {
            var tree = new BinaryTree<int>(num);
            tree.Add(num);
            Assert.Contains(num,tree);
        }
        #endregion
        
        #region TestCount
        [Test]
        [TestCase(4, 4,25,0,7)]
        [TestCase(2,3,3,0,0)]
        [TestCase(5, -25,3,5,0,4)]
        [TestCase(0)]
        public void GetCount(int count,params int[] values)
        {
            var tree = new BinaryTree<int>();
            foreach (var item in values)
            {
                tree.Add(item);
            }
            
            Assert.AreEqual(count,tree.Count);
        }
        #endregion

        #region Test Clear
        [Test]
        public void Clear()
        {
            var tree = new BinaryTree<int>() {1, 2, 3, 4, 5};
            tree.Clear();
            Assert.IsEmpty(tree);
        }
        #endregion

        #region Test Contains
        [Test]
        [TestCase(25, true,4,25,0,7)]
        [TestCase(-25, false,3,5,0,4)]
        [TestCase(0, true,3,5,0,4)]
        [TestCase(0, false)]
        public void Contains(int num, bool expected, params int[] values)
        {
            var tree = new BinaryTree<int>();
            foreach (var item in values)
            {
                tree.Add(item);
            }
            Assert.AreEqual(expected, tree.Contains(num));
        }
        #endregion

        #region TestFind
        [Test]
        [TestCase(4, 4,25,0,7)]
        [TestCase(-25, -25,3,5,0,4)]
        [TestCase(0, -25,3,5,0,4)]
        public void Find(int num, params int[] values)
        {
            var tree = new BinaryTree<int>();
            foreach (var item in values)
            {
                tree.Add(item);
            }

            var node = tree.Find(num);
            Assert.AreEqual(num, node?.Value);
        }

        [Test]
        [TestCase(3, 4,25,0,7)]
        [TestCase(2, -25,3,5,0,4)]
        [TestCase(2, -25,3,5,0,4)]
        public void FindNotIncluded(int num, params int[] values)
        {
            var tree = new BinaryTree<int>();
            foreach (var item in values)
            {
                tree.Add(item);
            }

            var node = tree.Find(num);
            
            Assert.Null(node);
        }
        #endregion

        #region Test Remove

        [Test]
        [TestCase(0, true, 0)] //Delete head
        [TestCase(0, true, 2,0)] //Delete right leaf
        [TestCase(0, true, -2,0)] //Delete left leaf
        [TestCase(2, true, 2,0)] // Delete head with right successor
        [TestCase(-2, true, -2,0)] // Delete head with left successor
        [TestCase(2, true, 4,2,0)] // Delete left node with left successor
        [TestCase(2, true, 4,2,3)] // Delete left node with right successor
        [TestCase(2, true, 1,2,3)] // Delete right node with right successor
        [TestCase(2, true, 1,2,3)] // Delete right node with left successor
        [TestCase(2, true, 4,2,3,1,5)] // Delete left node with two successors
        [TestCase(2, true, 2,1,3)] // Delete head with two successors
        [TestCase(2, false)] // Delete from null head
        [TestCase(2, false, 0)]// Delete not existing item
        public void Remove(int num, bool expected, params int[] values)
        {
            var tree = new BinaryTree<int>();
            foreach (var item in values)
            {
                tree.Add(item);
            }
            Assert.AreEqual(expected, tree.Remove(num));
            if (expected)
            {
                Assert.AreEqual(values.Length-1,tree.Count);
            }
            else
            {
                Assert.AreEqual(values.Length, tree.Count);
            }
            
        }
        #endregion

        #region Test CopyTo
        [Test]
        [TestCase(4,25,0,7)]
        [TestCase(-25,3,5,0,4)]
        [TestCase(1,2,3,4,8)]
        public void CopyTo(params int[] values)
        {
            var tree = new BinaryTree<int>();
            foreach (var item in values)
            {
                tree.Add(item);
            }

            int[] array = new int [values.Length];
            tree.CopyTo(array, 0);
            Array.Sort(values);
            Array.Sort(array);
            Assert.AreEqual(values, array);
        }

        [Test]
        [TestCase(4,25,0,7)]
        [TestCase(-25,3,5,0,4)]
        [TestCase(1,2,3,4,8)]
        public void CopyTo_GetIndexOutOfRangeException(int arrayIndex, params int[] values)
        {
            var tree = new BinaryTree<int>();
            foreach (var item in values)
            {
                tree.Add(item);
            }

            int[] array = new int [values.Length];
            Assert.Throws(typeof(IndexOutOfRangeException), () => { tree.CopyTo(array, arrayIndex);});
        }

        [Test]
        [TestCase(4, 25, 0, 7)]
        [TestCase(-25, 3, 5, 0, 4)]
        [TestCase(1, 2, 3, 4, 8)]
        public void CopyToArray(params int[] values)
        {
            var tree = new BinaryTree<int>();
            foreach (var item in values)
            {
                tree.Add(item);
            }

            int[] array = new int [values.Length];
            var newArray = (Array) array;
            tree.CopyTo(newArray, 0);
            Array.Sort(values);
            Array.Sort(newArray);
            Assert.AreEqual(values, newArray);
        }
        #endregion
    }
}