namespace DecorativeComponents
{
    public interface IComponent
    {
    }

    public abstract class ComponentBase : IComponent
    {
        private IActionContainer Greetings { get; init; } = new ActionContainer();

        public virtual IEnumerable<T> GetComponents<T>() where T : IComponent
        {
            if (this is T t)
            {
                yield return t;
            }
        }

        public IEnumerable<ComponentBase> Decomposed => GetComponents<ComponentBase>();

        protected void AddGreetingFor<T>(Action<T> greetingAction) where T : IComponent
        {
            Greetings.Register(greetingAction);
        }

        public virtual void Greet<T>(T obj) where T : ComponentBase
        {
            foreach (var component in obj.Decomposed)
            {
                Greetings.InvokeFor(component);
            }
        }
    }

    public class CompositeComponent<A, B> : ComponentBase where A : ComponentBase where B : ComponentBase
    {
        private readonly A _componentA;
        private readonly B _componentB;

        internal CompositeComponent(A componentA, B componentB)
        {
            _componentA = componentA;
            _componentB = componentB;
        }

        public override IEnumerable<T> GetComponents<T>()
        {
            foreach (var component in _componentA.GetComponents<T>())
            {
                yield return component;
            }
            foreach (var component in _componentB.GetComponents<T>())
            {
                yield return component;
            }
        }

        public override void Greet<T>(T obj)
        {
            _componentA.Greet(obj);
            _componentB.Greet(obj);
        }
    }

    public class ComponentShell : ComponentBase
    {
        private ComponentBase? _component;

        public override IEnumerable<T> GetComponents<T>()
        {
            if (_component is null)
            {
                return Enumerable.Empty<T>();
            }
            return _component.GetComponents<T>();
        }

        public ComponentShell CombineWith<T>(T component) where T : ComponentBase
        {
            if (_component is null)
            {
                _component = component;
                return this;
            }
            _component = _component.CombineWith(component);
            return this;
        }
    }

    public static class CompositeExtensions
    {
        public static CompositeComponent<A, B> CombineWith<A, B>(this A a, B b) where A : ComponentBase where B : ComponentBase
        {
            a.Greet(b);
            b.Greet(a);
            return new CompositeComponent<A, B>(a, b);
        }
    }
}
