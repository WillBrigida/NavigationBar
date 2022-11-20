namespace Sample;

public partial class TwoPage : ContentPage
{
    public TwoPage() => InitializeComponent();

    private void NavToThreePage(object sender, EventArgs e)
        => Shell.Current.GoToAsync(nameof(ThreePage));

    private void ShowModal(object sender, EventArgs e)
        => Shell.Current.GoToAsync(nameof(ModalPage));
}