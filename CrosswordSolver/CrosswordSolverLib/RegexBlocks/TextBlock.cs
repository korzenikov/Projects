namespace CrosswordSolverLib.RegexBlocks
{
    public class TextBlock : RegexBlock
    {
        public TextBlock(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
    }
}
