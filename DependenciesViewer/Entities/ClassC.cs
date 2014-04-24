using System;
using System.Collections.Generic;

namespace Entities
{
    public class ClassC
    {
        public DateTime ModifiedAt { get; set; }
     
        public ClassD D { get; set; }

        public ICollection<ClassE> E { get; set; }
    }
}