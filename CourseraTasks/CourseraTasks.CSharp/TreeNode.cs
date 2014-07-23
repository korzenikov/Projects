﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks.CSharp
{
    public class TreeNode<T>
    {

        public TreeNode(T value, TreeNode<T> left, TreeNode<T> right)
        {
            Value = value;
            Left = left;
            Right = right;
        }


        public T Value { get; private set; }


        public TreeNode<T> Left { get; private set; }

        public TreeNode<T> Right { get; private set; }
    }
}
