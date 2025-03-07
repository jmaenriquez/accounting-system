using AccountingSystem.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingSystem.Data
{
    public class Trial_Balance
    {
        public DateOnly datetime { get; set; }

        
        public int id { get; set; }
        public string description { get; set; }

        [ForeignKey("id")]
        public virtual Group_List? AccGrp  { get; set; }

        [ForeignKey("id")]
        public virtual Account_List? AccName { get; set; }

        public decimal? Debit{ get; set; }
        public decimal? Credit { get; set; }


        [NotMapped]
        public AccountType Type => AccGrp?.type ?? AccountType.Debit;
    }
}