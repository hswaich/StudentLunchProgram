using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class MemberChildAttribute
    {
        [Key]
        [Column(Order = 10)]
        public Guid MemberId { get; set; }

        [Key]
        [Column(Order = 20)]
        public int AttributeTypeId { get; set; }

        public bool IsSelected { get; set; }

        public virtual Member Member { get; set; }

        [ForeignKey("AttributeTypeId")]
        public virtual ChildAttributeType Attribute { get; set; }
    }
}
