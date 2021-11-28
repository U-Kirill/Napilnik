namespace Source
{
    public interface IAddKeyworldStep : IPaylinkBuilder
    {
        IAddInfoOrHashStep AddOrderKeyword(string keyword);
    }

    public class PaylinkBuilder : IAddKeyworldStep, IAddInfoOrHashStep
    {
        private const string HashSplitSymbol = "+";
        private const string InfoSplitSymbol = "&";
        private const string OrderSplitSymbol = "?";
        private const string SiteSplitSymbol = "/";
        
        private string accumulatedUrl;

        public static IAddKeyworldStep Create(string rootUrl) => new PaylinkBuilder(rootUrl);

        private PaylinkBuilder(string rootUrl)
        {
            accumulatedUrl = rootUrl + SiteSplitSymbol;
        }

        public IAddInfoOrHashStep AddOrderKeyword(string keyword)
        {
            Append(keyword, OrderSplitSymbol);
            return this;
        }

        public IAddInfoOrHashStep AddInfo(IInfoProvider infoProvider)
        {
            Append(infoProvider.Info, InfoSplitSymbol);
            return this;
        }

        public IAddHashStep AddHash(IHashProvider hashProvider)
        {
            Append(hashProvider.GetHash(), HashSplitSymbol);
            return this;
        }

        public string Build() => accumulatedUrl.TrimEnd(HashSplitSymbol.GetPinnableReference());

        private void Append(string body, string splitSymbol) =>
            accumulatedUrl += body + splitSymbol;
    }

    public interface IAddInfoOrHashStep : IAddHashStep
    {
        IAddInfoOrHashStep AddInfo(IInfoProvider infoProvider);
    }

    public interface IAddHashStep : IPaylinkBuilder
    {
        IAddHashStep AddHash(IHashProvider hashProvider);
    }

    public interface IHashProvider
    {
        string GetHash();
    }

    public interface IInfoProvider
    {
        string Info { get; }
    }

    public interface IPaylinkBuilder
    {
        string Build();
    }
    
    public interface IPaylinkTemplate
    {
    }
}