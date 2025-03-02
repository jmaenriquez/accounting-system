using System.ComponentModel.DataAnnotations;

namespace AccountingSystem.Data
{
    public class Account_List
    {
        public required DateOnly date{ get; set; }

        [Key]
        public required int accountId { get; set; }

        
        public required string name { get; set; } = string.Empty;

        
        public required string description { get; set; }=string.Empty;

        public required string status { get; set; } = "Active";
    }
}