using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace MyABP.Authorization
{
    public class MyABPAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Games, L("Games"));
            context.CreatePermission(PermissionNames.Pages_Operation, L("Operation"));
            context.CreatePermission(PermissionNames.Pages_Shop, L("Shop"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, MyABPConsts.LocalizationSourceName);
        }
    }
}
