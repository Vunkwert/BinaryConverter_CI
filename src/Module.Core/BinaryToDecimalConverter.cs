using System;
using System.Linq;
using Lab.Interfaces;

namespace Lab.Implementations.GenCode3;

public class BinaryToDecimalConverter : IBinaryToDecimalConverter
{
    public string ToBinary(int decimalNumber) => 
        decimalNumber >= 0 ? Convert.ToString(decimalNumber, 2) : throw new ArgumentException();

    public int ToDecimal(string binaryString)
    {
        if (string.IsNullOrWhiteSpace(binaryString) || !binaryString.All(c => c == '0' || c == '1'))
            throw new ArgumentException();
        return Convert.ToInt32(binaryString, 2);
    }
}