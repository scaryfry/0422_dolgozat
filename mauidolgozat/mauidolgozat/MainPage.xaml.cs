namespace mauidolgozat
{
    public partial class MainPage : ContentPage
    {
        public List<PremiumMembership> PremiumMemberships = [];
        public MainPage()
        {
            InitializeComponent();
            MembershipViews.ItemsSource = PremiumMemberships;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadData();
        }
        public void LoadData() 
        {
            FileService.ReadFromCsv();
            PremiumMemberships.Clear();
            foreach(var membership in FileService.Memberships)
            {
              PremiumMemberships.Add(membership);
            }
        }
        public void AddButton_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("Edit");
        }
        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var membership = (PremiumMembership)((Button)sender).BindingContext;

            FileService.Memberships.Remove(membership);
            FileService.WriteMemberShipsFromCsv();

            LoadData();
        }
        public void EditButton_Clicked(Object sender, EventArgs e) 
        {
            var membership = (PremiumMembership)((Button)sender).BindingContext;
            Shell.Current.GoToAsync("Edit", new ShellNavigationQueryParameters { { "Id", membership.Id } });
        }
    }
}
