using System;
using TestStack.BDDfy;

namespace Specify
{
    public abstract class SpecificationFor<TSubject> : ISpecification
        where TSubject : class
    {
        public ITestLifetimeScope Container { get; set; }
        public ExampleTable Examples { get; set; }

        public TSubject SUT { get; set; }

        public T Get<T>() where T : class
        {
            return Container.Resolve<T>();
        }

        public virtual Type Story
        {
            get { return typeof(TSubject); }
        }

        public virtual string Title { get { return GetType().Name.Humanize(LetterCasing.Title); } }

        public virtual void ExecuteTest()
        {
            Host.Specify(this);
        }

        protected void EstablishContext()
        {
            CreateSut();
        }

        protected virtual void CreateSut()
        {
            SUT = Container.SystemUnderTest<TSubject>();            
        }
    }
}