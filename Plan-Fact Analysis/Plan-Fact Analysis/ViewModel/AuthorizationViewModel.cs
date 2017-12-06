using System.Linq;
using PlanFactAnalysis.Model;
using System.Collections.Generic;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class AuthorizationViewModel : PropertyChangedBase
    {
        IList<User> _users;
        User _loggedUser;

        public AuthorizationViewModel (IList<User> users)
        {
            _users = users;
            _registeringUser = new UserViewModel (this);

            _authorizeCommand = new RelayCommand (param =>
            {
                Authorize ( );
            },
            param => !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace (Password));
        }

        public string Name => _loggedUser?.Name;
        public UserRole Role => _loggedUser == null ? UserRole.None : _loggedUser.Role;
        
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
            }
            else
                Authorized = false;
        }

        public bool CanRegister (string login)
        {
            return _users.FirstOrDefault (u => u.Login == login) == null;
        }

        public void Register (string login, string name, string password, UserRole role)
        {
            if (CanRegister (login))
            {
                User newUser = new User (name, login, password, role);
                _users.Add (newUser);
            }
        }
        
        public bool Authorized { get; set; }

        public void LogOff()
        {
            Authorized = false;
            Login = string.Empty;
            Password = string.Empty;
        }

        public UserRole LoggedUserRole => _loggedUser.Role;

        readonly UserViewModel _registeringUser;
        public UserViewModel RegisteringUser => _registeringUser;

        readonly RelayCommand _authorizeCommand;
        public RelayCommand AuthorizeCommand => _authorizeCommand;
    }
}
