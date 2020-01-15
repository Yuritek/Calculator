using NUnit.Framework;
using Service.Model;

namespace Service.Tests
{
	[TestFixture]
	class PriorityOperationTests
	{
		private IPriorityOperation _priorityOperation;

		[SetUp]
		public void Initialize()
		{
			_priorityOperation = new PriorityOperation();
		}

		[TestCase("+", 2)]
		[TestCase("*", 3)]
		public void GetPriority_should_return_number_priority(string operand, int priority)
		{
			var result = _priorityOperation.GetPriority(operand);

			Assert.AreEqual(priority, result);
		}

		[Test]
		public void GetPriority_should_return_zero_if_operand_is_empty_or_null()
		{
			var result = _priorityOperation.GetPriority(string.Empty);
			Assert.AreEqual(0, result);
		}
	}
}