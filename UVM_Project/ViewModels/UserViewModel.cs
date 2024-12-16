using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using UVM_Project.DataAccess;
using UVM_Project.DTOs;
using UVM_Project.Utilities;
using UVM_Project.Models;

namespace UVM_Project.ViewModels
{
    public partial class UserViewModel : ObservableObject, IQueryAttributable
    {
        private readonly UserDbContext _dbContext;

        [ObservableProperty]
        private UserDTO userDTO = new UserDTO();

        [ObservableProperty]
        private string pageTitle;

        private int _userID;

        [ObservableProperty]
        private bool loadingIsVisible = false;

        public UserViewModel(UserDbContext dbContext)
        {
            _dbContext = dbContext;
            UserDTO.CreatedDate = DateTime.Now;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var userId = int.Parse(query["id"].ToString());
            _userID = userId;

            if (_userID == 0)
            {
                pageTitle = "Nuevo Usuario";
            }
            else
            {
                pageTitle = "Editar Usuario";
                loadingIsVisible = true;
                await Task.Run(async () =>
                {
                    var userFound = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == _userID);

                    UserDTO.UserId = userFound.UserId;
                    UserDTO.UserName = userFound.UserName;
                    UserDTO.UserLastname = userFound.UserLastname;
                    UserDTO.Email = userFound.Email;
                    UserDTO.Department = userFound.Department;

                    MainThread.BeginInvokeOnMainThread(() => { loadingIsVisible = false; });
                });
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            loadingIsVisible = true;
            UserMessage message = new UserMessage();

            await Task.Run(async () =>
            {
                if (_userID == 0)
                {
                    var user = new User()
                    {
                        UserName = UserDTO.UserName,
                        UserLastname = UserDTO.UserLastname,
                        Email = UserDTO.Email,
                        Department = UserDTO.Department
                    };

                    _dbContext.Users.Add(user);
                    await _dbContext.SaveChangesAsync();

                    UserDTO.UserId = user.UserId;
                    message = new UserMessage()
                    {
                        IsToCreate = true,
                        UserDTO = UserDTO,
                    };
                }
                else
                {
                    var userFound = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == _userID);

                    userFound.UserName = UserDTO.UserName;
                    userFound.UserLastname = UserDTO.UserLastname;
                    userFound.Email = UserDTO.Email;
                    userFound.Department = UserDTO.Department;

                    await _dbContext.SaveChangesAsync();
                    message = new UserMessage()
                    {
                        IsToCreate = false,
                        UserDTO = UserDTO,
                    };
                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    loadingIsVisible = false;
                    WeakReferenceMessenger.Default.Send(new UserMessaging(message));
                    await Shell.Current.Navigation.PopAsync();
                });
            });
        }
    }
}

