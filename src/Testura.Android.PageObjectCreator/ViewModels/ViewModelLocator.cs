using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Testura.Android.PageObjectCreator.Services;
using Testura.Android.PageObjectCreator.Util;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Views
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DeviceViewModel>();
            SimpleIoc.Default.Register<OperationViewModel>();
            SimpleIoc.Default.Register<PageObjectViewModel>();
            SimpleIoc.Default.Register<ScreenViewModel>();
            SimpleIoc.Default.Register<CodeViewModel>();
            SimpleIoc.Default.Register<WithViewModel>();
            SimpleIoc.Default.Register<HierarchyViewModel>();

            // Services and utilities
            SimpleIoc.Default.Register<ITerminal, Terminal>();
            SimpleIoc.Default.Register<IDeviceService, DeviceService>();
            SimpleIoc.Default.Register<IDumpService, DumpService>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<IFileService, FileService>();
            SimpleIoc.Default.Register<IScreenService, ScreenService>();
            SimpleIoc.Default.Register<ICodeService, CodeService>();
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public DeviceViewModel DeviceViewModel => ServiceLocator.Current.GetInstance<DeviceViewModel>();

        public OperationViewModel OperationViewModel => ServiceLocator.Current.GetInstance<OperationViewModel>();

        public PageObjectViewModel PageObjectViewModel => ServiceLocator.Current.GetInstance<PageObjectViewModel>();

        public ScreenViewModel ScreenViewModel => ServiceLocator.Current.GetInstance<ScreenViewModel>();

        public CodeViewModel CodeViewModel => ServiceLocator.Current.GetInstance<CodeViewModel>();

        public WithViewModel WithViewModel => ServiceLocator.Current.GetInstance<WithViewModel>();

        public HierarchyViewModel XmlViewModel => ServiceLocator.Current.GetInstance<HierarchyViewModel>();
    }
}