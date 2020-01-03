using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Common.Dto
{
    public class FileUploadInput
    {
        public IFormFile File { set; get; }

        public string Type { set; get; }
    }
}
