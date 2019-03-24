using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace romannumbers
{
    public class UnitTest1
    {
        [Theory]
		[InlineData("I", 1)]
		[InlineData("II", 2)]
		[InlineData("IV", 4)]
		[InlineData("V", 5)]
		[InlineData("IX", 9)]
		[InlineData("X", 10)]
		[InlineData("XLII", 42)]
		[InlineData("XCIX", 99)]
		[InlineData("MMXIII", 2013)]
		public void TestRomanNumber(string romanNumber, int expected)
		{
			Assert.Equal(expected, this.RomanToDecimal(romanNumber));
		}

/* private int RomanToDecimal(string romanNumber) => 
	romanNumber
		.Select(RomanDigit)
		.Aggregate( (result: 0, last: 0), 
			(acc, digit) => (acc.result + digit - ((digit > acc.last) ? acc.last << 1: 0), digit),
			acc => acc.result);

*/
	private int RomanToDecimal(string romanNumber) => 
		romanNumber
			.Select(RomanDigit)
			.Zip(romanNumber
				.Select(RomanDigit)
				.Skip(1)
				.Append(0)
				.Select( digit => digit - 1), 
				(curr,next) => curr * Math.Sign(curr - next))
			.Sum();

        private int RomanDigit(char digit) => 
            new int[]{100, 500, 0, 0, 0, 0, 1, 1, 0, 50, 1000, 
                0, 0, 0, 0, 0, 0, 0, 5, 5, 0, 10}[digit - 'C'];
    }
}
