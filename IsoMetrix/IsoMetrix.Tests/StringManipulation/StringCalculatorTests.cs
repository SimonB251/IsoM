using IsoMetrix.BL.StringManipulation;

namespace IsoMetrix.Tests.StringManipulation
{
    [TestClass]
    public class StringCalculatorTests
    {
        // I did not add validation tests due to 'Rule 3'.
        // I.E. duplicate defined delimiters, more than one delimter between numbers, delimiters with characters in multiple defined delimiters, using a hyphen '-' as a delimiter
        // numbers other than 0,1,2,3, empty delimiters, strings with spaces, delimiters with numbers, nested delmiters, delimiter defined as a comma,
        // or any other invalid format (just delimiters, just new lines, multiple character delimters without brackets etc)

        private IStringCalculator _calculator = null!;

        [TestInitialize]
        public void Setup()
        {
            // Arrange
            _calculator = new StringCalculator();
        }

        //NOTE: Most of these tests could just be moved under the 'Add' test method, but for the sake of readability in Test Explorer I have separated them into different test methods. This is simply a preference, but does go against DRY.

        [TestMethod]
        [TestCategory("Basic Addition")]
        [DataRow("", 0)]
        [DataRow("0", 0)]
        [DataRow("1", 1)]
        [DataRow("2", 2)]
        [DataRow("2,3", 5)]
        [DataRow("2,1,1,1,2,2,2,3,3,3,20", 40)]
        [DataRow("100,203,2", 305)] // The requirment only gives examples of single digit numbers, but points out numbers over 1000 are ignored, so I assume numbers like 100 are acceptable.
        [DataRow("0,1000", 1000)]
        [DataRow("2,999,3", 1004)]
        [DataRow("1000", 1000)]
        public void Add(string numberString, int expectedValue)
        {
            //Act
            var result = _calculator.Add(numberString);

            //Assert
            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        [TestCategory("Multiple Delimiters")]
        [DataRow("//[*][&&]\n0*1*2*2&&2", 7)]
        [DataRow("//[***][&&]\n0***1***2***2&&2", 7)]
        [DataRow("//[*][$]\n0*1*2", 3)]
        [DataRow("//[*][$]\n0*1*2*3$3\n1\n1", 11)]
        [DataRow("//[*][$][£]\n0*1$1£1", 3)]
        public void Add_WithMultipleDefinedDelimiters(string numberString, int expectedValue)
        {
            //Act
            var result = _calculator.Add(numberString);

            //Assert
            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        [TestCategory("Multiple Character Delimiters")]
        [DataRow("//d\n", 0)]
        [DataRow("//[***]\n***", 0)]
        [DataRow("//[***]\n0***1", 1)]
        [DataRow("//[*****]\n0*****1*****2", 3)]
        [DataRow("//[$££]\n0$££22$££3$££2$££2", 29)] //I've assumed delimiters with different characters are allowed
        public void Add_WithMultipleCharacterDelimiter(string numberString, int expectedValue)
        {
            //Act
            var result = _calculator.Add(numberString);

            //Assert
            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        [TestCategory("Single Character Delimiters")]
        [DataRow("//;\n0;0", 0)]
        [DataRow("//;\n0;22;3;2;2", 29)]
        [DataRow("//v\n0v1v3v3", 7)]
        [DataRow("//[$]\n0$1", 1)] //I've assumed single delimiters in brackets is valid too
        [DataRow("//[$]\n", 0)]
        [DataRow("//d\n", 0)]
        public void Add_WithDelimiter(string numberString, int expectedValue)
        {
            //Act
            var result = _calculator.Add(numberString);

            //Assert
            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        [TestCategory("Addition With New Lines")]
        [DataRow("0\n0", 0)]
        [DataRow("0\n1", 1)]
        [DataRow("0\n1\n3,3", 7)]
        [DataRow("0\n1\n103,3", 107)]
        public void Add_WithNewLines(string numberString, int expectedValue)
        {
            //Act
            var result = _calculator.Add(numberString);

            //Assert
            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        [TestCategory("Numbers Larger Than 1000")]
        [DataRow("1001", 0)]
        [DataRow("1001,2", 2)]
        [DataRow("5000,1,3", 4)]
        public void Add_WithNumbersLargerThanOneThousand(string numberString, int expectedValue)
        {
            //Act
            var result = _calculator.Add(numberString);

            //Assert
            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        [TestCategory("Negative Numbers")]
        [DataRow("-0", "-0")]
        [DataRow("-1,-2", "-1,-2")]
        [DataRow("-2,-1,-3", "-2,-1,-3")]
        [DataRow("-2,1,3", "-2")]
        [DataRow("-233,1,3", "-233")]
        [DataRow("2,-233", "-233")]
        public void Add_WithNegativeNumbersThrowsExceptionWithCorrectMessage(string negativestring, string expectedNegatives)
        {
            //Act
            var ex = Assert.ThrowsException<InvalidOperationException>(
                () => _calculator.Add(negativestring)
            );

            //Assert
            Assert.AreEqual($"Negatives not allowed: {expectedNegatives}", ex.Message);
        }
    }
}