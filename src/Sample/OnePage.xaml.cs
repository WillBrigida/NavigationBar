namespace Sample;

public partial class OnePage : ContentPage
{
	public OnePage()
	{
		InitializeComponent();
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync(nameof(TwoPage));
	}
}