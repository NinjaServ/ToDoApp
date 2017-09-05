using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using ToDoApp.Models;


namespace ToDoApp.ViewModels
{
    public class AddItem_ViewModel : BindableBase, IConfirmNavigationRequest //INavigationAware,
    {

        public DateTime todayDate { get; set; } = DateTime.Now;

        IRegionManager _regionManager;
        IRegionNavigationJournal _journal;

        public DelegateCommand<ToDoItem> ItemSelectedCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; set; }
        public DelegateCommand GoBackCommand { get; set; }


        private string _title = "ToDo Application - Add Item";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }


        internal ObservableCollection<ToDoItem> _ToDoItemList;
        public ObservableCollection<ToDoItem> ToDoItemList
        {
            get { return _ToDoItemList; }
            set
            {
                SetProperty(ref _ToDoItemList, value);
                //if (value != _ToDoItemList)
                //{
                //    _ToDoItemList = value;
                //}
            }
        }


        //public AddItem_ViewModel()
        //{

        //}

        public AddItem_ViewModel(RegionManager regionManager)
        {
            _regionManager = regionManager;

            ItemSelectedCommand = new DelegateCommand<ToDoItem>(ItemSelected);
            CreateItems();

            GoForwardCommand = new DelegateCommand(GoForward, CanGoForward);
            GoBackCommand = new DelegateCommand(GoBack);
        }

        private void ItemSelected(ToDoItem item)
        {
            var parameters = new NavigationParameters();
            parameters.Add("ToDoItem", item);

            if (item != null)
                _regionManager.RequestNavigate("ContentRegion", "ItemList_View", parameters);
        }

        private void CreateItems()
        {
            var itemList = new ObservableCollection<ToDoItem>();
            itemList.Add(new ToDoItem("Item1", "These are Details 1", DateTime.Now, DateTime.Now.AddDays(1.0)));
            itemList.Add(new ToDoItem("Item2", "These are Details 2", DateTime.Now, DateTime.Now.AddDays(1.0)));
            itemList.Add(new ToDoItem("Item3", "These are Details 3", DateTime.Now, DateTime.Now.AddDays(1.0)));
            itemList.Add(new ToDoItem("aItem4", "Oh Details 4", DateTime.Now, DateTime.Now.AddDays(1.0)));
            itemList.Add(new ToDoItem("Item 5", "For 5 details are here", DateTime.Now, DateTime.Now.AddDays(1.0)));
            itemList.Add(new ToDoItem("zItem6", "Some 6 details are ny", DateTime.Now, DateTime.Now.AddDays(1.0)));

            _ToDoItemList = itemList;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            GoForwardCommand.RaiseCanExecuteChanged();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = true;

            if (MessageBox.Show("Do you want to Exit without saving?", "Navigate?", MessageBoxButton.YesNo) == MessageBoxResult.No) 
                result = false;

            continuationCallback(result);

            ////Window.ShowDialog();
            // Instantiate window
            //DialogBox dialogBox = new DialogBox();

            // Show window modally
            // NOTE: Returns only when window is closed
            //Nullable<bool> dialogResult = dialogBox.ShowDialog();

        }


        private void GoBack()
        {
            _journal.GoBack();
        }

        private void GoForward()
        {
            _journal.GoForward();
        }

        private bool CanGoForward()
        {
            return _journal != null && _journal.CanGoForward;
        }


        public DateTime DateToday()
        {
            return DateTime.Now;
        }

        


        ~AddItem_ViewModel()
        {
            _ToDoItemList = null;
        }
    }
}



//< prism:InteractionRequestTrigger SourceObject = "{Binding CustomPopupViewRequest, Mode=OneWay}" >
//    < prism:PopupWindowAction >
//        < prism:PopupWindowAction.WindowContent >
//            < views:CustomPopupView />
//        </ prism:PopupWindowAction.WindowContent >
//    </ prism:PopupWindowAction >
// </ prism:InteractionRequestTrigger >

//        < local:PopupPanel
// Content = "{Binding PopupContent}"
//local: PopupPanel.PopupParent = "{Binding ElementName=SomeParentPanel}"
//local: PopupPanel.IsPopupVisible = "{Binding IsPopupVisible}" >


// < local:PopupPanel.Resources >

//      < DataTemplate DataType = "{x:Type local:SomeViewModel}" >

//           < local:SomeView />

//        </ DataTemplate >

//        < DataTemplate DataType = "{x:Type local:DifferentViewModel}" >

//             < local:DifferentView />

//          </ DataTemplate >

//      </ local:PopupPanel.Resources >
//   </ local:PopupPanel >
