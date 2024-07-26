namespace UnitTestTraining.Business
{
    using UnitTestTraining.DataAccess.Entities;

    public interface IPersonValidator
    {
        bool Validate(Person person);
    }
}
