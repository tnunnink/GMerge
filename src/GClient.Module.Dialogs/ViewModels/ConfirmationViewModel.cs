using GClient.Core.Mvvm;
using Prism.Services.Dialogs;

namespace GClient.Module.Dialogs.ViewModels
{
    public class ConfirmationViewModel : DialogViewModelBase
    {
        private string _message;

        public string Message
        {
            get => _message;
            private set => SetProperty(ref _message, value);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("title");
            Message = parameters.GetValue<string>("message");
        }
    }
}