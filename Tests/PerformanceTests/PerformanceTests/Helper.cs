using System;
using Entitas;

public static class Helper {

    public class Context<Entity> : Entitas.Context<Entity> where Entity : class, IEntity {
        public Context(int totalComponents, int startCreationIndex, ContextInfo contextInfo, Func<IEntity, IAERC> aercFactory) : base(totalComponents, startCreationIndex, contextInfo, aercFactory) {
        }

        public Entity CreateEntity() {
            return InternalCreateEntity();
        }
    }
    
    public static Context<Entity> CreateContext() {
        return new Context<Entity>(CP.NumComponents, 0, new ContextInfo("Test Context", new string[CP.NumComponents], null), (entity) => new SafeAERC(entity));
    }
}
