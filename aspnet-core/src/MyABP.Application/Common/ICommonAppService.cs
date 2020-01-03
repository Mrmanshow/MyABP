using Abp.Application.Services;
using Microsoft.AspNetCore.Http;
using MyABP.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Common
{
    public interface ICommonAppService: IApplicationService
    {
        Task<FileUploadOutput> OnPostUploadImg(FileUploadInput input);
    }
}
