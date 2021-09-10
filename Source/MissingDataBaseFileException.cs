using System;

namespace Source
{
    public sealed class MissingDataBaseFileException : Exception
    {
        private const string _message = "Файл db.sqlite не найден. Положите файл в папку вместе с exe.";

        public MissingDataBaseFileException(string connectionString, Exception inner)
            : base(_message, inner)
        {
            Data["Connection String"] = connectionString;
        }
    }
}