using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexTest;

namespace RegexWorkbench.Tests
{
    [TestClass]
    public class TestInterpret
    {
        private string Interpret(string regex)
        {
            var buffer = new RegexBuffer(regex);
            var expression = new RegexExpression(buffer, new ExpressionLookup(), false, false);
            var output = expression.ToString(0);
            return output;
        }

        [TestMethod]
        public void TestNormalChars()
        {
            var output = Interpret("Test");
            Assert.AreEqual("Test\r\n", output);
        }


        [TestMethod]
        public void TestCharacterShortcuts()
        {
            var output = Interpret(@"\a");
            Assert.AreEqual("A bell (alarm) \\u0007 \r\n", output);

            output = Interpret(@"\t");
            Assert.AreEqual("A tab \\u0009 \r\n", output);

            output = Interpret(@"\r");
            Assert.AreEqual("A carriage return \\u000D \r\n", output);

            output = Interpret(@"\v");
            Assert.AreEqual("A vertical tab \\u000B \r\n", output);

            output = Interpret(@"\f");
            Assert.AreEqual("A form feed \\u000C \r\n", output);

            output = Interpret(@"\n");
            Assert.AreEqual("A new line \\u000A \r\n", output);

            output = Interpret(@"\e");
            Assert.AreEqual("An escape \\u001B \r\n", output);

            output = Interpret(@"\xFF");
            Assert.AreEqual("Hex FF\r\n", output);

            output = Interpret(@"\cC");
            Assert.AreEqual("CTRL-C\r\n", output);

            output = Interpret(@"\u1234");
            Assert.AreEqual("Unicode 1234\r\n", output);
        }

        [TestMethod]
        public void TestCharacterGroup()
        {
            var output = Interpret("[abcdef]");
            Assert.AreEqual("Any character in \"abcdef\"\r\n", output);
        }

        [TestMethod]
        public void TestCharacterGroupNegated()
        {
            var output = Interpret("[^abcdef]");
            Assert.AreEqual("Any character not in \"abcdef\"\r\n", output);
        }

        [TestMethod]
        public void TestCharacterPeriod()
        {
            var output = Interpret(".");
            Assert.AreEqual(". (any character)\r\n", output);
        }

        [TestMethod]
        public void TestCharacterWord()
        {
            var output = Interpret(@"\w");
            Assert.AreEqual("Any word character \r\n", output);
        }

        [TestMethod]
        public void TestCharacterNonWord()
        {
            var output = Interpret(@"\W");
            Assert.AreEqual("Any non-word character \r\n", output);
        }

        [TestMethod]
        public void TestCharacterWhitespace()
        {
            var output = Interpret(@"\s");
            Assert.AreEqual("Any whitespace character \r\n", output);
        }


        [TestMethod]
        public void TestCharacterNonWhitespace()
        {
            var output = Interpret(@"\S");
            Assert.AreEqual("Any non-whitespace character \r\n", output);
        }

        [TestMethod]
        public void TestCharacterDigit()
        {
            var output = Interpret(@"\d");
            Assert.AreEqual("Any digit \r\n", output);
        }

        [TestMethod]
        public void TestCharacterNonDigit()
        {
            var output = Interpret(@"\D");
            Assert.AreEqual("Any non-digit \r\n", output);
        }

        [TestMethod]
        public void TestQuantifierPlus()
        {
            var output = Interpret(@"+");
            Assert.AreEqual("+ (one or more times)\r\n", output);
        }

        [TestMethod]
        public void TestQuantifierStar()
        {
            var output = Interpret(@"*");
            Assert.AreEqual("* (zero or more times)\r\n", output);
        }

        [TestMethod]
        public void TestQuantifierQuestion()
        {
            var output = Interpret(@"?");
            Assert.AreEqual("? (zero or one time)\r\n", output);
        }

        [TestMethod]
        public void TestQuantifierFromNtoM()
        {
            var output = Interpret(@"{1,2}");
            Assert.AreEqual("At least 1, but not more than 2 times\r\n", output);
        }

        [TestMethod]
        public void TestQuantifierAtLeastN()
        {
            var output = Interpret(@"{5,}");
            Assert.AreEqual("At least 5 times\r\n", output);
        }

        [TestMethod]
        public void TestQuantifierExactlyN()
        {
            var output = Interpret(@"{12}");
            Assert.AreEqual("Exactly 12 times\r\n", output);
        }

        [TestMethod]
        public void TestQuantifierPlusNonGreedy()
        {
            var output = Interpret(@"+?");
            Assert.AreEqual("+ (one or more times) (non-greedy)\r\n", output);
        }

        [TestMethod]
        public void TestQuantifierStarNonGreedy()
        {
            var output = Interpret(@"*?");
            Assert.AreEqual("* (zero or more times) (non-greedy)\r\n", output);
        }

        [TestMethod]
        public void TestQuantifierQuestionNonGreedy()
        {
            var output = Interpret(@"??");
            Assert.AreEqual("? (zero or one time) (non-greedy)\r\n", output);
        }

        [TestMethod]
        public void TestQuantifierFromNtoMNonGreedy()
        {
            var output = Interpret(@"{1,2}?");
            Assert.AreEqual("At least 1, but not more than 2 times (non-greedy)\r\n", output);
        }

        [TestMethod]
        public void TestQuantifierAtLeastNNonGreedy()
        {
            var output = Interpret(@"{5,}?");
            Assert.AreEqual("At least 5 times (non-greedy)\r\n", output);
        }

        [TestMethod]
        public void TestQuantifierExactlyNNonGreedy()
        {
            var output = Interpret(@"{12}?");
            Assert.AreEqual("Exactly 12 times (non-greedy)\r\n", output);
        }
    }
}