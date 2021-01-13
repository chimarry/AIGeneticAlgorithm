using System;

namespace GeneticAlgorithm
{
    public sealed class MathUtil
    {
        public delegate double TwoVariableFunction(double x, double y);

        private readonly TwoVariableFunction currentFunction;

        public MathUtil()
            => this.currentFunction = CalculateDefaultFunction;

        public MathUtil(TwoVariableFunction currentFunction)
            => this.currentFunction = currentFunction;

        /// <summary>
        /// Invokes specified function on two parameters.
        /// </summary>
        /// <param name="x">Parameter on X axis</param>
        /// <param name="y">Parameter on Y axis</param>
        public double Calculate(double x, double y)
            => currentFunction(x, y);

        /// <summary>
        /// Calculates function given with the expression:
        /// z(x,y) = 3*(1−x)^2*e^(−(x^2+(y+1)^2))−7((x/5)−x^3−y^5)*e^(−(x^2+y^2))−(1/3)*e^(−((x+2)^2+y^2)).
        /// </summary>
        /// <param name="x">First parameter (on X axis)</param>
        /// <param name="y">Second paramter (on Y axis)</param>
        private static double CalculateDefaultFunction(double x, double y)
        {
            double part1exponent = NegativeOf(Square(x) + Square(y + 1));
            double result = 3 * Square(1 - x) * Math.Exp(part1exponent);

            double part2exponent = NegativeOf(Square(x) + Square(y));
            result += (-7) * (x / 5 - Math.Pow(x, 3) - Math.Pow(y, 5)) * Math.Exp(part2exponent);

            double part3exponent = NegativeOf(Square(x + 2) + Square(y));
            result += NegativeOf(Math.Exp(part3exponent)) / 3;

            return result;
        }

        private static double Square(double x)
            => Math.Pow(x, 2);

        private static double NegativeOf(double x)
            => (-1) * x;
    }
}
