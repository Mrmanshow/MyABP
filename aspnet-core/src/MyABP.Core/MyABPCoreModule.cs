using Abp.Localization;
using Abp.Modules;
using Abp.Quartz;
using Abp.Reflection.Extensions;
using Abp.Runtime.Caching.Redis;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyABP.Authorization.Roles;
using MyABP.Authorization.Users;
using MyABP.Configuration;
using MyABP.Localization;
using MyABP.MultiTenancy;
using MyABP.Timing;
using System;

namespace MyABP
{
    [DependsOn(typeof(AbpZeroCoreModule),
               typeof(AbpRedisCacheModule),
               typeof(AbpQuartzModule))]
    public class MyABPCoreModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public MyABPCoreModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
        }

        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            Configuration.Caching.ConfigureAll(cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(1);
            });

            //Configuration.Caching.UseRedis(options =>
            //{
            //    options.ConnectionString = _appConfiguration["RedisCache:ConnectionString"];
            //    options.DatabaseId = _appConfiguration.GetValue<int>("RedisCache:DatabaseId");
            //});

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            MyABPLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = MyABPConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MyABPCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
