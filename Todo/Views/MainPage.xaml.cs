using TODOApp.ViewModels;

namespace TODOApp;

public partial class MainPage : ContentPage
{
	public MainPage(TaskViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

		//focus todo input after adding todo
		TodoEntry.Loaded += FocusTodoEntry; //on load
        AddTodoButton.Clicked += FocusTodoEntry; //on add todo button click
		TodoEntry.TextChanged += FocusTodoEntry; //this is a hack to focus after hitting enter key
    }

	private void FocusTodoEntry(object sender, EventArgs e) => TodoEntry.Focus();
}