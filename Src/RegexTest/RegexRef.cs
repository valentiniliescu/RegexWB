using System;

namespace RegexTest
{
    public class RegexRef : IComparable<RegexRef>
    { 
        public RegexRef(string stringValue, int start, int end)
        {
            StringValue = stringValue;
            Start = start;
            End = end;
        }

        public string StringValue { get; }

        public int Start { get; }

        public int End { get; }

        public int Length => End - Start + 1;

        public int CompareTo(RegexRef ref2)
        {
            if (Length < ref2.Length)
                return -1;
            if (Length > ref2.Length)
                return 1;
            return 0;
        }

        public bool InRange(int location)
        {
            if (location >= Start && location <= End)
                return true;
            return false;
        }
    }
}