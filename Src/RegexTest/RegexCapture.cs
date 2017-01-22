using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace RegexTest
{
    public sealed class RegexCapture : IRegexItem
    {
        private static readonly Hashtable OptionNames = new Hashtable();
        private string _description = "Capture";
        private IRegexItem _expression;
        private readonly int _startLocation;

        static RegexCapture()
        {
            OptionNames.Add("i", "Ignore Case");
            OptionNames.Add("-i", "Ignore Case Off");
            OptionNames.Add("m", "Multiline");
            OptionNames.Add("-m", "Multiline Off");
            OptionNames.Add("n", "Explicit Capture");
            OptionNames.Add("-n", "Explicit Capture Off");
            OptionNames.Add("s", "Singleline");
            OptionNames.Add("-s", "Singleline Off");
            OptionNames.Add("x", "Ignore Whitespace");
            OptionNames.Add("-x", "Ignore Whitespace Off");
        }

        public RegexCapture(RegexBuffer buffer)
        {
            _startLocation = buffer.Offset;
            buffer.MoveNext();

            // we're not in a series of normal characters, so clear
            buffer.ExpressionLookup.ClearInSeries();

            // if the first character of the capture is a '?',
            // we need to decode what comes after it.
            if (buffer.Current == '?')
            {
                var decoded = CheckNamed(buffer);

                if (!decoded)
                    decoded = CheckBalancedGroup(buffer);

                if (!decoded)
                    decoded = CheckNonCapturing(buffer);

                if (!decoded)
                    decoded = CheckOptions(buffer);

                if (!decoded)
                    decoded = CheckLookahead(buffer);

                if (!decoded)
                    decoded = CheckNonBacktracking(buffer);

                if (!decoded)
                    CheckConditional(buffer);
            }
            else
                // plain old capture...
            {
                if (!HandlePlainOldCapture(buffer))
                    throw new Exception(
                        string.Format("Unrecognized capture: {0}", buffer.String));
            }
            int endLocation = buffer.Offset - 1;
            buffer.ExpressionLookup.AddLookup(new RegexRef(this.ToString(0), _startLocation, endLocation));
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

        private bool HandlePlainOldCapture(RegexBuffer buffer)
        {
            // we're already at the expression. Just create a new
            // expression, and make sure that we're at a ")" when 
            // we're done
            if (buffer.ExplicitCapture)
                _description = "Non-capturing Group";

            _expression = new RegexExpression(buffer);
            CheckClosingParen(buffer);
            return true;
        }

        private bool CheckNamed(RegexBuffer buffer)
        {
            Regex regex;
            Match match;

            // look for ?<Name> or ?'Name' syntax...
            regex = new Regex(@"
				        ^                         # anchor to start of string
						\?(\<|')                  # ?< or ?'
						(?<Name>[a-zA-Z0-9]+?)    # Capture name
						(\>|')                    # ?> or ?'
						(?<Rest>.+)               # The rest of the string
						",
                RegexOptions.IgnorePatternWhitespace);

            match = regex.Match(buffer.String);
            if (match.Success)
            {
                _description = string.Format("Capture to <{0}>", match.Groups["Name"]);

                // advance buffer to the rest of the expression
                buffer.Offset += match.Groups["Rest"].Index;
                _expression = new RegexExpression(buffer);

                CheckClosingParen(buffer);
                return true;
            }
            return false;
        }

        private bool CheckNonCapturing(RegexBuffer buffer)
        {
            // Look for non-capturing ?:

            var regex = new Regex(@"
				        ^                         # anchor to start of string
						\?:
						(?<Rest>.+)             # The rest of the expression
						",
                RegexOptions.IgnorePatternWhitespace);
            var match = regex.Match(buffer.String);
            if (match.Success)
            {
                _description = "Non-capturing Group";

                buffer.Offset += match.Groups["Rest"].Index;
                _expression = new RegexExpression(buffer);

                CheckClosingParen(buffer);
                return true;
            }
            return false;
        }


        private bool CheckBalancedGroup(RegexBuffer buffer)
        {
            // look for ?<Name1-Name2> or ?'Name1-Name2' syntax...
            // look for ?<Name> or ?'Name' syntax...
            var regex = new Regex(@"
				        ^                         # anchor to start of string
						\?[\<|']                  # ?< or ?'
						(?<Name1>[a-zA-Z]+?)       # Capture name1
						-
						(?<Name2>[a-zA-Z]+?)       # Capture name2
						[\>|']                    # ?> or ?'
						(?<Rest>.+)               # The rest of the expression
						",
                RegexOptions.IgnorePatternWhitespace);

            var match = regex.Match(buffer.String);
            if (match.Success)
            {
                _description = string.Format("Balancing Group <{0}>-<{1}>", match.Groups["Name1"], match.Groups["Name2"]);
                buffer.Offset += match.Groups["Rest"].Index;
                _expression = new RegexExpression(buffer);
                CheckClosingParen(buffer);
                return true;
            }
            return false;
        }


        private bool CheckOptions(RegexBuffer buffer)
        {
            // look for ?imnsx-imnsx:
            var regex = new Regex(@"
				        ^                         # anchor to start of string
						\?(?<Options>[imnsx-]+):
						",
                RegexOptions.IgnorePatternWhitespace);

            var match = regex.Match(buffer.String);
            if (match.Success)
            {
                var option = match.Groups["Options"].Value;
                _description = string.Format("Set options to {0}",
                    OptionNames[option]);
                _expression = null;
                buffer.Offset += match.Groups[0].Length;
                return true;
            }
            return false;
        }

        private bool CheckLookahead(RegexBuffer buffer)
        {
            var regex = new Regex(@"
				        ^                         # anchor to start of string
						\?
						(?<Assertion><=|<!|=|!)   # assertion char
						(?<Rest>.+)               # The rest of the expression
						",
                RegexOptions.IgnorePatternWhitespace);

            var match = regex.Match(buffer.String);
            if (match.Success)
            {
                switch (match.Groups["Assertion"].Value)
                {
                    case "=":
                        _description = "zero-width positive lookahead";
                        break;

                    case "!":
                        _description = "zero-width negative lookahead";
                        break;

                    case "<=":
                        _description = "zero-width positive lookbehind";
                        break;

                    case "<!":
                        _description = "zero-width negative lookbehind";
                        break;
                }
                buffer.Offset += match.Groups["Rest"].Index;
                _expression = new RegexExpression(buffer);
                CheckClosingParen(buffer);
                return true;
            }
            return false;
        }

        private bool CheckNonBacktracking(RegexBuffer buffer)
        {
            // Look for non-backtracking sub-expression ?>

            var regex = new Regex(@"
				        ^                         # anchor to start of string
						\?\>
						(?<Rest>.+)             # The rest of the expression
						",
                RegexOptions.IgnorePatternWhitespace);
            var match = regex.Match(buffer.String);
            if (match.Success)
            {
                _description = "Non-backtracking subexpressio";

                buffer.Offset += match.Groups["Rest"].Index;
                _expression = new RegexExpression(buffer);

                CheckClosingParen(buffer);
                return true;
            }
            return false;
        }

        private void CheckConditional(RegexBuffer buffer)
        {
            // Look for conditional (?(name)yesmatch|nomatch)
            // (name can also be an expression)

            var regex = new Regex(@"
				        ^                         # anchor to start of string
						\?\(
						(?<Rest>.+)             # The rest of the expression
						",
                RegexOptions.IgnorePatternWhitespace);
            var match = regex.Match(buffer.String);
            if (match.Success)
            {
                _description = "Conditional Subexpression";

                buffer.Offset += match.Groups["Rest"].Index;
                _expression = new RegexConditional(buffer);
            }
        }

        public string ToString(int offset)
        {
            var result = _description;
            if (_expression != null)
                result += "\r\n" + _expression.ToString(offset + 2) +
                          new string(' ', offset) + "End Capture";
            return result;
        }
    }
}