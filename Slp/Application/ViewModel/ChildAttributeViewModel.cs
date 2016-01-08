using System;

namespace Application.ViewModel
{
    public class ChildAttributeViewModel
    {
        public Guid MemberId { get; set; }

        public int AttributeTypeId { get; set; }

        public bool IsSelected { get; set; }

        public string AttributeName { get; set; }
    }
}
