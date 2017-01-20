namespace RegexTest
{
    internal sealed class RegexAlternate : IRegexItem
    {
        public RegexAlternate(RegexBuffer buffer)
        {
            buffer.AddLookup(this, buffer.Offset, buffer.Offset);

            buffer.MoveNext(); // skip "|"
        }

        public string ToString(int offset)
        {
            return new string(' ', offset) + "or";
        }
    }
}