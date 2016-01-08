using System;
using System.Collections.Generic;

namespace Application.ViewModel
{
    public class AppViewModel
    {
        public AppViewModel() {
            Children = new List<MemberViewModel>();
            Adults = new List<MemberViewModel>();
            AssistancePrograms = new List<NameValueViewModel>();
            Ethnicities = new List<NameValueViewModel>();
            Frequencies = new List<NameValueViewModel>();
            Races = new List<NameValueViewModel>();
            ModalMemberIncome = new IncomeViewModel();
            AppMembers = new List<AppMembersViewModel>();
        }

        public Guid Id { get; set; }

        public int? AssistanceProgramId { get; set; }

        public string AssistanceProgramCaseNumber { get; set; }

        public bool AssistanceProgramIsRequired { get; set; }

        public int AssistanceProgramNoneId { get; set; }

        public int? TotalMembers { get; set; }

        public int? MemberLastFourSSN { get; set; }

        public bool NoSSN { get; set; }

        public string StreetAddress { get; set; }

        public string AptNo { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string AdultFilledByName { get; set; }

        public string AdultFilledBySignature { get; set; }

        public int? EthnicityId { get; set; }
        
        public bool IsComplete { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public IList<MemberViewModel> Children { get; set; }
        
        public IList<MemberViewModel> Adults { get; set; }

        public IList<NameValueViewModel> AssistancePrograms { get; set; }

        public IList<NameValueViewModel> Ethnicities { get; set; }

        public IList<NameValueViewModel> Frequencies { get; set; }

        public IList<NameValueViewModel> Races { get; set; }

       // public MemberViewModel NewMember { get; set; }

        public IncomeViewModel ModalMemberIncome { get; set; }

        public List<AppMembersViewModel> AppMembers { get; set; }        

    }
}
