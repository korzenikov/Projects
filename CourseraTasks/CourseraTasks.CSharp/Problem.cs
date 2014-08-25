using System.Collections.Generic;

namespace CourseraTasks.CSharp
{
    public class Problem
    {
        public Problem(IReadOnlyCollection<ClauseStates> states)
        {
            States = states;
        }

        public IReadOnlyCollection<ClauseStates> States { get; private set; }
    }
}
