using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Model;
using Application.ViewModel;
using Application.Enums;
using System.Globalization;

namespace Application.Orchestrators
{
    public class AppOrchestrator
    {       
        public AppOrchestrator()
        {
        }
       
        public MemberViewModel GetNewMember(Guid appId, bool IsChild)
        {
            MemberViewModel mvm = new MemberViewModel
            {
                AppId = appId,
                IsChild = IsChild,
                IsRowInEditMode = true,
                IsNewRow = true,
                FirstName = "",
                MiddleInitial = "",
                LastName = ""
            };
            if (IsChild)
            {
                using (SlpContext context = new SlpContext())
                {
                    mvm.ChildAttributes = context.ChildAttributeTypes.Where(a => a.Visible == true).Select(a => new NameValueViewModel { Name = a.Name, Value = a.Id }).OrderBy(a => a.Value).ToList();
                }
            }
            return mvm;
        }

        public AppViewModel GetAppViewModel(Guid id)
        {
            AppViewModel avm = new AppViewModel();            
            avm.Id = id;
            using (SlpContext context = new SlpContext())
            {
                SlpApplication dbApp = context.SlpApplication.Find(avm.Id);
                if (dbApp != null)
                {
                    avm.AssistanceProgramId = dbApp.AssistanceProgramId;
                    avm.AssistanceProgramCaseNumber = dbApp.AssistanceProgramCaseNumber;
                    avm.TotalMembers = dbApp.TotalMembers;
                    avm.MemberLastFourSSN = dbApp.MemberLastFourSSN;
                    avm.StreetAddress = dbApp.StreetAddress;
                    avm.AptNo = dbApp.AptNo;
                    avm.City = dbApp.City;
                    avm.State = dbApp.State;
                    avm.Zip = dbApp.Zip;
                    avm.Email = dbApp.Email;
                    avm.Phone = dbApp.Phone;
                    avm.AdultFilledByName = dbApp.AdultFilledByName;
                    avm.EthnicityId = dbApp.EthnicityId;

                    avm.NoSSN = dbApp.NoSSN;
                }
            }
            List<MemberViewModel> members = GetMembers(avm.Id);
            avm.Children = members.Where(a => a.IsChild == true).ToList();
            avm.Adults = members.Where(a => a.IsChild == false).ToList();

            //set domain values, default for all applications new/old
            using (SlpContext context = new SlpContext())
            {
                avm.AssistancePrograms = context.AssistanceProgram.Where(a => a.Visible == true).Select(a => new NameValueViewModel { Name = a.Name, Value = a.Id }).OrderBy(a => a.Value).ToList();
                avm.Ethnicities = context.Ethnicity.Where(a => a.Visible == true).Select(a => new NameValueViewModel { Name = a.Name, Value = a.Id }).OrderBy(a => a.Value).ToList();
                avm.Frequencies = context.Frequency.Where(a => a.Visible == true).Select(a => new NameValueViewModel { Name = a.Name, Value = a.Id }).OrderBy(a => a.Value).ToList();
                avm.Races = context.Race.Where(a => a.Visible == true).Select(a => new NameValueViewModel { Name = a.Name, Value = a.Id }).OrderBy(a => a.Value).ToList();
                
                foreach (SlpApplicationRace dbrace in context.SlpApplicationRace.Where(a => a.SlpApplicationId == avm.Id))
                {
                    NameValueViewModel race = avm.Races.Where(a => a.Value == dbrace.RaceId).FirstOrDefault();
                    if (race != null)
                    {
                        race.SelectedItem = true;
                    }
                }
            }
            avm.AssistanceProgramNoneId = (int)AssistanceProgramsEnum.None;
            return avm;
        }

