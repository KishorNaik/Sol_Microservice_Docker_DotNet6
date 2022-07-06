using HashPassword;

namespace Customer.API.Business.Rule
{
    public interface IHashPasswordRule
    {
        Task<(string? salt, string? hash)>? CreatePasswordAsync(string? password);

        Task<bool?>? ValidatePassword(string? password, string? salt, string? hash);
    }

    public class HashPasswordRule : IHashPasswordRule
    {
        async Task<(string? salt, string? hash)>? IHashPasswordRule.CreatePasswordAsync(string? password)
        {
            try
            {
                var saltData = await Salt.CreateAsync(ByteRange.byte256);

                var hashData = await Hash.CreateAsync(password, saltData, ByteRange.byte256);

                return (salt: saltData, hash: hashData);
            }
            catch
            {
                throw;
            }
        }

        async Task<bool?>? IHashPasswordRule.ValidatePassword(string? password, string? salt, string? hash)
        {
            try
            {
                return await Hash.ValidateAsync(password, salt, hash, ByteRange.byte256);
            }
            catch
            {
                throw;
            }
        }
    }
}