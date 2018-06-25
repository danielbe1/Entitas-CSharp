using System.IO;
using System.Linq;

namespace Entitas.CodeGeneration.Plugins {

    public class ContextGenerator : ICodeGenerator {

        public string name { get { return "Context"; } }
        public int priority { get { return 0; } }
        public bool isEnabledByDefault { get { return true; } }
        public bool runInDryMode { get { return true; } }

        const string CONTEXT_TEMPLATE =
@"public sealed partial class ${ContextName}Context : Entitas.Context<${ContextName}Entity> {

    public Entitas.Context<${ContextName}Entity> InternalContext;

    public ${ContextName}Context()
        : base(
            ${Lookup}.TotalComponents,
            0,
            new Entitas.ContextInfo(
                ""${ContextName}"",
                ${Lookup}.componentNames,
                ${Lookup}.componentTypes
            ),
            (entity) =>

#if (ENTITAS_FAST_AND_UNSAFE)
                new Entitas.UnsafeAERC()
#else
                new Entitas.SafeAERC(entity)
#endif

        ) {
    }

    public ${ContextName}Entity CreateEntity(${CreationArgs}) {
        TEntity entity;

        if (_reusableEntities.Count > 0) {
            entity = _reusableEntities.Pop();
            entity.Reactivate(_creationIndex++);
        } else {
            entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
            entity.Initialize(_creationIndex++, _totalComponents, _componentPools, _contextInfo, _aercFactory(entity));
        }

        _entities.Add(entity);
        entity.Retain(this);
        _entitiesCache = null;
        entity.OnComponentAdded += _cachedEntityChanged;
        entity.OnComponentRemoved += _cachedEntityChanged;
        entity.OnComponentReplaced += _cachedComponentReplaced;
        entity.OnEntityReleased += _cachedEntityReleased;
        entity.OnDestroyEntity += _cachedDestroyEntity;

        if (OnEntityCreated != null) {
            OnEntityCreated(this, entity);
        }

        ${Creation}
        return entity;
    }
}
";

        public CodeGenFile[] Generate(CodeGeneratorData[] data) {
            return data
                .OfType<ContextData>()
                .Select(d => generateContextClass(d))
                .ToArray();
        }

        CodeGenFile generateContextClass(ContextData data) {
            var contextName = data.GetContextName();
            return new CodeGenFile(
                contextName + Path.DirectorySeparatorChar + contextName + "Context.cs",
                CONTEXT_TEMPLATE
                    .Replace("${ContextName}", contextName)
                    .Replace("${Lookup}", contextName + ComponentsLookupGenerator.COMPONENTS_LOOKUP),
                GetType().FullName
            );
        }
    }
}
