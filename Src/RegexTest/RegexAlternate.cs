namespace RegexTest
{
    internal sealed class RegexAlternate : IRegexItem
    {
        public RegexAlternate(RegexBuffer buffer, ExpressionLookup expressionLookup)
        {
            int startLocation = buffer.Offset;
            int endLocation = buffer.Offset;
            expressionLookup.AddLookup(new RegexRef(this.ToString(0), startLocation, endLocation));

            buffer.MoveNext(); // skip "|"
        }

        public string ToString(int offset)
        {
            return new string(' ', offset) + "or";
        }
    }
}