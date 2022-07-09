using AuthJwt.Generates;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Customer.API.Business.Rule
{
    public interface IJwtGeneratorRule
    {
        Task<string> GenerateJwtTokenAsync(Guid? customerId);
    }

    public class JwtTokenGeneratorRule : IJwtGeneratorRule
    {
        private readonly IGenerateJwtToken generateJwtToken;
        private readonly IOptions<AppSettingModel> options;

        public JwtTokenGeneratorRule(IGenerateJwtToken generateJwtToken, IOptions<AppSettingModel> options)
        {
            this.generateJwtToken = generateJwtToken;
            this.options = options;
        }

        async Task<string> IJwtGeneratorRule.GenerateJwtTokenAsync(Guid? customerId)
        {
            try
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, customerId?.ToString()!));

                return await this.generateJwtToken.CreateJwtTokenAsync(options?.Value.SecretKey, claims?.ToArray(), DateTime.Now.AddDays(1));
            }
            catch
            {
                throw;
            }
        }
    }
}