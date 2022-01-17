using System;
using System.Runtime.Serialization;
using System.Text;
using FluentValidation.Results;

namespace Infrastructure.Exceptions
{
    [Serializable]
    internal class CommandValidationException : Exception
    {
        public CommandValidationException()
        {
        }

        public CommandValidationException(string message) : base(message)
        {
        }

        public CommandValidationException(ValidationResult validationResult) 
        {
            StringBuilder sb = new StringBuilder("CommandException: ");
            foreach (var error in validationResult.Errors)
            {
                sb.Append(error.ErrorMessage); 
            }

            throw new CommandValidationException(sb.ToString());
        }

        public CommandValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CommandValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}