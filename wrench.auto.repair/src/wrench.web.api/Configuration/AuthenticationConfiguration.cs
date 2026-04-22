namespace wrench.web.api.Configuration
{
    public static class AuthenticationConfiguration
    {
        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            //AuthenticationBuilder authenticationBuilder = services.AddAuthentication(authenticationOptions =>
            //{
            //    authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //});

            //authenticationBuilder.ConfigureJwtBearer();
        }

        //private static void ConfigureJwtBearer(this AuthenticationBuilder authenticationBuilder)
        //{
        //    // TODO: Trocar Para Certificado
        //    byte[] key = Encoding.ASCII.GetBytes("01764DB54B384ABDBE65BD312D59E329");
        //    authenticationBuilder.AddJwtBearer(jwtBearerOptions =>
        //    {
        //        jwtBearerOptions.RequireHttpsMetadata = false;
        //        jwtBearerOptions.SaveToken = true;
        //        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false
        //        };
        //    });
        //}
    }
}
