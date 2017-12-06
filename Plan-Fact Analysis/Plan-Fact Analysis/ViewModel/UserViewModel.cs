using PlanFactAnalysis.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class UserViewModel: PropertyChangedBase
    {
        readonly AuthorizationViewModel _authorization;

        public UserViewModel (AuthorizationViewModel authorization)
        {
            _authorization = authorization;

            _registerCommand = new RelayCommand (param =>
            {
                _authorization.Register (Login, Name, Password, Role);
            },
            param => !string.IsNullOrWhiteSpace (Login) && !string.IsNullOrWhiteSpace (Name) && !string.IsNullOrWhiteSpace (Password) && Role != UserRole.None);
        }

        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public IEnumerable<UserRole> PossibleRoles
        {
            get
            {
                yield return UserRole.Planner;
                yield return UserRole.Executor;
                yield return UserRole.Manager;
            }
        }

        readonly RelayCommand _registerCommand;
        public RelayCommand RegisterCommand => _registerCommand;
    }
}