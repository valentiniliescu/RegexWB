using System.Text.RegularExpressions;

namespace RegexTest
{
    /// <summary>
    ///     Summary description for RegexQuantifier.
    /// </summary>
    public class RegexQuantifier : RegexItem
    {
        private readonly string _description;

        public RegexQuantifier(RegexBuffer buffer)
        {
            var startLoc = buffer.Offset;
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
                buffer.Offset += match.Groups[0].Length;

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
            buffer.AddLookup(this, startLoc, buffer.Offset - 1);
        }

        public override string ToString(int offset)
        {
            return _description;
        }
    }
}