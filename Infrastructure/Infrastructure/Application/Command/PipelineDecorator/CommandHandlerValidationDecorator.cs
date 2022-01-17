using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Command.Interface;
using Infrastructure.Exceptions;
using MediatR;

namespace Infrastructure.Application.Command.PipelineDecorator
{
    public class CommandHandlerValidationDecorator<TCommand,TResponse> : IPipelineBehavior<TCommand,TResponse>
        where TCommand : ICommandBase
    {
        private readonly ICommandValidator<TCommand> _commandValidator;
        
        public CommandHandlerValidationDecorator(IServiceProvider serviceProvider)
        {
            var queryValidator = (ICommandValidator<TCommand>)serviceProvider.GetService(typeof(ICommandValidator<TCommand>));
            _commandValidator = queryValidator;
        }
        
        public async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{typeof(TCommand)} is null");
            }
            
            if (_commandValidator == null)
            {
                return await next();
            }
            
            if ((_commandValidator is CommandValidator<TCommand>) == false)
            {
                throw new ArgumentException($"{typeof(ICommandValidator<TCommand>)} is not CommandValidator");
            }
            
            var validationResult = _commandValidator.Validate(request);
            if (validationResult.IsValid == false)
            {
                //TODO
                throw new CommandValidationException(validationResult);
               // throw new Exception($"CommandException {validationResult.Errors}");
            }

            return await  next();
        }
    }
    
    public class CommandWithReturnValueHandlerValidationDecorator<TCommand,TResponse> : IPipelineBehavior<TCommand,TResponse>
        where TCommand : ICommandBase<TResponse>
        where TResponse : ICommandResultBase
    {
        private readonly ICommandValidator<TCommand> _commandValidator;
        
        public CommandWithReturnValueHandlerValidationDecorator(IServiceProvider serviceProvider)
        {
            var queryValidator = (ICommandValidator<TCommand>)serviceProvider.GetService(typeof(ICommandValidator<TCommand>));
            _commandValidator = queryValidator;
        }
        
        public async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{typeof(TCommand)} is null");
            }
            
            if (_commandValidator == null)
            {
                return await next();
            }
            
            if ((_commandValidator is CommandValidator<TCommand>) == false)
            {
                throw new ArgumentException($"{typeof(ICommandValidator<TCommand>)} is not CommandValidator");
            }
            
            var validationResult = _commandValidator.Validate(request);
            if (validationResult.IsValid == false)
            {
                //TODO
                throw new CommandValidationException(validationResult);
               // throw new CommandValidationException($"CommandException: {validationResult.Errors[0].ErrorMessage}");
            }

            return await  next();
        }
    }
}