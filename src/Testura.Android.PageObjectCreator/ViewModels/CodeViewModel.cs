using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PropertyChanged;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.Models.Messages;
using Testura.Android.PageObjectCreator.Services;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    [ImplementPropertyChanged]
    public class CodeViewModel : ViewModelBase
    {
        private readonly ICodeService _codeService;
        private PageObject _lastPageObject;

        public CodeViewModel(ICodeService codeService)
        {
            _codeService = codeService;
            Code = string.Empty;
            UseAttributeCommand = new RelayCommand(OnUseAttributeChange);
            MessengerInstance.Register<PageObjectChangedMessage>(this, OnPageObjectChanged);
        }

        public string Code { get; set; }

        public bool UseAttribute { get; set; }

        public RelayCommand UseAttributeCommand { get; set; }

        private void OnPageObjectChanged(PageObjectChangedMessage message)
        {
            _lastPageObject = message.PageObject;
            GenerateCode();
        }

        private void OnUseAttributeChange()
        {
            GenerateCode();
        }

        private void GenerateCode()
        {
            if (_lastPageObject == null)
            {
                return;
            }

            Code = _codeService.GeneratePageObject(_lastPageObject.Name, _lastPageObject.Namespace, _lastPageObject.UiObjectInfos, UseAttribute);
        }
    }
}