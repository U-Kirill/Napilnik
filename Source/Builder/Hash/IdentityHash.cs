namespace Source.Builder.Hash
{
    public class IdentityHash : IHashProvider
    {
        private readonly string _hash;

        public IdentityHash(string hash)
        {
            _hash = hash;
        }

        public string GetHash() => _hash;
    }
}