namespace Lab.Interfaces;

public interface IBinaryToDecimalConverter
{
    string ToBinary(int decimalNumber);
    int ToDecimal(string binaryString);
}
