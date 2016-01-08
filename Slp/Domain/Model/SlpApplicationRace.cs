using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class SlpApplicationRace
    {
        [Key]
        [Column(Order = 10)]
        public Guid SlpApplicationId { get; set; }

        [Key]
        [Column(Order = 20)]
        public int RaceId { get; set; }

       
        public virtual SlpApplication SlpApplication { get; set; }

        public virtual Race Race { get; set; }

    }
}
