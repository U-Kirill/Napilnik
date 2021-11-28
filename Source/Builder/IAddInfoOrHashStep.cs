namespace Source.Builder
{
    public interface IAddInfoOrHashStep : IAddHashStep
    {
        IAddInfoOrHashStep WithInfo(IInfoProvider infoProvider);
    }
}