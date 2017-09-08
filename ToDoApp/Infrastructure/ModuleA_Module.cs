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

        }

        public void Initialize()
        {
            _container.RegisterType<IToDoList_Service, ToDoList_Service>(new ContainerControlledLifetimeManager());

            _container.RegisterTypeForNavigation<ItemList_View>();   //ItemList_View
            _container.RegisterTypeForNavigation<AddItem_View>();   //AddItem_View
            _container.RegisterTypeForNavigation<ItemDetail_View>();   //ItemDetail_View


            _regionManager.RequestNavigate("ContentRegion", "ItemList_View");
            
        }


    }
   
}


