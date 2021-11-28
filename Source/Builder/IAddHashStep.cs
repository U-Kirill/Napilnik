namespace Source.Builder
{
    public interface IAddHashStep
    {
        IAddHashOrBuildStep WithHash(IHashProvider hashProvider);
    }
}