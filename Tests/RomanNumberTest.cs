using App;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Reflection;

namespace Tests
{
    [TestClass]
    public class RomanNumberTest
    {
        [TestMethod]
        public void ParseTest()
        {
            Assert.AreEqual(1, RomanNumber.Parse("I").Value, "I => 1");
            Assert.AreEqual(2, RomanNumber.Parse("II").Value, "II => 2");
            Assert.AreEqual(3, RomanNumber.Parse("III").Value, "III => 3");

            Dictionary<String, int> validCases = new()
            {
                {"IV",     4},
                {"IIII",   4},
                {"V",      5},
                {"VIIII",  9},
                {"IX",     9},
                {"X",      10},
                {"XV",     15},
                {"XIX",    19},
                {"XX",     20},
                {"CXIV",   114},
                {"DC",     600},
                {"N",      0},
            };
            foreach (var validCase in validCases)
            {
                Assert.AreEqual(
                    validCase.Value,
                    RomanNumber.Parse(validCase.Key).Value,
                    $"Valid Parser Test: '{validCase.Key}' => {validCase.Value}"
                );
            }

            String[][] testCases = [
                ["IW",  "W", "1"],
                ["SI", "S", "0"],
                ["IXW", "W", "2"],
                ["IEX", "E", "1"],
                ["CDX1", "1", "3"],
            ];
            foreach (var testCase in testCases)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase[0]),
                    $"RomanNumber.Parse('{testCase[0]}') must throw FormatException"
                );
                Assert.IsTrue(
                    ex.Message.Contains("RomanNumber.Parse"),
                    $"ex.Message must contain origin (class and method): " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'{testCase[0]}'"),
                    $"ex.Message must contain input value '{testCase[0]}': " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'{testCase[1]}'") &&
                    ex.Message.Contains($"position {testCase[2]}"),
                    $"ex.Message must contain error char '{testCase[1]}' and its position {testCase[2]}: " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
            }

