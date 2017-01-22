using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexTest;

namespace RegexWorkbench.Tests
{
    [TestClass]
    public class TestInterpretAnchor
    {
        private string Interpret(string regex)
        {
            var buffer = new RegexBuffer(regex);
            var expression = new RegexExpression(buffer, buffer.ExpressionLookup);
            var output = expression.ToString(0);
            return output;
        }

        [TestMethod]
        public void TestBegOfString()
        {
            var output = Interpret("^");
            Assert.AreEqual("^ (anchor to start of string)\r\n", output);
        }

        [TestMethod]
        public void TestBegOfStringMultiline()
        {
            var output = Interpret("\\A");
            Assert.AreEqual("Anchor to start of string (ignore multiline)\r\n", output);
        }

        [TestMethod]
        public void TestEndOfString()
        {
            var output = Interpret("$");
            Assert.AreEqual("$ (anchor to end of string)\r\n", output);
        }

        [TestMethod]
        public void TestEndOfStringMultiline()
        {
            var output = Interpret("\\Z");
            Assert.AreEqual("Anchor to end of string or before \\n (ignore multiline)\r\n", output);
        }

        [TestMethod]
        public void TestEndOfStringMultiline2()
        {
            var output = Interpret("\\z");
            Assert.AreEqual("Anchor to end of string (ignore multiline)\r\n", output);
        }

        [TestMethod]
        public void TestWordBoundary()
        {
            var output = Interpret("\\b");
            Assert.AreEqual("Word boundary between //w and //W\r\n", output);
        }

        [TestMethod]
        public void TestNonWordBoundary()
        {
            var output = Interpret("\\B");
            Assert.AreEqual("Not at a word boundary between //w and //W\r\n", output);
        }
    }
}