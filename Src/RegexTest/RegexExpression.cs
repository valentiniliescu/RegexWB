using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexTest
{
    public sealed class RegexExpression : IRegexItem
    {
        public RegexExpression(RegexBuffer buffer, ExpressionLookup expressionLookup, bool ignorePatternWhitespace, bool explicitCapture)
        {
            Parse(buffer, expressionLookup, ignorePatternWhitespace, explicitCapture);
        }

        public List<IRegexItem> Items { get; } = new List<IRegexItem>();

        public string ToString(int indent)
        {
            var buf = new StringBuilder();
            var bufChar = new StringBuilder();

            foreach (IRegexItem item in Items)
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

        private void Parse(RegexBuffer buffer, ExpressionLookup expressionLookup, bool ignorePatternWhitespace, bool explicitCapture)
        {
            while (!buffer.AtEnd)
                if (ignorePatternWhitespace &&
                    (buffer.Current == ' ' ||
                     buffer.Current == '\r' ||
                     buffer.Current == '\n' ||
                     buffer.Current == '\t'))
                    buffer.MoveNext();
                else
                    switch (buffer.Current)
                    {
                        case '(':
                            Items.Add(new RegexCapture(buffer, expressionLookup, ignorePatternWhitespace, explicitCapture));
                            break;

                        case ')':
                            // end of closure; just return.
                            return;

                        case '[':
                            Items.Add(new RegexCharClass(buffer, expressionLookup));
                            break;

                        case '{':
                            Items.Add(new RegexQuantifier(buffer, expressionLookup));
                            break;

                        case '|':
                            Items.Add(new RegexAlternate(buffer, expressionLookup));
                            break;

                        case '\\':
                            Items.Add(new RegexCharacter(buffer, expressionLookup));
                            break;

                        case '#':
                            if (ignorePatternWhitespace)
                                EatComment(buffer);
                            else
                                Items.Add(new RegexCharacter(buffer, expressionLookup));
                            break;

                        default:
                            Items.Add(new RegexCharacter(buffer, expressionLookup));
                            break;
                    }
        }
    }
}