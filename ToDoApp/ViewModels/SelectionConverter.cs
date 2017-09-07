using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Data;
using ToDoApp.Models;

namespace ToDoApp.ViewModels
{
    public class SelectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            bool result = false;
            ToDoItem toDoItem = (ToDoItem)value;
            ToDoItem selectedItem = (ToDoItem)parameter;

            if( toDoItem == selectedItem)
                result = true;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

