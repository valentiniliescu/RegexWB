using System;

namespace RegexTest
{
    /// <summary>
    ///     String with a pointer in it.
    /// </summary>
    public class RegexBuffer
    {
        private readonly string _expression;

        public RegexBuffer(string expression)
        {
            _expression = expression;
        }

        public char Current
        {
            get
            {
                if (AtEnd)
                    throw new IndexOutOfRangeException();
                return _expression[Offset];
            }
        }

        public bool AtEnd => Offset >= _expression.Length;

        public int Offset { get; private set; }

        public string String => _expression.Substring(Offset);

        public int ErrorLocation { get; set; } = -1;

        public int ErrorLength { get; set; } = -1;

        public void MoveNext()
        {
            Offset++;
        }

        public void MoveBy(int offset)
        {
            Offset += offset;
        }
    }
}