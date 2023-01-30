using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MahApps;
using MahApps.Metro.IconPacks;

namespace SchoolAppViewModel.Views
{
    public class MainViewModel:ViewModelBase
    {

        #region Private Properties
        private ViewModelBase _currentChildView;
        private string _caption;
        private PackIconMaterial _icon;

        #endregion


        #region Public Properties

        public PackIconMaterial Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                if (_icon == value) return;
                _icon=value;
                onPropertyChanged("Icon");
            }
        }



        public ViewModelBase CurrentChildView 
        { 
            get
            {
                return _currentChildView;
            }
            set
            {
                if (_currentChildView == value) return;
                _currentChildView = value;
                onPropertyChanged("CurrentChildView");
            }
        }

        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                if (_caption == value) return;
                _caption = value;
                onPropertyChanged("Caption");
            }
        }

        #endregion



        public MainViewModel()
        {
            Icon= new PackIconMaterial();
            GetProfileView();
        }



        #region Private Commands

        private ICommand profileCommand;
        private ICommand calendarCommand;
        private ICommand dashboardCommand;
        private ICommand financeCommand;
        private ICommand chatCommand;



        #endregion



        #region Public Commands
        public ICommand ProfileCommand
        {
            get
            {
                if (profileCommand == null)
                {
                    profileCommand = new RelayCommand(param => GetProfileView());
                }
                return profileCommand;
            }
        }

        public ICommand DashboardCommand
        {
            get
            {
                if (dashboardCommand == null)
                {
                    dashboardCommand = new RelayCommand(param => GetDashboardView());
                }
                return dashboardCommand;
            }
        }


        public ICommand CalendarCommand
        {
            get
            {
                if (calendarCommand == null)
                {
                    calendarCommand = new RelayCommand(param => GetCalendarView());
                }
                return calendarCommand;
            }
        }

        public ICommand FinanceCommand
        {
            get
            {
                if (financeCommand == null)
                {
                    financeCommand = new RelayCommand(param => GetFinanceView());
                }
                return financeCommand;
            }
        }

        

        public ICommand ChatCommand
        {
            get
            {
                if (chatCommand == null)
                {
                    chatCommand = new RelayCommand(param => GetChatView());
                }
                return chatCommand;
            }
        }







        #endregion



        #region CommandExecute

        private void GetChatView()
        {
            CurrentChildView = new ChatViewModel();
            Caption = "Chat";
            Icon.Kind = PackIconMaterialKind.ForumOutline;
        }

        private void GetFinanceView()
        {
            CurrentChildView = new FinanceViewModel();
            Caption = "Finanzas";
            Icon.Kind = PackIconMaterialKind.CashMultiple;
        }

        private void GetProfileView()
        {
            CurrentChildView = new ProfileViewModel();
            Caption = "Perfil";
            Icon.Kind = PackIconMaterialKind.HomeVariant;
        }

        private void GetDashboardView()
        {
            CurrentChildView = new DashboardViewModel();
            Caption = "Dashboard";
            Icon.Kind = PackIconMaterialKind.AccountMultipleOutline;
        }

        private void GetCalendarView()
        {
            CurrentChildView=new CalendarViewModel();
            Caption = "Calendario";
            Icon.Kind = PackIconMaterialKind.CalendarClock;
        }





        #endregion



    }
}
