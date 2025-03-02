using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingSystem.Data
{
    public class Journal_Entry
    {
        public DateOnly datetime { get; set; }

        [Key]
        public int journal_code { get; set; }

        [ForeignKey("accountId")]
        public virtual Account_List? AccName { get; set; }

        [ForeignKey("debitId")]
        public virtual Debit_Accounts? Debit { get; set; }

        [ForeignKey("creditId")]
        public virtual Credited_Accounts? Credit { get; set; }
    }
}