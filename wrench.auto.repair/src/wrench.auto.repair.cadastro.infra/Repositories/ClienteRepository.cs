using wrench.auto.repair.cadastro.domain.Data;
using wrench.auto.repair.cadastro.domain.Entities;

namespace wrench.auto.repair.cadastro.infra.Repositories
{
    public class ClienteRepository(CadastroContext _contex) :
        Repository<Cliente>(_contex), IClienteRepository
    {

    }
}
