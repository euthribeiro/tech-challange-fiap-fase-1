using wrench.auto.repair.core.DomainObjects;
using wrench.auto.repair.core.ValueObjects;

namespace wrench.auto.repair.core.tests.ValueObjects
{
    public class CpfCnpjTests
    {
        [Fact(DisplayName = "Criar Documento Vazio Deve Retornar Exception")]
        [Trait("Core", "ValueObjects")]
        public void CriarCpfCnpj_DocumentoVazio_DeveRetornarException()
        {
            // Arrange
            var documento = "";

            // Act
            Assert.Throws<DomainException>(() => new CpfCnpj(documento));
        }

        [Fact(DisplayName = "Criar Documento Invalido Deve Retornar Exception")]
        [Trait("Core", "ValueObjects")]
        public void CriarCpfCnpj_DocumentoInvalido_DeveRetornarException()
        {
            // Arrange
            var documento = "1234";

            // Act && Assert
            Assert.Throws<DomainException>(() => new CpfCnpj(documento));
        }

        [Fact(DisplayName = "Criar Cpf Inválido Deve Retornar Exception")]
        [Trait("Core", "ValueObjects")]
        public void CriarCpfCnpj_CpfInvalido_DeveRetornarException()
        {
            // Arrange
            var documentoInvalido = "74580776039";

            // Act & Assert
            Assert.Throws<DomainException>(() => new CpfCnpj(documentoInvalido));
        }

        [Fact(DisplayName = "Criar Cpf Valido Sem Pontuacao Deve Ser Valido")]
        [Trait("Core", "ValueObjects")]
        public void CriarCpfCnpj_CpfValidoSemPontuacao_DeveSerValido()
        {
            // Arrange
            var documento = "74580776038";

            // Act
            var cpf = new CpfCnpj(documento);

            // Assert
            Assert.True(cpf.EhValido());
            Assert.Equal(TipoDocumentoEnum.Cpf, cpf.TipoDocumento);
        }

        [Fact(DisplayName = "Criar Cpf Valido Com Pontuacao Deve Ser Valido")]
        [Trait("Core", "ValueObjects")]
        public void CriarCpfCnpj_CpfValidoComPontuacao_DeveSerValido()
        {
            // Arrange
            var documento = "745.807.760-38";

            // Act
            var cpf = new CpfCnpj(documento);

            // Assert
            Assert.True(cpf.EhValido());
            Assert.Equal(TipoDocumentoEnum.Cpf, cpf.TipoDocumento);
        }

        [Fact(DisplayName = "Criar Cpf Valido Com Pontuacao Deve Retornar Sem Pontuacao")]
        [Trait("Core", "ValueObjects")]
        public void CriarCpfCnpj_CpfValidoComPontuacao_DeveRetornarSemPontuacao()
        {
            // Arrange
            var documento = "745.807.760-38";
            var documentoSemPontuacao = "74580776038";

            // Act
            var cpf = new CpfCnpj(documento);

            // Assert
            Assert.Equal(cpf.Numeracao, documentoSemPontuacao);
            Assert.Equal(TipoDocumentoEnum.Cpf, cpf.TipoDocumento);
        }

        [Fact(DisplayName = "Criar Cpf Valido Sem Pontuacao Deve Retornar Cpf Formatado")]
        [Trait("Core", "ValueObjects")]
        public void CriarCpfCnpj_CpfValidoSemPontuacao_DeveRetornarCpfFormatado()
        {
            // Arrange
            var documentoSemPontuacao = "74580776038";
            var documento = "745.807.760-38";

            // Act
            var cpf = new CpfCnpj(documentoSemPontuacao);

            // Assert
            Assert.Equal(cpf.ObterDocumentoFormatado(), documento);
            Assert.Equal(TipoDocumentoEnum.Cpf, cpf.TipoDocumento);
        }

        [Fact(DisplayName = "Criar Cnpj Inválido Deve Retornar Exception")]
        [Trait("Core", "ValueObjects")]
        public void CriarCpfCnpj_CnpjInvalido_DeveRetornarException()
        {
            // Arrange
            var documentoInvalido = "87332913000120";

            // Act & Assert
            Assert.Throws<DomainException>(() => new CpfCnpj(documentoInvalido));
        }

        [Fact(DisplayName = "Criar Cnpj Valido Sem Pontuacao Deve Ser Valido")]
        [Trait("Core", "ValueObjects")]
        public void CriarCpfCnpj_CnpjValidoSemPontuacao_DeveSerValido()
        {
            // Arrange
            var documento = "56961187000144";

            // Act
            var cnpj = new CpfCnpj(documento);

            // Assert
            Assert.True(cnpj.EhValido());
            Assert.Equal(TipoDocumentoEnum.Cnpj, cnpj.TipoDocumento);
        }

        [Fact(DisplayName = "Criar Cnpj Valido Com Pontuacao Deve Ser Valido")]
        [Trait("Core", "ValueObjects")]
        public void CriarCpfCnpj_CnpjValidoComPontuacao_DeveSerValido()
        {
            // Arrange
            var documento = "56.961.187/0001-44";

            // Act
            var cnpj = new CpfCnpj(documento);

            // Assert
            Assert.True(cnpj.EhValido());
            Assert.Equal(TipoDocumentoEnum.Cnpj, cnpj.TipoDocumento);
        }

        [Fact(DisplayName = "Criar Cnpj Valido Com Pontuacao Deve Retornar Sem Pontuacao")]
        [Trait("Core", "ValueObjects")]
        public void CriarCpfCnpj_CnpjValidoComPontuacao_DeveRetornarSemPontuacao()
        {
            // Arrange
            var documento = "56.961.187/0001-44";
            var documentoSemPontuacao = "56961187000144";

            // Act
            var cnpj = new CpfCnpj(documento);

            // Assert
            Assert.Equal(cnpj.Numeracao, documentoSemPontuacao);
            Assert.Equal(TipoDocumentoEnum.Cnpj, cnpj.TipoDocumento);
        }

        [Fact(DisplayName = "Criar Cnpj Valido Sem Pontuacao Deve Retornar Cpf Formatado")]
        [Trait("Core", "ValueObjects")]
        public void CriarCpfCnpj_CnpjValidoSemPontuacao_DeveRetornarCpfFormatado()
        {
            // Arrange
            var documentoSemPontuacao = "56961187000144";
            var documento = "56.961.187/0001-44";

            // Act
            var cnpj = new CpfCnpj(documentoSemPontuacao);

            // Assert
            Assert.Equal(cnpj.ObterDocumentoFormatado(), documento);
            Assert.Equal(TipoDocumentoEnum.Cnpj, cnpj.TipoDocumento);
        }
    }
}
