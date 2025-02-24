using System.ComponentModel.DataAnnotations;

namespace AccountingSystem.Data
{
    public class Account_List
    {
        [Key]
        public int accountId { get; set; }
        public required string date { get; set; } 
        public required string name { get; set; } 
        public required string description { get; set; } 
        public required string status { get; set; } 
    }
}