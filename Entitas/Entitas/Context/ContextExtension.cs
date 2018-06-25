namespace Entitas {

    public static class ContextExtension {

        /// Returns all entities matching the specified matcher.
        public static TEntity[] GetEntities<TEntity>(this IContext<TEntity> context, IMatcher<TEntity> matcher) where TEntity : class, IEntity {
            return context.GetGroup(matcher).GetEntities();
        }
    }
}
