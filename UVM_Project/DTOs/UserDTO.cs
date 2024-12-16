using CommunityToolkit.Mvvm.ComponentModel;

namespace UVM_Project.DTOs
{
    public partial class UserDTO : ObservableObject
    {
        [ObservableProperty]
        public int userId;
        [ObservableProperty]
        public string userName;
        [ObservableProperty]
        public string userLastname;
        [ObservableProperty]
        public string email;
        [ObservableProperty]
        public string department;
        [ObservableProperty]
        public DateTime createdDate;
    }
}
