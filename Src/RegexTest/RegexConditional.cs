using System;

namespace RegexTest
{
	/// <summary>
	/// Summary description for RegexConditional.
	/// </summary>
	public class RegexConditional: RegexItem
	{
		RegexExpression expression;
		RegexExpression yesNo;
		int startLocation;

			// Handle (?(expression)yes|no)
			// when we get called, we're pointing to the first character of the expression
		public RegexConditional(RegexBuffer buffer)
		{
			startLocation = buffer.Offset;

			expression = new RegexExpression(buffer);
			CheckClosingParen(buffer);

			yesNo = new RegexExpression(buffer);
			CheckClosingParen(buffer);

			buffer.AddLookup(this, startLocation, buffer.Offset - 1);
		}

		void CheckClosingParen(RegexBuffer buffer)
		{
			// check for closing ")"
			char current;
			try
			{
				current = buffer.Current;
			}
				// no closing brace. Set highlight for this capture...
			catch (Exception e)
			{
				buffer.ErrorLocation = startLocation;
				buffer.ErrorLength = 1;
				throw new Exception(
					"Missing closing \')\' in capture", e);
			}
			if (current != ')')
			{
				throw new Exception(
					String.Format("Unterminated closure at offset {0}",
					              buffer.Offset));
			}
			buffer.Offset++;	// eat closing parenthesis
		}

		public override string ToString(int offset)
		{
			string indent = new String(' ', offset);
			string result;
			result = indent + "if: " + expression.ToString(0);

			result += indent + "match: ";

				// walk through until we find an alternation
			foreach (RegexItem item in yesNo.Items)
			{
				if (item is RegexAlternate)
				{
					result += "\r\n" + indent + "else match: ";				
				}
				else
				{
					result += item.ToString(offset);
				}
			}
			result += "\r\n";
			return result;
		}
	}
}
