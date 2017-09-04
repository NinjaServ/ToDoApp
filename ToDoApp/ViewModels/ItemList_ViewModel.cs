using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoApp.Models;
using ToDoApp.Views;

namespace ToDoApp.ViewModels
{

    public partial class ItemList_ViewModel : BindableBase
    {
        IRegionManager _regionManager;
        IRegionNavigationJournal _journal;

        public IList<string> listProperty { get; set; }

        internal ObservableCollection<ToDoItem> _ToDoItemList;
        public ObservableCollection<ToDoItem> ToDoItemList
        {
            get { return _ToDoItemList; }
            set { SetProperty(ref _ToDoItemList, value); }
        }

        public DelegateCommand<ToDoItem> ItemSelectedCommand { get; private set; }
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

            ItemSelectedCommand = new DelegateCommand<ToDoItem>(ItemSelected);
            CreateItems();

            //Command to navigate to AddItem_View
            GoAddItemCommand = new DelegateCommand(GoForward, CanGoForward);
            //Command to navigate to ItemDetail_View
            GoDetailCommand = new DelegateCommand(GoForward, CanGoForward);
            GoStatsCommand = new DelegateCommand(() => new IMessage("My Test Message", "A test title"),
                            CanGoForward);


        }

        private void ItemSelected(ToDoItem item)
        {
            var parameters = new NavigationParameters();
            parameters.Add("ToDoItem", item);

            //ItemDetail_View needs to know the selected item from the DataGrid "ToDoList"
            if (item != null)
                _regionManager.RequestNavigate("ContentRegion", "ItemDetail_View", parameters);
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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //Log Navigation event in the journal 
            _journal = navigationContext.NavigationService.Journal;
            //Change command status
            GoAddItemCommand.RaiseCanExecuteChanged();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

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
