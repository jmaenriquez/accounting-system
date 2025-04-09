using AccountingSystem.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingSystem.Data
{
    public class Journal_Entry
    {
        public DateTime datetime { get; set; } = DateTime.Now;
        public int id { get; set; }
        public string? description { get; set; }
        public int AccGrpId { get; set; }
        public int AccNameId { get; set; }

        [ForeignKey("AccGrpId")]
        public virtual Group_List? AccGrp { get; set; }

        [ForeignKey("AccNameId")]
        public virtual Account_List? AccName { get; set; }

        public decimal? debit { get; set; } = 0;

        public decimal? credit { get; set; } = 0;

        public string enterRemark { get; set; } = string.Empty;

        public Guid TransactId { get; set;}


        [NotMapped]
        public AccountType Type => AccGrp?.type ?? AccountType.Debit;
 
        public bool isDeleted { get; set; } = false;
    }
    
}