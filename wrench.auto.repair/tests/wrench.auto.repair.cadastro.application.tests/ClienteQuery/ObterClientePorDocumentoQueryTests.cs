using wrench.auto.repair.cadastro.application.Queries;
using wrench.auto.repair.cadastro.application.tests.Fixture;

namespace wrench.auto.repair.cadastro.application.tests.ClienteQuery
{
    [Collection(nameof(ClienteCollection))]
    public class ObterClientePorDocumentoQueryTests(ClienteFixture _fixture)
    {
        [Fact(DisplayName = "Obter Cliente Por Documento Command Válido")]
        [Trait("Cadastro", "Application")]
        public void ObterClientePorDocumentCommand_ComandoValido_DevePassarNaValidacao()
        {
            // Arrange
            var documento = _fixture.GerarDocumentoValido(cpf: false);
            var obterClientePorDocumentQuery = new ObterClientePorDocumentoQuery(documento);

            // Act
            var valido = obterClientePorDocumentQuery.EhValido();

            // Assert
            Assert.True(valido);
        }

        [Fact(DisplayName = "Obter Cliente Por Documento Command Inválido")]
        [Trait("Cadastro", "Application")]
        public void ObterClientePorDocumentCommand_ComandoInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var obterClientePorDocumentQuery = new ObterClientePorDocumentoQuery("");

            // Act
            var valido = obterClientePorDocumentQuery.EhValido();

            // Assert
            Assert.False(valido);
            Assert.Contains(
                ObterClientePorDocumentoQueryValidator.DocumentoVazioError,
                obterClientePorDocumentQuery
                    .ValidationResult
                    .Errors.Select(e => e.ErrorMessage)
            );
            Assert.Contains(
                ObterClientePorDocumentoQueryValidator.DocumentoInvalidoError,
                obterClientePorDocumentQuery
                    .ValidationResult
                    .Errors.Select(e => e.ErrorMessage)
            );
        }
    }
}
