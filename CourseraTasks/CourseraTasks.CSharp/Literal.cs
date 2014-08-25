namespace CourseraTasks.CSharp
{
    public class Literal
    {
        public Literal(int index)
        {
            Index = index;
            Negation = false;
        }

        public int Index { get; private set; }

        public bool Negation { get; private set; }

        public Literal Negate()
        {
            return new Literal(Index) { Negation = !Negation };
        }

        public override string ToString()
        {
            return (Negation ? "¬" : string.Empty) + Index;
        }
    }
}
