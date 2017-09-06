using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace ToDoApp.Models
{
    class ToDoList_Model : INotifyPropertyChanged
    {
        internal ToDoListCollection _toDoList;
        public ToDoListCollection ToDoList
        {
            get { return _toDoList; }
            set
            {
                if (_toDoList != value)
                {
                    _toDoList = value;
                    NotifyPropertyChanged("ToDoList");
                }
            }
        }

        public ToDoList_Model()
        {
            _toDoList = new ToDoListCollection();

        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }
}
