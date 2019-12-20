using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Localization
{
    public class ChineseRequestCultureProvider : RequestCultureProvider
    {
        private readonly Dictionary<string, string> _culturesMaps;
        private readonly Dictionary<string, string> _uiCulturesMaps;

        public ChineseRequestCultureProvider(Dictionary<string, string> culturesMaps, Dictionary<string, string> uiCulturesMaps)
        {
            _culturesMaps = culturesMaps;
            _uiCulturesMaps = uiCulturesMaps;
        }

        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var chineseCultures = new List<StringSegment>();
            var chineseUiCultures = new List<StringSegment>();

            //var requestLocalizationOptionsProvider = httpContext.RequestServices.GetRequiredService<RequestLocalizationOptions>();
            //foreach (var provider in requestLocalizationOptionsProvider.GetLocalizationOptions().RequestCultureProviders)
            //{
            //    if (provider == this)
            //    {
            //        continue;
            //    }

            //    var providerCultureResult = await provider.DetermineProviderCultureResult(httpContext);
            //    if (providerCultureResult == null)
            //    {
            //        continue;
            //    }
            //    var cultures = providerCultureResult.Cultures;
            //    var uiCultures = providerCultureResult.UICultures;

            //    chineseCultures.AddRange(from cultureName in cultures
            //                             where cultureName != null && _culturesMaps.ContainsKey(cultureName.Value)
            //                             select new StringSegment(_culturesMaps[cultureName.Value]));

            //    chineseUiCultures.AddRange(from cultureName in uiCultures
            //                               where cultureName != null && _uiCulturesMaps.ContainsKey(cultureName.Value)
            //                               select new StringSegment(_uiCulturesMaps[cultureName.Value]));
            //}

            if (!chineseCultures.Any() || !chineseUiCultures.Any())
            {
                return await NullProviderCultureResult;
            }

            return new ProviderCultureResult(chineseCultures, chineseUiCultures);
        }
    }
}
