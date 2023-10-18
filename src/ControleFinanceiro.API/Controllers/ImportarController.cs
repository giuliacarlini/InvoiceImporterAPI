using ControleFinanceiro.Domain.Adapters.Repository;
using ControleFinanceiro.Domain.Data;
using ControleFinanceiro.Domain.Enum;
using ControleFinanceiro.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiroAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportarController : ControllerBase
    {
        private readonly ILogger<ImportarController> _logger;
        private readonly IConverterService _converterService;
        private readonly IFaturaRepository _faturaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ImportarController(  ILogger<ImportarController> logger, 
                                    IUnitOfWork unitOfWork, 
                                    IFaturaRepository faturaRepository,
                                    IConverterService converterService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _faturaRepository = faturaRepository;
            _converterService = converterService;
        }

        [HttpPost("ImportarCSVNubank")]
        public async Task<ActionResult> ImportarCSVNubank(string CaminhoArquivo, DateTime Vencimento)
        {
            if (_faturaRepository.BuscarFaturaPorNomeArquivo(Path.GetFileName(CaminhoArquivo)))
                return BadRequest("Arquivo já importado!");

            _unitOfWork.BeginTransaction();
            try
            {
                //var fatura = await _converterService.TransformarLinhasEmObjeto(CaminhoArquivo, Vencimento, TipoImportacao.Nubank);

               // var idFatura = _importarService.ImportarArquivo(CaminhoArquivo, Vencimento, TipoImportacao.Nubank, teste);
                
                _unitOfWork.Commit();

                //return idFatura > 0 ? Ok(idFatura) : 
                return NotFound("Fatura não importada.");
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return BadRequest(ex.Message);
            }
        }
    }
}