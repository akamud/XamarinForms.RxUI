using Xamarin.Forms;
using XamarinForms.RxUI.Views;

namespace XamarinForms.RxUI.ViewModels
{
    public class MainPageViewModel
    {
        public Command IrParaRxPageCommand { get; set; }
        public Command IrParaRxUIPageCommand { get; set; }
        private INavigation _navigation;

        public MainPageViewModel(INavigation navigation)
        {
            _navigation = navigation;

            IrParaRxPageCommand = new Command(async () => await _navigation.PushAsync(new RxPage()));
            IrParaRxUIPageCommand = new Command(async () => await _navigation.PushAsync(new RxUIPage()));
        }
    }
}
