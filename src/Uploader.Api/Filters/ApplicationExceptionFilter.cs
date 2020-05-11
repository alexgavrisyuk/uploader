using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Uploader.Api.Filters
{
    public class ApplicationExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public ApplicationExceptionFilter(
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(nameof(ApplicationExceptionFilter));
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Application threw exception: ");

            var response = new
            {
                ErrorMessage =  context.Exception.Message,
                IsSuccess = false
            };

            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            context.Result = new JsonResult(response);
        }
    }
}