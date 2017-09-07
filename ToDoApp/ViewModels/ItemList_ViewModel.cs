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

            // Initialize the CollectionView for the underlying model
            // and track the current selection.
            this.ItemsCV = CollectionViewSource.GetDefaultView(ToDoItemList);
            //this.ItemsCV = new ListCollectionView(ToDoItemList);
            this.ItemsCV.CurrentChanged += new EventHandler(this.ItemSelected); //SelectedItemChanged
            this.ItemsCV.Filter = SearchFilter;
            //_filterString = "Search Text..."; 


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
            bool contains = item.task.IndexOf(_filterString, StringComparison.CurrentCultureIgnoreCase) != -1 ;
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


        //private void ItemSelected(ToDoItem item)
        private void ItemSelected(object sender, EventArgs e)
        {
            ToDoItem item = this.ItemsCV.CurrentItem as ToDoItem;
            //var it = (ListCollectionView)sender.InternalList.CurrentItem as ToDoItem;

            if (item != null)
            {
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
            //if (item != null)
            //{
            //    // Publish the SelectedEvent event.
            //    this.eventAggregator.GetEvent<PubSubEvent<int>>().Publish(item.id);
            //}
        }

        private void ItemSelectedEvent(int id)
        {
            if (id == 0) return;

            // Get the employee entity with the selected ID.
            ToDoItem selectedEmployee = this.ToDoItemList.FirstOrDefault(item => item.id == id);
        }


        //Test method for itemList population - can migrate to Unit Test
        //private void CreateItems()
        //{
        //    var itemList = new ToDoListCollection();
        //    itemList.Add(new ToDoItem("Item1", "These are Details 1", DateTime.Now, DateTime.Now.AddDays(1.0)));
        //    itemList.Add(new ToDoItem("Item2", "These are Details 2", DateTime.Now, DateTime.Now.AddDays(1.0)));
        //    itemList.Add(new ToDoItem("Item3", "These are Details 3", DateTime.Now, DateTime.Now.AddDays(1.0)));
        //    itemList.Add(new ToDoItem("aItem4", "Oh Details 4", DateTime.Now, DateTime.Now.AddDays(1.0)));
        //    itemList.Add(new ToDoItem("Item 5", "For 5 details are here", DateTime.Now, DateTime.Now.AddDays(1.0)));
        //    itemList.Add(new ToDoItem("zItem6", "Some 6 details are ny", DateTime.Now, DateTime.Now.AddDays(1.0)));

        //    _ToDoItemList = itemList;
        //}

        private void GoAddItem()
        {
            _regionManager.RequestNavigate("ContentRegion", "AddItem_View");

            //IRegion contentRegion = _regionManager.Regions["ContentRegion"];
            //contentRegion.RequestNavigate(new Uri("AddItem_View", UriKind.Relative));
        }

        private void GoItemDetails()
        {
            ToDoItem item = this.ItemsCV.CurrentItem as ToDoItem;
            
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

            if (item != null)
            {
                bool result = item.CompleteToggle();
                //_ToDoItemListService
                //item.completeDate = DateTime.Now;
            }
        }

        private void GoItemDelete()
        {
            ToDoItem item = this.ItemsCV.CurrentItem as ToDoItem;

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



        //public delegate void PropertyChangedHandler(object newValue);
        //public event PropertyChangedHandler listChanged;

        //private void addItem_Click(object sender, RoutedEventArgs e)
        //{
        //    //var view = _container.Resolve<ItemList_View>();
        //    //IRegion region = _regionManager.Regions["ContentRegion"];
        //    //region.Add(view);

        //    //--View Injection 
        //    //IRegion region = _regionManager.Regions["ContentRegion"];
        //    //var view = _container.Resolve<ItemList_View>();
        //    //region.Add(view);

        //    //var view2 = _container.Resolve<AddItem_View>();
        //    //region.RequestNavigate(new Uri("AddItem_View", UriKind.Relative));
        //}

    }
}
