using InvoiceImporter.Domain.Adapters.Repository;
using InvoiceImporter.Domain.Commands;
using InvoiceImporter.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceImporter.API.Controllers
{
    [ApiController]
    [Route("InvoiceImporter")]
    [Authorize]
    public class TodoController : ControllerBase
    {
        [HttpPost("Importer")]
        [AllowAnonymous]
        public ActionResult Importer(
            [FromServices] InvoiceHandler _handler,
            [FromBody] CreateInvoiceCommand _command)
        {
            var result = (CommandResult)_handler.Handle(_command);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public ActionResult GettAll(
            [FromServices] IInvoiceRepository _repository)
        {
            var result = _repository.FindAll();

            return result.Count() > 0 ? Ok(result) : NotFound(result);
        }
    }

}
