using InvoiceImporter.Domain.Adapters.Repository;
using InvoiceImporter.Domain.Commands.Request;
using InvoiceImporter.Domain.Commands.Response;
using InvoiceImporter.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;
using System.Text;

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
            [FromForm] string jsonString,
            [FromForm] IFormFile file)
        {
            CreateInvoiceRequest command = JsonConvert.DeserializeObject<CreateInvoiceRequest>(jsonString);

            if (command == null)
                return BadRequest();            

            var lines = new List<string>();

            if (file.ContentType == "text/csv")
            {               
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                    {
                        var line = reader.ReadLine();
                        if (line != null)
                            lines.Add(line);
                    }
                };
            }

            command.Lines = lines;
            command.FileName = file.FileName;

            var result = (CommandResponse)_handler.Handle(command);

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
