using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyABP.Roles.Dto;
using MyABP.Users.Dto;

namespace MyABP.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
