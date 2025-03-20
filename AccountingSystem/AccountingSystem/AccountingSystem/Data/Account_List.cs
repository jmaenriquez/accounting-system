using AccountingSystem.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingSystem.Data
{
    public class Account_List
    {
        public required DateOnly date{ get; set; }
        public int id { get; set; }

        [Required]
        public string name { get; set; } = string.Empty;

        [Required]
        public string description { get; set; }=string.Empty;
        public Status status { get; set; }

        public bool isDeleted { get; set; } = false;

        [InverseProperty("AccName")]
        public virtual ICollection<Journal_Entry> Journal_Entries { get; set; } = new List<Journal_Entry>();
    }
}