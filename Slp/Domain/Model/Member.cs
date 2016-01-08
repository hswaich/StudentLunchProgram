using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Domain.Model
{
    public class Member
    {
        public Guid Id { get; set; }

        public Guid SlpApplicationId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(1)]
        public string MiddleInitial { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string LastName { get; set; }

        public bool IsChild { get; set; }        

        public DateTime CreatedDate { get; set; }

        public virtual SlpApplication SlpApplication { get; set; }

        public virtual ICollection<MemberChildAttribute> MemberChildAttributes { get; set; }

        public virtual ICollection<MemberIncomeResponse> MemberIncomeResponses { get; set; }

    }
}
