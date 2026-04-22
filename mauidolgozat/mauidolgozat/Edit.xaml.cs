using System.Threading.Tasks;
namespace mauidolgozat;

public partial class Edit : ContentPage, IQueryAttributable
{
    private PremiumMembership premiumMembership;
	public Edit()
	{
		InitializeComponent();
	}
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        query.TryGetValue("Id", out var idobject);
        var id = (int)(idobject ?? 0);

        if (id == 0)
        {
            premiumMembership = new PremiumMembership { Id = 0, GameName = "Új játék", OrderDate = DateTime.Now , ExpirationDate = DateTime.Now, Price = 6000};
        }
        else
        {
            premiumMembership = FileService.Memberships.Single(x => x.Id == id);
        }
        NameEntry.Text = premiumMembership.GameName;
        GenderEntry.Text = premiumMembership.OrderDate.ToString();
        HeightEntry.Text = premiumMembership.ExpirationDate.ToString();
        WeightEntry.Text = premiumMembership.Price.ToString();
    }
}