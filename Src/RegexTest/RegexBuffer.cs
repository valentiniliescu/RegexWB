using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegexTest
{
    /// <summary>
    ///     String with a pointer in it.
    /// </summary>
    public class RegexBuffer
    {
        private readonly string _expression;

        private readonly IList<RegexRef> _expressionLookup = new List<RegexRef>();
        private bool _inSeries;
        private readonly RegexOptions _regexOptions;

        public RegexBuffer(string expression, RegexOptions regexOptions = RegexOptions.None)
        {
            _expression = expression;
            _regexOptions = regexOptions;
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

        public void MoveNext()
        {
            Offset++;
        }

        public void ClearInSeries()
        {
            _inSeries = false;
        }

        public void AddLookup(IRegexItem item, int startLocation, int endLocation, bool canCoalesce = false)
        {
            if (_inSeries)
            {
                // in a series, add character to the previous one...
                if (canCoalesce)
                {
                    var lastItem = _expressionLookup[_expressionLookup.Count - 1];
                    _expressionLookup[_expressionLookup.Count - 1] = new RegexRef(lastItem.StringValue + item.ToString(0), lastItem.Start, endLocation );
                }
                else
                {
                    _expressionLookup.Add(new RegexRef(item.ToString(0), startLocation, endLocation));
                    _inSeries = false;
                }
            }
            else
            {
                if (canCoalesce)
                    _inSeries = true;
                _expressionLookup.Add(new RegexRef(item.ToString(0), startLocation, endLocation));
            }
        }

        public RegexRef MatchLocations(int spot)
        {
            var locations = new List<RegexRef>();
            foreach (RegexRef regexRef in _expressionLookup)
                if (regexRef.InRange(spot))
                    locations.Add(regexRef);
            locations.Sort();
            if (locations.Count != 0)
                return locations[0];
            return null;
        }
    }
}