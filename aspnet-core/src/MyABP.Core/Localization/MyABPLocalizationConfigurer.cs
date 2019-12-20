using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;
using System.Reflection;

namespace MyABP.Localization
{
    public static class MyABPLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(MyABPConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(MyABPLocalizationConfigurer).GetAssembly(),
                        "MyABP.Localization.SourceFiles"
                    )
                )
            );
            Assembly ca = typeof(MyABPLocalizationConfigurer).GetAssembly();
            int a = localizationConfiguration.Sources.Count;
        }
    }
}
