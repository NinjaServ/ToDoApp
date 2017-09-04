using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using ToDoApp.Models;


namespace ToDoApp.ViewModels
{
    public class AddItem_ViewModel : BindableBase, INavigationAware
    {
        IRegionManager _regionManager;
        IRegionNavigationJournal _journal;

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
        
        public DelegateCommand<ToDoItem> ItemSelectedCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; set; }
        public DelegateCommand GoBackCommand { get; set; }

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
            itemList.Add(new ToDoItem("Item1", "These are Details 1", DateTime.Now, DateTime.Now.AddDays(1.0)) );
            itemList.Add(new ToDoItem("Item2", "These are Details 2", DateTime.Now, DateTime.Now.AddDays(1.0)) );
            itemList.Add(new ToDoItem("Item3", "These are Details 3", DateTime.Now, DateTime.Now.AddDays(1.0)) );
            itemList.Add(new ToDoItem("aItem4", "Oh Details 4", DateTime.Now, DateTime.Now.AddDays(1.0)) );
            itemList.Add(new ToDoItem("Item 5", "For 5 details are here", DateTime.Now, DateTime.Now.AddDays(1.0)) );
            itemList.Add(new ToDoItem("zItem6", "Some 6 details are ny", DateTime.Now, DateTime.Now.AddDays(1.0)) );

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
