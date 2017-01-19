using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexTest
{
	[TestClass]
	public class TestInterpretOptions
	{
	    string Interpret(string regex)
		{
			RegexBuffer buffer = new RegexBuffer(regex);
			RegexExpression expression = new RegexExpression(buffer);
			string output = expression.ToString(0);
			return output;
		}

		[TestMethod]
		public void TestIgnoreCase()
		{
			string output = Interpret("(?i:)");
			Assert.AreEqual("Set options to Ignore Case\r\n", output);
		}

        [TestMethod]
        public void TestIgnoreCaseOff()
		{
			string output = Interpret("(?-i:)");
			Assert.AreEqual("Set options to Ignore Case Off\r\n", output);
		}

        [TestMethod]
        public void TestMultiline()
		{
			string output = Interpret("(?m:)");
			Assert.AreEqual("Set options to Multiline\r\n", output);
		}

        [TestMethod]
        public void TestMultilineOff()
		{
			string output = Interpret("(?-m:)");
			Assert.AreEqual("Set options to Multiline Off\r\n", output);
		}

        [TestMethod]
        public void TestExplicitCapture()
		{
			string output = Interpret("(?n:)");
			Assert.AreEqual("Set options to Explicit Capture\r\n", output);
		}

        [TestMethod]
        public void TestExplicitCaptureOff()
		{
			string output = Interpret("(?-n:)");
			Assert.AreEqual("Set options to Explicit Capture Off\r\n", output);
		}

        [TestMethod]
        public void TestSingleline()
		{
			string output = Interpret("(?s:)");
			Assert.AreEqual("Set options to Singleline\r\n", output);
		}

        [TestMethod]
        public void TestSinglelineOff()
		{
			string output = Interpret("(?-s:)");
			Assert.AreEqual("Set options to Singleline Off\r\n", output);
		}

        [TestMethod]
        public void TestIgnoreWhitespace()
		{
			string output = Interpret("(?x:)");
			Assert.AreEqual("Set options to Ignore Whitespace\r\n", output);
		}

        [TestMethod]
        public void TestIgnoreWhitespaceOff()
		{
			string output = Interpret("(?-x:)");
			Assert.AreEqual("Set options to Ignore Whitespace Off\r\n", output);
		}

	}
}
