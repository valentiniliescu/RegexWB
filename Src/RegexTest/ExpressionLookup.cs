using System.Collections.Generic;
using System.Linq;

namespace RegexTest
{
    public class ExpressionLookup
    {
        private readonly IList<RegexRef> _expressionLookup = new List<RegexRef>();
        private bool _inSeries;

        public void ClearInSeries()
        {
            _inSeries = false;
        }

        public void AddLookup(RegexRef regexRef, bool canCoalesce = false)
        {
            if (_inSeries)
            {
                // in a series, add character to the previous one...
                if (canCoalesce)
                {
                    var lastItem = _expressionLookup[_expressionLookup.Count - 1];
                    _expressionLookup[_expressionLookup.Count - 1] = new RegexRef(lastItem.StringValue + regexRef.StringValue, lastItem.Start, regexRef.End );
                }
                else
                {
                    _expressionLookup.Add(regexRef);
                    _inSeries = false;
                }
            }
            else
            {
                if (canCoalesce)
                    _inSeries = true;
                _expressionLookup.Add(regexRef);
            }
        }

        public RegexRef MatchLocations(int spot)
        {
            return _expressionLookup.Where(regexRef => regexRef.InRange(spot)).DefaultIfEmpty().Min();
        }
    }
}