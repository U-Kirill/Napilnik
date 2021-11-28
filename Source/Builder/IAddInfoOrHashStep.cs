namespace Source.Builder
{
    public interface IAddInfoOrHashStep : IAddHashStep
    {
        IAddInfoOrHashStep AddInfo(IInfoProvider infoProvider);
    }
}