        public List<MemberViewModel> MemberSave(MemberViewModel mvm)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            List<MemberViewModel> cvmList = new List<MemberViewModel>();
            using (SlpContext context = new SlpContext())
            {
                SlpApplication slpApp = context.SlpApplication.Find(mvm.AppId);
                if (slpApp == null)
                {
                    context.SlpApplication.Add(new SlpApplication { Id = mvm.AppId, CreateDate = DateTime.Now });
                }                

                if (mvm.Id == new Guid()) //add child
                {
                    mvm.Id = Guid.NewGuid();
                    context.Members.Add(new Member {
                        Id = mvm.Id,
                        SlpApplicationId = mvm.AppId,
                        FirstName = textInfo.ToTitleCase(mvm.FirstName.ToLower()),
                        LastName = textInfo.ToTitleCase(mvm.LastName.ToLower()),
                        MiddleInitial = mvm.MiddleInitial == "" || mvm.MiddleInitial == null ? "" : mvm.MiddleInitial.ToUpper(),
                        IsChild = mvm.IsChild,
                        CreatedDate = DateTime.Now });                    
                }
                else //update child
                {
                    Member child = context.Members.Where(a => a.Id == mvm.Id).FirstOrDefault();
                    child.FirstName = textInfo.ToTitleCase(mvm.FirstName.ToLower());                    
                    child.LastName = textInfo.ToTitleCase(mvm.LastName.ToLower());
                    child.MiddleInitial = mvm.MiddleInitial == "" || mvm.MiddleInitial == null ? "" : mvm.MiddleInitial.ToUpper();             
                }
                
                if (mvm.IsChild)
                {
                    context.MemberChildAttributes.RemoveRange(context.MemberChildAttributes.Where(a => a.MemberId == mvm.Id));
                    foreach (NameValueViewModel vm in mvm.ChildAttributes)
                    {
                        context.MemberChildAttributes.Add(new MemberChildAttribute { MemberId = mvm.Id, AttributeTypeId = vm.Value, IsSelected = vm.SelectedItem });
                    }
                }
                context.SaveChanges();
            }

            cvmList = GetMembers(mvm.AppId).Where(a => a.IsChild ==  mvm.IsChild).ToList();
            
            return cvmList;
        }

        public List<MemberViewModel> MemberDelete(MemberViewModel mvm)
        {
            List<MemberViewModel> cvmList = new List<MemberViewModel>();
            using (SlpContext context = new SlpContext())
            {
                Member member = context.Members.Find(mvm.Id);
                context.Members.Remove(member);
                context.SaveChanges();
            }
            cvmList = GetMembers(mvm.AppId).Where(a => a.IsChild == mvm.IsChild).ToList();
            return cvmList;
        }

