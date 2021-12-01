namespace Source.Builder.Hash
{
    public class FakeMd5Hash : IHashProvider
    {
        private readonly int _originalValue;

        public FakeMd5Hash(int originalValue)
        {
            _originalValue = originalValue;
        }

        public string GetHash() => "b10a8db164e0754105b7a99be72e3fe5"; //_originalValueHash
    }
}