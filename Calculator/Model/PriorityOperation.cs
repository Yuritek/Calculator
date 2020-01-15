using System.Collections.Generic;

namespace Service.Model
{
	public class PriorityOperation : IPriorityOperation
	{
		private readonly Dictionary<string, byte> _dictPriority;

		public PriorityOperation()
		{
			_dictPriority = new Dictionary<string, byte>
			{
				{"(", 1},
				{")", 1},
				{"+", 2},
				{"-", 2},
				{"*", 3},
				{"/", 3},
			};
		}
		public byte GetPriority(string s)
		{
			return string.IsNullOrEmpty(s) ? (byte) 0 : _dictPriority[s];
		}
	}
}