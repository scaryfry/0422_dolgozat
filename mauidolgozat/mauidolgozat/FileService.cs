namespace mauidolgozat;

public class FileService
{
    public static string FilePath = "premium_memberships.csv";
    public static List<PremiumMembership> Memberships = [];

    public static void ReadFromCsv()
    {
        var lines = File.ReadAllLines(FilePath);
        Memberships.Clear();
        int idCounter = 1;
        foreach (var line in lines.Skip(1))
        {
            var parts = line.Split(',');
            var member = new PremiumMembership()
            {
                Id = idCounter++,
                GameName = parts[0],
                OrderDate = DateTime.Parse(parts[1]),
                ExpirationDate = DateTime.Parse(parts[2]),
                Price = int.Parse(parts[3])
            };
            Memberships.Add(member);
        }
    }
    public static void WriteMemberShipsFromCsv()
    {
        var lines = new List<string>();
        foreach (var line in Memberships)
        {
            lines.Add($"{line.Id},{line.OrderDate},{line.ExpirationDate},{line.Price}");
        }
        File.WriteAllLines(FilePath, lines);
    }
}
