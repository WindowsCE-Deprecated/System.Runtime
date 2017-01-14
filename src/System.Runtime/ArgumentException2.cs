using System.Runtime.InteropServices;

namespace System
{
    /// <summary>The exception that is thrown when one of the arguments provided to a method is not valid.</summary>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true)]
    [Serializable]
    public class ArgumentException2 : ArgumentException
    {
        private readonly string _message;
        private readonly string _paramName;

        /// <summary>Gets the error message and the parameter name, or only the error message if no parameter name is set.</summary>
        /// <returns>A text string describing the details of the exception. The value of this property takes one of two forms: Condition Value The <paramref name="paramName" /> is a null reference (Nothing in Visual Basic) or of zero length. The <paramref name="message" /> string passed to the constructor. The <paramref name="paramName" /> is not null reference (Nothing in Visual Basic) and it has a length greater than zero. The <paramref name="message" /> string appended with the name of the invalid parameter. </returns>
        /// <filterpriority>1</filterpriority>
        public override string Message
        {
            get
            {
                if (_message != null)
                    return _message;

                return base.Message;
            }
        }

        /// <summary>Gets the name of the parameter that causes this exception.</summary>
        /// <returns>The parameter name.</returns>
        /// <filterpriority>1</filterpriority>
        public virtual string ParamName
        {
            get { return _paramName; }
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class.</summary>
        public ArgumentException2()
            : base()
        { }

        /// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message.</summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        public ArgumentException2(string message)
            : base(message)
        { }

        /// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
        public ArgumentException2(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="paramName">The name of the parameter that caused the current exception. </param>
        public ArgumentException2(string message, string paramName)
            : base(message, paramName)
        {
            _paramName = paramName;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message, the parameter name, and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="paramName">The name of the parameter that caused the current exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
        public ArgumentException2(string message, string paramName, Exception innerException)
            : base(message, innerException)
        {
            _message = new ArgumentException(message, paramName).Message;
            _paramName = paramName;
        }
    }
}
