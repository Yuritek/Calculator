using System;
using System.Collections.Generic;
using System.Globalization;
using Service.Extension;
using Service.Model;

namespace Service
{
	public class PostfixNotationExpression
	{
		private readonly IPostfixNotationParseEquation _postfixNotationParseEquation;
		private readonly ICalculationOperation _calculationOperation;

		public PostfixNotationExpression(ICalculationOperation calculationOperation,IPostfixNotationParseEquation postfixNotationParseEquation)
		{
			_postfixNotationParseEquation = postfixNotationParseEquation;
			_calculationOperation = calculationOperation;
		}
		public double Execute(string input)
		{
			if (string.IsNullOrEmpty(input))
				return 0;
			Stack<string> stack = new Stack<string>();
			Queue<string> queue = new Queue<string>(_postfixNotationParseEquation.GetPostfixNotationEquation(input));
			string str = queue.Dequeue();
			while (queue.Count >= 0)
			{
				if (str.ToTryParseToDouble(out _))
				{
					stack.Push(str);
					str = queue.Count > 0 ? queue.Dequeue() : string.Empty;
				}
				else
				{
					double summ = 0;
					try
					{
						if (stack.Count == 1)
						{
							if (stack.Pop().ToTryParseToDouble(out var arg3))
								summ = arg3;
						}
						else
						{
							bool res = stack.Pop().ToTryParseToDouble(out var arg1);
							bool res2 = stack.Pop().ToTryParseToDouble(out var arg2);

							if (res && res2)
							{
								summ = _calculationOperation.PerformOperation(str, arg2, arg1);
							}
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}

					stack.Push(summ.ToString(CultureInfo.InvariantCulture));
					if (queue.Count > 0)
						str = queue.Dequeue();
					else
						break;
				}
			}
			return stack.Pop().ConvertToDouble();
		}
	}
}