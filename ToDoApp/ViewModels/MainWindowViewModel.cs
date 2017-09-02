using Prism.Mvvm;
using System.Collections.ObjectModel;
using ToDoApp.Models;

namespace ToDoApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "ToDo Application - Prism Unity Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public int MyProperty { get; set; }

        internal ObservableCollection<ToDoItem> _ToDoItemList;
        public ObservableCollection<ToDoItem> ToDoItemList
        {
            get { return _ToDoItemList; }  
            set
            {
                if (value != _ToDoItemList)
                {
                    _ToDoItemList = value;
                    //if (listChanged != null)
                    //{
                    //    listChanged(_ToDoItemList);
                    //}
                }
            }
        }

        public MainWindowViewModel()
        {

            _ToDoItemList = new ObservableCollection<ToDoItem>();
            _ToDoItemList.Add(new ToDoItem("Item1", "These are Details 1"));
            _ToDoItemList.Add(new ToDoItem("Item2", "These are Details 2"));
            _ToDoItemList.Add(new ToDoItem("Item3", "These are Details 3"));

        }

        //public delegate void PropertyChagedHandler(object newValue);
        //public event PropertyChagedHandler listChanged; 

        ~MainWindowViewModel()
        {
            //ToDoItemList = null;
        }
    }
}
