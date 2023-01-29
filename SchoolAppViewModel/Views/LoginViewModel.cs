using LoginModel;
using SchoolAppViewModel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace SchoolAppViewModel.Views
{
    public class LoginViewModel:ViewModelBase
    {


        #region Private Properties
        private string email;
        private string password;
        private PasswordBox loginpasswordBox;
        private bool popupVisibilitySuccess;
        private bool popupVisibilityError;
        private string errorMessage;
        private string successMessage;
        private bool isViewVisible;
        #endregion



        #region Public Properties


     

        public bool IsViewVisible
        {
            get { return isViewVisible; }
            set 
            {
                if (IsViewVisible == value) return;
                isViewVisible = value;
                onPropertyChanged("IsViewVisible");
            }
        }





        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if (errorMessage == value) return;
                errorMessage = value;
                onPropertyChanged("ErrorMessage");
            }
        }

        public string SuccessMessage
        {
            get { return successMessage; }
            set
            {
                if (successMessage == value) return;
                successMessage = value;
                onPropertyChanged("SuccessMessage");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (email == value) return;
                email = value;
                onPropertyChanged("Email");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password == value) return;
                password = value;
                onPropertyChanged("Password");
            }
        }

        public PasswordBox LoginPasswordBox
        {
            get { return loginpasswordBox; }
            set
            {
                if (loginpasswordBox == value) return;
                loginpasswordBox = value;
                onPropertyChanged("LoginPasswordBox");
            }
        }


        public bool PopupVisibilitySuccess
        {
            get { return popupVisibilitySuccess; }
            set
            {
                if (popupVisibilitySuccess == value) return;
                popupVisibilitySuccess = value;
                onPropertyChanged("PopupVisibilitySuccess");
            }
        }


        public bool PopupVisibilityError
        {
            get { return popupVisibilityError; }
            set
            {
                if (popupVisibilityError == value) return;
                popupVisibilityError = value;
                onPropertyChanged("PopupVisibilityError");
            }
        }



        #endregion




        public DispatcherTimer timer;
        public Validations Util { get; set; }


        #region Constructor
        public LoginViewModel()
        {
            timer = new DispatcherTimer();
        }


        #endregion


        #region Commands

        private ICommand signUp;
        private ICommand signIn;
        private ICommand closeApp;

        #endregion


        #region Public Commands



        public ICommand SignUp
        {
            get
            {
                if (signUp == null)
                {
                    signUp = new RelayCommand(PasswordBox => this.RegisterNewUser(PasswordBox));
                }
                return signUp;
            }
        }

        public ICommand SignIn
        {
            get
            {
                if (signIn == null)
                {
                    signIn = new RelayCommand(param => this.PopupExecute(param));
                }
                return signIn;
            }
        }

        public ICommand CloseApp
        {
            get
            {
                if (closeApp == null)
                {
                    closeApp = new RelayCommand(param => this.CloseApplication(param));
                }
                return closeApp;
            }
        }

        private void CloseApplication(object param)
        {
            Application.Current.Shutdown();
        }






        #endregion





        #region CommandExecute



        private void PopupExecute(object param)
        { 
            ShowPopUpSuccess("Registro Existoso!");
            this.IsViewVisible = false;
        }

        private void RegisterNewUser(object PasswordBox)
        {
            Task.Run(() =>
            {


                Util = new Validations(Email, (PasswordBox)PasswordBox, this);


                if (!Util.IsEmailValid())
                {
                    ShowPopUpError("Email Inválido");
                    return;
                }


                if (!Util.ValidatePassword())
                {
                    var Parameters = Helpers.GenerateParameters(Email, PasswordBox);
                    DataAccess.Instance.RegisterNewUser(Parameters);
                    ShowPopUpSuccess("Registro Existoso!");
                }

                return;

            })
            .ContinueWith(t =>
            {
                this.CleanTextBox(PasswordBox);
            });



        }

        #endregion





        #region Methods

        private void CleanTextBox(object PasswordBox)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Email = string.Empty;
                ((PasswordBox)PasswordBox).Password = string.Empty;

            }));
        }

        public void ShowPopUpError(string message)
        {
            ErrorMessage = message;

            PopupVisibilityError = true;

            timer.Interval = TimeSpan.FromSeconds(1.5);
            timer.Tick += new EventHandler(ErrorTimer_Tick);
            timer.Start();
        }


        public void ShowPopUpSuccess(string message)
        {
            SuccessMessage = message;// "Registro Existoso!";

            PopupVisibilitySuccess = true;

            timer.Interval = TimeSpan.FromSeconds(1.5);
            timer.Tick += new EventHandler(SuccessTimer_Tick);
            timer.Start();
        }




        public void ErrorTimer_Tick(object? sender, EventArgs e)
        {
            PopupVisibilityError = false;
            timer.Stop();
        }

        public void SuccessTimer_Tick(object? sender, EventArgs e)
        {
            PopupVisibilitySuccess = false;
            timer.Stop();
        }




        #endregion



    }
}
