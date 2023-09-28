using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ControleFinanceiroAPI.Model;
using ControleFinanceiroAPI.Interface;

namespace ControleFinanceiroAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportarController : ControllerBase
    {
        private readonly ILogger<ImportarController> _logger;
        private readonly IImportar importarService;

        public ImportarController(ILogger<ImportarController> logger, IImportar importarService)
        {
            _logger = logger;
            this.importarService = importarService;
        }

        [HttpPost(Name = "ImportarCSVNubank")]
        public ActionResult Post(string CaminhoArquivo, DateTime Vencimento)
        {
            importarService.ImportarArquivo(CaminhoArquivo, Vencimento, 0);

            return Ok(); 
        }                
    }
}