namespace Lab.Implementations.GenCode1;
namespace Lab.Interfaces;

public interface IBinaryToDecimalConverter
{
    string ToBinary(int decimalNumber);
    int ToDecimal(string binaryString);
}
