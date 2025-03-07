using AccountingSystem.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingSystem.Data
{
    public class Journal_Entry
    {
        public DateOnly datetime { get; set; }

        
        public int id { get; set; }

        
        public string description { get; set; }


        public int AccGrpId { get; set; }
        public int AccNameId { get; set; }



        [ForeignKey("AccGrpId")]
        public virtual Group_List? AccGrp { get; set; }

        [ForeignKey("AccNameId")]
        public virtual Account_List? AccName { get; set; }

        //public string? AccName { get; set; }

        //public string? AccGrp { get; set; }

        public decimal? debit { get; set; } = 0;

        public decimal? credit { get; set; } = 0;


        [NotMapped]
        public AccountType Type => AccGrp?.type ?? AccountType.Debit;


    }
    
}