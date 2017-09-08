using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


//namespace ToDoApp.Models
//{
//    class MethodWrapper
//    {
//    }
//}


public class MethodWrapper : INotifyPropertyChanged
{
    private string m_name;

    public string Extension
    {
        get { return GetExtension(Task); }
    }

    public string Task
    {
        get { return m_name; }
        set
        {
            m_name = value;
            OnPropertyChanged("Task");
            OnPropertyChanged("Details");
        }
    }

    public string GetExtension(string filename)
    {
        //implementation
        new NotImplementedException();
        return null;
    }

    public event PropertyChangedEventHandler PropertyChanged = (a, b) => { };

    protected void OnPropertyChanged(string property)
    {
        PropertyChanged(this, new PropertyChangedEventArgs(property));
    }

}



//class implements INotifyPropertyChanged, because if a property can change in code, a way of telling the binding that it has done so.
//    Make the setter of the Filename property notify bindings that the Extension property has changed.
//Add a Binding to a TextBox that binds to the Filename property using the TwoWay mode.Add a Binding to a TextBox that binds to Extension using the default OneWay mode.

//The sequence of events is:

//User types a new Filename into a bound TextBox and presses TAB.
//The TextBox loses focus.
//Because the Binding's mode is TwoWay, and it's using the default behavior of updating the source when the target loses focus, that's what it does.
//The Binding updates the source by calling the Filename setter.
//The Filename setter raises PropertyChanged.
//The Binding handles PropertyChanged, looks at its argument, and sees that the Extension property has changed.
//The Binding calls the Extension property's getter.
//The Extension property's getter calls the method to determine the extension for Filename, and returns it to the Binding.
//The Binding updates its target TextBox with the new value of Extension.
