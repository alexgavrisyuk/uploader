using System;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Uploader.Api.Filters
{
    public class ValidateModelStateFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ModelState.IsValid)
            {
                Console.WriteLine("Successful model state validation");
                return;
            }

            var modelStateValues = actionContext.ModelState.Values.Select(kvp => string.Join(". ",
                kvp.Errors.Where(s => !string.IsNullOrEmpty(s.ErrorMessage)).Select(s => s.ErrorMessage)));
            modelStateValues = modelStateValues.Concat(actionContext.ModelState.Values.Select(kvp => string.Join(". ",
                kvp.Errors.Where(s => string.IsNullOrEmpty(s.ErrorMessage)).Select(s => s.Exception.Message))));

            var errorMessage = new StringBuilder();
            errorMessage.AppendJoin($"  ", modelStateValues);

            var response = new
            {
                ErrorMessage = errorMessage.ToString(),
                IsSuccess = false
            };

            actionContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            actionContext.Result = new JsonResult(response);
        }
    }
}