using Bogus;
using System.Text.RegularExpressions;
using wrench.auto.repair.cadastro.domain.Entities;

namespace wrench.auto.repair.cadastro.domain.tests.Fixtures
{

    [CollectionDefinition("EnderecoCollection")]
    public class EnderecoCollection : ICollectionFixture<EnderecoFixture>
    {

    }

    public class EnderecoFixture
    {
        public Endereco CriarEndereco()
        {
            return new Faker<Endereco>("pt_BR")
                .CustomInstantiator(f => new Endereco(
                    f.Address.StreetAddress(),
                    f.Address.BuildingNumber(),
                    f.Address.SecondaryAddress(),
                    f.Address.StreetName() + " " + f.Address.CitySuffix(),
                    Regex.Replace(f.Address.ZipCode(), "\\D", ""),
                    f.Address.City(),
                    f.Address.StateAbbr(),
                    f.Address.Country()
                ));
        }
    }
}