        public List<MemberViewModel> GetMembers(Guid? appId)
        {
            List<MemberViewModel> list = new List<MemberViewModel>();
            
            using (SlpContext context = new SlpContext())
            {
                //get names
                string membersQuery = "Select m.Id, m.SlpApplicationId AppId, m.createdDate,m.FirstName,m.MiddleInitial, m.lastName,m.IsChild, case when count(mi.memberId) = 0 then cast(0 as bit) else cast(1 as bit) end IsIncomeCompleted";
                membersQuery = membersQuery + " from Members m left outer join MemberIncomeResponses mi on m.id = mi.memberid";
                if (appId.HasValue)
                {
                    membersQuery = membersQuery + " where SlpApplicationId = {0}";
                }
                membersQuery = membersQuery + " group by m.Id, m.SlpApplicationId, m.createdDate, m.FirstName, m.MiddleInitial, m.lastName, m.IsChild ";
                if (appId.HasValue)
                {
                    list = context.Database.SqlQuery<MemberViewModel>(membersQuery, new object[] { appId }).ToList();
                }
                else {
                    list = context.Database.SqlQuery<MemberViewModel>(membersQuery).ToList();
                }

                //get  child attributes
                List<ChildAttributeViewModel> childAttributes = new List<ChildAttributeViewModel>();
                string childAttributeQuery = "Select mca.memberId,mca.AttributeTypeId,mca.IsSelected ,cat.name AttributeName ";
                childAttributeQuery = childAttributeQuery + " from Members m, MemberChildAttributes mca, ChildAttributeTypes cat ";
                childAttributeQuery = childAttributeQuery + " where m.id = mca.memberId and mca.AttributeTypeId = cat.id ";
                if (appId.HasValue)
                {
                    childAttributeQuery = childAttributeQuery + " and SlpApplicationId = {0}";
                    childAttributes = context.Database.SqlQuery<ChildAttributeViewModel>(childAttributeQuery, new object[] { appId }).ToList();
                }
                else {
                    childAttributes = context.Database.SqlQuery<ChildAttributeViewModel>(childAttributeQuery).ToList();
                }

                List<IncomeSummaryViewModel> incomeGroups = new List<IncomeSummaryViewModel>();
                string incomeQuery = "select memberId, Name FrequencyName, sum(amount)amount,fr.id FrequencyId ";
                incomeQuery = incomeQuery + " from Members m, MemberIncomeResponses mir, MemberIncomeResponseDetails mird, Frequencies fr ";
                incomeQuery = incomeQuery + " where m.id = mir.memberId and mir.Id = mird.memberincomeresponseId and mird.frequencyId = fr.id ";
                if (appId.HasValue)
                {
                    incomeQuery = incomeQuery + " and SlpApplicationId = {0}";
                }
                incomeQuery = incomeQuery + " group by memberId, Name,fr.id  order by memberId,fr.id";
                if (appId.HasValue)
                {
                    incomeGroups = context.Database.SqlQuery<IncomeSummaryViewModel>(incomeQuery, new object[] { appId }).ToList();
                }
                else {
                    incomeGroups = context.Database.SqlQuery<IncomeSummaryViewModel>(incomeQuery).ToList();
                }

                foreach (MemberViewModel member in list)
                {
                    member.ChildAttributes = childAttributes.Where(a => a.MemberId == member.Id).Select(z=> new NameValueViewModel { Name=z.AttributeName, SelectedItem=z.IsSelected, Value=z.AttributeTypeId }).ToList();
                    member.TotalIncomeReported = string.Join(", ", incomeGroups.Where(a => a.MemberId == member.Id).Select(z => z.Amount.ToString("c") + "/" + z.FrequencyName));
                    if (member.TotalIncomeReported == "")
                    {
                        member.TotalIncomeReported = "$ 0.00";
                    }
                }
            }
            return list.OrderBy(a => a.CreatedDate).ToList();
        }

        public void SaveAssistanceProgram(AssistanceProgramViewModel apvm)
        {
            using (SlpContext context = new SlpContext())
            {
                SlpApplication dbApp = context.SlpApplication.Find(apvm.AppId);
                dbApp.AssistanceProgramId = apvm.Id; // apvm.Id == null || apvm.Id ==  ? null : apvm.Id;
                dbApp.AssistanceProgramCaseNumber = apvm.Id == null || apvm.Id == (int)AssistanceProgramsEnum.None  ? "" : apvm.CaseNumber;
                context.SaveChanges();
            }
        }
        public void SaveStep3(Step3ViewModel svm)
        {
            using (SlpContext context = new SlpContext())
            {
                SlpApplication dbApp = context.SlpApplication.Find(svm.AppId);
                dbApp.TotalMembers = svm.TotalMembers;
                dbApp.MemberLastFourSSN = svm.MemberLastFourSSN;
                dbApp.NoSSN = svm.NoSSN;
                context.SaveChanges();
            }
        }
        
        public void SaveContactInformation(ContactViewModel cvm)
        {
            using (SlpContext context = new SlpContext())
            {
                SlpApplication dbApp = context.SlpApplication.Find(cvm.AppId);
                dbApp.StreetAddress = cvm.StreetAddress;
                dbApp.AptNo = cvm.AptNo;
                dbApp.City = cvm.City;
                dbApp.State = cvm.State;
                dbApp.Zip = cvm.Zip;
                dbApp.Email = cvm.Email;
                dbApp.Phone = cvm.Phone;
                dbApp.AdultFilledByName = cvm.AdultFilledByName == null || cvm.AdultFilledByName == "" ? "" : cvm.AdultFilledByName.ToUpper();
                dbApp.EthnicityId = cvm.EthnicityId;
                dbApp.CompletedDate = DateTime.Now;
                dbApp.Steps = string.Join(",", cvm.StepsRequired);

                if (!cvm.StepsRequired.Contains(2))  //if 2 not present, remove data for step 2
                {
                    dbApp.AssistanceProgramId = null;
                    dbApp.AssistanceProgramCaseNumber = "";
                }

                if (!cvm.StepsRequired.Contains(3))  //if 3 not present remove data from step 3, all adults, all income records, total members, ssn field and no ssn field
                {
                    dbApp.TotalMembers = null;
                    dbApp.MemberLastFourSSN = null;
                    dbApp.NoSSN = false;
                    context.Members.RemoveRange(context.Members.Where(a=>a.SlpApplicationId == cvm.AppId && a.IsChild == false)); //remove adults

                    //remove all income responses and income details
                    List<Guid> allmembers = context.Members.Where(a => a.SlpApplicationId == cvm.AppId).Select(a => a.Id).ToList();
                    context.MemberIncomeResponses.RemoveRange(context.MemberIncomeResponses.Where(a=> allmembers.Contains(a.MemberId)));
                }                

                context.SlpApplicationRace.RemoveRange(context.SlpApplicationRace.Where(a => a.SlpApplicationId == cvm.AppId));

                foreach (NameValueViewModel selectedRace in cvm.Races.Where(a=>a.SelectedItem))
                {
                    context.SlpApplicationRace.Add(new SlpApplicationRace { SlpApplicationId = cvm.AppId, RaceId = selectedRace.Value });
                }
                            
                context.SaveChanges();
            }
        }

