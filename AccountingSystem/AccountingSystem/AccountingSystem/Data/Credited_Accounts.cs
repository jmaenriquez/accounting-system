using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingSystem.Data
{
    public class Credited_Accounts
    {
        [Key]
        public int creditId { get; set; }

        [ForeignKey("accountId")]
        public virtual Account_List? AccName { get; set; }

         public int amount { get; set; }

        public DateOnly datetime { get; set; }
    }
}