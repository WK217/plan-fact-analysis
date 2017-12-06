using System.Timers;
using System.Windows;

namespace PlanFactAnalysis.View
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView ( )
        {
            InitializeComponent ( );
        }

        void PasswordBoxTextChanged (object sender, RoutedEventArgs e)
        {
            AuthPassTextBox.Text = AuthPassBox.Password;
        }

        void PasswordTextBoxChanged (object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (AuthPassBox.Password != AuthPassTextBox.Text)
                AuthPassBox.Password = AuthPassTextBox.Text;
        }

        //void AuthHyperlinkClicked (object sender, RoutedEventArgs e)
        //{
        //    LaunchAnimation (0.0, 1.0);
        //}

        //void RegisterHyperlinkClicked (object sender, RoutedEventArgs e)
        //{
        //    LaunchAnimation (1.0, 0.0);
        //}

        //void LaunchAnimation(double authGridOpacity, double registerGridOpacity)
        //{
        //    Auth.IsEnabled = false;
        //    Register.IsEnabled = false;

        //    Duration duration = new Duration (new TimeSpan (0, 0, 0, 0, 500));

        //    DoubleAnimation authGridAnimation = new DoubleAnimation (
        //        authGridOpacity,
        //        duration);

        //    if (authGridAnimation.To == 1.0)
        //    {
        //        authGridAnimation.BeginTime = duration.TimeSpan;
        //        authGridAnimation.Completed += (s, e) => { Auth.IsEnabled = true; };
        //    }

        //    Auth.BeginAnimation (OpacityProperty, authGridAnimation);

        //    DoubleAnimation registerGridAnimation = new DoubleAnimation (
        //        registerGridOpacity,
        //        duration);

        //    if (registerGridAnimation.To == 1.0)
        //    {
        //        registerGridAnimation.BeginTime = duration.TimeSpan;
        //        registerGridAnimation.Completed += (s, e) => { Register.IsEnabled = true; };
        //    }

        //    Register.BeginAnimation (OpacityProperty, registerGridAnimation);

        //    Panel.SetZIndex (Auth, authGridAnimation.To == 1.0 ? 1 : 0);
        //    Panel.SetZIndex (Register, registerGridAnimation.To == 1.0 ? 1 : 0);
        //}
    }
}
