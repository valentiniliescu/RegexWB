using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexTest
{
	[TestClass]
	public class TestInterpretAnchor
	{
	    string Interpret(string regex)
		{
			RegexBuffer buffer = new RegexBuffer(regex);
			RegexExpression expression = new RegexExpression(buffer);
			string output = expression.ToString(0);
			return output;
		}

		[TestMethod]
		public void TestBegOfString()
		{
			string output = Interpret("^");
			Assert.AreEqual("^ (anchor to start of string)\r\n", output);
		}

		[TestMethod]
		public void TestBegOfStringMultiline()
		{
			string output = Interpret("\\A");
			Assert.AreEqual("Anchor to start of string (ignore multiline)\r\n", output);
		}

		[TestMethod]
		public void TestEndOfString()
		{
			string output = Interpret("$");
			Assert.AreEqual("$ (anchor to end of string)\r\n", output);
		}

		[TestMethod]
		public void TestEndOfStringMultiline()
		{
			string output = Interpret("\\Z");
			Assert.AreEqual("Anchor to end of string or before \\n (ignore multiline)\r\n", output);
		}

		[TestMethod]
		public void TestEndOfStringMultiline2()
		{
			string output = Interpret("\\z");
			Assert.AreEqual("Anchor to end of string (ignore multiline)\r\n", output);
		}

        [TestMethod]
        public void TestWordBoundary()
		{
			string output = Interpret("\\b");
			Assert.AreEqual("Word boundary between //w and //W\r\n", output);
		}

        [TestMethod]
        public void TestNonWordBoundary()
		{
			string output = Interpret("\\B");
			Assert.AreEqual("Not at a word boundary between //w and //W\r\n", output);
		}

	}
}
