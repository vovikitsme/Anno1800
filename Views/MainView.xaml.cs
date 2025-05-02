namespace Anno1800.Views;

public partial class MainView : ContentPage
{
    private readonly MainViewModel _mainViewModel;

    public MainView(MainViewModel mainViewModel)
    {
        InitializeComponent();
        BindingContext = mainViewModel;
        _mainViewModel = mainViewModel;
    }

    private async void OldWorldButton_Pressed(object sender, EventArgs e)
    {
        if (_mainViewModel?.NavigateToFarmersCommand?.CanExecute(null) == true)
        {
            await AnimateButton(OldWorldButton);
            _mainViewModel.NavigateToFarmersCommand.Execute(null);
        }
    }

    private async void NewWorldButton_Pressed(object sender, EventArgs e)
    {
        await AnimateButton(NewWorldButton);

        // Здесь будет логика для Нового Света
    }

    private async Task AnimateButton(Button button)
    {
        await button.ScaleTo(1.1, 300);  // Увеличить на 10% за 100 мс
        await button.ScaleTo(1.0, 300);  // Вернуть обратно за 100 мс
    }
}