            String[][] testCases2 = [   // недозволені парні символи
                ["IL", "I", "L", "0"],
                ["IC", "I", "C", "0"],
                ["ID", "I", "D", "0"],
                ["IM", "I", "M", "0"],
                ["XD", "X", "D", "0"],
                ["XM", "X", "M", "0"],
                ["VX", "V", "X", "0"],
                ["VM", "V", "M", "0"],
                ["LM", "L", "M", "0"],
                ["DM", "D", "M", "0"],
            ];
            foreach (var testCase in testCases2)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase[0]),
                    $"RomanNumber.Parse('{testCase[0]}') must throw FormatException"
                );
                Assert.IsTrue(
                    ex.Message.Contains("RomanNumber.Parse"),
                    $"ex.Message must contain origin (class and method): " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'{testCase[0]}'"),
                    $"ex.Message must contain input value '{testCase[0]}': " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'{testCase[1]}' before '{testCase[2]}'") &&
                    ex.Message.Contains($"position {testCase[3]}"),
                    $"ex.Message must contain error char '{testCase[1]}' before '{testCase[2]}' and its position {testCase[3]}: " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
            }

            String[][] testCases3 = [   // неприпустимість цифри 'N' у числах
                ["VN", "1"],
                ["NV", "0"]
            ];
            foreach (var testCase in testCases3)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase[0]),
                    $"RomanNumber.Parse('{testCase[0]}') must throw FormatException"
                );
                Assert.IsTrue(
                    ex.Message.Contains("RomanNumber.Parse"),
                    $"ex.Message must contain origin (class and method): " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'{testCase[0]}'"),
                    $"ex.Message must contain input value '{testCase[0]}': " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"Digit 'N' must not be in number") &&
                    ex.Message.Contains($"position {testCase[1]}"),
                    $"ex.Message must contain error <Digit 'N' must not be in number> and error positions {testCase[1]}: " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
            }

            // роль пробільних символів
            String[][] testCases4 = [   // не дозвволяємо всередині числа
                ["V I",  "1"],
                ["X\tX", "1"],
                ["X\nX", "1"],
                ["X\0X", "1"]
            ];
            foreach (var testCase in testCases4)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase[0]),
                    $"RomanNumber.Parse('{testCase[0]}') must throw FormatException"
                );
            }

            String[] testCases5 = [   // допустимі пробільні символи
                " XX",  "CC ", "  XX",  "  CC  ", "\tIV", "XI\t\t", "CD\n"
            ];
            foreach (var testCase in testCases5)
            {
                Assert.IsNotNull(
                    RomanNumber.Parse(testCase),
                    "Ceiling and trailing spaces are allowed");
            }

            String[][] testCases6 = [   // не дозвволяємо всередині числа
                ["   XSX", "S",  "1"],
                [" VC ",   "V",  "0"],
            ];
            foreach (var testCase in testCases6)
            {
                var ex = Assert.ThrowsException<FormatException>(
                   () => RomanNumber.Parse(testCase[0]),
                   $"RomanNumber.Parse('{testCase[0]}') must throw FormatException"
               );
                Assert.IsTrue(
                    ex.Message.Contains($"'{testCase[1]}'") &&
                    ex.Message.Contains($"position {testCase[2]}"),
                    $"ex.Message must contain error char '{testCase[1]}' and its position {testCase[2]}: " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
                Assert.IsTrue(
                    ex.Message.Contains("RomanNumber.Parse"),
                    $"ex.Message must contain origin (class and method): " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'{testCase[0]}'"),
                    $"ex.Message must contain input value '{testCase[0]}': " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
            }

            /* Hw 13.09 */

            //String[][] testCases7 = [   // декілька менших цифр ліворуч від більшої
            //    ["IIX",  "X", "2"],     // при цьому кожна пара цифр - нормальна
            //    ["IVIX", "X", "3"],
            //    ["IIVI", "V", "2"],
            //    ["IIIX", "X", "3"],
            //];
            //foreach (var testCase in testCases7)
            //{
            //    var ex = Assert.ThrowsException<FormatException>(
            //        () => RomanNumber.Parse(testCase[0]),
            //        $"RomanNumber.Parse('{testCase[0]}') must throw FormatException"
            //    );
            //    Assert.IsTrue(
            //        ex.Message.Contains($"'{testCase[1]}'") &&
            //        ex.Message.Contains($"position {testCase[2]}"),
            //        $"ex.Message must contain error char '{testCase[1]}' and its position {testCase[2]}: " +
            //        $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
            //    );
            //    Assert.IsTrue(
            //        ex.Message.Contains("RomanNumber.Parse"),
            //        $"ex.Message must contain origin (class and method): " +
            //        $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
            //    );
            //    Assert.IsTrue(
            //        ex.Message.Contains($"'{testCase[0]}'"),
            //        $"ex.Message must contain input value '{testCase[0]}': " +
            //        $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
            //    );
            //    Assert.IsTrue(
            //        ex.Message.Contains($"more than one less digit before"),
            //        $"ex.Message must contain input value 'more than one less digit before': " +
            //        $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
            //    );
            //}

        }


        [TestMethod]
         public void DigitValueTest()
        {
            Dictionary<char, int> givenCases = new()
            {
                {'N',   0},
                {'I',   1},
                {'V',   5},
                {'X',   10},
                {'L',   50},
                {'C',   100},
                {'D',   500},
                {'M',   1000}
            };

            foreach (var givenCase in givenCases)
            {
                Assert.AreEqual(
                    givenCase.Value,
                    RomanNumber.DigitValue(givenCase.Key),
                    $"DigitValue('{givenCase.Key}') => {givenCase.Value}"
                );
            }

            int n = 100;
            Random random = new Random();

            List<char> testCases = new List<char>();
            char symbol;

            for (int i = 0; i < n; i++)
            {
                symbol = Convert.ToChar(random.Next(0, 300));
                if (givenCases.ContainsKey(symbol))
                    i--;
                else
                    testCases.Add(symbol);
            }

            foreach (var testCase in testCases)
            {
                var ex = Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.DigitValue(testCase),
                    $"RomanNumber.DigitValue('{testCase}') must throw FormatException"
                );

                Assert.IsTrue(ex.Source?.Contains("RomanNumber.DigitValue"),
                    $"ex.Source must contain origin (class and method): " + $"ex.Source='{ex.Source}'");
            }

        }
    }
}
