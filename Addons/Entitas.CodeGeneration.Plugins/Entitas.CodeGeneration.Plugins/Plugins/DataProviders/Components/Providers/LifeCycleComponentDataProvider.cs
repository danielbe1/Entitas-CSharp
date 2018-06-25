using System;
using System.Linq;
using Entitas.CodeGeneration.Attributes;

namespace Entitas.CodeGeneration.Plugins {

    public class LifeCycleComponentDataProvider : IComponentDataProvider {

        public void Provide(Type type, ComponentData data) {
            var lifecycleType = Attribute
                .GetCustomAttributes(type)
                .OfType<LifeCycleAttribute>()
                .Select(a => a.lifecycleType)
                .DefaultIfEmpty(LifeCycleAttribute.LifecycleType.Mutable)
                .First();
                
            data.GetLifeCycleType(lifecycleType);
        }
    }

    public static class LifeCycleComponentDataExtension {

        public const string COMPONENT_LIFECYCLE_TYPE = "component_lifecycle_type";

        public static LifeCycleAttribute.LifecycleType GetLifeCycleType(this ComponentData data) {
            return (LifeCycleAttribute.LifecycleType)data[COMPONENT_LIFECYCLE_TYPE];
        }

        public static void GetLifeCycleType(this ComponentData data, LifeCycleAttribute.LifecycleType lifecycleType) {
            data[COMPONENT_LIFECYCLE_TYPE] = lifecycleType;
        }
    }
}
