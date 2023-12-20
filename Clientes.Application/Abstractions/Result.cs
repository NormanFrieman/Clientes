
using FluentValidation.Results;
using System.Net;

namespace Clientes.Application.Abstractions
{
    public class Result
    {
        public bool IsSuccess { get; }
        public Error Error { get; }
        public object? Body { get; }

        private Result(bool isSuccess, Error error, object? body = null)
        {
            IsSuccess = isSuccess;
            Error = error;
            Body = body;
        }

        public static Result Success(object? body = null) => new(true, Error.None, body);
        public static Result Failure(Error error) => new(false, error);
        public static Result Failure(IEnumerable<ValidationFailure> errors) => new(false, new Error(HttpStatusCode.BadRequest, errors.Select(x => x.ErrorMessage).ToArray()));
    }
}
