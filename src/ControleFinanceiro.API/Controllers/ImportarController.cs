using ControleFinanceiro.Domain.Adapters;
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
        private readonly ICadastrarFaturaService _importarService;
        private readonly IConverterService _converterService;
        private readonly IFaturaRepository _faturaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ImportarController(  ILogger<ImportarController> logger, 
                                    ICadastrarFaturaService importarService, 
                                    IUnitOfWork unitOfWork, 
                                    IFaturaRepository faturaRepository,
                                    IConverterService converterService)
        {
            _logger = logger;
            _importarService = importarService;
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
                var fatura = await _converterService.TransformarLinhasEmObjeto(CaminhoArquivo, Vencimento, TipoImportacao.Nubank);

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

        [HttpPost("ImportarCSVC6Bank")]
        public ActionResult ImportarCSVC6Bank(string CaminhoArquivo, DateTime Vencimento)
        {
            _unitOfWork.BeginTransaction();
            try
            {
               // _importarService.ImportarArquivo(CaminhoArquivo, Vencimento, TipoImportacao.C6Bank);
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