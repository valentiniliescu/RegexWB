using System.Text.RegularExpressions;

namespace RegexTest
{
    public sealed class RegexQuantifier : IRegexItem
    {
        private string _description;

        public RegexQuantifier(RegexBuffer buffer, ExpressionLookup expressionLookup)
        {
            var startLoc = buffer.Offset;

            Parse(buffer);

            int endLocation = buffer.Offset - 1;
            expressionLookup.AddLookup(new RegexRef(this.ToString(0), startLoc, endLocation));
        }

        private void Parse(RegexBuffer buffer)
        {
            buffer.MoveNext();

            Regex regex;
            Match match;

            // look for "n}", "n,}", or "n,m}"
            regex = new Regex(@"(?<n>\d+)(?<Comma>,?)(?<m>\d*)\}");

            match = regex.Match(buffer.String);
            if (match.Success)
            {
                if (match.Groups["m"].Length != 0)
                    _description = string.Format("At least {0}, but not more than {1} times",
                        match.Groups["n"], match.Groups["m"]);
                else if (match.Groups["Comma"].Length != 0)
                    _description = string.Format("At least {0} times",
                        match.Groups["n"]);
                else
                    _description = string.Format("Exactly {0} times",
                        match.Groups["n"]);
                buffer.MoveBy(match.Groups[0].Length);

                if (!buffer.AtEnd && buffer.Current == '?')
                {
                    _description += " (non-greedy)";
                    buffer.MoveNext();
                }
            }
            else
            {
                _description = "missing '}' in quantifier";
            }
        }

        public string ToString(int offset)
        {
            return _description;
        }
    }
}