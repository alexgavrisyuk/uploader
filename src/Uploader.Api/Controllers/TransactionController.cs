using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Uploader.Core.Commands;
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
        //
        // [HttpGet]
        // public async Task<IActionResult> UploadFileAsync([FromQuery] string a )
        // {
        //     var file = HttpContext.Request.Form.Files[0];
        //     
        //     var command = new UploadFileCommand()
        //     {
        //         File = file
        //     };
        //
        //     var result = await _mediator.Send(command);
        //
        //     return Ok(result);
        // }
        
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