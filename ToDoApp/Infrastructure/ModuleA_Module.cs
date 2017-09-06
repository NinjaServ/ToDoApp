using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;
using ToDoApp.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;


namespace ToDoApp.Infrastructure
{
    public class ModuleA_Module : IModule
    {
        IRegionManager _regionManager;
        IUnityContainer _container;

        public ModuleA_Module(RegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;

            //RegionManager.SetRegionManager(view as DependencyObject, regionManager);
            //RegionManager.SetRegionName(control, "MyRegion");
        }

        public void Initialize()
        {
            _container.RegisterType<IToDoList_Service, ToDoList_Service>(new ContainerControlledLifetimeManager());

            _container.RegisterTypeForNavigation<ItemList_View>();   //ItemList_View
            _container.RegisterTypeForNavigation<AddItem_View>();   //AddItem_View
            _container.RegisterTypeForNavigation<ItemDetail_View>();   //ItemDetail_View


            _regionManager.RequestNavigate("ContentRegion", "ItemList_View");
            //_regionManager.RegisterViewWithRegion("ContentRegion", () =>
            //          _container.Resolve<ItemList_View>());

            //_regionManager.RequestNavigate("ContentRegion", "AddItem_View");

            //_regionManager.RequestNavigate("ContentRegion",
            //                    new Uri("ItemList_View", UriKind.Relative));

            //--View Discovery
            //_regionManager.RegisterViewWithRegion("ContentRegion", () =>
            //          _container.Resolve<ItemList_View>());

            //--View Injection 
            //IRegion mainRegion = _regionManager.Regions["ContentRegion"];
            //ItemList_View view = this._container.Resolve<ItemList_View>();
            //mainRegion.Add(view);
            //mainRegion.RequestNavigate(new Uri("ItemList_View", UriKind.Relative));

            //Console.WriteLine($"Region Count: {0}", _regionManager.Regions.Count());
        }

        //private void SelectedEmployeeChanged(object sender, EventArgs e)
        //{

        //    _regionManager.RequestNavigate(RegionNames.TabRegion,
        //                "ItemList_View", NavigationCompleted);
        //}

        //private void NavigationCompleted(NavigationResult result)
        //{
                //if(result)
                //{
                //    Debug.WriteLine("Sucess");
                //}
        //}

    }
   
}


