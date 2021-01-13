using System;
using System.Collections;

namespace GeneticAlgorithm
{
    public class Chromosome
    {
        public BitArray XGenes { get; set; }

        public BitArray YGenes { get; set; }

        public double XGenesAsRealNumber { get; set; }

        public double YGenesAsRealNumber { get; set; }

        public int Precision { get; set; } = 3;

        public (int lower, int upper) XLimit { get; set; } = (-3, 3);

        public (int lower, int upper) YLimit { get; set; } = (-4, 4);

        public (int x, int y) Lenght { get; private set; }

        public Chromosome(double x, double y)
        {
            SetLenghts();
            FromRealNumber(x, y);
            (XGenesAsRealNumber, YGenesAsRealNumber) = ToRealNumber();
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
                 => limit.upper + ((limit.upper - limit.lower) / (Math.Pow(2, n) - 1)) * number;

            return (getDouble(XGenes.ToInt(), XLimit, Lenght.x), getDouble(YGenes.ToInt(), YLimit, Lenght.y));
        }

        private void SetLenghts()
        {
            int calculateLenghtWithLog(int upper, int lower)
            {
                int precision = (int)Math.Pow(10, Precision);
                return (int)Math.Ceiling(Math.Log2((upper - lower) * precision + 1));
            }

            Lenght = (calculateLenghtWithLog(XLimit.upper, XLimit.lower), calculateLenghtWithLog(YLimit.upper, YLimit.lower));
        }
    }
}