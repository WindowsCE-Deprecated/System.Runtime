using System.Runtime.InteropServices;

namespace System
{
    /// <summary>The exception that is thrown when a null reference (Nothing in Visual Basic) is passed to a method that does not accept it as a valid argument. </summary>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    [Serializable]
    public class ArgumentNullException2 : ArgumentException2
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class.</summary>
        public ArgumentNullException2()
            : base(ExtractMessage())
        { }

        /// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with the name of the parameter that causes this exception.</summary>
        /// <param name="paramName">The name of the parameter that caused the exception. </param>
        public ArgumentNullException2(string paramName)
            : base(ExtractMessage(), paramName)
        { }

        /// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with a specified error message and the exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for this exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        public ArgumentNullException2(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>Initializes an instance of the <see cref="T:System.ArgumentNullException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
        /// <param name="paramName">The name of the parameter that caused the exception. </param>
        /// <param name="message">A message that describes the error. </param>
        public ArgumentNullException2(string paramName, string message)
            : base(message, paramName)
        { }

        private static string ExtractMessage()
            => new ArgumentNullException().Message;
    }
}
