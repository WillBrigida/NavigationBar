namespace Sample;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(OnePage), typeof(OnePage));
        Routing.RegisterRoute(nameof(TwoPage), typeof(TwoPage));
        Routing.RegisterRoute(nameof(ThreePage), typeof(ThreePage));
    }
}
