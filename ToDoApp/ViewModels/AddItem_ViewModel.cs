using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Mvvm;
using System.Collections.ObjectModel;
using ToDoApp.Models;


namespace ToDoApp.ViewModels
{
    public class AddItem_ViewModel : BindableBase
    {
        public AddItem_ViewModel()
        {

        }

        private string _title = "ToDo Application - Add Item";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        
        internal ObservableCollection<ToDoItem> _ToDoItemList;
        //public ObservableCollection<ToDoItem> ToDoItemList
        //{
        //    get { return _ToDoItemList; }
        //    set
        //    {
        //        if (value != _ToDoItemList)
        //        {
        //            _ToDoItemList = value;
        //        }
        //    }
        //}

        //public AddItem_ViewModel()
        //{

        //    _ToDoItemList = new ObservableCollection<ToDoItem>();
        //    _ToDoItemList.Add(new ToDoItem("Item1", "These are Details 1"));
        //    _ToDoItemList.Add(new ToDoItem("Item2", "These are Details 2"));
        //    _ToDoItemList.Add(new ToDoItem("Item3", "These are Details 3"));
        //    _ToDoItemList.Add(new ToDoItem("aItem4", "Oh Details 4"));
        //    _ToDoItemList.Add(new ToDoItem("Item 5", "For 5 details are here"));
        //    _ToDoItemList.Add(new ToDoItem("zItem6", "Some 6 details are ny"));
        //}

        ~AddItem_ViewModel()
        {
            _ToDoItemList = null;
        }
    }
}
