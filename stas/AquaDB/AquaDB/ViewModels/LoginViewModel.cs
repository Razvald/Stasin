using AquaDB.Database;
using AquaDB.Models;
using AquaDB.View;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace AquaDB.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _login;
        private string _password;

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginAction);
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; set; }

        private void LoginAction(object parameter)
        {
            var dbContext = DbContextProvider.Instance.GetDbContext();
            var user = dbContext.AppUsers.FirstOrDefault(u => u.Name == Login && u.Password == Password);
            if (user != null)
            {
                MainWindow mainWindow = new();
                mainWindow.Show();
                ((Window)parameter).Close();
            }
            else
            {
                MessageBox.Show("Ошибка входа. Проверьте логин и пароль.");
            }
        }
    }
}
