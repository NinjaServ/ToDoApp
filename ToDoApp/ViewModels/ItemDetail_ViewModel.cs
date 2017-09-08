using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Interactivity.InteractionRequest;

using System.ComponentModel;
using System.Collections.ObjectModel;
using ToDoApp.Models;
using ToDoApp.Infrastructure;
using Prism.Interactivity;

namespace ToDoApp.ViewModels
{
    public class ItemDetail_ViewModel : BindableBase, IConfirmNavigationRequest
    {
        IRegionNavigationJournal _journal;
        IToDoList_Service _toDoItemListService;
        bool contentDirty = false;

        
        public ToDoItem SelectedItem { get; set; }
       

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

        public InteractionRequest<IConfirmation> ConfirmationRequest { get; set; }
        public DelegateCommand ConfirmationCommand { get; set; }

        public static CompositeCommand confirmNavigationCommmand = new CompositeCommand();
        bool confirmed = false; 

        public ItemDetail_ViewModel(IToDoList_Service toDoListService)
        {
            _toDoItemListService = toDoListService;
            
            TempItem = new ToDoItem(); 
            //this.ItemsCV = CollectionViewSource.GetDefaultView(ToDoItemList);

            //< ToDoItem > = IObservable<ToDoItem>(SelectedItem); 
            GoBackCommand = new DelegateCommand(GoBack);
            CompletedCommand = new DelegateCommand(CompleteItem);
            SaveCommand = new DelegateCommand(SaveItem);

            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            ConfirmationCommand = new DelegateCommand(RaiseConfirmation);
            // ConfirmationCommand = new DelegateCommand(RaiseConfirmation);

            confirmNavigationCommmand.RegisterCommand(ConfirmationCommand);
            //confirmNavigationCommmand.RegisterCommand(GoBackCommand);

            //contentDirty = false;
        }

    
        void RaiseConfirmation()
        {
                    
            ConfirmationRequest.Raise(new Confirmation
            {
                Title = "Confirmation",
                Content = "Confirmation Message"
            },
              r => confirmed = r.Confirmed ? true : false);
            //r => Title = r.Confirmed ? "Confirmed" : "Not Confirmed");
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = true;

            if( contentDirty )
            {
                confirmed = (MessageBox.Show("Do you want to Return without saving?", "Return?", MessageBoxButton.YesNo) == MessageBoxResult.No);
                if(confirmed)
                    result = false;
                //InvokeCommandAction aCommandAction = new InvokeCommandAction();
                //aCommandAction.Command = ConfirmationCommand;
                //aCommandAction.InvokeAction(null);

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
            contentDirty = false; 

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
                contentDirty = false;
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
