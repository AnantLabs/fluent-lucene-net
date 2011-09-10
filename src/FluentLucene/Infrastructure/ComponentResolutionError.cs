namespace FluentLucene.Infrastructure
{
    internal enum ComponentResolutionError
    {
        None = 0,
        AlreadyRegistered,
        NotRegistered,
        ConstructorNotFound,
        DependencyResolution,
        CircularDependency
    }
}