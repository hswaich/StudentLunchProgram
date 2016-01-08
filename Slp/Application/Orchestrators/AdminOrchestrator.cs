using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Model;
using Application.ViewModel;
using Application.Enums;

namespace Application.Orchestrators
{
    public class AdminOrchestrator
    {
        public List<NameValueViewModel> GetLists() {
            List<NameValueViewModel> list = new List<NameValueViewModel>();
            list.Add(new NameValueViewModel { Name = "Assistance Programs", Value = (int)ListEnum.AssistancePrograms });
            list.Add(new NameValueViewModel { Name = "Child Categories", Value = (int)ListEnum.ChildCategories });
            list.Add(new NameValueViewModel { Name = "Ethnicity List", Value = (int)ListEnum.EthnicityList });
            list.Add(new NameValueViewModel { Name = "Race List", Value = (int)ListEnum.RaceList });
            list.Add(new NameValueViewModel { Name = "Income Frequencies", Value = (int)ListEnum.IncomeFrequencies });
            list.Add(new NameValueViewModel { Name = "Income Questions - Child", Value = (int)ListEnum.IncomeQuestionsChild });
            list.Add(new NameValueViewModel { Name = "Income Questions - Adult", Value = (int)ListEnum.IncomeQuestionsAdult });

            return list;
        }

        public ListViewModel GetListViewModel(int id)
        {
            ListViewModel lvm = new ListViewModel { ListId = id };
            
            using (SlpContext context = new SlpContext())
            {
                if (lvm.ListId == (int)ListEnum.AssistancePrograms)
                {
                    lvm.TableValues.AddRange(context.AssistanceProgram.Select(a => new NameValueViewModel { Name = a.Name, Value = a.Id, SelectedItem = a.Visible }));
                }
                else if (lvm.ListId == (int)ListEnum.ChildCategories)
                {
                    lvm.TableValues.AddRange(context.ChildAttributeTypes.Select(a => new NameValueViewModel { Name = a.Name, Value = a.Id, SelectedItem = a.Visible }));
                }
                else if (lvm.ListId == (int)ListEnum.EthnicityList)
                {
                    lvm.TableValues.AddRange(context.Ethnicity.Select(a => new NameValueViewModel { Name = a.Name, Value = a.Id, SelectedItem = a.Visible }));
                }
                else if (lvm.ListId == (int)ListEnum.RaceList)
                {
                    lvm.TableValues.AddRange(context.Race.Select(a => new NameValueViewModel { Name = a.Name, Value = a.Id, SelectedItem = a.Visible }));
                }
                else if (lvm.ListId == (int)ListEnum.IncomeFrequencies)
                {
                    lvm.TableValues.AddRange(context.Frequency.Select(a => new NameValueViewModel { Name = a.Name, Value = a.Id, SelectedItem = a.Visible }));
                }
                else if (lvm.ListId == (int)ListEnum.IncomeQuestionsChild)
                {
                    lvm.TableValues.AddRange(context.IncomeQuestions.Where(b=>b.IsChild == true).Select(a => new NameValueViewModel { Name = a.Text, Value = a.Id, SelectedItem = a.Visible, Type= a.Type }));
                }
                else if (lvm.ListId == (int)ListEnum.IncomeQuestionsAdult)
                {
                    lvm.TableValues.AddRange(context.IncomeQuestions.Where(b => b.IsChild == false).Select(a => new NameValueViewModel { Name = a.Text, Value = a.Id, SelectedItem = a.Visible, Type = a.Type }));
                }
            }
            return lvm;
        }

