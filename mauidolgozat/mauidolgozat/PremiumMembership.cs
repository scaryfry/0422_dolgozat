namespace mauidolgozat;

public class PremiumMembership
{
    public int Id { get; set; }
    public string GameName { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Price { get; set; }
}
