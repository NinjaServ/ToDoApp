using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoApp.Views
{
    /// <summary>
    /// Interaction logic for ItemList_View.xaml
    /// </summary>
    public partial class ItemList_View : UserControl
    {
        public ItemList_View()
        {
            InitializeComponent();

            refreshTimerSetup();
        }

        ~ItemList_View()
        {
            if (dt != null)
                dt.Stop(); 
        }

        private DispatcherTimer dt = null;

        private void refreshTimerSetup()
        {
            refreshTimerStart(); 
        }

        private void refreshTimerStart()
        {
            dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 1, 0); // TimeSpan.FromMinutes(1);
            dt.Tick += timer_Tick; //new EventHandler()
            dt.Start();
            //dt.IsEnabled = true;

        }


        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.ToDoGrid != null)
            {
                this.ToDoGrid.Items.Refresh(); 
            }
        }
        //refreshTimerSetup();

    }

    //public class MockRecord
    //{
    //    public string id { get; set; }
    //    public string task { get; set; }
    //}
}

////awaint Task.Run();
////usage - await loadInfo(); 
//private async Task loadInfo()
//{

//}


//// await Task.Delay(TimeSpan.FromMinutes(1)) ;
//// usage - await 5.Seconds;
//public static Task Seconds(this int seconds)
//{
//    return Task.Delay(new TimeSpan(0, 0, seconds));
//}

//private void addItem_Click(object sender, RoutedEventArgs e)
//{


////}


//private bool _isEnabled;
//public bool IsEnabled
//{
//    get { return _isEnabled; }
//    set
//    {
//        SetProperty(ref _isEnabled, value);
//        ExecuteDelegateCommand.RaiseCanExecuteChanged();
//    }
//}

//private string _updateText;
//public string UpdateText
//{
//    get { return _updateText; }
//    set { SetProperty(ref _updateText, value); }
//}


//public DelegateCommand ExecuteDelegateCommand { get; private set; }

//public DelegateCommand<string> ExecuteGenericDelegateCommand { get; private set; }

//public DelegateCommand DelegateCommandObservesProperty { get; private set; }

//public DelegateCommand DelegateCommandObservesCanExecute { get; private set; }


//public MainWindowViewModel()
//{
//    ExecuteDelegateCommand = new DelegateCommand(Execute, CanExecute);

//    DelegateCommandObservesProperty = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => IsEnabled);

//    DelegateCommandObservesCanExecute = new DelegateCommand(Execute).ObservesCanExecute((vm) => IsEnabled);

//    ExecuteGenericDelegateCommand = new DelegateCommand<string>(ExecuteGeneric).ObservesCanExecute((vm) => IsEnabled);
//}

//private void Execute()
//{
//    UpdateText = $"Updated: {DateTime.Now}";
//}

//private void ExecuteGeneric(string parameter)
//{
//    UpdateText = parameter;
//}

//private bool CanExecute()
//{
//    return IsEnabled;
//}





//private string _selectedItemText;
//public string SelectedItemText
//{
//    get { return _selectedItemText; }
//    private set { SetProperty(ref _selectedItemText, value); }
//}

//public IList<string> Items { get; private set; }

//public DelegateCommand<object[]> SelectedCommand { get; private set; }

//public MainWindowViewModel()
//{
//    Items = new List<string>();

//    Items.Add("Item1");
//    Items.Add("Item2");
//    Items.Add("Item3");
//    Items.Add("Item4");
//    Items.Add("Item5");

//    // This command will be executed when the selection of the ListBox in the view changes.
//    SelectedCommand = new DelegateCommand<object[]>(OnItemSelected);
//}

//private void OnItemSelected(object[] selectedItems)
//{
//    if (selectedItems != null && selectedItems.Count() > 0)
//    {
//        SelectedItemText = selectedItems.FirstOrDefault().ToString();
//    }
//}



//<ListBox Grid.Row="1" Margin= "5" ItemsSource= "{Binding Items}" SelectionMode= "Single" >
//        < i:Interaction.Triggers>
//            <!-- This event trigger will execute the action when the corresponding event is raised by the ListBox. -->
//            <i:EventTrigger EventName = "SelectionChanged" >
//                < !--This action will invoke the selected command in the view model and pass the parameters of the event to it. -->
//                <prism:InvokeCommandAction Command = "{Binding SelectedCommand}" TriggerParameterPath= "AddedItems" />
//            </ i:EventTrigger>
//        </i:Interaction.Triggers>
//    </ListBox>



//<StackPanel Grid.Row= "2" Margin= "5" Orientation= "Horizontal" >
//        < TextBlock Foreground= "DarkRed" FontWeight= "Bold" > Selected Item:</TextBlock>
//        <TextBlock AutomationProperties.AutomationId= "SelectedItemTextBlock" Foreground= "DarkRed" FontWeight= "Bold" Margin= "5,0" Text= "{Binding SelectedItemText}" />
//    </ StackPanel >


