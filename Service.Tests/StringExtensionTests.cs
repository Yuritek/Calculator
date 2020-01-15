using NUnit.Framework;
using Service.Extension;

namespace Service.Tests
{
	class StringExtensionTests
	{
		[Test]
		public void ToTryParseToDouble_should_return_formatted_number()
		{
			var result = "4,2".ToTryParseToDouble(out var estimatedValue);

			Assert.IsTrue(result);
			Assert.AreEqual(4.2, estimatedValue);
		}

		[Test]
		public void ToTryParseToDouble_should_return_false_if_string_not_contains_number()
		{
			var result = "wwe".ToTryParseToDouble(out var estimatedValue);
			Assert.IsFalse(result);
			Assert.AreEqual(0, estimatedValue);
		}

		[Test]
		public void ConvertToDouble_should_return_false_if_string_not_contains_number()
		{
			var result = "3.1".ConvertToDouble();
			Assert.AreEqual(3.1, result);
		}
	}
}