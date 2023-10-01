using ControleFinanceiro.Domain.Data;
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
        private readonly IImportar _importarService;
        private readonly IUnitOfWork _unitOfWork;

        public ImportarController(ILogger<ImportarController> logger, IImportar importarService, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _importarService = importarService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("ImportarCSVNubank")]
        public ActionResult ImportarCSVNubank(string CaminhoArquivo, DateTime Vencimento)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var idFatura = _importarService.ImportarArquivo(CaminhoArquivo, Vencimento, TipoImportacao.Nubank);
                
                _unitOfWork.Commit();

                return idFatura > 0 ? Ok(idFatura) : NotFound("Fatura não importada.");
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ImportarCSVC6Bank")]
        public ActionResult ImportarCSVC6Bank(string CaminhoArquivo, DateTime Vencimento)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                _importarService.ImportarArquivo(CaminhoArquivo, Vencimento, TipoImportacao.C6Bank);
                _unitOfWork.Commit();

                return Ok();
            }
            catch (Exception ex) 
            {
                _unitOfWork.Rollback();
                return BadRequest(ex.Message);
            }
        }
    }
}