using System;

namespace Entitas.CodeGeneration.Attributes {
    
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
    public class LifeCycleAttribute : Attribute {

        public readonly LifecycleType lifecycleType;

        public LifeCycleAttribute(LifecycleType lifecycleType = LifecycleType.Mutable) {
            this.lifecycleType = lifecycleType;
        }
        
        public enum LifecycleType {
            Mutable,
            Immutable,
            Mandatory
        }
    }
}
