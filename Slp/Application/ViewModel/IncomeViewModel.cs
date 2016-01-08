using System;
using System.Collections.Generic;

namespace Application.ViewModel
{
    public class IncomeViewModel
    {
        public IncomeViewModel() {
            IncomeResponses = new List<IncomeResponseViewModel>();
        }

        public Guid MemberId { get; set; }

        public string MemberName { get; set; }

        public bool IsChild { get; set; }

        public List<IncomeResponseViewModel> IncomeResponses { get; set; }
    }
}
