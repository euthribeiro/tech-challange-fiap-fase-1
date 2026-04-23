namespace wrench.auto.repair.autenticacao.application.Models.ViewModels
{
    public class CriarUsuarioViewModel
    {
        public string Email { get; set; }
        public Guid PerfilId { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }
}
