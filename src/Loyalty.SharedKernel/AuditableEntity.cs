using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyalty.SharedKernel
{
    // This project usually will contain enterprise-wide abstractions and base constructs,
    // in order to standardize some concerns across different components.
    // It will be deployed as Nuget package.
    // This is just a sample for auditing information. More details in the following link
    // https://fiseni.com/posts/how-to-implement-auditing-on-your-entities/
    public class AuditableEntity
    {
        public DateTime? AuditCreatedTime { get; set; }
        public string AuditCreatedByUserId { get; set; }
        public string AuditCreatedByUsername { get; set; }
        public DateTime? AuditModifiedTime { get; set; }
        public string AuditModifiedByUserId { get; set; }
        public string AuditModifiedByUsername { get; set; }
    }
}
