namespace Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Domain.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<Domain.SlpContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Domain.SlpContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method to avoid creating duplicate seed data.
            context.AssistanceProgram.AddOrUpdate(
              p => p.Name,
              new AssistanceProgram { Name = "SNAP", Visible = true },
              new AssistanceProgram { Name = "TANF", Visible = true },
              new AssistanceProgram { Name = "FDPIR", Visible = true },
               new AssistanceProgram { Name = "None", Visible = true }
            );

            context.Frequency.AddOrUpdate(
              p => p.Name,
              new Frequency { Name = "Weekly", Visible = true },
              new Frequency { Name = "Bi-Weekly", Visible = true },
              new Frequency { Name = "2x Month", Visible = true },
              new Frequency { Name = "Monthly", Visible = true }
            );

            context.Ethnicity.AddOrUpdate(
             p => p.Name,
             new Ethnicity { Name = "Hispanic or Latino", Visible = true },
             new Ethnicity { Name = "Not Hispanic or Latino", Visible = true }
           );

            context.Race.AddOrUpdate(
              p => p.Name,
              new Race { Name = "American Indian or Alaskan Native", Visible = true },
              new Race { Name = "Asian", Visible = true },
              new Race { Name = "Black or African American", Visible = true },
              new Race { Name = "Native Hawaiian or Other Pacic Islander", Visible = true },
              new Race { Name = "White", Visible = true }
            );

            context.ChildAttributeTypes.AddOrUpdate(
             p => p.Name,
             new ChildAttributeType { Name = "Student", Visible = true },
             new ChildAttributeType { Name = "Foster Child", Visible = true },
             new ChildAttributeType { Name = "Homeless, Migrant or Runaway", Visible = true },
             new ChildAttributeType { Name = "Head Start participant", Visible = true }
           );

            context.IncomeQuestions.AddOrUpdate(
             p => p.Text,
             new IncomeQuestion { Type = "Salary or wages", Text = "Does child earn a salary or wages from a job?", IsChild = true, Visible = true },
             new IncomeQuestion { Type = "Social Security benefits", Text = "Does child receive Social Security benefits for the child’s own blindness or disability, or because a parent is disabled, retired, or deceased?", IsChild = true, Visible = true },
             new IncomeQuestion { Type = "Spending money or other income", Text = "Does child regularly receive spending money or other income from a person outside the household such as an extended family member or friend?", IsChild = true, Visible = true },
             new IncomeQuestion { Type = "Private pension fund, annuity, or trust", Text = "Does child receive income from any other source such as from a private pension fund, annuity, or trust?", IsChild = true, Visible = true },
            

             new IncomeQuestion { Type = "earnings from work", Text = "Does adult receive any earnings from work such as salary, wages, cash bonuses, net income from self-employment(farm income, partnership income, professional practice income, or other), strike benefits, unemployment insurance, or any other earned income ?", IsChild = false, Visible = true },
             new IncomeQuestion { Type = "U.S. military income", Text = "Is adult in the U.S. military and does adult[n] receive military basic pay or cash bonuses, or military allowances for off-base housing, food or clothing?[Do NOT report combat pay, FSSA or privatized housing allowances]", IsChild = false, Visible = true },
             new IncomeQuestion { Type = "public assistance, alimony, or child support", Text = "Does adult receive income from public assistance, alimony, or child support?", IsChild = false, Visible = true },
             new IncomeQuestion { Type = "retirement or disability income", Text = "Does adult receive retirement or disability income such as, but not limited to Social Security, railroad retirement, pensions, annuities, survivor’s benefits, disability benefits from Supplemental Security Income(SSI), private disability benefits, black lung benefits, worker’s compensation, veteran’s benefits, or related sources?", IsChild = false, Visible = true },
             new IncomeQuestion { Type = "investment or any other income", Text = "Does adult receive investment or any other income?", IsChild = false, Visible = true }

           );

            

        }
    }
}
