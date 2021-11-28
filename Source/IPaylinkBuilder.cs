namespace Source
{
    public interface IAddKeyworldStep : IPaylinkBuilder
    {
        IAddInfoOrHashStep AddOrderKeyword(string keyword);
    }

    public interface IAddInfoOrHashStep
    {
        IAddInfoOrHashStep AddInfo(IInfoProvider infoProvider);
        IPaylinkBuilder AddHash(IHashProvider hashProvider);
    }

    public class PaylinkBuilder : IAddKeyworldStep, IAddInfoOrHashStep
    {
        private string accumulatedUrl;
        
        private PaylinkBuilder(string rootUrl)
        {
            accumulatedUrl = rootUrl + "/";
        }

        public IAddInfoOrHashStep AddOrderKeyword(string keyword)
        {
            accumulatedUrl += keyword + "?";
            return this;
        }

        public IAddInfoOrHashStep AddInfo(IInfoProvider infoProvider)
        {
            accumulatedUrl += infoProvider.Info + "&";
            return this;
        }

        public IPaylinkBuilder AddHash(IHashProvider hashProvider)
        {
            accumulatedUrl += hashProvider.GetHash();
            return this;
        }
        
        public static IAddKeyworldStep Create(string rootUrl)
        {
            return new PaylinkBuilder(rootUrl);
        }
        
        public string Build() => accumulatedUrl;
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