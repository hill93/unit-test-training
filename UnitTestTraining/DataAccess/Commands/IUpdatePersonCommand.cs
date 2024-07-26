using UnitTestTraining.DataAccess.Entities;

namespace UnitTestTraining.DataAccess.Commands
{
    public interface IUpdatePersonCommand
    {
        void Execute(Person person);
    }
}
