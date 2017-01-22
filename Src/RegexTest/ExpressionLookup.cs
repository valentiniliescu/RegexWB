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
            return _expressionLookup.Where(regexRef => regexRef.InRange(spot)).DefaultIfEmpty().Min();
        }
    }
}