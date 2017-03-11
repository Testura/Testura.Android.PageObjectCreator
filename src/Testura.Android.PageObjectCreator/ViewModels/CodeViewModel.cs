using GalaSoft.MvvmLight;
using PropertyChanged;
using Testura.Android.PageObjectCreator.Models.Messages;
using Testura.Android.PageObjectCreator.Services;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    [ImplementPropertyChanged]
    public class CodeViewModel : ViewModelBase
    {
        private readonly ICodeService _codeService;

        public CodeViewModel(ICodeService codeService)
        {
            _codeService = codeService;
            Code = string.Empty;
            MessengerInstance.Register<PageObjectChangedMessage>(this, OnPageObjectChanged);
        }

        public string Code { get; set; }

        private void OnPageObjectChanged(PageObjectChangedMessage message)
        {
            Code = _codeService.GeneratePageObject(message.PageObject.Name, message.PageObject.Namespace, message.PageObject.UiObjectInfos);
        }
    }
}