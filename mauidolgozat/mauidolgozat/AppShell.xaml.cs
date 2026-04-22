
namespace mauidolgozat
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Main", typeof(MainPage));
            Routing.RegisterRoute("Edit", typeof(Edit));
        }
    }
}
