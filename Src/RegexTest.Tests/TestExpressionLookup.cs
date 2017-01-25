using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexTest;

namespace RegexWorkbench.Tests
{
    [TestClass]
    public class TestExpressionLookup
    {
        [TestMethod]
        public void Match()
        {
            var regexRef = new RegexRef("something", 0, 3);
            var expressionLookup = new ExpressionLookup();

            expressionLookup.AddLookup(regexRef);

            expressionLookup.MatchLocations(1).Should().Be(regexRef);
        }

        [TestMethod]
        public void NoMatch()
        {
            var regexRef = new RegexRef("something", 0, 3);
            var expressionLookup = new ExpressionLookup();

            expressionLookup.AddLookup(regexRef);

            expressionLookup.MatchLocations(5).Should().BeNull();
        }

        [TestMethod]
        public void MatchNested()
        {
            var regexRefInner = new RegexRef("something", 0, 3);
            var regexRefOuter = new RegexRef("something", 0, 6);
            var expressionLookup = new ExpressionLookup();

            expressionLookup.AddLookup(regexRefInner);
            expressionLookup.AddLookup(regexRefOuter);

            expressionLookup.MatchLocations(1).Should().Be(regexRefInner);
        }

        [TestMethod]
        public void MatchCoalesce()
        {
            var regexRef1 = new RegexRef("something", 0, 3);
            var regexRef2 = new RegexRef("else", 4, 5);
            var expressionLookup = new ExpressionLookup();

            expressionLookup.AddLookup(regexRef1, true);
            expressionLookup.AddLookup(regexRef2, true);

            expressionLookup.MatchLocations(1).Should().Be(regexRef1.Coalesce(regexRef2));
        }

        [TestMethod]
        public void MatchCoalesceAndRegular()
        {
            var regexRef1 = new RegexRef("something", 0, 3);
            var regexRef2 = new RegexRef("else", 4, 5);
            var regexRef3 = new RegexRef("totally", 6, 7);
            var expressionLookup = new ExpressionLookup();

            expressionLookup.AddLookup(regexRef1, true);
            expressionLookup.AddLookup(regexRef2);
            expressionLookup.AddLookup(regexRef3, true);

            expressionLookup.MatchLocations(1).Should().Be(regexRef1);
            expressionLookup.MatchLocations(4).Should().Be(regexRef2);
            expressionLookup.MatchLocations(6).Should().Be(regexRef3);
        }

        [TestMethod]
        public void MatchCoalesceWithReset()
        {
            var regexRef1 = new RegexRef("something", 0, 3);
            var regexRef2 = new RegexRef("else", 4, 5);
            var expressionLookup = new ExpressionLookup();

            expressionLookup.AddLookup(regexRef1, true);
            expressionLookup.ClearInSeries();
            expressionLookup.AddLookup(regexRef2, true);

            expressionLookup.MatchLocations(1).Should().Be(regexRef1);
            expressionLookup.MatchLocations(4).Should().Be(regexRef2);
        }
    }
}
