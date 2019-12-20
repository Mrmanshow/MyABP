using System.Threading.Tasks;
using MyABP.Configuration.Dto;

namespace MyABP.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
