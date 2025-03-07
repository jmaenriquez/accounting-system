using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingSystem.Data
{
    public class ChartofAccs
    {
        public int id { get; set; }
        public required string AccountName { get; set; }

        [ForeignKey ("id")]
        public virtual Journal_Entry? group { get; set; }
        public DateOnly date { get; set; }
    }
}