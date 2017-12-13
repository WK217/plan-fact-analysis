using PlanFactAnalysis.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class RegistrationViewModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly IList<User> _users;

        public RegistrationViewModel (IList<User> users)
        {
            _users = users;

            _registerCommand = new RelayCommand (param =>
            {
                Register ( );
            },
            param => !string.IsNullOrWhiteSpace (Login) && !string.IsNullOrWhiteSpace (Password) &&
                !string.IsNullOrWhiteSpace (Name) && CanRegister (Login) && CanRegister (Role));
        }

        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public IEnumerable<UserRole> PossibleRoles
        {
            get
            {
                yield return UserRole.None;
                yield return UserRole.Planner;
                yield return UserRole.Executor;
                yield return UserRole.Manager;
            }
        }

        public bool CanRegister (string login)
        {
            return _users.FirstOrDefault (u => u.Login == login) == null;
        }

        public bool CanRegister (UserRole role)
        {
            if (role == UserRole.Manager)
            {
                var managers = from user in _users
                               where user.Role == UserRole.Manager
                               select user;

                return managers.Count ( ) == 0;
            }
            else
                return role != UserRole.None;
        }

        public void Register ( )
        {
            Register (Login, Name, Password, Role);
        }

        public void Register (string login, string name, string password, UserRole role)
        {
            if (CanRegister (login))
            {
                User newUser = new User (name, login, password, role);
                _users.Add (newUser);

                Login = string.Empty;
                Name = string.Empty;
                Role = UserRole.None;
            }

            Password = string.Empty;
        }
        
        public bool Registered { get; set; }

        public string Error => throw new System.NotImplementedException ( );
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof (Name):
                        if (string.IsNullOrWhiteSpace (Name))
                            return "Имя нового пользователя не должно быть пустым.";
                        break;
                    case nameof (Login):
                        if (string.IsNullOrWhiteSpace (Login))
                            return "Логин нового пользователя не должен быть пустым.";

                        if (!CanRegister (Name))
                            return "Пользователь с таким логином уже существует.";
                        break;
                    case nameof (Password):
                        if (string.IsNullOrWhiteSpace (Password))
                            return "Пароль нового пользователя не должен быть пустым.";
                        break;
                    case nameof (Role):
                        if (Role == UserRole.None)
                            return "Роль нового пользователя должна быть определена.";
                        else
                            if (!CanRegister (Role))
                                return "В базе не может быть более одного руководителя.";
                        break;
                }

                return string.Empty;
            }
        }

        readonly RelayCommand _registerCommand;
        public RelayCommand RegisterCommand => _registerCommand;
    }
}