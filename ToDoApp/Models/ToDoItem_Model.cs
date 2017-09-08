using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;


namespace ToDoApp.Models
{
    class ToDoItem_Model : BindableBase
    {
        private ToDoItem _toDoItem;
        public ToDoItem toDoItem
        { get { return _toDoItem; }
            set { SetProperty(ref _toDoItem, value);  }
        }

        public bool overdue { get {return _toDoItem.IsOverdue(); } }
        public bool due { get { return _toDoItem.IsDue(); } }
        public bool complete { get { return _toDoItem.IsComplete(); } }

        public ToDoItem_Model()
        {
            toDoItem = new ToDoItem();

        }

    }
}
