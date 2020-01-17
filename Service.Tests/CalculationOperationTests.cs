using System;
using NUnit.Framework;
using Service.Enums;
using Service.Model;
using Assert = NUnit.Framework.Assert;

namespace Service.Tests
{
	[TestFixture]
	public class CalculationOperationTests
	{
		private ICalculationOperation _calculationOperation;

		[SetUp]
		public void Initialize()
		{
			_calculationOperation = new CalculationOperation();
		}

		[Test]
		public void AddOperation_should_return_ArgementException_since_operation_already_exists()
		{
			Assert.Throws<ArgumentException>(() => _calculationOperation.AddOperation("+", (x, y) => x + y,OperationCategory.Additive),
				"Операция '+' уже существует");
		}

		[Test]
		public void PerformOperation_should_return_value_for_remainder_of_division()
		{
			_calculationOperation.AddOperation("%", (x, y) => x % y,OperationCategory.Multiplicative);

			var result = _calculationOperation.PerformOperation("%", 5, 4);

			Assert.AreEqual(1, result);
		}

		[TestCase("+", 4, 4, 8)]
		[TestCase("+", -4, 5, 1)]
		[TestCase("-", 5, 2, 3)]
		[TestCase("*", 3, 3, 9)]
		[TestCase("/", 6, 3, 2)]
		[TestCase("+", 4.2, 4.2, 8.4)]
		public void PerformOperation_should_return_correct_value(string operand, double arg1, double arg2,
			double expectedResult)
		{
			var result = _calculationOperation.PerformOperation(operand, arg1, arg2);

			Assert.AreEqual(expectedResult, result);
		}

		[Test]
		public void PerformOperation_should_return_ArgementException_if_operation_not_found()
		{
			Assert.Throws<ArgumentException>(() => _calculationOperation.PerformOperation("%", 1, 1),
				"Операция '%' не определена");
		}


		[TestCase("+", 1)]
		[TestCase("*", 2)]
		public void GetPriority_should_return_number_priority(string operand, int priority)
		{
			var result = _calculationOperation.GetPriority(operand);

			Assert.AreEqual(priority, result);
		}

		[Test]
		public void GetPriority_should_return_zero_if_operand_is_empty_or_null()
		{
			var result = _calculationOperation.GetPriority(string.Empty);
			Assert.AreEqual(0, result);
		}
    }
}