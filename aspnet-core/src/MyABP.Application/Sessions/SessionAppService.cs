using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Auditing;
using MyABP.Sessions.Dto;

namespace MyABP.Sessions
{
    public class SessionAppService : MyABPAppServiceBase, ISessionAppService
    {
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>()
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
            }

            if (AbpSession.UserId.HasValue)
            {
                output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
            }

            output.Application.Features.Add("SignalR", true);
            output.Application.Features.Add("SignalR.AspNetCore", true);

            return output;
        }
    }
}
