using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace RegexTest
{
    /// <summary>
    ///     String with a pointer in it.
    /// </summary>
    public class RegexBuffer
    {
        private readonly string _expression;

        private readonly ArrayList _expressionLookup = new ArrayList();
        private bool _inSeries;
        private RegexOptions _regexOptions;

        public RegexBuffer(string expression)
        {
            _expression = expression;
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

        public bool AtEnd
        {
            get { return Offset >= _expression.Length; }
        }

        public int Offset { get; set; }

        public string String
        {
            get { return _expression.Substring(Offset); }
        }

        public int ErrorLocation { get; set; } = -1;

        public int ErrorLength { get; set; } = -1;

        public RegexOptions RegexOptions
        {
            set { _regexOptions = value; }
        }

        public bool IgnorePatternWhitespace
        {
            get { return (_regexOptions & RegexOptions.IgnorePatternWhitespace) != 0; }
        }

        public bool ExplicitCapture
        {
            get { return (_regexOptions & RegexOptions.ExplicitCapture) != 0; }
        }

        public void MoveNext()
        {
            Offset++;
        }

        public void AddLookup(RegexItem item, int startLocation, int endLocation)
        {
            AddLookup(item, startLocation, endLocation, false);
        }

        public void ClearInSeries()
        {
            _inSeries = false;
        }

        public void AddLookup(RegexItem item, int startLocation, int endLocation, bool canCoalesce)
        {
            if (_inSeries)
            {
                // in a series, add character to the previous one...
                if (canCoalesce)
                {
                    var lastItem = (RegexRef) _expressionLookup[_expressionLookup.Count - 1];
                    lastItem.StringValue += item.ToString(0);
                    lastItem.Length += endLocation - startLocation + 1;
                }
                else
                {
                    _expressionLookup.Add(new RegexRef(item, startLocation, endLocation));
                    _inSeries = false;
                }
            }
            else
            {
                if (canCoalesce)
                    _inSeries = true;
                _expressionLookup.Add(new RegexRef(item, startLocation, endLocation));
            }
        }

        public RegexRef MatchLocations(int spot)
        {
            var locations = new ArrayList();
            foreach (RegexRef regexRef in _expressionLookup)
                if (regexRef.InRange(spot))
                    locations.Add(regexRef);
            locations.Sort();
            if (locations.Count != 0)
                return (RegexRef) locations[0];
            return null;
        }
    }
}