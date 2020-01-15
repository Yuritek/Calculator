using Moq;
using NUnit.Framework;
using Service.Model;

namespace Service.Tests
{
	class PostfixNotationParseEquationTests
	{
		private IPostfixNotationParseEquation _calculationOperation;

		[SetUp]
		public void Initialize()
		{
			var mock = new Mock<IPriorityOperation>();
			mock.Setup(m => m.GetPriority("+")).Returns(2);
			mock.Setup(m => m.GetPriority("*")).Returns(3);
			_calculationOperation = new PostfixNotationParseEquation(mock.Object);
		}

		[TestCase("(4+2)*3", new[] {"4", "2", "+", "3", "*"})]
		[TestCase("4+2*3", new[] {"4", "2", "3", "*", "+"})]
		[TestCase("4+2", new[] {"4", "2", "+"})]
		[TestCase("4%2", new[] {"4", "2", "%"})]
		public void ConvertToPostfixNotation_should_return_massive_value_string(string inputValue, string[] expectedResult)
		{
			var result = _calculationOperation.GetPostfixNotationEquation(inputValue);

			Assert.AreEqual(expectedResult, result);
		}
	}
}