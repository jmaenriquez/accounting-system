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

        [Required(ErrorMessage = "Name is Empty!")]
        public required string name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is Empty!")]
        public required string description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Group Type is Required!")]
        public required string type { get; set; }

        [Required(ErrorMessage = "Status is Required!")]
        public required string status { get; set; }
    }
}