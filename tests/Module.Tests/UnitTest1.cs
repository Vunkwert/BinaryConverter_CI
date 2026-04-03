using Module.Core;
using NUnit.Framework;
using Lab.Interfaces;
using Lab.Implementations.GenCode3; //для смены реализации меня цифру 1,2,3

namespace Lab.Tests;

[TestFixture]
public class BinaryConverterTests
{
    private IBinaryToDecimalConverter _converter;

    [SetUp]
    public void Setup() => _converter = new BinaryToDecimalConverter();

    // ЧЕРНЫЙ ЯЩИК
    [Test]
    [TestCase(10, "1010")]
    [TestCase(0, "0")]
    public void ToBinary_Positive_ReturnsCorrect(int input, string expected) 
        => Assert.That(_converter.ToBinary(input), Is.EqualTo(expected));

    [Test]
    public void ToBinary_Negative_Throws() 
        => Assert.Throws<ArgumentException>(() => _converter.ToBinary(-1));

    [Test]
    [TestCase("1010", 10)]
    [TestCase("0", 0)]
    public void ToDecimal_Valid_ReturnsCorrect(string input, int expected) 
        => Assert.That(_converter.ToDecimal(input), Is.EqualTo(expected));

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("102")]
    public void ToDecimal_Invalid_Throws(string? input) 
        => Assert.Throws<ArgumentException>(() => _converter.ToDecimal(input));

    // БЕЛЫЙ ЯЩИК
    [Test]
    public void ToDecimal_WithSpaces_CheckBehavior()
    {
        Assert.Throws<ArgumentException>(() => _converter.ToDecimal(" 101 "));
    }
}