using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AccountingSystem.Data
{
    public class Debit_Accounts
    {

        [Key]
        public int debitId { get; set; }

        [ForeignKey("accountId")]
        public virtual Account_List? AccName { get; set; }

        public int amount { get; set; }
        public DateTime datetime { get; set; }
    }
}