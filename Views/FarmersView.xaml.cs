namespace Anno1800.Views;

public partial class FarmersView : ContentPage
{
    public FarmersView(FarmersViewModel farmersViewModel)
    {
        InitializeComponent();
        BindingContext = farmersViewModel;
    }

    private async void OnCalculateButtonClicked(object sender, EventArgs e)
    {
        // �������� "�������"
        //await CalculateButton.ScaleTo(0.95, 50, Easing.CubicIn);
        //await CalculateButton.ScaleTo(1.0, 50, Easing.CubicOut);

        // ��������� ������� ����� ��������
        //if (CalculateButton.Command?.CanExecute(null) == true)
        //{
        //CalculateButton.Command.Execute(null);
        //}
    }
}