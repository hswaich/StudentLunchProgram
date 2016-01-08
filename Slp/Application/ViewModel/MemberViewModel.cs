using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Enums;
namespace Application.ViewModel
{
    public class MemberViewModel
    {
        public MemberViewModel() {
            ChildAttributes = new List<NameValueViewModel>();            
        }

        public Guid Id { get; set; }

        public Guid AppId { get; set; }

        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        public string LastName { get; set; }

        //public string DisplayName { get; set; }

        public bool IsChild { get; set; }

        public bool IsRowInEditMode { get; set; }

        public bool IsNewRow { get; set; }

        public bool IsHMR {
            get
            {
                if (ChildAttributes == null || ChildAttributes.Count == 0 || !IsChild)
                {
                    return false;
                }
                return ChildAttributes.Count(a => a.Value == (int)ChildAttributeTypeEnum.HomelessMigrantRunaway && a.SelectedItem) > 0;
            }
        }

        public bool IsFoster {
            get
            {
                if (ChildAttributes == null || ChildAttributes.Count == 0 || !IsChild)
                {
                    return false;
                }
                return ChildAttributes.Count(a=>a.Value == (int)ChildAttributeTypeEnum.FosterChild && a.SelectedItem) > 0;
            }
        }

        public string DisplayName {
            get {
                return FirstName + (MiddleInitial == null || MiddleInitial == string.Empty ? " " : " " + MiddleInitial + " ") + LastName;
            }
        }

        public DateTime CreatedDate { get; set; }

        public List<NameValueViewModel> ChildAttributes { get; set; }

       // public IncomeViewModel MemberIncome { get; set; }

        public bool IsIncomeCompleted { get; set; }

        public string TotalIncomeReported { get; set; }

        public DateTime? ApplicationCompletedDate { get; set; }
    }
}
