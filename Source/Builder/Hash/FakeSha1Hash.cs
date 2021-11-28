namespace Source.Builder.Hash
{
    public class FakeSha1Hash : IHashProvider
    {
        private readonly int _originalValue;

        public FakeSha1Hash(int originalValue)
        {
            _originalValue = originalValue;
        }

        public string GetHash() => "0a4d55a8d778e5022fab701977c5d840bbc486d0"; //_originalValueHash
    }
}