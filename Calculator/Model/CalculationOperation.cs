using System;
using System.Collections.Generic;

namespace Service.Model
{
	public class CalculationOperation : ICalculationOperation
	{
		private readonly Dictionary<string, Func<double, double, double>> _operations;

		public CalculationOperation()
		{
			_operations = new Dictionary<string, Func<double, double, double>>
			{
				{"+", (x, y) => x + y},
				{"-", (x, y) => x - y},
				{"*", (x, y) => x * y},
				{"/", (x, y) => x / y}
			};
		}

		public double PerformOperation(string op, double x, double y)
		{
			if (!_operations.ContainsKey(op))
				throw new ArgumentException($"Операция '{op}' не определена");
			return _operations[op](x, y);
		}

		public void AddOperation(string op, Func<double, double, double> body)
		{
			if (_operations.ContainsKey(op))
				throw new ArgumentException($"Операция '{op}' уже существует");
			_operations.Add(op, body);
		}
	}
}