using Xamarin.Forms;
using XamarinForms.RxUI.ViewModels;

namespace XamarinForms.RxUI.Views
{
    public partial class RxUIPage : ContentPage
    {
        public RxUIPage()
        {
            InitializeComponent();

            BindingContext = new RxUIPageViewModel();
        }
    }
}