        public void SaveListViewModel(ListViewModel lvm)
        {
            using (SlpContext context = new SlpContext())
            {
                if (lvm.ListId == (int)ListEnum.AssistancePrograms)
                {
                    foreach (NameValueViewModel item in lvm.TableValues)
                    {
                        if (item.Value == 0 && item.Name != null && item.Name.Trim()!= "")
                        {
                            context.AssistanceProgram.Add(new AssistanceProgram { Name = item.Name, Visible = item.SelectedItem });
                        }
                        else {
                            var dbitem = context.AssistanceProgram.Find(item.Value);
                            dbitem.Name = item.Name;
                            dbitem.Visible = item.SelectedItem;
                        }
                    }
                }
                else if (lvm.ListId == (int)ListEnum.ChildCategories)
                {
                    foreach (NameValueViewModel item in lvm.TableValues)
                    {
                        if (item.Value == 0 && item.Name != null && item.Name.Trim() != "")
                        {
                            context.ChildAttributeTypes.Add(new ChildAttributeType { Name = item.Name, Visible = item.SelectedItem });
                        }
                        else
                        {
                            var dbitem = context.ChildAttributeTypes.Find(item.Value);
                            dbitem.Name = item.Name;
                            dbitem.Visible = item.SelectedItem;
                        }
                    }
                }
                else if (lvm.ListId == (int)ListEnum.EthnicityList)
                {
                    foreach (NameValueViewModel item in lvm.TableValues)
                    {
                        if (item.Value == 0 && item.Name != null && item.Name.Trim() != "")
                        {
                            context.Ethnicity.Add(new Ethnicity { Name = item.Name, Visible = item.SelectedItem });
                        }
                        else
                        {
                            var dbitem = context.Ethnicity.Find(item.Value);
                            dbitem.Name = item.Name;
                            dbitem.Visible = item.SelectedItem;
                        }
                    }
                }
                else if (lvm.ListId == (int)ListEnum.RaceList)
                {
                    foreach (NameValueViewModel item in lvm.TableValues)
                    {
                        if (item.Value == 0 && item.Name != null && item.Name.Trim() != "")
                        {
                            context.Race.Add(new Race { Name = item.Name, Visible = item.SelectedItem });
                        }
                        else
                        {
                            var dbitem = context.Race.Find(item.Value);
                            dbitem.Name = item.Name;
                            dbitem.Visible = item.SelectedItem;
                        }
                    }
                }
                else if (lvm.ListId == (int)ListEnum.IncomeFrequencies)
                {
                    foreach (NameValueViewModel item in lvm.TableValues)
                    {
                        if (item.Value == 0 && item.Name != null && item.Name.Trim() != "")
                        {
                            context.Frequency.Add(new Frequency { Name = item.Name, Visible = item.SelectedItem });
                        }
                        else
                        {
                            var dbitem = context.Frequency.Find(item.Value);
                            dbitem.Name = item.Name;
                            dbitem.Visible = item.SelectedItem;
                        }
                    }
                }
                else if (lvm.ListId == (int)ListEnum.IncomeQuestionsChild || lvm.ListId == (int)ListEnum.IncomeQuestionsAdult)
                {
                    foreach (NameValueViewModel item in lvm.TableValues)
                    {
                        if (item.Value == 0 && item.Name != null && item.Name.Trim() != "")
                        {
                            context.IncomeQuestions.Add(new IncomeQuestion {
                                Text = item.Name,
                                Visible = item.SelectedItem,
                                IsChild = lvm.ListId == (int)ListEnum.IncomeQuestionsChild,
                                Type = item.Type });
                        }
                        else
                        {
                            var dbitem = context.IncomeQuestions.Find(item.Value);
                            dbitem.Text = item.Name;
                            dbitem.Visible = item.SelectedItem;
                            dbitem.Type = item.Type;
                        }
                    }
                }
               


                context.SaveChanges();
            }

        }

