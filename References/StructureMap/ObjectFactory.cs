namespace StructureMap
{
    using StructureMap.Graph;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [Obsolete("ObjectFactory will be removed in a future 4.0 release of StructureMap. Favor the usage of the Container class for future work")]
    public static class ObjectFactory
    {
        private static Lazy<StructureMap.Container> _containerBuilder = new Lazy<StructureMap.Container>(new Func<StructureMap.Container>(ObjectFactory.defaultContainer), LazyThreadSafetyMode.ExecutionAndPublication);
        private static readonly object _lockObject = new object();

        public static void BuildUp(object target)
        {
            Container.BuildUp(target);
        }

        public static void Configure(Action<ConfigurationExpression> configure)
        {
            Container.Configure(configure);
        }

        private static StructureMap.Container defaultContainer()
        {
            StructureMap.Container container = new StructureMap.Container();
            nameContainer(container);
            return container;
        }

        public static IEnumerable<TPluginType> GetAllInstances<TPluginType>()
        {
            return Container.GetAllInstances<TPluginType>();
        }

        public static IEnumerable GetAllInstances(Type pluginType)
        {
            return Container.GetAllInstances(pluginType);
        }

        public static TPluginType GetInstance<TPluginType>()
        {
            return Container.GetInstance<TPluginType>();
        }

        public static object GetInstance(Type pluginType)
        {
            return Container.GetInstance(pluginType);
        }

        public static TPluginType GetNamedInstance<TPluginType>(string name)
        {
            return Container.GetInstance<TPluginType>(name);
        }

        public static object GetNamedInstance(Type pluginType, string name)
        {
            return Container.GetInstance(pluginType, name);
        }

        public static void Initialize(Action<IInitializationExpression> action = null)
        {
            if (action != null)
            {
                lock (_lockObject)
                {
                    InitializationExpression expression = new InitializationExpression();
                    action(expression);
                    PluginGraph pluginGraph = expression.BuildGraph();
                    StructureMap.Container container = new StructureMap.Container(pluginGraph);
                    _containerBuilder = new Lazy<StructureMap.Container>(() => container);
                    nameContainer(container);
                }
            }
        }

        private static void nameContainer(IContainer container)
        {
            container.Name = "ObjectFactory-" + container.Name;
        }

        public static IContainer Container
        {
            get
            {
                return _containerBuilder.Value;
            }
        }
    }
}
