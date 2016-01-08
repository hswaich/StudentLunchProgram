using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class IncomeQuestion
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Type { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(1000)]
        public string Text { get; set; }

        public bool IsChild { get; set; }

        public bool Visible { get; set; }
    }
}
