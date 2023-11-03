using InvoiceImporter.Domain.Adapters.Repository;
using InvoiceImporter.Domain.Commands.Request;
using InvoiceImporter.Domain.Commands.Response;
using InvoiceImporter.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceImporter.API.Controllers
{
    [ApiController]
    [Route("InvoiceImporter")]
    [Authorize]
    public class InvoiceImporterController : ControllerBase
    {
        [HttpPost("Importer")]
        [AllowAnonymous]
        public ActionResult Importer(
            [FromServices] InvoiceHandler _handler,
            [FromBody] CreateInvoiceRequest _command)
        {
            var result = (CommandResponse)_handler.Handle(_command);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public ActionResult GetAll(
            [FromServices] InvoiceHandler _handler)
        {
            var result = (CommandResponse)_handler.Handle(new FindAllInvoicesRequest());

            return result.Success ? Ok(result) : NotFound(result);
        }

    }

}
