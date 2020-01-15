using Moq;
using NUnit.Framework;
using Service.Model;

namespace Service.Tests
{
	[TestFixture]
	public class PostfixNotationExpressionTests
	{
		private PostfixNotationExpression _calculationOperation;

		[SetUp]
		public void Initialize()
		{
			Mock<ICalculationOperation> mockCalculationOperation = new Mock<ICalculationOperation>();
			mockCalculationOperation.Setup(m => m.PerformOperation("+", 4, 6)).Returns(10);
			mockCalculationOperation.Setup(m => m.PerformOperation("+", 4, 2)).Returns(6);
			mockCalculationOperation.Setup(m => m.PerformOperation("*", 6, 3)).Returns(18);
			mockCalculationOperation.Setup(m => m.PerformOperation("*", 2, 3)).Returns(6);

			Mock<IPostfixNotationParseEquation> mockPostfixNotationParseEquation = new Mock<IPostfixNotationParseEquation>();
			mockPostfixNotationParseEquation.Setup(m => m.GetPostfixNotationEquation("(4+2)*3")).Returns(new[] {"4", "2", "+", "3", "*"});
			mockPostfixNotationParseEquation.Setup(m => m.GetPostfixNotationEquation("4+2*3")).Returns(new[] {"4", "2", "3", "*", "+"});
			mockPostfixNotationParseEquation.Setup(m => m.GetPostfixNotationEquation("5")).Returns(new[] {"5"});

			_calculationOperation =
				new PostfixNotationExpression(mockCalculationOperation.Object, mockPostfixNotationParseEquation.Object);
		}

		[TestCase("4+2*3", 10)]
		[TestCase("(4+2)*3", 18)]
		[TestCase("5", 5)]
		public void Execute_must_return_calculated_value(string inputValue, double expectedResult)
		{
			var result = _calculationOperation.Execute(inputValue);

			Assert.AreEqual(expectedResult, result);
		}
	}
}