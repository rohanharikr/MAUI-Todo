using TODOApp.ViewModels;

namespace TODOApp.Views;

public partial class Completed : ContentPage
{
	public Completed(TaskViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}
