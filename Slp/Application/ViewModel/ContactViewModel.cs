using System;
using System.Collections.Generic;

namespace Application.ViewModel
{
    public class ContactViewModel
    {
        public ContactViewModel() {
            Races = new List<NameValueViewModel>();
            StepsRequired = new List<int>();
        }
        public Guid AppId { get; set; }
        public string StreetAddress { get; set; }

        public string AptNo { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string AdultFilledByName { get; set; }

        public int? EthnicityId { get; set; }

        public List<NameValueViewModel> Races { get; set; }

        public List<int> StepsRequired { get; set; }
    }
}
