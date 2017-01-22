namespace RegexTest
{
    internal sealed class RegexAlternate : IRegexItem
    {
        public RegexAlternate(RegexBuffer buffer)
        {
            int startLocation = buffer.Offset;
            int endLocation = buffer.Offset;
            buffer.ExpressionLookup.AddLookup(new RegexRef(this.ToString(0), startLocation, endLocation));

            buffer.MoveNext(); // skip "|"
        }

        public string ToString(int offset)
        {
            return new string(' ', offset) + "or";
        }
    }
}