using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;
using ToDoist.Services;
using System.Collections.ObjectModel;
using ToDoist.Models;
using Windows.Networking.Connectivity;
using ToDoist.Interface;
using Windows.UI.Xaml.Input;
using System.Diagnostics;

namespace ToDoist.ViewModels
{
    public class MainPageViewModel : ViewModelBase, IMainPageViewModel
    {

        #region instances
        private readonly IToDoistApiClient _ToDoistApiClient;
        private readonly IDataService _DataService;
        #endregion

        #region fields
        private List<TodoistTask> itemListResource;
        private bool progressBarIsActive;
        private string errorMessage;
        #endregion

        #region properties
        public List<TodoistTask> ItemListResource
        {
            get => itemListResource;
            set => SetProperty(ref itemListResource, value);
        }
        public bool ProgressBarIsActive
        {
            get => progressBarIsActive;
            set => SetProperty(ref progressBarIsActive,value);
        }
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage,value);
        }

        #endregion

        #region Constructor
        public MainPageViewModel(IToDoistApiClient toDoistApiClient, IDataService dataServices)
        {
            _ToDoistApiClient = toDoistApiClient;
            _DataService = dataServices;
        }
        #endregion

        #region override methods
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);   
            //sync data automatically from background
            SyncData();
        }
        #endregion

        #region private methods
        /// <summary>
        /// Syncdata from internet or load from local when having no internet connection
        /// </summary>
        private async void SyncData()
        {
            //clear error message
            ErrorMessage = String.Empty;

            //check network
            if (IsInternet())
            {
                //show progress bar
                ProgressBarIsActive = true;
                //load data from the api
                var data = await _ToDoistApiClient.GetItemsAzync();
                if (data == null || data?.Count == 0)
                {
                    ErrorMessage = "You don't have any task. Please go to Todoist website to add more task";
                    //hide progress bar after data rendered
                    ProgressBarIsActive = false;
                    return;
                }

                //render data
                ItemListResource = data.OrderBy(x => x.content).ToList();

                //hide progress bar after data rendered
                ProgressBarIsActive = false;

                //save data to local
                for(int i = 0; i < 100; i ++)
                    _DataService.SaveData(data);
            }
            else
            {
                //show progessbar
                ProgressBarIsActive = true;

                //check local data
                var data = await _DataService.LoadData();
                if (data != null)
                {
                    //render data from local
                    ItemListResource = data;
                    ErrorMessage = "Offline mode! you are using local data";
                }
                else
                {
                    //notify when running offline without local data
                    ErrorMessage = "you are offine and there are no local data. Check your internet connection and sync again";
                }

                //hide progessbar
                ProgressBarIsActive = false;
            }


        }

        /// <summary>
        /// checking internet status 
        /// </summary>
        /// <returns></returns>
        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }
        #endregion

        #region event methods
        /// <summary>
        /// SyncButton tapped event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SyncButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //call sync data method
            SyncData();
        }
        #endregion
    }
}
