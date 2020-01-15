using System;

namespace Service.Model
{
	public interface ICalculationOperation
	{
		double PerformOperation(string op, double x, double y);
		void AddOperation(string op, Func<double, double, double> body);
	}
}