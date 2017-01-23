using System;
using System.Runtime.InteropServices;

#if NET35_CF
namespace System
#else
namespace Mock.System
#endif
{
    /// <summary>
    /// The ArgumentException is thrown when an argument does not meet
    /// the contract of the method.  Ideally it should give a meaningful error
    /// message describing what was wrong and which parameter is incorrect.
    /// </summary>
    [ComVisible(true)]
    [Serializable]
    public class ArgumentException2 : ArgumentException
    {
        private readonly string _message;
        private readonly string _paramName;

        public override string Message
        {
            get
            {
                if (_message != null)
                    return _message;

                return base.Message;
            }
        }

        public virtual string ParamName
        {
            get { return _paramName; }
        }

        /// <summary>
        /// Creates a new ArgumentException with its message
        /// string set to the empty string.
        /// </summary>
        public ArgumentException2()
            : base()
        { }

        /// <summary>
        /// Creates a new ArgumentException with its message
        /// string set to message.
        /// </summary>
        public ArgumentException2(string message)
            : base(message)
        { }

        public ArgumentException2(string message, Exception innerException)
            : base(message, innerException)
        { }

        public ArgumentException2(string message, string paramName)
            : base(message, paramName)
        {
            _paramName = paramName;
        }

        public ArgumentException2(string message, string paramName, Exception innerException)
            : base(message, innerException)
        {
            _message = new ArgumentException(message, paramName).Message;
            _paramName = paramName;
        }
    }
}
