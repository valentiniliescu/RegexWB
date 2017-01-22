using System.Text.RegularExpressions;

namespace RegexTest
{
    public sealed class RegexCharClass : IRegexItem
    {
        //RegexExpression expression;
        private readonly string _description;

        public RegexCharClass(RegexBuffer buffer, ExpressionLookup expressionLookup)
        {
            var startLoc = buffer.Offset;

            buffer.MoveNext();

            Regex regex;
            Match match;

            regex = new Regex(@"(?<Negated>\^?)(?<Class>.+?)\]");

            match = regex.Match(buffer.String);
            if (match.Success)
            {
                if (match.Groups["Negated"].ToString() == "^")
                    _description = string.Format("Any character not in \"{0}\"",
                        match.Groups["Class"]);
                else
                    _description = string.Format("Any character in \"{0}\"",
                        match.Groups["Class"]);
                buffer.Offset += match.Groups[0].Length;
            }
            else
            {
                _description = "missing ']' in character class";
            }
            int endLocation = buffer.Offset - 1;
            expressionLookup.AddLookup(new RegexRef(this.ToString(0), startLoc, endLocation));
        }

        public string ToString(int offset)
        {
            return _description;
        }
    }
}