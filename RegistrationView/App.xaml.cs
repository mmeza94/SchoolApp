using RegistrationView.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RegistrationView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender,StartupEventArgs e)
        {
            var login = new Login();
            login.IsVisibleChanged += (s, ev) =>
            {
                if(login.IsVisible==false && login.IsLoaded)
                {
                    var mainView = new MainWindow();
                    mainView.Show();
                    login.Close();
                }
            };
        }
    }
}
