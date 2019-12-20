using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace MyABP.Controllers
{
    public abstract class MyABPControllerBase: AbpController
    {
        protected MyABPControllerBase()
        {
            LocalizationSourceName = MyABPConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
