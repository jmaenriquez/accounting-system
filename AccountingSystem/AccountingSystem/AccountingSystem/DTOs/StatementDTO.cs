namespace AccountingSystem.DTOs;
public class StatementDTO
{
    public string AccountName { get; set; }

    public string AccountGroup { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }

    public StatementDTO()
    {
    }

    public StatementDTO(string accountName, string accountGroup, decimal debit, decimal credit)
    {
        AccountName = accountName;
        AccountGroup = accountGroup;
        Debit = debit;
        Credit = credit;
    }
}

