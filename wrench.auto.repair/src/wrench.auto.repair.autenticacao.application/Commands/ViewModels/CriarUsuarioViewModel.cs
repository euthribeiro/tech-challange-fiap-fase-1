namespace wrench.auto.repair.autenticacao.application.Commands.ViewModels
{
    public class CriarUsuarioViewModel
    {
        /// <summary>
        /// E-mail do usuário
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// ID do Perfil do Usuário conforme Lista de Perfis
        /// </summary>
        public Guid PerfilId { get; set; }

        /// <summary>
        /// Senha para acesso a API
        /// </summary>
        public string? Senha { get; set; }

        /// <summary>
        /// Ativo ou Inativo
        /// </summary>
        public bool Ativo { get; set; }
    }
}
