using System.Collections.Generic;
using Service.Extension;
using static System.Char;

namespace Service.Model
{
    public class PostfixNotationParseEquation : IPostfixNotationParseEquation
	{
		private readonly ICalculationOperation _calculationOperation;

		public PostfixNotationParseEquation(ICalculationOperation calculationOperation)
		{
			_calculationOperation = calculationOperation;
		}

		private IEnumerable<string> ParseEquation(string input)
		{
			int pos = 0;
			while (pos < input.Length)
			{
				string s = input[pos].ToString();
				if (IsDigit(input[pos]) || input[pos] == '-' && ((pos > 0 && !IsDigit(input[pos - 1])) || pos == 0))
				{
					for (int i = pos + 1;
						i < input.Length &&
						(IsDigit(input[i]) || input[i] == ',' || input[i] == '.');
						i++)
						s += input[i];
				}
				yield return s;
				pos += s.Length;
			}
		}

		public string[] GetPostfixNotationEquation(string input)
		{
			List<string> outputSeparated = new List<string>();
			Stack<string> stack = new Stack<string>();
			foreach (string c in ParseEquation(input))
			{
				if (!c.ToTryParseToDouble(out _))
				{
					if (stack.Count > 0 && !c.Equals("("))
					{
						if (c.Equals(")"))
						{
							string s = stack.Pop();
							while (s != "(")
							{
								outputSeparated.Add(s);
								s = stack.Pop();
							}
						}
						else if (stack.Peek() != "(" &&_calculationOperation.GetPriority(c) > _calculationOperation.GetPriority(stack.Peek()))
							stack.Push(c);
						else
						{
							while (stack.Count > 0 && stack.Peek() != "(" && _calculationOperation.GetPriority(c) <= _calculationOperation.GetPriority(stack.Peek()))
								outputSeparated.Add(stack.Pop());
							stack.Push(c);
						}
					}
					else
						stack.Push(c);
				}
				else
					outputSeparated.Add(c);
			}

			if (stack.Count > 0)
				foreach (string c in stack)
					outputSeparated.Add(c);

			return outputSeparated.ToArray();
		}
	}
}