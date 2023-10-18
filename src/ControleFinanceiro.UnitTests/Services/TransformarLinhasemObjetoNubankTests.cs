using ControleFinanceiro.Application;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Enum;

namespace ControleFinanceiro.UnitTests.Services
{
    public class TransformarLinhasemObjetoNubankTests
    {
        private const string ArquivoNEncontrado = "Arquivo não encontrado:";
        private const string ErroConversao = "Erro ao converter linhas em objetos:";
        private const string CaminhoArquivoNubank = "\\arquivos\\nubank\\nubank-2023-09.csv";
        private const string CaminhoArquivoNubankErrado = "\\arquivos\\nubank\\nubank-2023-09-invalido.csv";
        private const string CaminhoArquivoNubankExcel = "\\arquivos\\nubank\\nubank-2023-09-excel.xls";
        private const string CaminhoArquivoC6Bank = "\\arquivos\\c6bank\\Fatura_2023-09-15-c6.csv";

        private readonly ConverterService _converterService;

        public TransformarLinhasemObjetoNubankTests()
        {
            _converterService = new ConverterService();
        }

        [Fact]
        public void testar_gerar_objeto_importacao_nubank_com_sucesso()
        {
            Fatura lista = _converterService.TransformarLinhasEmObjeto(
                    Environment.CurrentDirectory + CaminhoArquivoNubank,
                    DateTime.Now,
                    TipoImportacao.Nubank);

            Assert.NotNull(lista);
        }

        [Fact]
        public void testar_gerar_objeto_importacao_nubank_com_registros_errados()
        {
            var result = Assert.Throws<FormatException>(() =>
                        _converterService.TransformarLinhasEmObjeto(
                                    Environment.CurrentDirectory + CaminhoArquivoNubankErrado,
                                    DateTime.Now,
                                    TipoImportacao.Nubank));

            Assert.Contains(ErroConversao, result.Message);
        }

        [Fact]
        public void testar_gerar_objeto_importacao_nubank_com_registros_excel()
        {
            var result = Assert.Throws<FileLoadException>(() =>
                        _converterService.TransformarLinhasEmObjeto(
                                    Environment.CurrentDirectory + CaminhoArquivoNubankExcel,
                                    DateTime.Now,
                                    TipoImportacao.Nubank));

            Assert.Contains("A extensão de arquivo é inválida para a importação", result.Message);
        }

        [Fact]
        public void testar_gerar_objeto_com_tipo_importacao_errado()
        {
            var result = Assert.Throws<Exception>(() =>
                 _converterService.TransformarLinhasEmObjeto(
                    Environment.CurrentDirectory + CaminhoArquivoNubank,
                    DateTime.Now,
                    TipoImportacao.C6Bank));

            Assert.Contains("Tipo de importação inválida", result.Message);
        }

        [Fact]
        public void testar_gerar_objeto_com_arquivo_invalido()
        {
            var result = Assert.Throws<FormatException>(() =>
                        _converterService.TransformarLinhasEmObjeto(
                                    Environment.CurrentDirectory + CaminhoArquivoC6Bank,
                                    DateTime.Now,
                                    TipoImportacao.Nubank));

            Assert.Contains(ErroConversao, result.Message);
        }

        [Fact]
        public void testar_gerar_objeto_com_arquivo_inexistente()
        {
            var result = Assert.Throws<DirectoryNotFoundException>(() =>
                        _converterService.TransformarLinhasEmObjeto(
                    "C:\\TesteDiretorioFalso\\testearquivo.csv",
                    DateTime.Now,
                    TipoImportacao.Nubank));

            Assert.Contains("Diretório de arquivo não encontrado", result.Message);
        }

        [Fact]
        public void testar_gerar_objeto_com_arquivo_inexistente_e_diretorio_certo_excecao()
        {
            var result = Assert.Throws<FileNotFoundException>(() =>
                        _converterService.TransformarLinhasEmObjeto(
                    Environment.CurrentDirectory + "\\NaoExiste.csv",
                    DateTime.Now,
                    TipoImportacao.Nubank));

            Assert.Contains(ArquivoNEncontrado, result.Message);
        }
    }
}