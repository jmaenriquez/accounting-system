using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingSystem.Data
{
    public class Trial_Balance
    {
        public DateOnly datetime { get; set; }

        [Key]
        public int balance_Id { get; set; }
        public required string description { get; set; }

        [ForeignKey("groupId")]
        public virtual Group_List? AccGrp  { get; set; }

        [ForeignKey("accountId")]
        public virtual Account_List? AccName { get; set; }

        [ForeignKey("amountId")]
        public virtual Amount? Amount  { get; set; }
    }
}