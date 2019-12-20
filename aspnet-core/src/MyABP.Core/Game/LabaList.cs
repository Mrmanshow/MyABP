using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Game
{
    public class LabaList: AuditedEntity<int>
    {
        public int X { set; get; }

        public int Y { set; get; }

        public string Content { set; get; }
     }
}
