using AccountingSystem.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingSystem.Data
{
    public class Group_List
    {
        public DateOnly datetime { get; set; }


        public int id { get; set; }

        [Required]
        public required string name { get; set; } = string.Empty;

        [Required]
        public required string description { get; set; } = string.Empty;

        [Required]
        public AccountType type { get; set; }

        [Required]
        public Status status { get; set; }


        [InverseProperty("AccGrp")]
        public virtual ICollection<Journal_Entry> Journal_Entries { get; set; } = new List<Journal_Entry>();


    }
}