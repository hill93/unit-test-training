namespace UnitTestTraining.Tests
{
    using Moq.AutoMock;
    public class BaseTest<T> where T : class
    {
        protected AutoMocker AutoMocker { get; set; }

        public BaseTest() 
        {
            this.AutoMocker = new AutoMocker();
        }

        protected T CreateTestSubject()
        {
            return this.AutoMocker.CreateInstance<T>();
        }
    }
}
