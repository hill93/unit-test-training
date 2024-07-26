namespace UnitTestTraining.Tests
{
    using Moq;
    using Newtonsoft.Json;
    using System.Text.Json.Nodes;
    using UnitTestTraining.Business;
    using UnitTestTraining.Configuration;
    using UnitTestTraining.DataAccess.Commands;
    using UnitTestTraining.DataAccess.Entities;
    using UnitTestTraining.DataAccess.Queries;
    using Xunit;

    public class SecondSessionTests : BaseTest<SecondSession>
    {
        [Fact]
        public void UpdateAgeOfPersonOnBirthdayProcessesCorrectly()
        {
            //ARRANGE
            var stubPerson = new Person
            {
                Id = 587329,
                Birthday = new DateTime(1993, 9, 3),
                Age = 29
            };

            var stubDate = new DateTime(2023, 9, 3);

            this.AutoMocker.GetMock<IGetPersonByIdQuery>()
                .Setup(x => x.Execute(stubPerson.Id))
                .Returns(stubPerson);

            this.AutoMocker.GetMock<IDateTimeProvider>()
                .Setup(x => x.Now())
                .Returns(stubDate);

            var sut = this.CreateTestSubject();

            //ACT
            sut.UpdateAgeOfPerson(stubPerson.Id);

            //ASSERT
            this.AutoMocker.GetMock<IUpdatePersonCommand>()
                .Verify(x => x.Execute(stubPerson), Times.Once);

            Assert.Equal(30, stubPerson.Age);
        }

        [Fact]
        public void UpdateAgeOfPersonNotOnBirthdayProcessesCorrectly()
        {
            //ARRANGE
            var stubPerson = new Person
            {
                Id = 587329,
                Birthday = new DateTime(1993, 9, 3),
                Age = 29
            };

            var stubDate = new DateTime(2023, 10, 3);

            this.AutoMocker.GetMock<IGetPersonByIdQuery>()
                .Setup(x => x.Execute(stubPerson.Id))
                .Returns(stubPerson);

            this.AutoMocker.GetMock<IDateTimeProvider>()
                .Setup(x => x.Now())
                .Returns(stubDate);

            var sut = this.CreateTestSubject();

            //ACT
            sut.UpdateAgeOfPerson(stubPerson.Id);

            //ASSERT
            this.AutoMocker.GetMock<IUpdatePersonCommand>()
                .Verify(x => x.Execute(stubPerson), Times.Never);

            Assert.Equal(29, stubPerson.Age);
        }

        [Fact]
        public void UpdateAgeOfPersonNotOnBirthdayProcessesCorrectlyItIsAny()
        {
            //ARRANGE
            var stubPerson = new Person
            {
                Id = 587329,
                Birthday = new DateTime(1993, 9, 3),
                Age = 29
            };

            var stubDate = new DateTime(2023, 10, 3);

            this.AutoMocker.GetMock<IGetPersonByIdQuery>()
                .Setup(x => x.Execute(stubPerson.Id))
                .Returns(stubPerson);

            this.AutoMocker.GetMock<IDateTimeProvider>()
                .Setup(x => x.Now())
                .Returns(stubDate);

            var sut = this.CreateTestSubject();

            //ACT
            sut.UpdateAgeOfPerson(stubPerson.Id);

            //ASSERT
            this.AutoMocker.GetMock<IUpdatePersonCommand>()
                .Verify(x => x.Execute(It.IsAny<Person>()), Times.Never);

            Assert.Equal(29, stubPerson.Age);
        }

        [Fact]
        public void ProcessPeopleProcessesWhenEnabled()
        {
            //ARRANGE
            var stubSettings = new ProcessorSettings
            {
                IsProcessorEnabled = false
            };

            this.AutoMocker.Use(stubSettings);

            var sut = this.CreateTestSubject();

            //ACT
            sut.ProcessPeople();

            //ASSERT
            this.AutoMocker.GetMock<IPeopleProcessor>()
                .Verify(x => x.Process(), Times.Never);
        }

        [Fact]
        public void CreatePersonValidDetailsReturnsExpected_ThisWontWork()
        {
            //ARRANGE
            var stubFirstName = "Henry";
            var stubLastName = "Hill";

            var stubPerson = new Person
            {
                Name = $"{stubFirstName} {stubLastName}",
                Age = 25,
                Birthday = new DateTime(1990, 3, 5)
            };

            this.AutoMocker.GetMock<IPersonValidator>()
                .Setup(x => x.Validate(stubPerson))
                .Returns(true);

            var sut = this.CreateTestSubject(); 
            
            //ACT
            var result = sut.CreatePerson(stubFirstName, stubLastName);

            //ASSERT
            Assert.Equal(stubPerson, result);
        }

        [Fact]
        public void CreatePersonValidDetailsReturnsExpected_ThisWillWork()
        {
            //ARRANGE
            var stubFirstName = "Henry";
            var stubLastName = "Hill";

            var stubPerson = new Person
            {
                Name = $"{stubFirstName} {stubLastName}",
                Age = 25,
                Birthday = new DateTime(1990, 3, 5)
            };

            // this.AutoMocker.GetMock<IPersonValidator>()
            //     .Setup(x => x.Validate(
            //         It.Is<Person>(y => y.Id == stubPerson.Id
            //             && y.Name == stubPerson.Name
            //             && y.Age == stubPerson.Age
            //             && y.Birthday.DayOfYear == stubPerson.Birthday.DayOfYear)))
            //     .Returns(true);

            this.AutoMocker.GetMock<IPersonValidator>()
                .Setup(x => x.Validate(
                    It.Is<Person>(y => JsonConvert.SerializeObject(y) == JsonConvert.SerializeObject(stubPerson))))
                .Returns(true);

            var sut = this.CreateTestSubject();

            //ACT
            var result = sut.CreatePerson(stubFirstName, stubLastName);

            //ASSERT
            Assert.Equal(JsonConvert.SerializeObject(stubPerson), JsonConvert.SerializeObject(result));
        }
    }
}
