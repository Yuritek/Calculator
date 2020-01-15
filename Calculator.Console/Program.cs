using System;
using Service;
using Service.Model;

namespace Calculator.App
{
	static class Program
	{
		static void Main(string[] args)
		{
			var parser = new PostfixNotationExpression(new CalculationOperation(), new PostfixNotationParseEquation(new PriorityOperation()));
			while (true)
			{
				Console.WriteLine("Результат:"+parser.Execute(Console.ReadLine()));
			}
		}
	}
}