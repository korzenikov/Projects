namespace CourseraTasks.CSharp
{
    public class Clause
    {
        public Clause(Literal literal1, Literal literal2)
        {
            Literal1 = literal1;
            Literal2 = literal2;
        }

        public Literal Literal1 { get; private set; }

        public Literal Literal2 { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} ∨ {1}", Literal1, Literal2);
        }
    }
}
