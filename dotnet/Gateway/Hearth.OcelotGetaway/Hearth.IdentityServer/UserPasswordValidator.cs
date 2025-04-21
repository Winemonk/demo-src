using Duende.IdentityModel;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using System.Security.Claims;

namespace Hearth.IdentityServer
{
    public class UserPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (!string.IsNullOrEmpty(context.UserName))
            {
                context.Result = new GrantValidationResult(subject: context.UserName,
                               authenticationMethod: GrantType.ResourceOwnerPassword,
                               claims: GetUserClaims(context.UserName));
            }
            else
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "账号或密码错误");
            return Task.CompletedTask;
        }

        private List<Claim> GetUserClaims(string userName)
        {
            List<Claim> list = new List<Claim>();
            list.Add(new Claim(JwtClaimTypes.Name, userName));
            list.Add(new Claim(JwtClaimTypes.Id, Guid.NewGuid().ToString()));
            return list;
        }
    }
}
