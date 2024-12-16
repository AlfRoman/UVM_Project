using UVM_Project.ViewModels;

namespace UVM_Project
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }

    }

}
