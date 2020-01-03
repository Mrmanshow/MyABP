using Abp.Application.Services;
using Abp.Localization;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyABP.Common.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Common
{
    public class CommonAppService: ApplicationService
    {
        private readonly IConfiguration _configuration;
        private readonly ILocalizationManager _localizationManager;
        private readonly IHttpContextAccessor _httpContext;

        public CommonAppService(IConfiguration configuration,
            ILocalizationManager localizationManager,
            IHttpContextAccessor httpContext)
        {
            this._configuration = configuration;
            this._localizationManager = localizationManager;
            this._httpContext = httpContext;
        }

        public async Task<FileUploadOutput> OnPostUploadImg([Required]IFormFile file)
        {
            string type = _httpContext.HttpContext.Request.Form["type"];
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new UserFriendlyException(_localizationManager.GetString(MyABPConsts.LocalizationSourceName, "UploadTypeError"));
            }

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
            string fileSaveDir = "";
            switch (type)
            {
                case "banner":
                    fileSaveDir = _configuration.GetValue<string>("Upload:BannerDir");
                    break;
                case "article":
                    fileSaveDir = _configuration.GetValue<string>("Upload:ArticleDir");
                    break;
                case "editor":
                    fileSaveDir = _configuration.GetValue<string>("Upload:EditorDir");
                    break;   
                default:
                    throw new UserFriendlyException(_localizationManager.GetString(MyABPConsts.LocalizationSourceName, "UploadTypeError"));
            }

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

            FileUploadOutput result = new FileUploadOutput();

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
