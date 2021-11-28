using System;

namespace Source.Builder
{
    public class PaylinkBuilder : IAddKeywordStep, IAddInfoOrHashStep, IAddHashOrBuildStep
    {
        private const string HashSplitSymbol = "+";
        private const string InfoSplitSymbol = "&";
        private const string OrderSplitSymbol = "?";
        private const string SiteSplitSymbol = "/";

        private string _accumulatedUrl;

        public static IAddKeywordStep Create(string rootUrl) => new PaylinkBuilder(rootUrl);

        private PaylinkBuilder(string rootUrl)
        {
            if (string.IsNullOrWhiteSpace(rootUrl))
                throw new ArgumentNullException(nameof(rootUrl));

            _accumulatedUrl = rootUrl + SiteSplitSymbol;
        }

        public IAddInfoOrHashStep AddOrderKeyword(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentNullException(nameof(keyword));

            Append(keyword, OrderSplitSymbol);
            return this;
        }

        public IAddInfoOrHashStep AddInfo(IInfoProvider infoProvider)
        {
            infoProvider = infoProvider ?? throw new ArgumentNullException(nameof(infoProvider));

            Append(infoProvider.Info, InfoSplitSymbol);
            return this;
        }

        public IAddHashOrBuildStep AddHash(IHashProvider hashProvider)
        {
            hashProvider = hashProvider ?? throw new ArgumentNullException(nameof(hashProvider));

            Append(hashProvider.GetHash(), HashSplitSymbol);
            return this;
        }

        public string Build() => _accumulatedUrl.TrimEnd(HashSplitSymbol.GetPinnableReference());

        private void Append(string body, string splitSymbol) =>
            _accumulatedUrl += body + splitSymbol;
    }
}