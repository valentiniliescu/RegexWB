using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexTest;

namespace RegexWorkbench.Tests
{
    [TestClass]
    public class TestRegexRef
    {
        [TestMethod]
        public void Properties()
        {
            var stringValue = "something";
            var start = 2;
            var end = 5;
            var regexRef = new RegexRef(stringValue, start, end);

            regexRef.StringValue.Should().Be(stringValue);
            regexRef.Start.Should().Be(start);
            regexRef.End.Should().Be(end);
            regexRef.Length.Should().Be(end - start + 1);
        }

        [TestMethod]
        public void LessThanComparison()
        {
            var stringValue = "something";
            var regexRef1 = new RegexRef(stringValue, 1, 5);
            var regexRef2 = new RegexRef(stringValue, 3, 8);

            regexRef1.Should().BeLessThan(regexRef2);
        }

        [TestMethod]
        public void GreaterThanComparison()
        {
            var stringValue = "something";
            var regexRef1 = new RegexRef(stringValue, 1, 5);
            var regexRef2 = new RegexRef(stringValue, 3, 8);

            regexRef2.Should().BeGreaterThan(regexRef1);
        }

        [TestMethod]
        public void EqualsComparison()
        {
            var stringValue = "something";
            var regexRef1 = new RegexRef(stringValue, 1, 5);
            var regexRef2 = new RegexRef(stringValue, 3, 7);

            regexRef1.Should().Be(regexRef2);
        }

        [TestMethod]
        public void InRange()
        {
            var stringValue = "something";
            var start = 2;
            var end = 5;
            var regexRef = new RegexRef(stringValue, start, end);

            regexRef.InRange(start).Should().BeTrue();
            regexRef.InRange((start + end) / 2).Should().BeTrue();
            regexRef.InRange(end).Should().BeTrue();
        }

        [TestMethod]
        public void NotInRange()
        {
            var stringValue = "something";
            var start = 2;
            var end = 5;
            var regexRef = new RegexRef(stringValue, start, end);

            regexRef.InRange(start - 1).Should().BeFalse();
            regexRef.InRange(end + 1).Should().BeFalse();
        }

        [TestMethod]
        public void Coalesce()
        {
            var stringValue1 = "something";
            var start1 = 2;
            var end1 = 5;
            var regexRef1 = new RegexRef(stringValue1, start1, end1);
            var stringValue2 = "else";
            var start2 = 6;
            var end2 = 8;
            var regexRef2 = new RegexRef(stringValue2, start2, end2);

            var regexRef = regexRef1.Coalesce(regexRef2);

            regexRef.StringValue.Should().Be(stringValue1 + stringValue2);
            regexRef.Start.Should().Be(start1);
            regexRef.End.Should().Be(end2);
        }
    }
}
