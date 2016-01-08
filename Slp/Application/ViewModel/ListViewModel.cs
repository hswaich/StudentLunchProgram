using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel
{
    public class ListViewModel
    {
        public ListViewModel() {
            TableValues = new List<NameValueViewModel>();
        }

        public int ListId { get; set; }

        public List<NameValueViewModel> TableValues { get; set; }

    }
}
