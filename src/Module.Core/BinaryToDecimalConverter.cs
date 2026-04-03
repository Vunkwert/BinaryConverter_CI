using System;
using System.Text;
using Lab.Interfaces;

namespace Lab.Implementations.GenCode2;

public class BinaryToDecimalConverter : IBinaryToDecimalConverter
{
    public string ToBinary(int decimalNumber)
    {
        if (decimalNumber < 0) throw new ArgumentException();
        if (decimalNumber == 0) return "0";
        StringBuilder sb = new StringBuilder();
        while (decimalNumber > 0) {
            sb.Insert(0, decimalNumber % 2);
            decimalNumber /= 2;
        }
        return sb.ToString();
    }

    public int ToDecimal(string binaryString)
    {
        if (binaryString == null || binaryString == "") throw new ArgumentException();
        int res = 0;
        foreach (char c in binaryString) {
            if (c != '0' && c != '1') throw new ArgumentException();
            res = (res << 1) + (c - '0');
        }
        return res;
    }
}