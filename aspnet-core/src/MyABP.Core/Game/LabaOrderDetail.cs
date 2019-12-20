using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Game
{
    public class LabaOrderDetail : AuditedEntity<int>
    {
        public int OrderId { set; get; }

        public int RouteId { set; get; }

        public int Amount { set; get; }

        public int WinAmount { set; get; }

        public string WinContent { set; get; }

        public int Status { get; set; }
    }
}
