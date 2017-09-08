using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Prism.Events;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using ToDoApp.Infrastructure;
using ToDoApp.Models;
using ToDoApp.Views;

namespace ToDoApp.ViewModels
{

    public partial class ItemList_ViewModel : BindableBase, INavigationAware
    {
        IRegionManager _regionManager;
        IRegionNavigationJournal _journal;
        IRegionNavigationService _navService;

        internal string _filterString;
        //public IList<string> listProperty { get; set; }

        IToDoList_Service _ToDoItemListService;

        //ObservableCollection<ToDoItem>
        internal ToDoListCollection _ToDoItemList;
        public ToDoListCollection ToDoItemList
        {
            get { return _ToDoItemList; }
            set { SetProperty(ref _ToDoItemList, value); }
        }

        public ICollectionView ItemsCV { get; private set; }
        //private readonly IEventAggregator eventAggregator;

        private ToDoItem _selectedItem;
        public ToDoItem SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }


        //public DelegateCommand<ToDoItem> ItemSelectedCommand { get; private set; }
        public DelegateCommand GoAddItemCommand { get; set; }
        public DelegateCommand GoStatsCommand { get; set; }
        public DelegateCommand GoSelectCommand { get; set; }
        public DelegateCommand GoDetailCommand { get; set; }
        public DelegateCommand GoDeleteCommand { get; set; }
        public DelegateCommand GoCompleteCommand { get; set; }

        //public bool IsDateValid { get { return todoitem.dueDate < DateTime.Now; } }

        //public ItemList_ViewModel()
        //{

        //}

        public ItemList_ViewModel(RegionManager regionManager, IToDoList_Service toDoListService)
        {
            //View discovery
            //this.regionManager.RegisterViewWithRegion("ContentRegion", typeof(ItemList_View));

            _regionManager = regionManager;
            _ToDoItemListService = toDoListService;
            _ToDoItemList = toDoListService.GetToDoList();

            //ItemSelectedCommand = new DelegateCommand<ToDoItem>(ItemSelected);
            _filterString = "";
            //_filterString = "Search Text..."; 


            // Initialize the CollectionView for the underlying model
            // and track the current selection.
            this.ItemsCV = CollectionViewSource.GetDefaultView(ToDoItemList);
            //this.ItemsCV = new ListCollectionView(ToDoItemList);
            this.ItemsCV.CurrentChanged += ItemSelected; //new EventHandler(this.ItemSelected); //SelectedItemChanged
            this.ItemsCV.Filter = SearchFilter;
            SelectedItem = this.ItemsCV.CurrentItem as ToDoItem;

            //Command to navigate to AddItem_View
            GoAddItemCommand = new DelegateCommand(GoAddItem, () => true);
            //Command to navigate to ItemDetail_View
            GoStatsCommand = new DelegateCommand(GoForward, CanGoForward);
            //GoMessage, () => true);
            GoSelectCommand = new DelegateCommand(GoForward, CanGoForward);

            GoDetailCommand = new DelegateCommand(GoItemDetails, () => true); //CanGoForward
            GoDeleteCommand = new DelegateCommand(GoItemDelete, () => true);
            GoCompleteCommand = new DelegateCommand(GoItemComplete, () => true); //CanGoForward

            //GoStatsCommand = new DelegateCommand(() => new IMessage("My Test Message", "A test title"),
            //                ()=>true);

            //this.eventAggregator.GetEvent<PubSubEvent<int>>().Subscribe(this.ItemSelectedEvent, true);
        }



        private bool SearchFilter(object sender)
        {
            ToDoItem item = sender as ToDoItem;
            //bool contains = item.task.IndexOf(_filterString, StringComparison.OrdinalIgnoreCase) >= 0;
            bool contains = item.task.IndexOf(_filterString, StringComparison.CurrentCultureIgnoreCase) != -1;
            return contains;
        }

        public string FilterString
        {
            get { return _filterString; }
            set
            {
                SetProperty(ref _filterString, value);
                if (this.ItemsCV != null)
                    this.ItemsCV.Refresh();
            }
        }



