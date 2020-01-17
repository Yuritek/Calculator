using System;
using System.Collections.Generic;
using Service.Enums;

namespace Service.Model
{
	public class CalculationOperation : ICalculationOperation
	{
		private readonly Dictionary<string, (OperationCategory Op, Func<double, double, double> Fn)> _operations;

		public CalculationOperation()
		{
			_operations = new Dictionary<string, (OperationCategory Op, Func<double, double, double> Fn)>
			{
				{"+", (OperationCategory.Additive, (x, y) => x + y)},
				{"-", (OperationCategory.Additive, (x, y) => x - y)},
				{"*", (OperationCategory.Multiplicative, (x, y) => x * y)},
				{"/", (OperationCategory.Multiplicative, (x, y) => x / y)}
			};
		}

		public double PerformOperation(string op, double x, double y)
		{
			if (!_operations.ContainsKey(op))
				throw new ArgumentException($"Операция '{op}' не определена");
			return _operations[op].Fn(x, y);
		}

		public void AddOperation(string op, Func<double, double, double> body, OperationCategory operationCategory)
		{
			if (_operations.ContainsKey(op))
				throw new ArgumentException($"Операция '{op}' уже существует");
			_operations.Add(op, (operationCategory, body));
		}

		public byte GetPriority(string op)
		{
			return (byte) (string.IsNullOrEmpty(op) ? 0 : _operations[op].Op);
		}
	}
}