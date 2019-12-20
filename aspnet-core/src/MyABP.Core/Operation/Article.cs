using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyABP.Operation
{
    public class Article: AuditedEntity<int>
    {
        [MaxLength(50)]
        public string Title { set; get; }

        [MaxLength(200)]
        public string Img { set; get; }

        public int Sort { set; get; }

        public string Content { set; get; }

        [Required]
        public int Type { set; get; }

        [Required]
        public int Status { set; get; }

    }
}
