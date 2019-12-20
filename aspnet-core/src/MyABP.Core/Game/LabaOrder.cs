using Abp.Domain.Entities;
using MyABP.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyABP.Game
{
    public class LabaOrder : AuditedEntity<int>
    {
        public int Amount { set; get; }

        public int WinAmount { set; get; }

        public int Count { set; get; }

        public int Status { set; get; }

        public string Position { set; get; }

        [ForeignKey("CreatorUserId")]
        public virtual User Users { get; set; }
    }
}
