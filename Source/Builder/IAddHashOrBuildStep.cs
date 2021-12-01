namespace Source.Builder
{
    public interface IAddHashOrBuildStep : IAddHashStep
    {
        string Build();
    }
}