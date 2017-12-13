using System.Linq;
using PlanFactAnalysis.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class AuthorizationViewModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly MainViewModel _context;
        readonly RegistrationViewModel _registration;
        public RegistrationViewModel Registration => _registration;

        readonly IList<User> _users;
        User _loggedUser;

        public AuthorizationViewModel (MainViewModel context, IList<User> users)
        {
            _context = context;
            _users = users;

            _registration = new RegistrationViewModel (users);

            _authorizeCommand = new RelayCommand (param =>
            {
                Authorize ( );
            },
            param => !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace (Password));
        }

        public string Name
        {
            get => _loggedUser?.Name;
            set
            {
                if (_loggedUser != null)
                    _loggedUser.Name = value;
            }
        }
        public UserRole Role
        {
            get => _loggedUser == null ? UserRole.None : _loggedUser.Role;
            set
            {
                if (_loggedUser != null)
                    _loggedUser.Role = value;
            }
        }

        public string Login { get; set; }
        public string Password { get; set; }

        public void Authorize ( )
        {
            User user = _users.FirstOrDefault (u => u.Login == Login && u.Verify(Password));

            if (user != null)
                Authorize (user);
            else
                Authorized = false;
        }

        public void Authorize (User user)
        {
            if (_users.FirstOrDefault (u => u == user) != null)
            {
                _loggedUser = user;

                Authorized = true;

                RaisePropertyChanged (nameof (Name));
                RaisePropertyChanged (nameof (Role));

                Login = string.Empty;
                Password = string.Empty;
                
                _context.PlanRegistry.UpdateAllProperties ( );
                _context.FactRegistry.UpdateAllProperties ( );
                _context.PlanFactTable.UpdateAllProperties ( );
            }
            else
                Authorized = false;
        }

        public bool Authorized { get; set; }

        public void Logout()
        {
            Authorized = false;
            _loggedUser = null;
            RaisePropertyChanged (nameof (Name));
            RaisePropertyChanged (nameof (Role));
        }

        readonly RelayCommand _authorizeCommand;
        public RelayCommand AuthorizeCommand => _authorizeCommand;

        public string Error => throw new System.NotImplementedException ( );
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof (Login):
                        if (string.IsNullOrWhiteSpace (Login))
                            return "Вводимый логин не должен быть пустым.";
                        break;
                    case nameof (Password):
                        if (string.IsNullOrWhiteSpace (Password))
                            return "Вводимый пароль не должен быть пустым.";
                        break;
                    default:
                        break;
                }

                return string.Empty;
            }
        }
    }
}
