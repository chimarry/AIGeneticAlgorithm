using System;
using System.Collections;

namespace GeneticAlgorithm.Chromosomes
{
    public class Chromosome
    {
        public double FittnessValue { get; private set; }

        public static readonly (int lower, int upper) XLimit = (Constants.XLower, Constants.XUpper);

        public static readonly (int lower, int upper) YLimit = (Constants.YLower, Constants.YUpper);

        public BitArray XGenes { get; set; }

        public BitArray YGenes { get; set; }

        public double XGenesAsRealNumber { get; private set; }

        public double YGenesAsRealNumber { get; private set; }

        public (int x, int y) Lenght { get; private set; }

        public Chromosome(double x, double y)
        {
            SetLenghts();
            FromRealNumber(x, y);
            UpdateValues();
        }

        public Chromosome(BitArray childTwoX, BitArray childTwoY)
        {
            XGenes = childTwoX;
            YGenes = childTwoY;
            Lenght = (childTwoX.Length, childTwoY.Length);
            UpdateValues();
        }

        public void UpdateValues()
        {
            (XGenesAsRealNumber, YGenesAsRealNumber) = ToRealNumber();
            FittnessValue = MathUtil.CalculateFunction(XGenesAsRealNumber, YGenesAsRealNumber);
        }

        public void FromRealNumber(double x, double y)
        {
            static int convertToInt(double number, (int upper, int lower) limit, int n)
            {
                double expression = (number - limit.upper) / (limit.upper - limit.lower);
                return (int)Math.Ceiling(expression * (Math.Pow(2, n) - 1));
            }
            int xInt = convertToInt(x, XLimit, Lenght.x);
            int yInt = convertToInt(y, YLimit, Lenght.y);
            XGenes = new BitArray(BitConverter.GetBytes(xInt));
            YGenes = new BitArray(BitConverter.GetBytes(yInt));
        }

        public (double x, double y) ToRealNumber()
        {
            static double getDouble(int number, (int upper, int lower) limit, int n)
                 => Math.Round(limit.upper + ((limit.upper - limit.lower) / (Math.Pow(2, n) - 1)) * number, Constants.Precision);

            return (getDouble(XGenes.ToInt(), XLimit, Lenght.x), getDouble(YGenes.ToInt(), YLimit, Lenght.y));
        }

        private void SetLenghts()
        {
            static int calculateLenghtWithLog(int upper, int lower)
            {
                int precision = (int)Math.Pow(10, Constants.ConversionPrecision);
                return (int)Math.Ceiling(Math.Log2((upper - lower) * precision + 1));
            }

            Lenght = (calculateLenghtWithLog(XLimit.upper, XLimit.lower), calculateLenghtWithLog(YLimit.upper, YLimit.lower));
        }
    }
}