using TODOApp.ViewModels;

namespace TODOApp.Views;

public partial class Completed : ContentPage
{
	public Completed()
	{
		InitializeComponent();
		BindingContext = new TaskViewModel();
    }
}
