namespace UnitTestTraining.Tests
{
    using Xunit;

    public class FirstSessionTests : BaseTest<FirstSession>
    {
        
        
        [Fact]
        public void AddTwoNumbersAddsCorrectly()
        {
            //Arrange
            var stubFirstNumber = 4;
            var stubSecondNumber = 5;

            var sut = this.CreateTestSubject();

            //Act
            var result = sut.AddTwoNumbers(stubFirstNumber, stubSecondNumber);

            //Assert
            Assert.Equal(9, result);
        }

        [Fact]
        public void FindNameBeginningWithReturnsExpected()
        {
            //Arrange
            var stubPrefix = "M";

            var sut = this.CreateTestSubject();

            //Act
            var result = sut.FindNameBeginningWith(stubPrefix);

            //Assert
            Assert.Contains(result, x => x == "Martin");
            Assert.Contains(result, x => x == "Mick");
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void FindNameBeginningWithReturnsSingle()
        {
            //Arrange
            var stubPrefix = "H";

            var sut = this.CreateTestSubject();

            //Act
            var result = sut.FindNameBeginningWith(stubPrefix);

            //Assert
            Assert.Contains(result, x => x == "Henry");
            Assert.Single(result);
        }

        [Fact]
        public void FindNameBeginningWithReturnsEmpty()
        {
            //Arrange
            var stubPrefix = "S";

            var sut = this.CreateTestSubject();

            //Act
            var result = sut.FindNameBeginningWith(stubPrefix);

            //Assert
            Assert.DoesNotContain(result, x => x == "Henry");
            Assert.Empty(result);
        }

        [Fact]
        public void FindTopNameEndWithReturnsNull()
        {
            //Arrange
            var stubPostfix = "u";

            var sut = this.CreateTestSubject();

            //Act
            var result = sut.FindTopNameEndWith(stubPostfix);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void FindTopNameEndWithReturnsNotNull()
        {
            //Arrange
            var stubPostfix = "nry";

            var sut = this.CreateTestSubject();

            //Act
            var result = sut.FindTopNameEndWith(stubPostfix);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Henry", result);
        }

        [Fact]
        public void AreStringsEqualLengthEqualReturnsTrue()
        {
            //Arrange
            var stubStringOne = "test";
            var stubStringTwo = "jump";

            var sut = this.CreateTestSubject();

            //Act
            var result = sut.AreStringsEqualLength(stubStringOne, stubStringTwo);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void AreStringsEqualLengthUnequalReturnsFalse()
        {
            //Arrange
            var stubStringOne = "test";
            var stubStringTwo = "jumpy";

            var sut = this.CreateTestSubject();

            //Act
            var result = sut.AreStringsEqualLength(stubStringOne, stubStringTwo);

            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("Hello")]
        [InlineData("World")]
        public void ProcessWordBannedWordThrowsCorrectly(string stubWord)
        {
            //Arrange
            var sut = this.CreateTestSubject();

            //Act/Assert
            var ex = Assert.Throws<NullReferenceException>(() => sut.ProcessWord(stubWord));

            //Assert
            Assert.Equal("Word not allowed", ex.Message);
            Assert.IsType<NullReferenceException>(ex);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(3, 8, 11)]
        [InlineData(4, 2, 6)]
        [InlineData(9, 4, 13)]
        public void AddTwoNumbersManyNumbersAddCorrectly(int stubFirstNumber, int stubSecondNumber, int expectedResult)
        {
            //Arrange
            var sut = this.CreateTestSubject();

            //Act
            var result = sut.AddTwoNumbers(stubFirstNumber, stubSecondNumber);

            //Assert
            Assert.Equal(expectedResult, result);
        }
    }
}