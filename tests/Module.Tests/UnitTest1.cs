using NUnit.Framework;
using Lab.Implementations.GenCode2;

namespace Lab.Tests;

public class BinaryConverterTests
{
    private BinaryToDecimalConverter _converter;

    [SetUp]
    public void Setup()
    {
        _converter = new BinaryToDecimalConverter();
    }

    [Test]
    [TestCase(10, "1010")]
    [TestCase(0, "0")]
    public void ToBinary_ValidInput_ReturnsCorrectString(int input, string expected)
    {
        Assert.That(_converter.ToBinary(input), Is.EqualTo(expected));
    }

    [Test]
    [TestCase("1010", 10)]
    public void ToDecimal_ValidString_ReturnsCorrectInt(string input, int expected)
    {
        Assert.That(_converter.ToDecimal(input), Is.EqualTo(expected));
    }
}
