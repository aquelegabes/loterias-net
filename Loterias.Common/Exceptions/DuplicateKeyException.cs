using System;

namespace Loterias.Common.Exceptions
{
    /// <summary>
    /// Thrown when an attempt is made to add an object to the collection by using a key that is already being used.
    /// </summary>
    [Serializable]
    public class DuplicateKeyException : Exception
    {
        /// <summary>
        /// The duplicated object
        /// </summary>
        public object DuplicateKey { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateKeyException"/> class
        /// </summary>
        public DuplicateKeyException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateKeyException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public DuplicateKeyException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateKeyException"/> class by providing an error message, and specifying the exception that caused this exception to be thrown.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference</param>
        public DuplicateKeyException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateKeyException"/> class by referencing the duplicate object, providing an error message, and specifying the exception that caused this exception to be thrown.
        /// </summary>
        /// <param name="duplicate">The object that was already added. <see cref="DuplicateKey"/></param>
        /// <param name="message">The error message that explains the reason for the exception.></param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference</param>
        public DuplicateKeyException(object duplicate, string message, Exception inner) : base (message,inner)
        {
            this.DuplicateKey = duplicate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateKeyException"/> class by referencing the duplicate object, providing an error message.
        /// </summary>
        /// <param name="duplicate">The object that was already added. <see cref="DuplicateKey"/></param>
        /// <param name="message">The error message that explains the reason for the exception.></param>
        public DuplicateKeyException(object duplicate, string message) : base(message)
        {
            this.DuplicateKey = duplicate;
        }

        protected DuplicateKeyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
