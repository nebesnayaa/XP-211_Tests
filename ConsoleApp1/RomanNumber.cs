using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public record RomanNumber(int Value)
    {
        private static readonly Dictionary<string, int> RomanToInt = new()
        {
            {"I",    1},
            {"II",   2},
            {"III",  3},
            {"IV",   4},
            {"V",    5},
            {"VI",   6},
            {"VII",  7},
            {"VIII", 8},
            {"IX",   9},
        };

        public static RomanNumber Parse(string input)
        {
            if (input == null || !RomanToInt.ContainsKey(input))
            {
                throw new ArgumentException("Invalid Roman numeral");
            }

            return new RomanNumber(RomanToInt[input]);
        }

        public static int DigitValue(char digit) => digit switch
        {
            'N' => 0,
            'I' => 1,
            'V' => 5,
            'X' => 10,
            'L' => 50,
            'C' => 100,
            'D' => 500,
            _ => throw new ArgumentException("'RomanNumber.DigitValue': argument 'digit' has invalid value") { Source = "RomanNumber.DigitValue" },
        };
    }
}
