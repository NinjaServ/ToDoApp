using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoApp.Models;
using ToDoApp.Views;

namespace ToDoApp.ViewModels
{
    
    public partial class ItemList_ViewModel : BindableBase
    {

        public ItemList_ViewModel()
        {

        }

        public IList<string> listProperty { get; set; }

        internal ObservableCollection<ToDoItem> _ToDoItemList;
        //public ObservableCollection<ToDoItem> ToDoItemList
        //{
        //    get { return _ToDoItemList; }
        //    set
        //    {
        //        if (value != _ToDoItemList)
        //        {
        //            _ToDoItemList = value;
        //            //if (listChanged != null)
        //            //{
        //            //    listChanged(_ToDoItemList);
        //            //}
        //        }
        //    }
        //}

        //public ItemList_ViewModel()
        //{
        //    //View discovery
        //    //this.regionManager.RegisterViewWithRegion("ContentRegion", typeof(ItemList_View));

        //    _ToDoItemList = new ObservableCollection<ToDoItem>();
        //    _ToDoItemList.Add(new ToDoItem("Item1", "These are Details 1"));
        //    _ToDoItemList.Add(new ToDoItem("Item2", "These are Details 2"));
        //    _ToDoItemList.Add(new ToDoItem("Item3", "These are Details 3"));
        //    _ToDoItemList.Add(new ToDoItem("aItem4", "Oh Details 4"));
        //    _ToDoItemList.Add(new ToDoItem("Item 5", "For 5 details are here"));
        //    _ToDoItemList.Add(new ToDoItem("zItem6", "Some 6 details are ny"));
        //}

        //public delegate void PropertyChagedHandler(object newValue);
        //public event PropertyChagedHandler listChanged; 

        ~ItemList_ViewModel()
        {
            _ToDoItemList = null;
        }
    }

}
