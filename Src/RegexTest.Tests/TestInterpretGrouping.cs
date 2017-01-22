using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexTest;

namespace RegexWorkbench.Tests
{
    [TestClass]
    public class TestInterpretGrouping
    {
        private string Interpret(string regex)
        {
            var buffer = new RegexBuffer(regex);
            var expression = new RegexExpression(buffer, buffer.ExpressionLookup);
            var output = expression.ToString(0);
            return output;
        }

        [TestMethod]
        public void TestCapture()
        {
            var output = Interpret("(abc)");
            Assert.AreEqual("Capture\r\n  abc\r\nEnd Capture\r\n", output);
        }

        [TestMethod]
        public void TestNamedCapture()
        {
            var output = Interpret("(?<L>abc)");
            Assert.AreEqual("Capture to <L>\r\n  abc\r\nEnd Capture\r\n", output);
        }

        [TestMethod]
        public void TestNonCapture()
        {
            var output = Interpret("(?:abc)");
            Assert.AreEqual("Non-capturing Group\r\n  abc\r\nEnd Capture\r\n", output);
        }

        [TestMethod]
        public void TestAlternation()
        {
            var output = Interpret("(a|b)");
            Assert.AreEqual("Capture\r\n  a\r\n    or\r\n  b\r\nEnd Capture\r\n", output);
        }

        [TestMethod]
        public void TestPositiveLookahead()
        {
            var output = Interpret("(?=a)");
            Assert.AreEqual("zero-width positive lookahead\r\n  a\r\nEnd Capture\r\n", output);
        }

        [TestMethod]
        public void TestNegativeLookahead()
        {
            var output = Interpret("(?!b)");
            Assert.AreEqual("zero-width negative lookahead\r\n  b\r\nEnd Capture\r\n", output);
        }

        [TestMethod]
        public void TestPositiveLookbehind()
        {
            var output = Interpret("(?<=c)");
            Assert.AreEqual("zero-width positive lookbehind\r\n  c\r\nEnd Capture\r\n", output);
        }

        [TestMethod]
        public void TestNegativeLookbehind()
        {
            var output = Interpret("(?<!d)");
            Assert.AreEqual("zero-width negative lookbehind\r\n  d\r\nEnd Capture\r\n", output);
        }

        [TestMethod]
        public void TestConditionalExpression()
        {
            var output = Interpret("(?(abc)yes|no)");
            Assert.AreEqual(
                "Conditional Subexpression\r\n  if: abc\r\n  match: yes\r\n  else match: no\r\nEnd Capture\r\n", output);
        }

        [TestMethod]
        public void TestConditionalNamed()
        {
            var output = Interpret("(?(<V>)yes|no)");
            Assert.AreEqual(
                "Conditional Subexpression\r\n  if: <V>\r\n  match: yes\r\n  else match: no\r\nEnd Capture\r\n", output);
        }
    }
}