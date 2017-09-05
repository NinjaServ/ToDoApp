using System.Collections.Generic;
using System.Collections.ObjectModel;

using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ToDoApp.Models;

using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace ToDoApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private string _title = "ToDo App Shell - Prism Unity Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);

        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath, NavigationComplete);
        }

        private void NavigationComplete(NavigationResult result)
        {
            System.Windows.MessageBox.Show(String.Format("Navigation to {0} complete. ", result.Context.Uri));
        }

        

        //public IList<string> listProperty { get; set; }

        //internal ObservableCollection<ToDoItem> _ToDoItemList;
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

        //public MainWindowViewModel()
        //{

        //    _ToDoItemList = new ObservableCollection<ToDoItem>();
        //    _ToDoItemList.Add(new ToDoItem("Item1", "These are Details 1"));
        //    _ToDoItemList.Add(new ToDoItem("Item2", "These are Details 2"));
        //    _ToDoItemList.Add(new ToDoItem("Item3", "These are Details 3"));
        //    _ToDoItemList.Add(new ToDoItem("aItem4", "Oh Details 4"));
        //    _ToDoItemList.Add(new ToDoItem("Item 5", "For 5 details are here"));
        //    _ToDoItemList.Add(new ToDoItem("zItem6", "Some 6 details are ny"));
        //}

        ////public delegate void PropertyChagedHandler(object newValue);
        ////public event PropertyChagedHandler listChanged; 

        ~MainWindowViewModel()
        {
            //_ToDoItemList = null;
        }



        internal  ITimer timer;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        internal  Dispatcher dispatcher;
        internal  Random random;


        public void MessageService()
        {
            this.dispatcher = Application.Current.Dispatcher;
            this.random = new Random();
            this.timer = new DispatcherTimerWrapper(new TimeSpan(0, 0, 5));
            this.timer.Tick += this.OnTimerTick;
            this.timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs args)
        {
            var coinToss = this.random.Next(2);
            if (coinToss == 0)
            {
                this.SendMessage("My Message", "My Title");
            }
            else
            {
                coinToss = this.random.Next(150);
                if (coinToss == 0)
                {
                    this.SendMessage("My Other Message", "My Other Title");
                }
            }
        }

        private void SendMessage(string message, string title)
        {
            var handler = this.MessageReceived;
            if (handler != null)
            {
                handler(this, new MessageReceivedEventArgs(title, message));
            }
        }

        public class MessageReceivedEventArgs : EventArgs
        {
            public MessageReceivedEventArgs(string title, string message)
            {
                this.Message = new IMessage(message, title);
            }

            public IMessage Message { get; private set; }
        }



    }
}

