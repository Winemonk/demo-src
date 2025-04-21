using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace Hearth.IdentityServer
{
    public class Config
    {
        /// <summary>
        /// 定义API资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1","测试API"),
                new ApiResource("api2","测试API2")
            };
        }

        /// <summary>
        /// 定义客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client{
                    ClientId="client",
                    //授权方式为客户端验证，类型可参考GrantTypes枚举
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    //秘钥
                    ClientSecrets=
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes=new []{ "api1" }
                 },
                new Client{
                    ClientId="client2",
                    //授权方式为用户密码验证，类型可参考GrantTypes枚举
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    //秘钥
                    ClientSecrets=
                    {
                        new Secret("secret2".Sha256())
                    },
                    AllowedScopes=new []{ "api2", IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile }
                 }
            };
        }

        /// <summary>
        /// 定义身份资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
             };
        }
    }
}
