using UnitTestTraining.DataAccess.Entities;

namespace UnitTestTraining.DataAccess.Queries
{
    public interface IGetPersonByIdQuery
    {
        Person Execute(int id);
    }
}
