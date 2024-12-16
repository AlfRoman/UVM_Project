using UVM_Project.ViewModels;

namespace UVM_Project.Views;

public partial class UserPage : ContentPage
{
	public UserPage(UserViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel;
	}
}