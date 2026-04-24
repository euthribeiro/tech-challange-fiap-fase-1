using MediatR;
using wrench.auto.repair.autenticacao.domain.Data;
using wrench.auto.repair.autenticacao.domain.Entities;
using wrench.auto.repair.autenticacao.domain.Models;
using wrench.auto.repair.autenticacao.domain.Security;
using wrench.auto.repair.autenticacao.infra.Security;
using wrench.auto.repair.core.Errors;
using wrench.auto.repair.core.ValueObjects;

namespace wrench.auto.repair.autenticacao.application.Commands
{
    public class UsuarioCommandHandler(
        IPasswordHasher _passwordHasher,
        IUsuarioRepository _usuarioRepository,
        IJwtTokenGenerator _jwtTokenGenerator
    ) : IRequestHandler<CriarUsuarioCommand, Result<Guid>>,
        IRequestHandler<AutenticarUsuarioCommand, Result<TokenAcesso>>
    {
        public async Task<Result<Guid>> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido())
                return Result<Guid>.WithFailure(TipoErroEnum.VALIDACAO, request.ObterErros());

            var usuario = await _usuarioRepository
                .ObterPorEmailAsync(Email.CriarEmail(request.Email), cancellationToken);

            if (usuario != null) return Result<Guid>.WithFailure(TipoErroEnum.VALIDACAO, "Usuário já cadastrado");

            var perfil = await _usuarioRepository.ObterPerfilPorIdAsync(request.PerfilId);

            if (perfil == null) return Result<Guid>.WithFailure(TipoErroEnum.VALIDACAO, "Perfil não encontrado");

            var novoUsuario = new Usuario(Email.CriarEmail(request.Email), perfil.Id, request.Ativo, DateTime.Now);

            if (!string.IsNullOrWhiteSpace(request.Senha))
            {
                var hash = _passwordHasher.GerarHash(request.Senha);
                novoUsuario.DefinirSenha(hash);
            }

            await _usuarioRepository.Adicionar(novoUsuario, cancellationToken);

            var salvo = await _usuarioRepository.UnitOfWork.CommitAsync();

            if (!salvo) return Result<Guid>.WithFailure(TipoErroEnum.INESPERADO, "Não foi possível cadastrar o usuário, por favor tente novamente.");

            return Result<Guid>.Created(novoUsuario.Id);
        }

        public async Task<Result<TokenAcesso>> Handle(AutenticarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido())
                return Result<TokenAcesso>.WithFailure(TipoErroEnum.VALIDACAO, request.ObterErros());

            var usuario = await _usuarioRepository
                .ObterPorEmailAsync(Email.CriarEmail(request.Email), cancellationToken);

            if (usuario == null) return Result<TokenAcesso>.WithFailure(TipoErroEnum.NAO_AUTORIZADO, "Usuário não autorizado.");

            var senhaValida = _passwordHasher.ValidarSenha(request.Senha, usuario.Senha);

            if (!senhaValida) return Result<TokenAcesso>.WithFailure(TipoErroEnum.NAO_AUTORIZADO, "Usuário não autorizado.");

            var tokenAccess = _jwtTokenGenerator.GerarToken(usuario);

            return Result<TokenAcesso>.Ok(tokenAccess);
        }
    }
}
