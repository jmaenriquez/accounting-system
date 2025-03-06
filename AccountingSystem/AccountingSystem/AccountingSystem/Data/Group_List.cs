using AccountingSystem.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingSystem.Data
{
    public class Group_List
    {
        public DateOnly datetime { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int groupId { get; set; }

        [Required]
        public required string name { get; set; } = string.Empty;

        [Required]
        public required string description { get; set; } = string.Empty;

        [Required]
        public required string type { get; set; }

        [Required]
        public required string status { get; set; }
    }
}