        public AppViewModel CompletedAppMembers(Guid applicationGuid) {
            List<AppMembersViewModel> avm = new List<AppMembersViewModel>();
            List<MemberViewModel> mvm = new List<MemberViewModel>();
            using (SlpContext context = new SlpContext())
            {
                 mvm =
                    context.Members.Where(a => a.SlpApplication.CompletedDate != null).Select(b =>
                  new MemberViewModel
                  {
                      AppId = b.SlpApplicationId,
                      FirstName = b.FirstName,
                      MiddleInitial = b.MiddleInitial,
                      LastName = b.LastName,
                      IsChild = b.IsChild,
                      ApplicationCompletedDate = b.SlpApplication.CompletedDate
                  }).ToList();
            }

            foreach (Guid appId in mvm.Select(a => a.AppId).Distinct())
            {
                List<MemberViewModel> appMembers = mvm.Where(a => a.AppId == appId).ToList();
                avm.Add(
                    new AppMembersViewModel
                    {
                        AppId = appId,
                        CompletedDate = appMembers.FirstOrDefault().ApplicationCompletedDate.ToString(),
                        CompletedDateTime = appMembers.FirstOrDefault().ApplicationCompletedDate.Value,
                        Adults = string.Join(", ", appMembers.Where(a => a.IsChild == false).Select(b => b.DisplayName)),
                        Children = string.Join(", ", appMembers.Where(a => a.IsChild == true).Select(b => b.DisplayName))
                    }
                    );
            }
            AppViewModel appVM = new AppViewModel();            
            AppOrchestrator appOrc = new AppOrchestrator();
            appVM = appOrc.GetAppViewModel(applicationGuid); //init objects with random guid
            appVM.AppMembers = avm.OrderByDescending(a => a.CompletedDateTime).ToList(); //members for report
            return appVM;
        }

