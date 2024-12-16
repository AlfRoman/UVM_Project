using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using UVM_Project.DataAccess;
using UVM_Project.DTOs;
using UVM_Project.Utilities;
using UVM_Project.Models;
using System.Collections.ObjectModel;
using UVM_Project.Views;

namespace UVM_Project.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly UserDbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<UserDTO> userList = new ObservableCollection<UserDTO>();

        public MainViewModel(UserDbContext dbContext)
        {
            _dbContext = dbContext;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await Get()));

            WeakReferenceMessenger.Default.Register<UserMessaging>(this, (r, m) =>
            {
                UserMessageReceived(m.Value);
            });
        }

        public async Task Get()
        {
            var userList = await _dbContext.Users.ToListAsync();
            if (UserList.Any())
            {
                foreach (var user in userList)
                {
                    UserList.Add(new UserDTO()
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        UserLastname = user.UserLastname,
                        Email = user.Email,
                        Department = user.Department,
                        CreatedDate = user.CreatedDate,
                    });
                }
            }
        }
        private void UserMessageReceived(UserMessage userMessage)
        {
            var userDto = userMessage.UserDTO;

            if (userMessage.IsToCreate)
            {
                UserList.Add(userDto);
            }
            else
            {
                var userFound = UserList.FirstOrDefault(e => e.UserId == userDto.UserId);

                userFound.UserName = userDto.UserName;
                userFound.UserLastname = userDto.UserLastname;
                userFound.Email = userDto.Email;
                userFound.Department = userDto.Department;
                userFound.CreatedDate = userDto.CreatedDate;
            }
        }

        [RelayCommand]
        private async Task Create()
        {
            var uri = $"{nameof(UserPage)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Edit(UserDTO userDTO)
        {
            var uri = $"{nameof(UserPage)}?id={userDTO.userId}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Delete(UserDTO userDTO)
        {
            bool response = await Shell.Current.DisplayAlert("Message", "Do you want to delete this user?", "Yes", "No");

            if (response)
            {
                var userFound = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userDTO.UserId);
                _dbContext.Users.Remove(userFound);
                await _dbContext.SaveChangesAsync();
                userList.Remove(userDTO);
            }

        }
    }
}
