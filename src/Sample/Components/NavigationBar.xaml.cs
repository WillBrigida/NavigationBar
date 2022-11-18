namespace Sample.Components;

public partial class NavigationBar : Grid
{
    public enum SeparatorType { LineSimple, LineWithShadow, NoLine };

    const string BACK_ICON_IOS = "⋖";
    const string BACK_ICON_DEFAULT = "◀";
    const string MENU_ICON = "▨";
    const string EXIT_ICON = "✕";
    const string SEARCH_ICON = "◯";

    public event EventHandler NavButtonOneClicked;
    public event EventHandler NavButtonTwoClicked;
    public event EventHandler NavButtonThreeClicked;

    public NavigationBar()
    {
        InitializeComponent();
        NavButtonOne.Clicked += NotifyNavButtonOneClicked;
        NavButtonTwo.Clicked += NotifyNavButtonTwoClicked;
        NavButtonThree.Clicked += NotifyNavButtonThreeClicked;

        Shell.Current.Navigating += Navigating;
        Shell.Current.Navigated += Navigated;
    }

    private void Navigated(object sender, ShellNavigatedEventArgs e)
    {
        InitValues();

        var modalStack = Shell.Current.Navigation.ModalStack.ToList();
        var navigationStack = Shell.Current.Navigation.NavigationStack.ToList();

        switch (e.Source)
        {
            case ShellNavigationSource.Push:
            case ShellNavigationSource.Pop:
                if (modalStack.Count == 0)
                    NavButtonOne.Text = DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.MacCatalyst
                      ? BACK_ICON_IOS
                      : BACK_ICON_DEFAULT;
                else
                {
                    NavButtonOne.IsVisible = false;
                    NavButtonThree.IsVisible = true;
                    Title.HorizontalTextAlignment = TextAlignment.Center;
                    Title.Margin = new Thickness(50, 0, 5, 0);
                }
                break;
            case ShellNavigationSource.ShellItemChanged:
            case ShellNavigationSource.PopToRoot:
                NavButtonOne.Text = MENU_ICON;
                break;
        }
    }

    private void Navigating(object sender, ShellNavigatingEventArgs e)
    {
        InitValues();
    }

    private void InitValues()
    {
        NavButtonOne.IsVisible = true;
        NavButtonThree.IsVisible = false;

        if (DeviceInfo.Platform != DevicePlatform.iOS)
            Title.HorizontalTextAlignment = TextAlignment.Start;

        Title.Margin = DeviceInfo.Platform == DevicePlatform.iOS
            ? new Thickness(5, 0)
            : new Thickness(15, 0, 5, 0);
    }


    #region _ NavBarTitle
    public static readonly BindableProperty NavBarTitleProperty =
      BindableProperty.Create(nameof(NavBarTitle),
          typeof(string),
          typeof(NavigationBar),
          defaultValue: string.Empty,
          defaultBindingMode: BindingMode.OneWay,
          coerceValue: NavBarTitleCoerceValue,
          propertyChanged: NavBarTitlePropertyChanged);

    public static object NavBarTitleCoerceValue(BindableObject bindable, object value)
    {
        var control = bindable as NavigationBar;

        string title = (string)value;
        control.Title.Text = title;
        //control.HandleNavIconOne(title);
        return title;
    }

    static void NavBarTitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        => bindable.CoerceValue(NavBarTitleProperty);

    public string NavBarTitle
    {
        get => (string)GetValue(NavBarTitleProperty);
        set => SetValue(NavBarTitleProperty, value);
    }
    #endregion

    #region _ NavBarBackgroundColor
    public static readonly BindableProperty NavBarBackgroundColorProperty =
        BindableProperty.Create(nameof(NavBarBackgroundColor),
        typeof(Color),
        typeof(NavigationBar),
        defaultValue: Colors.Transparent,
        defaultBindingMode: BindingMode.OneWay,
        coerceValue: NavBarBackgroundColorCoerceValue,
        propertyChanged: NavBarBackgroundColorPropertyChanged);

    public static object NavBarBackgroundColorCoerceValue(BindableObject bindable, object value)
    {
        var control = bindable as NavigationBar;
        control.NavBar.BackgroundColor = (Color)value;
        return (Color)value;
    }
    static void NavBarBackgroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        => bindable.CoerceValue(NavBarBackgroundColorProperty);

    public Color NavBarBackgroundColor
    {
        get => (Color)GetValue(NavBarBackgroundColorProperty);
        set => SetValue(NavBarBackgroundColorProperty, value);
    }
    #endregion

    #region _ NavBarTextColor . . .
    public static readonly BindableProperty NavBarTextColorProperty =
        BindableProperty.Create(nameof(NavBarTextColor),
            typeof(Color),
            typeof(NavigationBar),
            defaultValue: Colors.Gray,
            defaultBindingMode: BindingMode.TwoWay,
            coerceValue: NavBarTextColorCoerceValue,
            propertyChanged: NavBarTextColorPropertyChanged);

    public static object NavBarTextColorCoerceValue(BindableObject bindable, object value)
    {
        var control = bindable as NavigationBar;
        var color = (Color)value;

        control.Title.TextColor = color;
        control.NavButtonOne.TextColor = color;
        control.NavButtonTwo.TextColor = color;
        control.NavButtonThree.TextColor = color;
        control.NavBarActivityIndicator.Color = color;

        return color;
    }
    static void NavBarTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        => bindable.CoerceValue(NavBarTextColorProperty);

