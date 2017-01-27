namespace RegexTest
{
    internal sealed class RegexAlternate : IRegexItem
    {
        public RegexAlternate(RegexBuffer buffer, ExpressionLookup expressionLookup)
        {
            int startLocation = buffer.Offset;

            Parse(buffer);

            int endLocation = buffer.Offset - 1;
            expressionLookup.AddLookup(new RegexRef(this.ToString(0), startLocation, endLocation));
        }

        private void Parse(RegexBuffer buffer)
        {
            buffer.MoveNext(); // skip "|"
        }

        public string ToString(int offset)
        {
            return new string(' ', offset) + "or";
        }
    }
}