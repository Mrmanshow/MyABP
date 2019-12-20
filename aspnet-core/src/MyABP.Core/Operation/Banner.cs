using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Operation
{
    public class Banner: AuditedEntity<int>
    {
        public string BannerImg { set; get; }

        public string BannerLink { set; get; }

        public int BannerOrder { set; get; }

        public int Status { set; get; }

        public int Type { set; get; }

        public int LinkType { set; get; }

        public string Theme { set; get; }

        public DateTime ShowBeginDate { set; get; }

        public DateTime ShowEndDate { set; get; }


    }
}
