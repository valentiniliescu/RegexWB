using System;
using System.Text.RegularExpressions;

namespace RegexTest
{
    /// <summary>
    ///     String with a pointer in it.
    /// </summary>
    public class RegexBuffer
    {
        private readonly string _expression;

        private readonly RegexOptions _regexOptions;

        public RegexBuffer(string expression, RegexOptions regexOptions = RegexOptions.None)
        {
            _expression = expression;
            _regexOptions = regexOptions;
            ExpressionLookup = new ExpressionLookup();
        }

        public char Current
        {
            get
            {
                if (Offset >= _expression.Length)
                    throw new Exception("Beyond end of buffer");
                return _expression[Offset];
            }
        }

        public bool AtEnd => Offset >= _expression.Length;

        public int Offset { get; set; }

        public string String => _expression.Substring(Offset);

        public int ErrorLocation { get; set; } = -1;

        public int ErrorLength { get; set; } = -1;

        public bool IgnorePatternWhitespace => (_regexOptions & RegexOptions.IgnorePatternWhitespace) != 0;

        public bool ExplicitCapture => (_regexOptions & RegexOptions.ExplicitCapture) != 0;

        public ExpressionLookup ExpressionLookup { get; }

        public void MoveNext()
        {
            Offset++;
        }
    }
}