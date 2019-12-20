using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using MyABP.Authorization;
using MyABP.Operation.Dto;
using MyABP.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MyABP.Configuration;
using Abp.Localization;

namespace MyABP.Operation
{
    [AbpAuthorize(PermissionNames.Pages_Operation)]
    public class BannerAppService: AsyncCrudAppService<Banner, BannerDto, int, PagedAndSortedBannerResultRequestDto, BannerInput>, IBannerAppService
    {
        private readonly IRepository<Banner> _bannerRepository;
        private readonly IConfiguration _configuration;
        private readonly ILocalizationManager _localizationManager;

        public BannerAppService(IRepository<Banner> bannerRepository,
            IConfiguration configuration,
            ILocalizationManager localizationManager)
            :base(bannerRepository)
        {
            this._bannerRepository = bannerRepository;
            this._configuration = configuration;
            this._localizationManager = localizationManager;

            CreatePermissionName = "Pages.Operation";
        }

        public override async Task<BannerDto> Create(BannerInput input)
        {
            var banner = ObjectMapper.Map<Banner>(input);
            //banner.BannerImg = "";
            banner = await this._bannerRepository.InsertAsync(banner);

            return MapToEntityDto(banner);
        }

        public override async Task<BannerDto> Update(BannerInput input)
        {
            CheckUpdatePermission();

            var banner = await _bannerRepository.GetAsync(input.Id);
            MapToEntity(input, banner);

            await this._bannerRepository.UpdateAsync(banner);

            return await Get(input);
        }

        protected override IQueryable<Banner> ApplySorting(IQueryable<Banner> query, PagedAndSortedBannerResultRequestDto input)
        {
            return query.OrderByDescending(r => r.BannerOrder).OrderByDescending(r => r.CreationTime);
        }

        protected override IQueryable<Banner> CreateFilteredQuery(PagedAndSortedBannerResultRequestDto input)
        {
            return Repository.GetAll().WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Theme.Contains(input.Keyword))
                .WhereIf(input.From.HasValue, x => x.CreationTime >= input.From.Value)
                .WhereIf(input.To.HasValue, x => x.CreationTime < input.To.Value.AddDays(1));
        }

        /// <summary>
        /// 获取轮播图类型列表
        /// </summary>
        /// <returns></returns>
        public IList<EnumEntity> GetBannerType()
        {
            var list = new List<EnumEntity>();

            foreach (var e in Enum.GetValues(typeof(BannerType)))
            {
                EnumEntity m = new EnumEntity();

                m.Value = Convert.ToInt32(e);
                m.Name = e.ToString();
                list.Add(m);
            }
            return list;
        }

        public async Task<FileUploadOutputDto> OnPostUploadBannerImg(IFormFile file)
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
            string fileSaveDir = _configuration.GetValue<string>("Upload:BannerDir");
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
