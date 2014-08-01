namespace CourseraTasks.CSharp
{
    public static class BitHelper
    {
        public static bool IsBitSet(int number, int position)
        {
            return (number & (1 << position)) != 0;
        }

        public static int SetBit(int number, int position)
        {
            return number | (1 << position);
        }

        public static int ClearBit(int number, int position)
        {
            return number & ~(1 << position);
        }
    }
}