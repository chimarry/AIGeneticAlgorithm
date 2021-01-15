using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GlobalMaximum
{
    public class Chromosome : IComparable<Chromosome>
    {
        public double FittnessValue { get; private set; }

        public static readonly (int lower, int upper) Limit = (Constants.YLower, Constants.YUpper);

        public BitArray Genes { get; set; }

        public double GenesAsRealNumber { get; private set; }

        public int Lenght { get; private set; }

        public Chromosome(double y)
        {
            SetLenght();
            FromRealNumber(y);
            UpdateValues();
        }

        public Chromosome(BitArray genes)
        {
            Genes = genes;
            Lenght = genes.Length;
            UpdateValues();
        }

        public void UpdateValues()
        {
            GenesAsRealNumber = ToRealNumber();
            FittnessValue = MathUtil.CalculateFunction(GenesAsRealNumber);
        }

        public bool IsValid() => GenesAsRealNumber < Constants.YUpper && GenesAsRealNumber > Constants.YLower;

        public void FromRealNumber(double y)
        {
            static int convertToInt(double number, (int upper, int lower) limit, int n)
            {
                double expression = (number - limit.upper) / (limit.upper - limit.lower);
                return (int)Math.Ceiling(expression * (Math.Pow(2, n) - 1));
            }
            int yInt = convertToInt(y, Limit, Lenght);
            Genes = new BitArray(BitConverter.GetBytes(yInt));
        }

        public double ToRealNumber()
        {
            static double getDouble(int number, (int upper, int lower) limit, int n)
                 => Math.Round(limit.upper + ((limit.upper - limit.lower) / (Math.Pow(2, n) - 1)) * number, Constants.Precision);

            return getDouble(Genes.ToInt(), Limit, Lenght);
        }

        private void SetLenght()
        {
            static int calculateLenghtWithLog(int upper, int lower)
            {
                int precision = (int)Math.Pow(10, Constants.ConversionPrecision);
                return (int)Math.Ceiling(Math.Log2((upper - lower) * precision + 1));
            }

            Lenght = calculateLenghtWithLog(Limit.upper, Limit.lower);
        }

        public override string ToString()
             => $"y = {GenesAsRealNumber} with function value = {FittnessValue}.";

        public int CompareTo([AllowNull] Chromosome other)
        {
            return FittnessValue.CompareTo(other.FittnessValue);
        }
    }

    public class ChromosomeEqualityComparer : IEqualityComparer<Chromosome>
    {
        public bool Equals([AllowNull] Chromosome x, [AllowNull] Chromosome y)
             => x != null && y != null && x.GetHashCode() == y.GetHashCode();

        public int GetHashCode([DisallowNull] Chromosome obj)
             => obj.Genes.GetHashCode();
    }
}