        // View.CurrentChanged += View_CurrentChanged;
        //  void View_CurrentChanged(object sender, EventArgs e)
        //private void ItemSelected(ToDoItem item)
        private void ItemSelected(object sender, EventArgs e)
        {
            ToDoItem item = this.ItemsCV.CurrentItem as ToDoItem;
            bool test = SelectedItem == item;
            //var it = (ListCollectionView)sender.InternalList.CurrentItem as ToDoItem;
            //CollectionViewSource.GetDefaultView(ToDoItemList).MoveCurrentTo(this.datagrid.CurrentItem);


            if (item != null)
            {
                //New parameters with each new selection
                var parameters = new NavigationParameters();
                parameters.Add("ToDoItem", item);
            }

            //ItemDetail_View needs to know the selected item from the DataGrid "ToDoList"
            //if (item != null)
            //    _regionManager.RequestNavigate("ContentRegion", "ItemDetail_View", parameters);
        }

        private void SelectedItemChanged(object sender, EventArgs e)
        {
            ToDoItem item = this.ItemsCV.CurrentItem as ToDoItem;
            bool test = SelectedItem == item;
            //if (item != null)
            //{
            //    // Publish the SelectedEvent event.
            //    this.eventAggregator.GetEvent<PubSubEvent<int>>().Publish(item.id);
            //}
        }

        private void ItemSelectedEvent(int id)
        {
            if (id == 0) return;

            // Get the  entity with the selected ID.
            ToDoItem selectedItem = this.ToDoItemList.FirstOrDefault(item => item.id == id);
        }

               
        private void GoAddItem()
        {
            _regionManager.RequestNavigate("ContentRegion", "AddItem_View");

            //IRegion contentRegion = _regionManager.Regions["ContentRegion"];
            //contentRegion.RequestNavigate(new Uri("AddItem_View", UriKind.Relative));
        }

        private void GoItemDetails()
        {
            ToDoItem item = this.ItemsCV.CurrentItem as ToDoItem;
            bool test = SelectedItem == item;

            if (item != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("ToDoItem", item);

                //ItemDetail_View needs to know the selected item from the DataGrid "ToDoList"
                _regionManager.RequestNavigate("ContentRegion", "ItemDetail_View", parameters);
            }
        }

        private void GoItemComplete()
        {
            ToDoItem item = this.ItemsCV.CurrentItem as ToDoItem;
            bool test = SelectedItem == item;

            if (item != null)
            {
                _ToDoItemListService.ToggleItemCompleted(item);
                //bool result = item.CompleteToggle();
                //item.completeDate = DateTime.Now;
            }
        }

        private void GoItemDelete()
        {
            ToDoItem item = this.ItemsCV.CurrentItem as ToDoItem;
            bool test = SelectedItem == item;

            if (item != null)
            {
                _ToDoItemListService.DeleteToDoItem(item);
                item = null;
            }
        }


        private void GoMessage()
        {
            IMessage message = new IMessage("My Test Message", "A test title");

            //message(this, new MessageReceivedEventArgs(contact, message)); 

            //var handler = this.MessageReceived;
            //if (handler != null)
            //{
            //    handler(this, new MessageReceivedEventArgs(contact, message));
            //}
        }

        //private void OnMessageReceived(object sender, MessageReceivedEventArgs a)
        //{
        //    this.showReceivedMessageRequest.Raise(a.Message);
        //}


        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatingTo(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navService = navigationContext.NavigationService;
            //Get Navigation journal reference
            _journal = navigationContext.NavigationService.Journal;
            //Change command status
            GoAddItemCommand.RaiseCanExecuteChanged();

            //this.ItemsCV = CollectionViewSource.GetDefaultView(ToDoItemList);
            this.ItemsCV.Refresh();

        }


        //Navigate backwards based on the journal
        private void GoBack()
        {
            _journal.GoBack();
        }

        //Navigate forwards based on the journal
        private void GoForward()
        {
            _journal.GoForward();
        }

        //Check for Navigation allowed by the journal
        private bool CanGoForward()
        {
            return _journal != null && _journal.CanGoForward;
            //return true;
        }

        ~ItemList_ViewModel()
        {
            _ToDoItemList = null;
        }

        
    }
}
