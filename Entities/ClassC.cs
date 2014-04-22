using System.Collections.Generic;

namespace Entities
{
    public class ClassC
    {
        public ClassD D { get; set; }

        public ICollection<ClassE> E { get; set; }
    }
}