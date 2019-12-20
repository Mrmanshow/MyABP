using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Game
{
    public class LabaMultiple: AuditedEntity<int>
    {
        public int Multiple { set; get; }

        public string Content { set; get; }

        public int Amount { set; get; }
    }
}
