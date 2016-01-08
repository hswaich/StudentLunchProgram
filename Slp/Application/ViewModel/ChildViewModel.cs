using System;

namespace Application.ViewModel
{
    public class ChildViewModel
    {
        public Guid Id { get; set; }

        public Guid ApplicationId { get; set; }

        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        public string LastName { get; set; }

        public bool IsStudent { get; set; }

        public bool IsFosterChild { get; set; }

        /// <summary>
        /// Homeless, Migrant, Runaway
        /// </summary>
        public bool IsHMR { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
