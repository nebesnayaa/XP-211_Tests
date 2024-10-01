using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public record RomanNumber(int Value)
    {
        public static RomanNumber Parse(string input)
        {
            String argValue = input;
            input = input.Trim();
            int pos = 0;
            int previousValue = 0;
            int result = 0;

            foreach (char c in input)
            {
                int currentValue;
                try
                {
                    currentValue = DigitValue(c);
                    if (previousValue < currentValue)
                        result += currentValue - 2 * previousValue;
                    else
                        result += currentValue;
                }
                catch
                {
                    throw new FormatException($"RomanNumber.Parse('{argValue}'): invalid input '{input}' has illegal char '{c}' and position {pos}");
                }
                if (previousValue < currentValue &&
                    previousValue != 0)
                {
                    //не всі комбінації віднімання припустимі
                    if (currentValue / previousValue > 10 ||
                        previousValue.ToString()[0] == '5')
                    {
                        throw new FormatException($"RomanNumber.Parse('{argValue}') error: '{input[pos - 1]}' before '{input[pos]}' in position {pos - 1}");
                    }
                }
                if (currentValue == 0 && input.Length > 1)
                {
                    throw new FormatException($"RomanNumber.Parse('{argValue}') error: Digit 'N' must not be in number in position {pos}");
                }
                pos++;
                previousValue = currentValue;
            }
            return new RomanNumber(result);
        }

        /* Hw 13.09 */

        //public static void Parse(string input)
        //{
        //    char maxChar = '\0'; // найбільший символ
        //    int maxCharPos = 0;  // позиція найбільшого символа у числі
        //    int maxValue = 0;    // найбільше числове значення символу
        //    int countLess = 0;   // Лічильник меншої цифри перед більшою
        //    int pos = 0;

        //    for (int i = input.Length - 1; i  >= 0; i--)
        //    {
        //        char c = input[i];
        //        int currentValue = DigitValue(c);
        //        if(currentValue > maxValue && currentValue != 0)
        //        {
        //            maxValue = currentValue;
        //            maxChar = c;
        //            maxCharPos = i;
        //        }
        //        else if(currentValue < maxValue && currentValue != 0)
        //        {
        //            countLess ++;
        //            if(countLess > 1)
        //            {
        //                throw new FormatException($"RomanNumber.Parse('{input}') has more than one less digit before char '{maxChar}' on position {maxCharPos}");
        //            }
        //        }
        //        pos++;
        //    }
        //}

        public static int DigitValue(char digit) => digit switch
        {
            'N' => 0,
            'I' => 1,
            'V' => 5,
            'X' => 10,
            'L' => 50,
            'C' => 100,
            'D' => 500,
            'M' => 1000,
            _ => throw new ArgumentException("'RomanNumber.DigitValue': argument 'digit' has invalid value") { Source = "RomanNumber.DigitValue" },
        };
    }
}
