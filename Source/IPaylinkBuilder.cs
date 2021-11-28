using System;

namespace Source
{
    public interface IAddKeyworldStep
    {
        IAddInfoOrHashStep AddOrderKeyword(string keyword);
    }

    public class PaylinkBuilder : IAddKeyworldStep, IAddInfoOrHashStep, IAddHashOrBuildStep
    {
        private const string HashSplitSymbol = "+";
        private const string InfoSplitSymbol = "&";
        private const string OrderSplitSymbol = "?";
        private const string SiteSplitSymbol = "/";

        private string _accumulatedUrl;

        public static IAddKeyworldStep Create(string rootUrl) => new PaylinkBuilder(rootUrl);

        private PaylinkBuilder(string rootUrl)
        {
            ValidateString(rootUrl, nameof(rootUrl));
            _accumulatedUrl = rootUrl + SiteSplitSymbol;
        }

        public IAddInfoOrHashStep AddOrderKeyword(string keyword)
        {
            ValidateString(keyword, nameof(keyword));
            Append(keyword, OrderSplitSymbol);
            return this;
        }

        public IAddInfoOrHashStep AddInfo(IInfoProvider infoProvider)
        {
            ValidateReference(infoProvider, nameof(infoProvider));
            Append(infoProvider.Info, InfoSplitSymbol);
            return this;
        }

        public IAddHashOrBuildStep AddHash(IHashProvider hashProvider)
        {
            ValidateReference(hashProvider, nameof(hashProvider));
            Append(hashProvider.GetHash(), HashSplitSymbol);
            return this;
        }

        public string Build() => _accumulatedUrl.TrimEnd(HashSplitSymbol.GetPinnableReference());

        private void Append(string body, string splitSymbol) =>
            _accumulatedUrl += body + splitSymbol;

        private void ValidateString(string testedString, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(testedString))
                throw new ArgumentNullException(argumentName);
        }

        private void ValidateReference(object testedObject, string argumentName)
        {
            if (testedObject == null)
                throw new ArgumentNullException(argumentName);
        }
    }

    public interface IAddInfoOrHashStep : IAddHashStep
    {
        IAddInfoOrHashStep AddInfo(IInfoProvider infoProvider);
    }

    public interface IAddHashStep
    {
        IAddHashOrBuildStep AddHash(IHashProvider hashProvider);
    }

    public interface IAddHashOrBuildStep : IAddHashStep
    {
        string Build();
    }
    public interface IHashProvider
    {
        string GetHash();
    }

    public interface IInfoProvider
    {
        string Info { get; }
    }

    public interface IPaylinkTemplate
    {
    }
}