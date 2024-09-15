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
            Dictionary<string, int> testCases = new()
            {
                {"I",    1},
                {"II",   2},
                {"III",  3},
                {"IV",   4},
                {"V",    5},
                {"VI",   6},
                {"VII",  7},
                {"VIII", 8},
                {"IX",   9}
            };
            foreach (var testCase in testCases) { 
                Assert.AreEqual(
                    testCase.Value,
                    RomanNumber.DigitValue(testCase.Key),
                    $"DigitValue('{testCase.Key}') => {testCase.Value}"
                );
            }
        }
    }
}