using System.ComponentModel.DataAnnotations;

namespace AccountingSystem.Data
{
    public class ChartofAccs
    {
        public int id { get; set; }
        public required string AccountName { get; set; }
        public required string AccountGroup { get; set; }
        public DateOnly date { get; set; }
    }
}