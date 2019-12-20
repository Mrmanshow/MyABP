using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyABP
{
    public class AuditedEntity<T> : FullAuditedEntity<int>, IFullAudited
    {
    }
}
