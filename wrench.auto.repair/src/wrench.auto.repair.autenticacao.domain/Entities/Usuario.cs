using wrench.auto.repair.core.DomainObjects;
using wrench.auto.repair.core.ValueObjects;

namespace wrench.auto.repair.autenticacao.domain.Entities
{
    public class Usuario : Entity, IAggregateRoot
    {
        private Usuario() { } // EF Core

        public Usuario(Email email, Guid perfilId, bool ativo, DateTime dateCadastro)
        {
            Email = email;
            PerfilId = perfilId;
            Ativo = ativo;
            DateCadastro = dateCadastro;

            Validar();
        }

        public Email Email { get; private set; }
        public string Senha { get; private set; }
        public Guid PerfilId { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DateCadastro { get; private set; }
        public Perfil Perfil { get; private set; }

        private void Validar()
        {
            Validacoes.ValidarSeNulo(Email, "E-mail deve ser informado");
            Validacoes.ValidarSeVerdadeiro(PerfilId == Guid.Empty, "Perfil não pode ser vazio");
        }

        public void DefinirSenha(string senha)
        {
            Validacoes.ValidarSeVazio(senha, "Senha não pode ser vazio");
            Validacoes.ValidarMinimoMaximo(senha.Length, 12, 24, "A Senha deve ter entre 12 e 24 caracteres");

            Senha = BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool ValidarSenha(string senhaInformada)
        {
            return BCrypt.Net.BCrypt.Verify(senhaInformada, Senha);
        }

        public void AlterarPerfil(Perfil perfil)
        {
            Perfil = perfil;
            PerfilId = perfil.Id;
        }
    }
}
