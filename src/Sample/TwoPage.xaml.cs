namespace Sample;

public partial class TwoPage : ContentPage
{
    public TwoPage()
    {
        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

        Shell.Current.GoToAsync(nameof(ThreePage));
    }
}