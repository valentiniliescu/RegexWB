using System;

namespace RegexTest
{
    /// <summary>
    ///     Summary description for RegexRef.
    /// </summary>
    public class RegexRef : IComparable
    {
        private int _end;

        public RegexRef(IRegexItem regexItem, int start, int end)
        {
            StringValue = regexItem.ToString(0);
            Start = start;
            _end = end;
        }

        public string StringValue { get; set; }

        public int Start { get; }

        public int Length
        {
            get { return _end - Start + 1; }
            set { _end = Start + value - 1; }
        }

        public int CompareTo(object o2)
        {
            var ref2 = (RegexRef) o2;
            if (Length < ref2.Length)
                return -1;
            if (Length > ref2.Length)
                return 1;
            return 0;
        }

        public bool InRange(int location)
        {
            if (location >= Start && location <= _end)
                return true;
            return false;
        }
    }
}