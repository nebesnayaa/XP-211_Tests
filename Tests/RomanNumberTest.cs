using ConsoleApp1;

namespace Tests
{
    [TestClass]
    public class RomanNumberTest
    {
        [TestMethod]
        
        public void ParseTest_1()
        {
            Assert.AreEqual(1, RomanNumber.Parse("I").Value, "I => 1");
        }

        [TestMethod]
        public void ParseTest_2()
        {
            Assert.AreEqual(2, RomanNumber.Parse("II").Value, "II => 2");
        }

        [TestMethod]
        public void ParseTest_3()
        {
            Assert.AreEqual(3, RomanNumber.Parse("III").Value, "III => 3");
        }

        [TestMethod]
        public void ParseTest_4()
        {
            Assert.AreEqual(4, RomanNumber.Parse("IV").Value, "IV => 4");
        }

        [TestMethod]
        public void ParseTest_5()
        {
            Assert.AreEqual(5, RomanNumber.Parse("V").Value, "V => 5");
        }

        [TestMethod]
        public void ParseTest_6()
        {
            Assert.AreEqual(6, RomanNumber.Parse("VI").Value, "VI => 6");
        }

        [TestMethod]
        public void ParseTest_7()
        {
            Assert.AreEqual(7, RomanNumber.Parse("VII").Value, "VII => 7");
        }

        [TestMethod]
        public void ParseTest_8()
        {
            Assert.AreEqual(8, RomanNumber.Parse("VIII").Value, "VIII => 8");
        }

        [TestMethod]
        public void ParseTest_9()
        {
            Assert.AreEqual(9, RomanNumber.Parse("IX").Value, "IX => 9");
        }
    }
}