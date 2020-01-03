using Abp.Dependency;
using Abp.Localization;
using Abp.Modules;
using Abp.Quartz;
using Abp.Reflection.Extensions;
using Abp.Runtime.Caching.Redis;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyABP.Authorization.Roles;
using MyABP.Authorization.Users;
using MyABP.Configuration;
using MyABP.Localization;
using MyABP.MultiTenancy;
using MyABP.Redis;
using MyABP.Timing;
using ServiceStack.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

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

            Configuration.Caching.UseRedis(options =>
            {
                options.ConnectionString = _appConfiguration["RedisCache:ConnectionString"];
                options.DatabaseId = _appConfiguration.GetValue<int>("RedisCache:DatabaseId");
            });

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
            IocManager.Register(typeof(IRedisManager<>), typeof(RedisManager<>), DependencyLifeStyle.Transient);

            string[] redisReadOnlyHosts = { _appConfiguration["ServiceStackRedis:ConnectionString"] };
            IocManager.IocContainer.Register(Component.For<IRedisClientsManager>()
                .Instance(new PooledRedisClientManager(int.Parse(_appConfiguration["ServiceStackRedis:DatabaseId"]), redisReadOnlyHosts))
                .LifestyleSingleton());

            var redisConnectOptions = ConfigurationOptions.Parse(_appConfiguration["StackExchangeRedis:ConnectionString"]);
            redisConnectOptions.DefaultDatabase = int.Parse(_appConfiguration["StackExchangeRedis:DatabaseId"]);
            IocManager.IocContainer.Register(Component.For<IConnectionMultiplexer>()
              .Instance(ConnectionMultiplexer.Connect(redisConnectOptions))
              .LifestyleSingleton());

            IocManager.RegisterAssemblyByConvention(typeof(MyABPCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
