using System.Collections;
using System.Text.RegularExpressions;

namespace RegexTest
{
    public sealed class RegexCharacter : IRegexItem
    {
        private string _character;

        public RegexCharacter(RegexBuffer buffer)
        {
            var startLoc = buffer.Offset;
            var quantifier = false;

            switch (buffer.Current)
            {
                case '.':
                    _character = ". (any character)";
                    buffer.MoveNext();
                    Special = true;
                    break;

                case '+':
                    _character = "+ (one or more times)";
                    buffer.MoveNext();
                    Special = true;
                    quantifier = true;
                    break;

                case '*':
                    _character = "* (zero or more times)";
                    buffer.MoveNext();
                    Special = true;
                    quantifier = true;
                    break;

                case '?':
                    _character = "? (zero or one time)";
                    buffer.MoveNext();
                    Special = true;
                    quantifier = true;
                    break;

                case '^':
                    _character = "^ (anchor to start of string)";
                    buffer.MoveNext();
                    break;

                case '$':
                    _character = "$ (anchor to end of string)";
                    buffer.MoveNext();
                    break;

                case ' ':
                    _character = "' ' (space)";
                    buffer.MoveNext();
                    break;

                case '\\':
                    DecodeEscape(buffer);
                    break;

                default:
                    _character = buffer.Current.ToString();
                    buffer.MoveNext();
                    Special = false;
                    break;
            }
            if (quantifier)
                if (!buffer.AtEnd && buffer.Current == '?')
                {
                    _character += " (non-greedy)";
                    buffer.MoveNext();
                }
            buffer.ExpressionLookup.AddLookup(this, startLoc, buffer.Offset - 1, _character.Length == 1);
        }

        private static readonly Hashtable Escaped = new Hashtable();

        static RegexCharacter()
        {
            // character escapes
            Escaped.Add('a', @"A bell (alarm) \u0007 ");
            Escaped.Add('b', @"Word boundary between //w and //W");
            Escaped.Add('B', @"Not at a word boundary between //w and //W");
            Escaped.Add('t', @"A tab \u0009 ");
            Escaped.Add('r', @"A carriage return \u000D ");
            Escaped.Add('v', @"A vertical tab \u000B ");
            Escaped.Add('f', @"A form feed \u000C ");
            Escaped.Add('n', @"A new line \u000A ");
            Escaped.Add('e', @"An escape \u001B ");

            // character classes
            Escaped.Add('w', "Any word character ");
            Escaped.Add('W', "Any non-word character ");
            Escaped.Add('s', "Any whitespace character ");
            Escaped.Add('S', "Any non-whitespace character ");
            Escaped.Add('d', "Any digit ");
            Escaped.Add('D', "Any non-digit ");

            // anchors
            Escaped.Add('A', "Anchor to start of string (ignore multiline)");
            Escaped.Add('Z', "Anchor to end of string or before \\n (ignore multiline)");
            Escaped.Add('z', "Anchor to end of string (ignore multiline)");
        }

        private void DecodeEscape(RegexBuffer buffer)
        {
            buffer.MoveNext();

            _character = (string) Escaped[buffer.Current];
            if (_character == null)
            {
                bool decoded;

                decoded = CheckBackReference(buffer);

                if (!decoded)
                    switch (buffer.Current)
                    {
                        case 'u':
                            buffer.MoveNext();
                            var unicode = buffer.String.Substring(0, 4);
                            _character = "Unicode " + unicode;
                            buffer.Offset += 4;
                            break;

                        case ' ':
                            _character = "' ' (space)";
                            Special = false;
                            buffer.MoveNext();
                            break;

                        case 'c':
                            buffer.MoveNext();
                            _character = "CTRL-" + buffer.Current;
                            buffer.MoveNext();
                            break;

                        case 'x':
                            buffer.MoveNext();
                            var number = buffer.String.Substring(0, 2);
                            _character = "Hex " + number;
                            buffer.Offset += 2;
                            break;

                        default:
                            _character = new string(buffer.Current, 1);
                            Special = false;
                            buffer.MoveNext();
                            break;
                    }
            }
            else
            {
                Special = true;
                buffer.MoveNext();
            }
        }

        private bool CheckBackReference(RegexBuffer buffer)
        {
            // look for \k<name>
            var regex = new Regex(@"
						k\<(?<Name>.+?)\>
						",
                RegexOptions.IgnorePatternWhitespace);

            var match = regex.Match(buffer.String);
            if (match.Success)
            {
                Special = true;
                _character = string.Format("Backreference to match: {0}", match.Groups["Name"]);
                buffer.Offset += match.Groups[0].Length;
                return true;
            }
            return false;
        }

#if fred
\040 An ASCII character as octal (up to three digits); numbers with no leading zero are backreferences if they have only one digit or if they correspond to a capturing group number. (See Backreferences.) The character \040 represents a space. 
\x20 An ASCII character using hexadecimal representation (exactly two digits). 
\cC An ASCII control character; for example, \cC is control-C. 
\u0020 A Unicode character using hexadecimal representation (exactly four digits). 
\ When followed by a character that is not recognized as an escaped character, matches that character. For example, \* is the same as \x2A. 

#endif

        public string ToString(int offset)
        {
            return _character;
        }

        // true if not a normal character...
        public bool Special { get; private set; }
    }
}