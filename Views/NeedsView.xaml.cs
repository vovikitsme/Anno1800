namespace Anno1800.Views;

public partial class NeedsView : ContentPage
{
    public NeedsView(NeedsViewModel needsViewModel)
    {
        InitializeComponent();
        BindingContext = needsViewModel;
    }
}