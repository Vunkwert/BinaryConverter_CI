using System;
using Lab.Interfaces;

namespace Lab.Implementations.GenCode1
{
    public class BinaryToDecimalConverter : IBinaryToDecimalConverter
    {
        public string ToBinary(int decimalNumber)
        {
            if (decimalNumber < 0)
                throw new ArgumentException("Число должно быть положительным.");
            return Convert.ToString(decimalNumber, 2);
        }

        public int ToDecimal(string binaryString)
        {
            if (string.IsNullOrWhiteSpace(binaryString))
                throw new ArgumentException("Строка не может быть пустой.");
            
            // Проверка на корректность символов (0 и 1)
            foreach (char c in binaryString)
            {
                if (c != '0' && c != '1')
                    throw new ArgumentException("Строка содержит недопустимые символы.");
            }

            return Convert.ToInt32(binaryString, 2);
        }
    }
}
