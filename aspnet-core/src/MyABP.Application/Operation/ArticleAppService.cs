using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyABP.Authorization;
using MyABP.Operation.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyABP.Operation
{
    [AbpAuthorize(PermissionNames.Pages_Operation)]
    public class ArticleAppService: AsyncCrudAppService<Article, ArticleDto, int, PagedArticleResultRequestDto>, IArticleAppService
    {
        private readonly IRepository<Article> _repository;
        private readonly IConfiguration _configuration;
        private readonly ILocalizationManager _localizationManager;

        public ArticleAppService(IRepository<Article> repository,
            IConfiguration configuration,
            ILocalizationManager localizationManager)
            :base(repository)
        {
            this._repository = repository;
            this._configuration = configuration;
            this._localizationManager = localizationManager;
        }

        protected override IQueryable<Article> CreateFilteredQuery(PagedArticleResultRequestDto input)
        {
            return Repository.GetAll().WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Title.Contains(input.Keyword));
        }

        /// <summary>
        /// 获取文章类型
        /// </summary>
        /// <returns></returns>
        public IList<EnumEntity> GetArticleType()
        {
            var list = new List<EnumEntity>();

            foreach (var e in Enum.GetValues(typeof(ArticleType)))
            {
                EnumEntity m = new EnumEntity();

                m.Value = Convert.ToInt32(e);
                m.Name = e.ToString();
                list.Add(m);
            }
            return list;
        }

        public async Task<FileUploadOutputDto> OnPostUploadArticleImg(IFormFile file)
        {
            if (file.Length > 1048576)
            {

                throw new UserFriendlyException(string.Format(_localizationManager.GetString(MyABPConsts.LocalizationSourceName, "UploadExceedingSizeTip"), file.FileName, "1M"));
            }

            string FileOriginName = file.FileName;

            //生成文件的名称
            string Extension = Path.GetExtension(FileOriginName);//获取文件的源后缀
            string[] permittedExtensions = { ".png", ".jpg", "jpeg" };

            if (string.IsNullOrEmpty(Extension) || !permittedExtensions.Contains(Extension))
            {
                throw new UserFriendlyException(string.Format(_localizationManager.GetString(MyABPConsts.LocalizationSourceName, "UploadFormatIncorrectTip"), file.FileName));
            }

            //读取文件保存的根目录
            string fileSaveRootDir = _configuration.GetValue<string>("Upload:UploadDir");
            //读取办公管理文件保存的模块的根目录
            string fileSaveDir = _configuration.GetValue<string>("Upload:ArticleDir");
            //文件保存的相对文件夹(保存到wwwroot目录下)
            string absoluteFileDir = fileSaveRootDir + "/" + fileSaveDir;

            //文件保存的路径(应用的工作目录+文件夹相对路径);
            string fileSavePath = Environment.CurrentDirectory + "/wwwroot/" + absoluteFileDir;
            if (!Directory.Exists(fileSavePath))
            {
                Directory.CreateDirectory(fileSavePath);
            }


            string fileName = Guid.NewGuid().ToString() + Extension;//通过uuid和原始后缀生成新的文件名

            //最终生成的文件的相对路径（xxx/xxx/xx.xx）
            string finalyFilePath = fileSavePath + "/" + fileName;

            FileUploadOutputDto result = new FileUploadOutputDto();

            //打开上传文件的输入流
            Stream stream = file.OpenReadStream();
            //创建输入流的reader
            //var fileType = stream.GetFileType();
            //文件大小
            result.FileLength = stream.Length;
            result.FileName = fileName;
            result.FileType = Extension.Substring(1);

            //开始保存拷贝文件
            FileStream targetFileStream = new FileStream(finalyFilePath, FileMode.OpenOrCreate);
            await stream.CopyToAsync(targetFileStream);

            return result;
        }
    }
}