        public IncomeViewModel GetMemberIncome(Guid memberId)
        {
            IncomeViewModel ivm = new IncomeViewModel();
            using (SlpContext context = new SlpContext())
            {
                Member member = context.Members.Find(memberId);
                ivm.MemberId = memberId;
                ivm.MemberName = member.FirstName + (member.MiddleInitial == null || member.MiddleInitial == string.Empty ? " " : " " + member.MiddleInitial + " ") + member.LastName;
                ivm.IsChild = member.IsChild;
                ivm.IncomeResponses = member.MemberIncomeResponses.Select(a => 
                    new IncomeResponseViewModel {
                        IncomeQuestionId = a.IncomeQuestionId,
                        QuestionText = a.IncomeQuestion.Text,
                        QuestionType = a.IncomeQuestion.Type,
                        Response = a.Response,
                        ResponseId = a.Id,
                        ResponseDetails = a.IncomeResponseDetails.ToList().OrderBy(s=>s.CreatedDate).Select(z=> new IncomeResponseDetailViewModel { Amount= z.Amount, DetailError="", FrequencyId=z.FrequencyId } ).ToList()
                    }).ToList();

                if (ivm.IncomeResponses.Count == 0)
                {
                    ivm.IncomeResponses = context.IncomeQuestions.Where(a=>a.IsChild == member.IsChild && a.Visible).Select(a => 
                    new IncomeResponseViewModel {
                        IncomeQuestionId = a.Id,
                        QuestionText = a.Text,
                        QuestionType = a.Type }).ToList();
                }
            }

            ivm.IncomeResponses = ivm.IncomeResponses.OrderBy(a => a.IncomeQuestionId).ToList();
            return ivm;
        }

        public List<MemberViewModel> SaveMemberIncome(IncomeViewModel ivm)
        {
            Member member = null;

            using (SlpContext context = new SlpContext())
            {
                member = context.Members.Find(ivm.MemberId);
                
                context.MemberIncomeResponses.RemoveRange(context.MemberIncomeResponses.Where(a => a.MemberId == ivm.MemberId));

                foreach (IncomeResponseViewModel irm in ivm.IncomeResponses)
                {
                    List<MemberIncomeResponseDetail> details = new List<MemberIncomeResponseDetail>();
                    if (irm.ResponseDetails.Count > 0)
                    {
                        details = irm.ResponseDetails.Select(a => new MemberIncomeResponseDetail { Amount = a.Amount.Value, CreatedDate = DateTime.Now, FrequencyId = a.FrequencyId.Value }).ToList();
                    }
                    context.MemberIncomeResponses.Add(new MemberIncomeResponse {
                        IncomeQuestionId = irm.IncomeQuestionId,
                        MemberId = ivm.MemberId,
                        Response = irm.Response.Value,
                        IncomeResponseDetails = details
                    });
                }
                context.SaveChanges();
            }

            return GetMembers(member.SlpApplicationId).Where(a => a.IsChild == member.IsChild).ToList();
        }
    }
}
