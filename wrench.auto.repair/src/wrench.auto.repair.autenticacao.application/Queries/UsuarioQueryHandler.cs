using AutoMapper;
using MediatR;
using wrench.auto.repair.autenticacao.application.Queries.ViewModels;
using wrench.auto.repair.autenticacao.domain.Data;
using wrench.auto.repair.autenticacao.domain.Entities;
using wrench.auto.repair.core.Errors;

namespace wrench.auto.repair.autenticacao.application.Queries
{
    public class UsuarioQueryHandler(
        IMapper _mapper,
        IUsuarioRepository _usuarioRepository
    ) : IRequestHandler<ObterTodosPerfisQuery, Result<IEnumerable<PerfilViewModel>>>,
        IRequestHandler<ObterTodosUsuariosQuery, Result<IEnumerable<UsuarioViewModel>>>,
        IRequestHandler<ObterUsuarioPorIdQuery, Result<UsuarioViewModel>>
    {
        public async Task<Result<IEnumerable<PerfilViewModel>>> Handle(ObterTodosPerfisQuery request, CancellationToken cancellationToken)
        {
            var perfis = await _usuarioRepository.ObterTodosPerfisAsync(cancellationToken);

            if (perfis == null) return Result<IEnumerable<PerfilViewModel>>.WithFailure(TipoErroEnum.NAO_ENCONTRADO, "Nenhum perfil cadastrado.");

            var perfilViewModels = _mapper.Map<IEnumerable<Perfil>, IEnumerable<PerfilViewModel>>(perfis);

            return Result<IEnumerable<PerfilViewModel>>.Ok(perfilViewModels);
        }

        public async Task<Result<IEnumerable<UsuarioViewModel>>> Handle(ObterTodosUsuariosQuery request, CancellationToken cancellationToken)
        {
            // TODO: Aplicar testes

            var usuarios = await _usuarioRepository.ObterTodosAsync(cancellationToken);

            if (usuarios == null)
                return Result<IEnumerable<UsuarioViewModel>>.WithFailure(TipoErroEnum.NAO_ENCONTRADO, "Nenhum usuário cadastrado.");

            var usuarioViewModels = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(usuarios);

            return Result<IEnumerable<UsuarioViewModel>>.Ok(usuarioViewModels);
        }

        public async Task<Result<UsuarioViewModel>> Handle(ObterUsuarioPorIdQuery request, CancellationToken cancellationToken)
        {
            // TODO: Aplicar testes

            var usuario = await _usuarioRepository.ObterPorIdAsync(request.Id, cancellationToken);

            if (usuario == null)
                return Result<UsuarioViewModel>.WithFailure(TipoErroEnum.NAO_ENCONTRADO, "Usuário não encontrado");

            var usuarioViewModel = _mapper.Map<Usuario, UsuarioViewModel>(usuario);

            return Result<UsuarioViewModel>.Ok(usuarioViewModel);
        }
    }
}
