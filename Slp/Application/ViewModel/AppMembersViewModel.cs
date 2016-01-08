using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel
{
    public class AppMembersViewModel
    {
        public Guid AppId { get; set; }
        public String CompletedDate { get; set; }

        public DateTime CompletedDateTime { get; set; }

        public string Children { get; set; }

        public string Adults { get; set; }
    }    
}
