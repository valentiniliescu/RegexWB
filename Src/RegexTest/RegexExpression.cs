using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexTest
{
    /// <summary>
    ///     Summary description for RegexExpression.
    /// </summary>
    public class RegexExpression : RegexItem
    {
        public RegexExpression(RegexBuffer buffer)
        {
            Parse(buffer);
        }

        public ArrayList Items { get; } = new ArrayList();

        public override string ToString(int indent)
        {
            var buf = new StringBuilder();
            var bufChar = new StringBuilder();

            foreach (RegexItem item in Items)
            {
                var regexChar = item as RegexCharacter;
                if (regexChar != null && !regexChar.Special)
                {
                    bufChar.Append(regexChar.ToString(indent));
                }
                else
                {
                    // add any buffered chars...
                    if (bufChar.Length != 0)
                    {
                        buf.Append(new string(' ', indent));
                        buf.Append(bufChar + "\r\n");
                        bufChar = new StringBuilder();
                    }
                    buf.Append(new string(' ', indent));
                    var itemString = item.ToString(indent);
                    if (itemString.Length != 0)
                    {
                        buf.Append(itemString);
                        var newLineAlready = new Regex(@"\r\n$");
                        if (!newLineAlready.IsMatch(itemString))
                            buf.Append("\r\n");
                    }
                }
            }
            if (bufChar.Length != 0)
            {
                buf.Append(new string(' ', indent));
                buf.Append(bufChar + "\r\n");
            }
            return buf.ToString();
        }

        // eat the whole comment until the end of line...
        private void EatComment(RegexBuffer buffer)
        {
            while (buffer.Current != '\r')
                buffer.MoveNext();
        }

        private void Parse(RegexBuffer buffer)
        {
            while (!buffer.AtEnd)
                if (buffer.IgnorePatternWhitespace &&
                    (buffer.Current == ' ' ||
                     buffer.Current == '\r' ||
                     buffer.Current == '\n' ||
                     buffer.Current == '\t'))
                    buffer.MoveNext();
                else
                    switch (buffer.Current)
                    {
                        case '(':
                            Items.Add(new RegexCapture(buffer));
                            break;

                        case ')':
                            // end of closure; just return.
                            return;

                        case '[':
                            Items.Add(new RegexCharClass(buffer));
                            break;

                        case '{':
                            Items.Add(new RegexQuantifier(buffer));
                            break;

                        case '|':
                            Items.Add(new RegexAlternate(buffer));
                            break;

                        case '\\':
                            Items.Add(new RegexCharacter(buffer));
                            break;

                        case '#':
                            if (buffer.IgnorePatternWhitespace)
                                EatComment(buffer);
                            else
                                Items.Add(new RegexCharacter(buffer));
                            break;

                        default:
                            Items.Add(new RegexCharacter(buffer));
                            break;
                    }
        }
    }
}