using System;
using System.Collections.Generic;

namespace Application.ViewModel
{
    public class IncomeResponseViewModel
    {
        public IncomeResponseViewModel()
        {
            ResponseDetails = new List<IncomeResponseDetailViewModel>();
        }

        public int? ResponseId { get; set; }

        public int IncomeQuestionId { get; set; }

        public string QuestionType { get; set; }

        public string QuestionText { get; set; }

        public bool? Response { get; set; }

        public string ResponseError { get; set; }

        public List<IncomeResponseDetailViewModel> ResponseDetails { get; set; }
    }
}
