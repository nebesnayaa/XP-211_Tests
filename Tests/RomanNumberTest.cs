using ConsoleApp1;

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
            Assert.AreEqual(4, RomanNumber.Parse("IV").Value, "IV => 4");
            Assert.AreEqual(5, RomanNumber.Parse("V").Value, "V => 5");
            Assert.AreEqual(6, RomanNumber.Parse("VI").Value, "VI => 6");
            Assert.AreEqual(7, RomanNumber.Parse("VII").Value, "VII => 7");
            Assert.AreEqual(8, RomanNumber.Parse("VIII").Value, "VIII => 8");
            Assert.AreEqual(9, RomanNumber.Parse("IX").Value, "IX => 9");
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
                {'D',   500}
            };
            foreach (var givenCase in givenCases) { 
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

            //{ 
            //    char testCase = '1';
            //    var ex = Assert.ThrowsException<ArgumentException>(
            //       () => RomanNumber.DigitValue(testCase),
            //       $"DigitValue({testCase}) must trow ArgumentException"
            //    );

            //    Assert.IsTrue(
            //        ex.Message.Contains($"invalid value '{testCase}'") &&
            //        ex.Message.Contains("argument 'digit'"),
            //        $"ex.Message must contain param name('digit') and its value '{testCase}'. ex.Message: '{ex.Message}'"
            //    );

            //    Assert.IsTrue(
            //       ex.Message.Contains("'RomanNumber.DigitValue'"),
            //       $"ex.Message must contain origin (class and method): '{ex.Message}'"
            //   );
            //}
        }
    }
}