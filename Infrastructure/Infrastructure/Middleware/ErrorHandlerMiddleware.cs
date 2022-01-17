using System;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {

                    case BadHttpRequestException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case CommandValidationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case AuthenticationException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case SecurityTokenExpiredException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case UnauthorizedAccessException:
                        response.StatusCode = (int) HttpStatusCode.Forbidden;
                        break;
                    case NotImplementedException:
                        response.StatusCode = (int)HttpStatusCode.NotImplemented;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new {status = response.StatusCode, message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}