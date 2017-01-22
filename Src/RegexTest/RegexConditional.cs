using System;

namespace RegexTest
{
    public sealed class RegexConditional : IRegexItem
    {
        private readonly RegexExpression _expression;
        private readonly int _startLocation;
        private readonly RegexExpression _yesNo;

        // Handle (?(expression)yes|no)
        // when we get called, we're pointing to the first character of the expression
        public RegexConditional(RegexBuffer buffer)
        {
            _startLocation = buffer.Offset;

            _expression = new RegexExpression(buffer);
            CheckClosingParen(buffer);

            _yesNo = new RegexExpression(buffer);
            CheckClosingParen(buffer);

            buffer.ExpressionLookup.AddLookup(this, _startLocation, buffer.Offset - 1);
        }

        private void CheckClosingParen(RegexBuffer buffer)
        {
            // check for closing ")"
            char current;
            try
            {
                current = buffer.Current;
            }
            // no closing brace. Set highlight for this capture...
            catch (Exception e)
            {
                buffer.ErrorLocation = _startLocation;
                buffer.ErrorLength = 1;
                throw new Exception(
                    "Missing closing \')\' in capture", e);
            }
            if (current != ')')
                throw new Exception(
                    string.Format("Unterminated closure at offset {0}",
                        buffer.Offset));
            buffer.Offset++; // eat closing parenthesis
        }

        public string ToString(int offset)
        {
            var indent = new string(' ', offset);
            string result;
            result = indent + "if: " + _expression.ToString(0);

            result += indent + "match: ";

            // walk through until we find an alternation
            foreach (IRegexItem item in _yesNo.Items)
                if (item is RegexAlternate)
                    result += "\r\n" + indent + "else match: ";
                else
                    result += item.ToString(offset);
            result += "\r\n";
            return result;
        }
    }
}