        public List<AppDataFlatViewModel> AllAppData() {
            List<AppDataFlatViewModel> apps = new List<AppDataFlatViewModel>();
            List<NameViewModel> allRaces = new List<NameViewModel>();
            using (SlpContext context = new SlpContext())
            {
                apps =
                   context.SlpApplication.Where(a => a.CompletedDate != null).Select(b =>
                 new AppDataFlatViewModel
                 {
                     AppId = b.Id,
                     AssistanceProgramName = b.AssistanceProgramId.HasValue && b.AssistanceProgram != null ? b.AssistanceProgram.Name : "",
                     AssistanceProgramCaseNumber = b.AssistanceProgramCaseNumber,
                     TotalMembers = b.TotalMembers.ToString(),
                     MemberLastFourSSN = b.MemberLastFourSSN.ToString(),
                     NoSSNFlag = b.NoSSN ? "true" : "",
                     StreetAddress = b.StreetAddress,
                     AptNo = b.AptNo,
                     City = b.City,
                     State = b.State,
                     Zip = b.Zip,
                     Phone = b.Phone,
                     Email = b.Email,
                     AdultFilledByName = b.AdultFilledByName,
                     CompletedDate = b.CompletedDate.ToString(),
                     Ethnicity = b.EthnicityId.HasValue ? b.Ethnicity.Name : ""
                 }).ToList();

                //Race { get; set; }
                allRaces = context.SlpApplicationRace.Select(a => new NameViewModel { Id = a.SlpApplicationId, Name = a.Race.Name }).ToList();
            }

            //child name and income
            //adult names and income...
            List<MemberViewModel> _allMembers = new List<MemberViewModel>();
            AppOrchestrator appOrc = new AppOrchestrator();
            _allMembers = appOrc.GetMembers(null);


            foreach (AppDataFlatViewModel appdata in apps)
            {
                appdata.Race = string.Join(", ", allRaces.Where(a => a.Id == appdata.AppId).Select(a => a.Name));
                List<MemberViewModel> appChilds = _allMembers.Where(a => a.AppId == appdata.AppId && a.IsChild == true).OrderBy(a => a.CreatedDate).Take(5).ToList();
                List<MemberViewModel> appAdults = _allMembers.Where(a => a.AppId == appdata.AppId && a.IsChild == false).OrderBy(a => a.CreatedDate).Take(5).ToList();

                int childCount = 0;
                foreach (MemberViewModel child in appChilds)
                {
                    childCount++;
                    if (childCount == 1)
                    {
                        appdata.Child1FirstName = child.FirstName;
                        appdata.Child1MiddleInitial = child.MiddleInitial;
                        appdata.Child1LastName = child.LastName;
                        appdata.Child1Info = string.Join(", ", child.ChildAttributes.Where(a => a.SelectedItem == true).Select(a => a.Name));
                        appdata.Child1Income = child.TotalIncomeReported;
                    }

                    if (childCount == 2)
                    {
                        appdata.Child2FirstName = child.FirstName;
                        appdata.Child2MiddleInitial = child.MiddleInitial;
                        appdata.Child2LastName = child.LastName;
                        appdata.Child2Info = string.Join(", ", child.ChildAttributes.Where(a => a.SelectedItem == true).Select(a => a.Name));
                        appdata.Child2Income = child.TotalIncomeReported;
                    }

                    if (childCount == 3)
                    {
                        appdata.Child3FirstName = child.FirstName;
                        appdata.Child3MiddleInitial = child.MiddleInitial;
                        appdata.Child3LastName = child.LastName;
                        appdata.Child3Info = string.Join(", ", child.ChildAttributes.Where(a => a.SelectedItem == true).Select(a => a.Name));
                        appdata.Child3Income = child.TotalIncomeReported;
                    }

                    if (childCount == 4)
                    {
                        appdata.Child4FirstName = child.FirstName;
                        appdata.Child4MiddleInitial = child.MiddleInitial;
                        appdata.Child4LastName = child.LastName;
                        appdata.Child4Info = string.Join(", ", child.ChildAttributes.Where(a => a.SelectedItem == true).Select(a => a.Name));
                        appdata.Child4Income = child.TotalIncomeReported;
                    }

                    if (childCount == 5)
                    {
                        appdata.Child5FirstName = child.FirstName;
                        appdata.Child5MiddleInitial = child.MiddleInitial;
                        appdata.Child5LastName = child.LastName;
                        appdata.Child5Info = string.Join(", ", child.ChildAttributes.Where(a => a.SelectedItem == true).Select(a => a.Name));
                        appdata.Child5Income = child.TotalIncomeReported;
                    }

                }

                int adultCount = 0;
                foreach (MemberViewModel adult in appAdults)
                {
                    adultCount++;
                    if (adultCount == 1)
                    {
                        appdata.Adult1FirstName = adult.FirstName;
                        appdata.Adult1MiddleInitial = adult.MiddleInitial;
                        appdata.Adult1LastName = adult.LastName;
                        appdata.Adult1Income = adult.TotalIncomeReported;
                    }

                    if (adultCount == 2)
                    {
                        appdata.Adult2FirstName = adult.FirstName;
                        appdata.Adult2MiddleInitial = adult.MiddleInitial;
                        appdata.Adult2LastName = adult.LastName;
                        appdata.Adult2Income = adult.TotalIncomeReported;
                    }

                    if (adultCount == 3)
                    {
                        appdata.Adult3FirstName = adult.FirstName;
                        appdata.Adult3MiddleInitial = adult.MiddleInitial;
                        appdata.Adult3LastName = adult.LastName;
                        appdata.Adult3Income = adult.TotalIncomeReported;
                    }

                    if (adultCount == 4)
                    {
                        appdata.Adult4FirstName = adult.FirstName;
                        appdata.Adult4MiddleInitial = adult.MiddleInitial;
                        appdata.Adult4LastName = adult.LastName;
                        appdata.Adult4Income = adult.TotalIncomeReported;
                    }

                    if (adultCount == 5)
                    {
                        appdata.Adult5FirstName = adult.FirstName;
                        appdata.Adult5MiddleInitial = adult.MiddleInitial;
                        appdata.Adult5LastName = adult.LastName;
                        appdata.Adult5Income = adult.TotalIncomeReported;
                    }

                }


            }

            return apps;

        }
    }
}
