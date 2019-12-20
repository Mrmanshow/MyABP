using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Operation.Dto
{
    /// <summary>
    /// 文件上传成功后的实体
    /// </summary>
    public class FileUploadOutputDto
    {
        /// <summary>
        /// 文件名称：文件上传的原始名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件的大小
        /// </summary>
        public long FileLength { get; set; }

        /// <summary>
        /// 文件的类型
        /// </summary>
        public string FileType { get; set; }
    }
}
