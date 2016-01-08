using System;

namespace Application.ViewModel
{
    public class Step3ViewModel
    {        
        public Guid AppId { get; set; }
     
        public int TotalMembers { get; set; }

        public int MemberLastFourSSN { get; set; }

        public bool NoSSN { get; set; }

    }
}
