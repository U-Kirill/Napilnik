using System;

namespace Source.Builder.Hash
{
    public class IdentityHash : IHashProvider
    {
        private readonly string _originalValue;

        public IdentityHash(string originalValue)
        {
            if (string.IsNullOrWhiteSpace(originalValue))
                throw new ArgumentNullException(nameof(originalValue));

            _originalValue = originalValue;
        }

        public string GetHash() => _originalValue;
    }
}