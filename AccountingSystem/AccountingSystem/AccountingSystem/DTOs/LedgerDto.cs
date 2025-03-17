namespace AccountingSystem.DTOs;

public class LedgerDto
{
    public string AccountName { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }

    public LedgerDto()
    {
    }
    public LedgerDto(string accountName, decimal debit, decimal credit)
    {
        AccountName = accountName;
        Debit = debit;
        Credit = credit;
    }

    
}
