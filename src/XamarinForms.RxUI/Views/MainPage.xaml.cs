using Xamarin.Forms;
using XamarinForms.RxUI.ViewModels;

namespace XamarinForms.RxUI.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(Navigation);
        }
    }
}