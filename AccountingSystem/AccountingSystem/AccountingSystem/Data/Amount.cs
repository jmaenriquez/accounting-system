using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingSystem.Data
{
    public class Amount
    {
        [Key]
        public int amountId { get; set; }

        [ForeignKey("debitId")]
        public virtual Debit_Accounts? Debit  { get; set; }

        [ForeignKey("creditId")]
        public virtual Credited_Accounts? Credit  { get; set; }

        public DateOnly datetime { get; set; }

    }
}