    public Color NavBarTextColor
    {
        get => (Color)GetValue(NavBarTextColorProperty);
        set => SetValue(NavBarTextColorProperty, value);
    }
    #endregion

    #region _ NavBarButtonOneIsVisible . . .
    public static readonly BindableProperty NavBarButtonOneIsVisibleProperty =
        BindableProperty.Create(nameof(NavBarButtonOneIsVisible),
            typeof(bool),
            typeof(NavigationBar),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay,
            coerceValue: NavBarButtonOneIsVisibleCoerceValue,
            propertyChanged: NavBarButtonOneIsVisiblePropertyChanged);

    public static object NavBarButtonOneIsVisibleCoerceValue(BindableObject bindable, object value)
    {
        var control = bindable as NavigationBar;
        var result = (bool)value;

        control.Title.Margin = DeviceInfo.Platform == DevicePlatform.iOS
            ? new Thickness(5, 0)
            : new Thickness(15, 0, 5, 0);

        if (!result)
            control.Title.Margin = DeviceInfo.Platform == DevicePlatform.iOS
             ? new Thickness(51, 0, 5, 0)
             : new Thickness(15, 0, 5, 0);

        control.NavButtonOne.IsVisible = result;

        return result;
    }
    static void NavBarButtonOneIsVisiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        => bindable.CoerceValue(NavBarButtonOneIsVisibleProperty);

    public bool NavBarButtonOneIsVisible
    {
        get => (bool)GetValue(NavBarButtonOneIsVisibleProperty);
        set => SetValue(NavBarButtonOneIsVisibleProperty, value);
    }
    #endregion

    #region _ NavBarSeparatorType . . .
    public static readonly BindableProperty NavBarSeparatorTypeProperty =
        BindableProperty.Create(nameof(NavBarSeparatorType),
            typeof(SeparatorType),
            typeof(NavigationBar),
            defaultValue: SeparatorType.LineWithShadow,
            defaultBindingMode: BindingMode.TwoWay,
            coerceValue: NavBarSeparatorTypeCoerceValue,
            propertyChanged: NavBarSeparatorTypePropertyChanged);

    public static object NavBarSeparatorTypeCoerceValue(BindableObject bindable, object value)
    {
        var control = bindable as NavigationBar;
        var result = (SeparatorType)value;

        control.NavBarSeparator.IsVisible = true;
        control.NavBarSeparator.HeightRequest = 0.5;

        switch ((SeparatorType)value)
        {
            case SeparatorType.LineSimple:
                control.NavBarSeparator.HeightRequest = 1;
                control.NavBarSeparator.Shadow = null;
                break;
            case SeparatorType.LineWithShadow:
                control.NavBarSeparator.Shadow = new()
                {
                    Brush = AppInfo.RequestedTheme == AppTheme.Dark
                        ? Color.FromArgb("#60ffffff")
                        : Color.FromArgb("#50000000"),
                    Opacity = 0.7f,
                    Radius = 3,
                    Offset = new Point(0, 2)
                };
                break;
            case SeparatorType.NoLine:
                control.NavBarSeparator.IsVisible = false;
                break;
            default:
                break;
        }

        return result;
    }
    static void NavBarSeparatorTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        => bindable.CoerceValue(NavBarButtonOneIsVisibleProperty);

    public SeparatorType NavBarSeparatorType
    {
        get => (SeparatorType)GetValue(NavBarSeparatorTypeProperty);
        set => SetValue(NavBarSeparatorTypeProperty, value);
    }
    #endregion

    public void NotifyNavButtonOneClicked(object sender, EventArgs e)
    {
        if (NavButtonOneClicked is not null)
        {
            NavButtonOneClicked(sender, e);
            return;
        }

        if (NavButtonOne.Text.Equals(BACK_ICON_IOS) || NavButtonOne.Text.Equals(BACK_ICON_DEFAULT))
            Shell.Current.GoToAsync("..");

        else if (NavButtonOne.Text.Equals(MENU_ICON))
        {
            Shell.Current.FlyoutIsPresented = true;
            if (DeviceInfo.Platform == DevicePlatform.Android) //resolve bug in the initial launch of Flyout in Droid
                Shell.Current.CurrentPage.Layout(new Rect(0, 0, Shell.Current.CurrentPage.Width + 1, Shell.Current.CurrentPage.Height + 1));
        }
    }

    public void NotifyNavButtonTwoClicked(object sender, EventArgs e)
    {
        if (NavButtonTwoClicked is not null)
        {
            NavButtonTwoClicked(sender, e);
            return;
        }
    }

    public async void NotifyNavButtonThreeClicked(object sender, EventArgs e)
    {
        if (NavButtonThreeClicked is not null)
        {
            NavButtonThreeClicked(sender, e);
            return;
        }

        await Shell.Current.GoToAsync("..");
    }

    private void HandleNavIconOne(string title)
    {
        bool isFlyoutPage = false;

        foreach (IEnumerable<object> flyoutItems in Shell.Current.FlyoutItems)
            foreach (Microsoft.Maui.Controls.ShellItem flyoutItem in flyoutItems)
                if (title == flyoutItem.Title || Title.Text == flyoutItem.Title)
                {
                    isFlyoutPage = true;
                    break;
                }

        if (isFlyoutPage)
            NavButtonOne.Text = MENU_ICON;
        else
            NavButtonOne.Text = DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.MacCatalyst
            ? BACK_ICON_IOS
            : BACK_ICON_DEFAULT;
    }
}