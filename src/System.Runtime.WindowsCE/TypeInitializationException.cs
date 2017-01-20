namespace System
{
    [Serializable]
    public sealed class TypeInitializationException : Exception
    {
        private const string TypeInitialization_Default = "The type initializer for '{0}' threw an exception.";
        private readonly string _fullTypeName;

        public TypeInitializationException(string fullTypeName, Exception innerException)
            : base(GetDefaultMessage(fullTypeName), innerException)
        {
            _fullTypeName = fullTypeName;
        }

        public string TypeName
            => _fullTypeName ?? string.Empty;

        //public override void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    base.GetObjectData(info, context);
        //    info.AddValue("TypeName", TypeName, typeof(String));
        //}

        private static string GetDefaultMessage(string fullTypeName)
            => string.Format(TypeInitialization_Default, fullTypeName ?? string.Empty);
    }
}
