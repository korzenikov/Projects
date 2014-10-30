﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexBlocks
{
    public class BackreferenceBlock : RegexBlock
    {
        public BackreferenceBlock(int groupIndex)
        {
            GroupIndex = groupIndex;
        }
        
        public int GroupIndex { get; private set; }


        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            return Equals((BackreferenceBlock)obj);
        }

        private bool Equals(BackreferenceBlock obj)
        {
            return GroupIndex == obj.GroupIndex;
        }
    }
}
