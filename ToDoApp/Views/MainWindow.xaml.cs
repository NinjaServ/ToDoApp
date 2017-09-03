using System;
using System.Windows;
using System.Diagnostics;
using Prism.Mvvm;
using ToDoApp.ViewModels;

using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System.Linq;

namespace ToDoApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public MainWindow()
        //{
        //    InitializeComponent();
        //}

        ////Unity - Creating the View and View Model
        //public MainWindow (ItemList_ViewModel viewModel)
        //{
        //    this.DataContext = viewModel;
        //}

        //public static class RegionNames
        //{
        //    public static readonly string MainRegion = "MainRegion";
        //    public static readonly string ContentRegion = "ContentRegion";
        //}

        //[Dependency]
        //public ShellViewModel ViewModel
        //{
        //    set { this.DataContext = value; }
        //}

        IUnityContainer _container;
        IRegionManager _regionManager;
        IRegion _region;

        public MainWindow(IUnityContainer container, IRegionManager regionManager)
        {
            InitializeComponent();
            _container = container;
            _regionManager = regionManager;

            this.Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"Region Count: {0}", _regionManager.Regions.Count());

            _region = _regionManager.Regions["ContentRegion"];
            var view = _container.Resolve<ItemList_View>();
            _region.Add(view);

            //var view2 = _container.Resolve<AddItem_View>();
            //_region.RequestNavigate(new Uri("AddItem_View", UriKind.Relative));
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var view = _container.Resolve<ItemList_View>();
            //IRegion region = _regionManager.Regions["ContentRegion"];
            //region.Add(view);

            //--View Injection 
            //IRegion region = _regionManager.Regions["ContentRegion"];
            //var view = _container.Resolve<ItemList_View>();
            //region.Add(view);

            //var view2 = _container.Resolve<AddItem_View>();
            //region.RequestNavigate(new Uri("AddItem_View", UriKind.Relative));
        }
    }
}
