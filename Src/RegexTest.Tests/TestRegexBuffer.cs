using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using RegexTest;
using System.Text.RegularExpressions;

namespace RegexWorkbench.Tests
{
    [TestClass]
    public class TestRegexBuffer
    {
        [TestMethod]
        public void PropertyInitialValues()
        {
            var expression = "something";
            var regexBuffer = new RegexBuffer(expression);

            regexBuffer.Offset.Should().Be(0);
            regexBuffer.Current.Should().Be(expression[0]);
            regexBuffer.AtEnd.Should().BeFalse();
            regexBuffer.String.Should().Be(expression);
            regexBuffer.ErrorLocation.Should().Be(-1);
            regexBuffer.ErrorLength.Should().Be(-1);
        }

        [TestMethod]
        public void MoveNext()
        {
            var expression = "something";
            var regexBuffer = new RegexBuffer(expression);

            regexBuffer.MoveNext();

            regexBuffer.Offset.Should().Be(1);
            regexBuffer.Current.Should().Be(expression[1]);
            regexBuffer.AtEnd.Should().BeFalse();
            regexBuffer.String.Should().Be(expression.Substring(1));
        }

        [TestMethod]
        public void MoveBy()
        {
            var expression = "something";
            var regexBuffer = new RegexBuffer(expression);

            regexBuffer.MoveBy(2);

            regexBuffer.Offset.Should().Be(2);
            regexBuffer.Current.Should().Be(expression[2]);
            regexBuffer.AtEnd.Should().BeFalse();
            regexBuffer.String.Should().Be(expression.Substring(2));
        }

        [TestMethod]
        public void AtEnd()
        {
            var expression = "something";
            var regexBuffer = new RegexBuffer(expression);

            regexBuffer.MoveBy(expression.Length);

            regexBuffer.AtEnd.Should().BeTrue();
            regexBuffer.String.Should().Be(string.Empty);

            Action a = () => { Console.WriteLine(regexBuffer.Current); };
            a.ShouldThrow<IndexOutOfRangeException>();
        }
    }
}
