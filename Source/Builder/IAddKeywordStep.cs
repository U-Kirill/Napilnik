namespace Source.Builder
{
    public interface IAddKeywordStep
    {
        IAddInfoOrHashStep WithOrderKeyword(string keyword);
    }
}