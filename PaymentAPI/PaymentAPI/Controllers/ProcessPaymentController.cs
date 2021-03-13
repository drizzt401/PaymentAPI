using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PaymentAPI.Core.Commands;
using PaymentAPI.Domain.ModelView;
using System.Threading.Tasks;

namespace PaymentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessPaymentController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost]
        public async Task<ActionResult<PaymentResponseMV>> Post(ProcessPaymentCommand processPaymentCommand)
        {
            var respose = await Mediator.Send(processPaymentCommand);
            if (respose.ResponseCode == "00") return Ok(respose); else return BadRequest(respose);
        }

    }
}
