using System;

namespace CourseraTasks.CSharp
{
    [Flags]
    public enum ClauseStates
    {
        Undetermined = 0,
        LeftFalse = 1,
        RightFalse = 2,
        False = LeftFalse | RightFalse,
        True = 4
    }
}
