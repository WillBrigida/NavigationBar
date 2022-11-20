namespace Sample;

public partial class OnePage : ContentPage
{
    public OnePage() => InitializeComponent();

    private void NavToTwoPage(object sender, EventArgs e)
       => Shell.Current.GoToAsync(nameof(TwoPage));

    private void ShowModal(object sender, EventArgs e)
        => Shell.Current.GoToAsync(nameof(ModalPage));
}