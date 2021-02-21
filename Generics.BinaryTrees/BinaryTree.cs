using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.BinaryTrees
{
    public class BinaryTree<TParametrs> : IEnumerable
        where TParametrs : IComparable
    {
        public TParametrs Value;
        public BinaryTree<TParametrs> Head;
        public BinaryTree<TParametrs> Left;
        public BinaryTree<TParametrs> Right;
        private int total = 0;
                    
        public BinaryTree()
        {
            Head = this;
           // Value = default(TParametrs);
        }
        public void Add(TParametrs value)
        {
            if (total==0)
            {
                total = 1;
                Value = value;
                return;
            }
            BinaryTree<TParametrs> Current = Head;
            while (true)
            {
                if (Current.Value.CompareTo(value) < 0)
                {
                    if (Current.Right == null)
                    {
                        Current.Right = new BinaryTree<TParametrs> { Value = value };
                        break;
                    }
                    else
                        Current = Current.Right;
                }
                else
                {
                    if (Current.Left == null)
                    {
                        Current.Left = new BinaryTree<TParametrs> { Value = value };
                        break;
                    }
                    else
                        Current = Current.Left;
                }
            }
            total++;
        }
        public static implicit operator TParametrs[] (BinaryTree<TParametrs> m)
        {
            return new TParametrs[0];
        }
        public IEnumerable<TParametrs> Walk()
        {
            if (total != 0)
            {
                Stack<BinaryTree<TParametrs>> trees = new Stack<BinaryTree<TParametrs>>();
                BinaryTree<TParametrs> Current = Head;
                trees.Push(Current);
                bool goToLeft = true;
                while (trees.Count > 0)
                {
                    if (goToLeft)
                    {
                        while (Current.Left != null)
                        {
                            trees.Push(Current);
                            Current = Current.Left;
                        }
                    }
                    yield return Current.Value;

                    if (Current.Right != null)
                    {
                        Current = Current.Right;
                        goToLeft = true;
                    }
                    else
                    {
                        Current = trees.Pop();
                        goToLeft = false;
                    }
                }
            }
            yield break;
        }
        public static BinaryTree<TParametrs> Create(params TParametrs[] value)
        {
            BinaryTree<TParametrs> tree = new BinaryTree<TParametrs>();
            for (int i = 0; i < value.Length; i++)
            {
                tree.Add(value[i]);
            }
            return tree;
        }
        public TParametrs First()
        {
            if (total != 0)
            {
                BinaryTree<TParametrs> Current = Head;
                while (Current.Left != null)
                {
                    Current = Current.Left;
                }
                return Current.Value;
            }
            return default(TParametrs);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Walk().GetEnumerator();
        }
    }
    public class BinaryTree : IEnumerable
    {
        public int Value;
        public BinaryTree Head;
        public BinaryTree Left;
        public BinaryTree Right;
        public void Add(int value)
        {
            if (Head == null)
            {
                Head = new BinaryTree { Value = value };
                Head.Head = Head;
                Value = value;
                return;
            }
            BinaryTree Current = Head;
            while (true)
            {
                if (Current.Value.CompareTo(value) <= 0)
                {
                    if (Current.Right == null)
                    {
                        Current.Right = new BinaryTree { Value = value };
                        break;
                    }
                    else
                        Current = Current.Right;
                }
                else
                {
                    if (Current.Left == null)
                    {
                        Current.Left = new BinaryTree { Value = value };
                        break;
                    }
                    else
                        Current = Current.Left;
                }
            }
        }
        public IEnumerable Walk()
        {
            if (Head != null)
            {
                Stack<BinaryTree> trees = new Stack<BinaryTree>();
                BinaryTree Current = Head;
                trees.Push(Current);
                bool goToLeft = true;
                while (trees.Count > 0)
                {
                    if (goToLeft)
                    {
                        while (Current.Left != null)
                        {
                            trees.Push(Current);
                            Current = Current.Left;
                        }
                    }
                    yield return Current.Value;

                    if (Current.Right != null)
                    {
                        Current = Current.Right;
                        goToLeft = true;
                    }
                    else
                    {
                        Current = trees.Pop();
                        goToLeft = false;
                    }
                }
            }
            yield break;
        }
        public static BinaryTree Create(params int[] value)
        {
            BinaryTree tree = new BinaryTree();
            for (int i = 0; i < value.Length; i++)
            {
                tree.Add(value[i]);
            }
            return tree;
        }
        public int First()
        {
            if (Head != null)
            {
                BinaryTree Current = Head;
                while (Current.Left != null)
                {
                    Current = Current.Left;
                }
                return Current.Value;
            }
            return 0;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Walk().GetEnumerator();
        }
    }
}
