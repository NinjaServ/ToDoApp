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
using ToDoApp.Models;
using ToDoApp.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ToDoApp.ViewModels
{

    public partial class ItemList_ViewModel : BindableBase, INavigationAware
    {
        IRegionManager _regionManager;
        IRegionNavigationJournal _journal;
        IRegionNavigationService _navService;

        public IList<string> listProperty { get; set; }

        internal ObservableCollection<ToDoItem> _ToDoItemList;
        public ObservableCollection<ToDoItem> ToDoItemList
        {
            get { return _ToDoItemList; }
            set { SetProperty(ref _ToDoItemList, value); }
        }

        public ICollectionView ItemsCV { get; private set; }
        private readonly IEventAggregator eventAggregator;


        //public DelegateCommand<ToDoItem> ItemSelectedCommand { get; private set; }
        public DelegateCommand GoAddItemCommand { get; set; }
        public DelegateCommand GoDetailCommand { get; set; }
        public DelegateCommand GoStatsCommand { get; set; }


        //public ItemList_ViewModel()
        //{

        //}

        public ItemList_ViewModel(RegionManager regionManager)
        {
            //View discovery
            //this.regionManager.RegisterViewWithRegion("ContentRegion", typeof(ItemList_View));

            _regionManager = regionManager;

            //ItemSelectedCommand = new DelegateCommand<ToDoItem>(ItemSelected);
            CreateItems();

            // Initialize the CollectionView for the underlying model
            // and track the current selection.
            this.ItemsCV = new ListCollectionView(ToDoItemList);
            this.ItemsCV.CurrentChanged += new EventHandler(this.ItemSelected); //SelectedItemChanged


            //Command to navigate to AddItem_View
            GoAddItemCommand = new DelegateCommand(GoAddItem, () => true);
            //Command to navigate to ItemDetail_View
            GoDetailCommand = new DelegateCommand(GoItemDetails, () => true); //CanGoForward
            //GoStatsCommand = new DelegateCommand(() => new IMessage("My Test Message", "A test title"),
            //                ()=>true);
            GoStatsCommand = new DelegateCommand(GoForward, CanGoForward); 
                //GoMessage, () => true);

            //this.eventAggregator.GetEvent<PubSubEvent<int>>().Subscribe(this.ItemSelectedEvent, true);
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
            if (item != null)
            {
                // Publish the EmployeeSelectedEvent event.
                this.eventAggregator.GetEvent<PubSubEvent<int>>().Publish(item.id);
            }
        }

        private void ItemSelectedEvent(int id)
        {
            if (id == 0) return;

            // Get the employee entity with the selected ID.
            ToDoItem selectedEmployee = this.ToDoItemList.FirstOrDefault(item => item.id == id);
        }


        //Test method for itemList population - can migrate to Unit Test
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
