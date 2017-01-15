using System.Runtime.InteropServices;

namespace System
{
    /// <summary>
    /// The ArgumentException is thrown when an argument
    /// is null when it shouldn't be.
    /// </summary>
    [ComVisible(true)]
    [Serializable]
    public class ArgumentNullException2 : ArgumentException2
    {
        /// <summary>
        /// Creates a new ArgumentNullException with its message
        /// string set to a default message explaining an argument was null.
        /// </summary>
        public ArgumentNullException2()
            : base(ExtractMessage())
        { }

        public ArgumentNullException2(string paramName)
            : base(ExtractMessage(), paramName)
        { }

        public ArgumentNullException2(string message, Exception innerException)
            : base(message, innerException)
        { }

        public ArgumentNullException2(string paramName, string message)
            : base(message, paramName)
        { }

        private static string ExtractMessage()
            => new ArgumentNullException().Message;
    }
}
