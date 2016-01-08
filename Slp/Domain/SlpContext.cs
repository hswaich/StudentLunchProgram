using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domain.Model;
using System.Diagnostics;

namespace Domain
{
    public class SlpContext : DbContext
    {
        public SlpContext() : base("SlpConnection")
        {
            Database.Log = message => Trace.WriteLine(message); //Console.Write;
        }

        
        public DbSet<SlpApplication> SlpApplication { get; set; }

        //public DbSet<Child> Children { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<AssistanceProgram> AssistanceProgram { get; set; }

        public DbSet<Frequency> Frequency { get; set; }

        public DbSet<Ethnicity> Ethnicity { get; set; }

        public DbSet<Race> Race { get; set; }

        public DbSet<SlpApplicationRace> SlpApplicationRace { get; set; }


        public DbSet<ChildAttributeType> ChildAttributeTypes { get; set; }

        public DbSet<MemberChildAttribute> MemberChildAttributes { get; set; }

        public DbSet<IncomeQuestion> IncomeQuestions { get; set; }

        public DbSet<MemberIncomeResponse> MemberIncomeResponses { get; set; }

        public DbSet<MemberIncomeResponseDetail> MemberIncomeResponseDetails { get; set; }

    }
}
