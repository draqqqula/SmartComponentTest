using DecorativeComponents;
using ExtendedDecorativeComponents;

namespace ExampleClasses
{
    public class SomeData
    {
        public string Line;
        public SomeData(string line)
        {
            Line = line;
        }
    }

    public interface IUpdateComponent : IComponent
    {
        public void Update();
    }

    public interface IDataProviderComponent : IComponent
    {
        public SomeData GetData();
    }

    public class ObjectA : ComponentBase, IDataProviderComponent, IUpdateComponent
    {
        public ObjectA()
        {
            AddGreetingFor<ObjectB>(it => Console.WriteLine($"{this.GetType().Name} joined with {it.GetType().Name}"));
        }

        public SomeData GetData()
        {
            return new SomeData("a");
        }

        public void Update()
        {
            Console.WriteLine("Updated A");
        }

    }

    public class ObjectB : ComponentBase, IUpdateComponent
    {
        public ObjectB()
        {
            AddGreetingFor<ComponentBase>(it => Console.WriteLine($"{this.GetType().Name} joined with {it.GetType().Name}"));
        }
        public void Update()
        {
            Console.WriteLine("Updated B");
        }
    }

    public class ObjectC : ComponentBase, IUpdateComponent, IDataProviderComponent
    {
        public ObjectC()
        {
            AddGreetingFor<ComponentBase>(it => Console.WriteLine($"{this.GetType().Name} joined with {it.GetType().Name}"));
        }

        public SomeData GetData()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            Console.WriteLine("Updated C");
        }
    }

    public class ObjectD : ExtendedComponent, IUpdateComponent
    {
        public ObjectD() : base()
        {
        }

        public void Update()
        {
            Console.WriteLine("Updated D");
        }

        [ContactComponent]
        public void GreetObjectC(ObjectC obj)
        {
            Console.WriteLine("Special greetings, C!");
        }
    }
}
