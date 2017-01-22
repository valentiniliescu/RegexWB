using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexTest;

namespace RegexWorkbench.Tests
{
    [TestClass]
    public class TestInterpretOptions
    {
        private string Interpret(string regex)
        {
            var buffer = new RegexBuffer(regex);
            var expression = new RegexExpression(buffer, new ExpressionLookup());
            var output = expression.ToString(0);
            return output;
        }

        [TestMethod]
        public void TestIgnoreCase()
        {
            var output = Interpret("(?i:)");
            Assert.AreEqual("Set options to Ignore Case\r\n", output);
        }

        [TestMethod]
        public void TestIgnoreCaseOff()
        {
            var output = Interpret("(?-i:)");
            Assert.AreEqual("Set options to Ignore Case Off\r\n", output);
        }

        [TestMethod]
        public void TestMultiline()
        {
            var output = Interpret("(?m:)");
            Assert.AreEqual("Set options to Multiline\r\n", output);
        }

        [TestMethod]
        public void TestMultilineOff()
        {
            var output = Interpret("(?-m:)");
            Assert.AreEqual("Set options to Multiline Off\r\n", output);
        }

        [TestMethod]
        public void TestExplicitCapture()
        {
            var output = Interpret("(?n:)");
            Assert.AreEqual("Set options to Explicit Capture\r\n", output);
        }

        [TestMethod]
        public void TestExplicitCaptureOff()
        {
            var output = Interpret("(?-n:)");
            Assert.AreEqual("Set options to Explicit Capture Off\r\n", output);
        }

        [TestMethod]
        public void TestSingleline()
        {
            var output = Interpret("(?s:)");
            Assert.AreEqual("Set options to Singleline\r\n", output);
        }

        [TestMethod]
        public void TestSinglelineOff()
        {
            var output = Interpret("(?-s:)");
            Assert.AreEqual("Set options to Singleline Off\r\n", output);
        }

        [TestMethod]
        public void TestIgnoreWhitespace()
        {
            var output = Interpret("(?x:)");
            Assert.AreEqual("Set options to Ignore Whitespace\r\n", output);
        }

        [TestMethod]
        public void TestIgnoreWhitespaceOff()
        {
            var output = Interpret("(?-x:)");
            Assert.AreEqual("Set options to Ignore Whitespace Off\r\n", output);
        }
    }
}