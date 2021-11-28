namespace Source.Builder
{
    public interface IAddHashStep
    {
        IAddHashOrBuildStep AddHash(IHashProvider hashProvider);
    }
}