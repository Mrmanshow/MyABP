using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Authorization.Users
{
    public class UserAssets: AuditedEntity<int>
    {
        public int GoldCoin { set; get; }
    }
}
