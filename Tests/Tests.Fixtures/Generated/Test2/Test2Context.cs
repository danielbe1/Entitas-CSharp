//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class Test2Context : Entitas.Context<Test2Entity> {

    public Test2Context()
        : base(
            Test2ComponentsLookup.TotalComponents,
            0,
            new Entitas.ContextInfo(
                "Test2",
                Test2ComponentsLookup.componentNames,
                Test2ComponentsLookup.componentTypes
            ),
            (entity) =>

#if (ENTITAS_FAST_AND_UNSAFE)
                new Entitas.UnsafeAERC()
#else
                new Entitas.SafeAERC(entity)
#endif

        ) {
    }
    
    public Test2Entity CreateEntity() {
        return InternalCreateEntity();
    }
}
