using System.ComponentModel.DataAnnotations;

namespace AccountingSystem.Data
{
    public class Account_List
    {
        public required DateOnly date{ get; set; }
        public int id { get; set; }

        [Required]
        public string name { get; set; } = string.Empty;

        [Required]
        public string description { get; set; }=string.Empty;

        [Required]
        public string status { get; set; } = "Active";
    }
}