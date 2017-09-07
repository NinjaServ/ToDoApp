using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using System.ComponentModel;
using System.Collections.ObjectModel;
using ToDoApp.Models;
using ToDoApp.Infrastructure;


namespace ToDoApp.ViewModels
{
    public class ItemDetail_ViewModel : BindableBase, IConfirmNavigationRequest
    {
        IRegionNavigationJournal _journal;
        IToDoList_Service _toDoItemListService;
        bool contentDirty = false;

        private ToDoItem _selectedItem;
        public ToDoItem SelectedItem { get; set; }
        //{
        //    get { return _selectedItem; }
        //    set { SetProperty(ref _selectedItem, value); }
        //}

        private ToDoItem _tempItem;
        public ToDoItem TempItem
        {
            get { return _tempItem; }
            set
            {
                SetProperty(ref _tempItem, value);
                contentDirty = true;
            }
        }

        public DelegateCommand GoBackCommand { get; set; }
        public DelegateCommand CompletedCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        //public ICollectionView ItemsCV { get; private set; }

        public ItemDetail_ViewModel(IToDoList_Service toDoListService)
        {
            _toDoItemListService = toDoListService;
            
            TempItem = new ToDoItem(); 
            //this.ItemsCV = CollectionViewSource.GetDefaultView(ToDoItemList);

            //< ToDoItem > = IObservable<ToDoItem>(SelectedItem); 
            GoBackCommand = new DelegateCommand(GoBack);
            CompletedCommand = new DelegateCommand(CompleteItem);
            SaveCommand = new DelegateCommand(SaveItem);
        }

        //public ICollectionView ItemsCV { get; private set; }
        // // Initialize the CollectionView for the underlying model
        // // and track the current selection.
        //this.ItemsCV = CollectionViewSource.GetDefaultView(ToDoItemList);
   

        //public void SaveToDoItem()
        //(
        //    //SelectedItem
        //    //_ToDoItemListService.SaveToDoItem();
        //)

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = true;

            if( contentDirty )
            {
                if (MessageBox.Show("Do you want to Return without saving?", "Return?", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    result = false;
            }
            
            continuationCallback(result);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //return true;

            var item = navigationContext.Parameters["ToDoItem"] as ToDoItem;
            if (item != null)
                return SelectedItem != null && SelectedItem.id == item.id;
            else
                return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;

            var item = navigationContext.Parameters["ToDoItem"] as ToDoItem;
            if (item != null)
            {
                SelectedItem = item;
                TempItem = null; 
                TempItem = SelectedItem.DeepCopy(); 
            }
        }


        //Navigate backwards based on the journal
        private void GoBack()
        {
            _journal.GoBack();
        }

        private void CompleteItem()
        {
            bool result = TempItem.CompleteToggle();
            contentDirty = true; 
        }

        private void SaveItem()
        {
            //if(contentUpdated)
            if(true) 
            {
                //SelectedItem = null; 
                //ToDoList_service;
                _toDoItemListService.ReplaceToDoItem(SelectedItem, TempItem);
                //SelectedItem = TempItem;
                //TempItem = SelectedItem.DeepCopy();
                
                contentDirty = false;

                bool response = (MessageBox.Show("Changes to the task have been saved", "Task Saved", MessageBoxButton.OK) == MessageBoxResult.OK);
            }
        }

    }
}
