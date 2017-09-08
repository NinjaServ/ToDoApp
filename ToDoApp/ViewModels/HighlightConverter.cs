using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Data;
using ToDoApp.Models;


//<UserControl.Resources>
//       <src:HighlightConverter x:Name="highlightConverter" />
//</UserControl.Resources> 

//<vm:OverDueConverter x:Key="OverdueConverter" />
//    <vm:DueConverter x:Key="DueConverter" />
//    <vm:CompletedConverter x:Key="OverdueConverter" />

//<DataTrigger Binding = "{Binding Path=., Converter={StaticResource OverDueConverter}, ConverterParameter=dueDate}" Value="True">

namespace ToDoApp.ViewModels
{
    public class HighlightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            ToDoItem toDoItem = (ToDoItem)value;
            //DateTime dueDate = (DateTime)value ;

            bool result = toDoItem.IsOverdue();

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class OverDueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            ToDoItem toDoItem = (ToDoItem)value;
            //DateTime dueDate = (DateTime)value ;

            bool result = toDoItem.IsOverdue();

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            ToDoItem toDoItem = (ToDoItem)value;
    
            bool result = toDoItem.IsDue();

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CompletedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            ToDoItem toDoItem = (ToDoItem)value;
           
            bool result = toDoItem.IsComplete();

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
