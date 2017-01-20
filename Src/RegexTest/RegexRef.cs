using System;

namespace RegexTest
{
    public class RegexRef : IComparable
    {
        private int _end;

        public RegexRef(string stringValue, int start, int end)
        {
            StringValue = stringValue;
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