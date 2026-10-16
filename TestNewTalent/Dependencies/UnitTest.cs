using System;
using Xunit;
using NewTalent.Dependencies;

namespace TestNewTalent.Dependencies
{
    public class UnitTest
    {
        private readonly Calculator _calc;

        public UnitTest()
        {
            _calc = new Calculator();
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(10, 15, 25)]
        [InlineData(-5, 5, 0)]
        public void Add_TwoIntegers_ReturnsCorrectSum(int num1, int num2, int expected)
        {
            int result = _calc.Add(num1, num2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 2, 3)]
        [InlineData(20, 30, -10)]
        [InlineData(0, 0, 0)]
        public void Subtract_TwoIntegers_ReturnsCorrectDifference(int num1, int num2, int expected)
        {
            int result = _calc.Subtract(num1, num2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2, 3, 6)]
        [InlineData(-4, 5, -20)]
        [InlineData(10, 0, 0)]
        public void Multiply_TwoIntegers_ReturnsCorrectProduct(int num1, int num2, int expected)
        {
            int result = _calc.Multiply(num1, num2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(6, 3, 2)]
        [InlineData(10, 2, 5)]
        [InlineData(-12, 4, -3)]
        public void Divide_TwoIntegers_ReturnsCorrectQuotient(int num1, int num2, int expected)
        {
            int result = _calc.Divide(num1, num2);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => _calc.Divide(10, 0));
        }

        [Fact]
        public void GetHistory_MultipleOperations_ReturnsOnlyTheLastThreeOperations()
        {
            _calc.Add(1, 1);      // 1st operation (should be discarded from top 3)
            _calc.Subtract(5, 2); // 2nd operation
            _calc.Multiply(3, 3); // 3rd operation
            _calc.Divide(10, 2);  // 4th operation

            var history = _calc.GetHistory();

            Assert.Equal(3, history.Count);
            Assert.Contains("5 - 2 = 3", history);
            Assert.Contains("3 * 3 = 9", history);
            Assert.Contains("10 / 2 = 5", history);
            Assert.DoesNotContain("1 + 1 = 2", history);
        }
    }
}
