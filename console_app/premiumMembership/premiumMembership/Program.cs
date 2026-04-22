using premiumMembership;

var filePath = "premium_memberships.csv";

var memberships = new List<PremiumMembership>();

var fileRows = File.ReadAllLines(filePath);

foreach(var row in fileRows.Skip(1))
{
    var data = row.Split(',');
    var memberShip = new PremiumMembership();
    memberShip.GameName = data[0];
    memberShip.OrderDate = DateTime.Parse(data[1]);
    memberShip.ExpirationDate = DateTime.Parse(data[2]);
    memberShip.Price = int.Parse(data[3]);
    memberships.Add(memberShip);
}
/*
Console app csak statisztikai célokra:

Feladatok:
1. Számoljuk ki hogy, hány prémium tagság van jelenleg érvényben.
2. Számoljuk ki hogy, mennyi bevétel származott a prémium tagságokból az elmúlt hónapban (március).
3. Készítsünk egy listát a lejáró prémium tagságokról a következő hónapban.
4. Számoljuk ki mindegyik prémium tagságra, hogy hány napig tartott.
5. Átlagosan hány napig tart egy prémium tagság?
6. Melyik játékhoz mennyi prémium tagságot vásároltak?
7. Melyik hónapban volt a legtöbb prémium tagság vásárlás?
 */
var tartott = 0;

var premiumMemberCount = memberships.Where((x) => x.ExpirationDate > DateTime.Now).Count();
Console.WriteLine($"Érvényes prémium tagság: {premiumMemberCount}");
var incomeinMarch = memberships.Where((x) => x.OrderDate.Month == 3 && x.OrderDate.Year == DateTime.Now.Year).Sum(x => x.Price);
Console.WriteLine($"Márciusi bevétel: {incomeinMarch}");
var expiringMemberships = memberships.Where((x) => x.ExpirationDate.Month == 5 && x.ExpirationDate.Year == DateTime.Now.Year);
Console.WriteLine($"Lejáró tagságok: ");
foreach (var membership in expiringMemberships)
{
    Console.WriteLine($"{membership.GameName}, {membership.OrderDate}, {membership.ExpirationDate}, {membership.Price}");
}
foreach(var membership in memberships)
{
    tartott = (membership.ExpirationDate - membership.OrderDate).Days;
    Console.WriteLine($"Eddig tartottak a az egyes tagságok: {int.Abs(tartott)}");
}
var avgDuration = memberships.Average(m => (m.ExpirationDate - m.OrderDate).Days);
Console.WriteLine($"Átlagos egy tagság {avgDuration} napig tart");

var premiummembershipForGame = memberships.GroupBy((x) => x.GameName).Select((g) => new { GameName = g.Key, Count = g.Key.Count() });

foreach(var membership in premiummembershipForGame)
{
    Console.WriteLine($"{membership.GameName}, {membership.Count}");
}
var monthMembershipCounts = memberships.GroupBy(m => m.OrderDate.Month)
                                       .Select((g) => new { Month = g.Key, Count = g.Count() });
var maxCount = monthMembershipCounts.Max(m => m.Count);
var mostPopularMonths = monthMembershipCounts.Where(m => m.Count == maxCount).Select((m) => m.Month).ToList();

foreach(var  month in mostPopularMonths)
{
    Console.WriteLine($"{month}. hónap: {maxCount} prémium tagság vásarlás");
}