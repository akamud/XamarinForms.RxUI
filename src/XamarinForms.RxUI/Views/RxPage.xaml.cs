using Xamarin.Forms;
using XamarinForms.RxUI.ViewModels;

namespace XamarinForms.RxUI.Views
{
    public partial class RxPage : ContentPage
    {
        public RxPage()
        {
            InitializeComponent();
            BindingContext = new RxPageViewModel();
        }
    }
}