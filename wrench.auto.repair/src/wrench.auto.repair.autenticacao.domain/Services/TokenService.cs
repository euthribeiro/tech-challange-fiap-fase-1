namespace wrench.auto.repair.autenticacao.domain.Services
{
    public class TokenService : ITokenService
    {
        private const int HOURS_TO_EXPIRE_TOKEN = 2;

        //public TokenAcesso GerarToken(Usuario usuario)
        //{
        //    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        //    byte[] key = Encoding.ASCII.GetBytes("");
        //    SecurityTokenDescriptor tokenDescriptor = DescribeTokenSpecification(usuario, key);
        //    SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
        //    string token = tokenHandler.WriteToken(securityToken);
        //    return new TokenAcesso(token, usuario.Email.Endereco, usuario.Perfil.Nome);
        //}

        //private SecurityTokenDescriptor DescribeTokenSpecification(Usuario usuario, byte[] key)
        //{
        //    return new SecurityTokenDescriptor
        //    {
        //        Subject = ConfigureClaimIdentity(usuario),
        //        Expires = DateTime.UtcNow.AddHours(HOURS_TO_EXPIRE_TOKEN),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //}

        //private ClaimsIdentity ConfigureClaimIdentity(Usuario usuario)
        //{
        //    return new ClaimsIdentity(
        //    [
        //        new(ClaimTypes.Name, usuario.Email.Endereco.ToString()),
        //        new(ClaimTypes.Role, usuario.Perfil.Nome.ToString()),
        //    ]);
        //}
    }
}
