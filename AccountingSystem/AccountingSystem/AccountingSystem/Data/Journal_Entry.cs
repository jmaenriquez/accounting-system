using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingSystem.Data
{
    public class Journal_Entry
    {
        public DateOnly datetime { get; set; }

        
        public int id { get; set; }

        
        public required string description { get; set; }

        [ForeignKey("id")]
        public virtual Account_List? AccName { get; set; }

        public decimal debit { get; set; } = 0;

        public decimal credit { get; set; } = 0;
    }
}