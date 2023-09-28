using ControleFinanceiro.Domain.Interface;
using ControleFinanceiro.Domain.Utils;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("ImportarCSVNubank")]
        public ActionResult ImportarCSVNubank(string CaminhoArquivo, DateTime Vencimento)
        {
            importarService.ImportarArquivo(CaminhoArquivo, Vencimento, TipoImportacao.Nubank);

            return Ok(); 
        }

        [HttpPost("ImportarCSVC6Bank")]
        public ActionResult ImportarCSVC6Bank(string CaminhoArquivo, DateTime Vencimento)
        {
            importarService.ImportarArquivo(CaminhoArquivo, Vencimento, TipoImportacao.C6Bank);

            return Ok();
        }
    }
}