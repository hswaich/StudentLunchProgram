using System;

namespace Application.ViewModel
{
    class IncomeSummaryViewModel
    {
        public Guid MemberId { get; set; }

        public string FrequencyName { get; set; }

        public decimal Amount { get; set; }

        public int FrequencyId { get; set; }
    }
}
