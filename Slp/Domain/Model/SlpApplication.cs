using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class SlpApplication
    {
        public Guid Id { get; set; }

        public int? AssistanceProgramId { get; set; }

        public string AssistanceProgramCaseNumber { get; set; }
        
        public int? TotalMembers { get; set; }

        public int? MemberLastFourSSN { get; set; }

        public bool NoSSN { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string StreetAddress { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string AptNo { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string City { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string State { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string Zip { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Email { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string AdultFilledByName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string AdultFilledBySignature { get; set; }
        
        public int? EthnicityId { get; set; }


        public DateTime CreateDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public string Steps { get; set; }

        //public virtual ICollection<Child> Children { get; set; }

        public virtual ICollection<Member> Members { get; set; }

        public virtual ICollection<SlpApplicationRace> SlpApplicationRaces { get; set; }

        public virtual AssistanceProgram AssistanceProgram { get; set; }
        
        public virtual Ethnicity Ethnicity { get; set; }
    }
}
