using UnitTestTraining.Business;
using UnitTestTraining.Configuration;
using UnitTestTraining.DataAccess.Commands;
using UnitTestTraining.DataAccess.Entities;
using UnitTestTraining.DataAccess.Queries;

namespace UnitTestTraining
{
    public class SecondSession
    {
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IGetPersonByIdQuery getPersonByIdQuery;
        private readonly IUpdatePersonCommand updatePersonCommand;
        private readonly IPeopleProcessor peopleProcessor;
        private readonly IPersonValidator personValidator;
        private readonly ProcessorSettings processorSettings;

        public SecondSession(IDateTimeProvider dateTimeProvider, IGetPersonByIdQuery getPersonByIdQuery, IUpdatePersonCommand updatePersonCommand, IPeopleProcessor peopleProcessor, ProcessorSettings processorSettings, IPersonValidator personValidator)
        {
            this.dateTimeProvider = dateTimeProvider;
            this.getPersonByIdQuery = getPersonByIdQuery;
            this.updatePersonCommand = updatePersonCommand;
            this.peopleProcessor = peopleProcessor;
            this.processorSettings = processorSettings;
            this.personValidator = personValidator;
        }

        public void UpdateAgeOfPerson(int personId)
        {
            var person = this.getPersonByIdQuery.Execute(personId);

            if (person?.Birthday.DayOfYear == this.dateTimeProvider.Now().DayOfYear) 
            {
                person.Age++;
                this.updatePersonCommand.Execute(person);
            }
        }

        public void ProcessPeople()
        {
            if (this.processorSettings.IsProcessorEnabled)
            {
                this.peopleProcessor.Process();
            }
        }

        public Person CreatePerson(string  firstName, string lastName)
        {
            var person = new Person
            {
                Name = $"{firstName} {lastName}",
                Age = 25,
                Birthday = new DateTime(1990, 3, 5)
            };

            if (this.personValidator.Validate(person))
            {
                return person;
            }

            throw new Exception("Details given are not valid!");
        }
    }
}
