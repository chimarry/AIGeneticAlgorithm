using System;

namespace GlobalMaximum
{
    public static class MathUtil
    {
        /// <summary>
        /// Calculates function given with the expression:
        /// z(y) = 3*e^(−(y+1)^2)+7(y^5)*e^(−(y^2))−(1/3)*e^(−(4+y^2)).
        /// </summary>
        /// <param name="y">Second paramter (on Y axis)</param>
        public static double CalculateFunction(double y)
        {
            double result = 3 * Math.Exp(NegativeOf(Square(y + 1)));

            double part2exponent = NegativeOf(Square(y));
            result += 7 * Math.Pow(y, 5) * Math.Exp(part2exponent);

            double part3exponent = NegativeOf(4 + Square(y));
            result += NegativeOf(Math.Exp(part3exponent)) / 3;

            return result;
        }

        private static double Square(double x)
            => Math.Pow(x, 2);

        private static double NegativeOf(double x)
            => (-1) * x;
    }
}