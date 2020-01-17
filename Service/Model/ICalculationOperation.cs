using System;
using Service.Enums;

namespace Service.Model
{
	public interface ICalculationOperation
	{
		double PerformOperation(string op, double x, double y);
		void AddOperation(string op, Func<double, double, double> body, OperationCategory operationCategory);
		byte GetPriority(string op);
	}
}