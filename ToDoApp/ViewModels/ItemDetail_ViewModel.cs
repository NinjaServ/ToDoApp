using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using ToDoApp.Models;

namespace ToDoApp.ViewModels
{
    public class ItemDetail_ViewModel : BindableBase, IConfirmNavigationRequest
    {
        IRegionNavigationJournal _journal;

        private ToDoItem _selectedItem;
        public ToDoItem SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public DelegateCommand GoBackCommand { get; set; }

        public ItemDetail_ViewModel()
        {
            GoBackCommand = new DelegateCommand(GoBack);
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = true;

            if (MessageBox.Show("Do you want to Exit without saving?", "Exit?", MessageBoxButton.YesNo) == MessageBoxResult.No)
                result = false;

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
                SelectedItem = item;
        }


        //Navigate backwards based on the journal
        private void GoBack()
        {
            _journal.GoBack();
        }

    }
}
