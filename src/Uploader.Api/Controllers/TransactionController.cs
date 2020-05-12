using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Uploader.Core.Commands;
using Uploader.Core.Queries;
using Uploader.Core.Validators;

namespace Uploader.Api.Controllers
{
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route("Filter/ByCurrency")]
        public async Task<IActionResult> GetTransactionsByCurrencyAsync([FromQuery] string currency )
        {
            if (string.IsNullOrEmpty(currency)) throw new Exception("Invalid currency");
            
            var command = new GetTransactionsByCurrencyQuery
            {
                Currency = currency
            };
        
            var result = await _mediator.Send(command);
        
            return Ok(result);
        }
        
        
        [HttpGet]
        [Route("Filter/ByDateRange")]
        public async Task<IActionResult> UploadFileAsync([FromQuery] string start, [FromQuery] string end )
        {
            if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end)) throw new Exception("Invalid start or end date");
            
            var command = new GetTransactionsByDateRangeQuery
            {
                Start = start,
                End = end
            };
        
            var result = await _mediator.Send(command);
        
            return Ok(result);
        }
        
        
        [HttpGet]
        [Route("Filter/Status")]
        public async Task<IActionResult> UploadFileAsync([FromQuery] string status )
        {
            if (string.IsNullOrEmpty(status)) throw new Exception("Invalid status");
            
            var command = new GetTransactionsByStatusQuery()
            {
                Status = status
            };
        
            var result = await _mediator.Send(command);
        
            return Ok(result);
        }
        
        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFileAsync()
        {
            var file = HttpContext.Request.Form.Files[0];
            
            var command = new UploadFileCommand()
            {
                File = file
            };

            UploadFileCommandValidator.FileIsValid(file);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
        
    }
}