using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Game
{
    public class LabaWinRoute : AuditedEntity<int>
    {
        public int X1 { set; get; }
        public int Y1 { set; get; }
        public int X2 { set; get; }
        public int Y2 { set; get; }
        public int X3 { set; get; }
        public int Y3 { set; get; }
        public int X4 { set; get; }
        public int Y4 { set; get; }
        public int X5 { set; get; }
        public int Y5 { set; get; }
        public int Status { get; set; }
        public int Sequence { set; get; }
    }
}
