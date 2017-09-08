using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace ToDoApp.Infrastructure
//{

//    [Export(typeof(IDialogService))]
//    public sealed class DialogService : IDialogService
//    {
//        #region fields - ctor

//        readonly IServiceLocator serviceLocator;

//        [ImportingConstructor]
//        public DialogService(IServiceLocator serviceLocator)
//        {
//            this.serviceLocator = serviceLocator;
//        }

//        #endregion // fields - ctor

//        #region IDialogService

//        public void ShowDialog<T>() where T : IDialogViewModel
//        {
//            IDialogViewModel context = serviceLocator.GetInstance(typeof(T)) as IDialogViewModel;
//            ShowDialog(context);
//        }

//        public void ShowDialog<T>(T context) where T : IDialogViewModel
//        {
//            Invoke(context, null);
//        }

//        public void ShowDialog<T>(T context, Action<T> callback) where T : IDialogViewModel
//        {
//            Invoke(context, callback);
//        }

//        #endregion // IDialogService

//        #region Invoke

//        private void Invoke<T>(T dialogViewModel, Action<T> callback) where T : IDialogViewModel
//        {
//            Window wrapperWindow = GetWindow(dialogViewModel);

//            EventHandler viewModelClosedHandler = (sender, e) => wrapperWindow.Close();
//            dialogViewModel.Closed += viewModelClosedHandler;

//            // We invoke the callback when the interaction's window is closed.
//            EventHandler windowClosedHandler = null;

//            windowClosedHandler =
//                (sender, e) =>
//                {
//                    wrapperWindow.Closed -= windowClosedHandler;
//                    dialogViewModel.Closed -= viewModelClosedHandler;

//                    wrapperWindow.Content = null;
//                    if (callback != null) callback(dialogViewModel);
//                };

//            wrapperWindow.Closed += windowClosedHandler;
//            wrapperWindow.ShowDialog();
//        }

//        private static Window GetWindow(IDialogViewModel dialogViewModel)
//        {
//            Window wrapperWindow = new Window
//            {
//                Title = dialogViewModel.Title,
//                ShowInTaskbar = false,
//                WindowStartupLocation = WindowStartupLocation.CenterOwner,
//                WindowState = WindowState.Normal,
//                SizeToContent = SizeToContent.WidthAndHeight
//            };

//            Window mainWindow = System.Windows.Application.Current.MainWindow;

//            if (mainWindow != null)
//            {
//                wrapperWindow.Owner = mainWindow;
//                wrapperWindow.Icon = mainWindow.Icon;
//            }

//            FrameworkElement view = YA.Windows.ViewLocationProvider.GetViewByViewModel(dialogViewModel);
//            wrapperWindow.Content = view;

//            return wrapperWindow;
//        }

//        #endregion // Invoke
//    }


//    IDialogViewModel

//public interface IDialogViewModel
//    {
//        string Title { get; }
//        event EventHandler Closed;
//    }
//    IDialogService

//public interface IDialogService
//    {
//        void ShowDialog<T>() where T : IDialogViewModel;
//        void ShowDialog<T>(T context) where T : IDialogViewModel;
//        void ShowDialog<T>(T context, Action<T> callback) where T : IDialogViewModel;
//    }