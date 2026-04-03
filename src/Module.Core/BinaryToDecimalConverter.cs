using System;
using Lab.Interfaces;

namespace Lab.Implementations.GenCode1;

public class BinaryToDecimalConverter : IBinaryToDecimalConverter
{
    public string ToBinary(int decimalNumber)
    {
        if (decimalNumber < 0) throw new ArgumentException("Число < 0");
        return Convert.ToString(decimalNumber, 2);
    }

    public int ToDecimal(string binaryString)
    {
        if (string.IsNullOrEmpty(binaryString)) throw new ArgumentException("Пусто");
        try { return Convert.ToInt32(binaryString, 2); }
        catch { throw new ArgumentException("Ошибка формата"); }
    }
}