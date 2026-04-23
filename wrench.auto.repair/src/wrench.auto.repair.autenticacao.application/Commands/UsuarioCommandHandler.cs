using MediatR;
using wrench.auto.repair.autenticacao.domain.Data;
using wrench.auto.repair.autenticacao.domain.Entities;
using wrench.auto.repair.core.Errors;
using wrench.auto.repair.core.ValueObjects;

namespace wrench.auto.repair.autenticacao.application.Commands
{
    public class UsuarioCommandHandler
        : IRequestHandler<CriarUsuarioCommand, Result>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Result> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido())
                return Result.WithFailure(TipoErroEnum.VALIDACAO, request.ObterErros());

            var usuario = await _usuarioRepository
                .ObterPorEmailAsync(Email.CriarEmail(request.Email), cancellationToken);

            if (usuario != null) return Result.WithFailure(TipoErroEnum.VALIDACAO, "Usuário já cadastrado");

            var perfil = await _usuarioRepository.ObterPerfilPorIdAsync(request.PerfilId);

            if (perfil == null) return Result.WithFailure(TipoErroEnum.VALIDACAO, "Perfil não encontrado");

            var novoUsuario = new Usuario(Email.CriarEmail(request.Email), perfil.Id, request.Ativo, DateTime.Now);

            if (!string.IsNullOrWhiteSpace(request.Senha))
                novoUsuario.DefinirSenha(request.Senha);

            await _usuarioRepository.Adicionar(novoUsuario, cancellationToken);

            var salvo = await _usuarioRepository.UnitOfWork.CommitAsync();

            if (!salvo) return Result.WithFailure(TipoErroEnum.INESPERADO, "Não foi possível cadastrar o usuário, por favor tente novamente.");

            return Result.Created();
        }
    }
}
