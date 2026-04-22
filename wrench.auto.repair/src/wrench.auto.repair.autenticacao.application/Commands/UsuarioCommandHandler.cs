using MediatR;
using wrench.auto.repair.autenticacao.domain.Data;
using wrench.auto.repair.core.Errors;

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
                return Result.WithFailure(request.ObterErros());

            return Result.WithSuccess();
        }
    }
}
