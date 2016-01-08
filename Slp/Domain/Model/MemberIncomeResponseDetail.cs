using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class MemberIncomeResponseDetail
    {
        [Key]
        public int Id { get; set; }

        public int MemberIncomeResponseId { get; set; }

        public Decimal Amount { get; set; }

        public int FrequencyId { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Frequency Frequency { get; set; }

        public virtual MemberIncomeResponse MemberIncomeResponse { get; set; }
    }
}
