﻿using System.Reflection;
using System.Runtime.InteropServices;
using DecorativeComponents;

namespace ExtendedDecorativeComponents
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ContactComponentAttribute : Attribute
    {
    }

    public abstract class ExtendedComponent : ComponentBase
    {
        protected ExtendedComponent()
        {
            MapConnections();
        }

        private void MapConnections()
        {
            foreach (var method in GetType().GetMethods())
            {
                var attribute = method.GetCustomAttribute<ContactComponentAttribute>();
                if (attribute is null)
                {
                    continue;
                }
                var args = method.GetParameters();

                if (args.Length != 1)
                {
                    continue;
                }

                var targetType = args.First().ParameterType;
                if (!targetType.IsSubclassOf(typeof(ComponentBase)) &&
                    targetType != typeof(ComponentBase))
                {
                    continue;
                }

                var commonDelegate = AddGreetingFor<ComponentBase>;
                commonDelegate
                    .GetMethodInfo()
                    .GetGenericMethodDefinition()
                    .MakeGenericMethod(targetType)
                    .Invoke(this, new object[] { method.CreateDelegate(typeof(Action<>).MakeGenericType(targetType), this) });
            }
        }
    }
}
