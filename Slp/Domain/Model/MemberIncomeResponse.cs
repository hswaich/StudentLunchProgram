using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class MemberIncomeResponse
    {
        [Key]
        public int Id { get; set; }

        public Guid MemberId { get; set; }

        public int IncomeQuestionId { get; set; }

        public bool Response { get; set; }

        public virtual Member Member { get; set; }

        public virtual IncomeQuestion IncomeQuestion { get; set; }

        public virtual ICollection<MemberIncomeResponseDetail> IncomeResponseDetails { get; set; }
    }
}
