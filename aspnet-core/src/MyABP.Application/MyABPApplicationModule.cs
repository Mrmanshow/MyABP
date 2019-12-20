using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MyABP.Authorization;
using MyABP.Game;
using MyABP.Game.Laba.Dto;

namespace MyABP
{
    [DependsOn(
        typeof(MyABPCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MyABPApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MyABPAuthorizationProvider>();
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<LabaOrder, LabaOrderDto>()
                      .ForMember(u => u.UserName, options => options.MapFrom(input => input.Users.UserName));
            });
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MyABPApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
