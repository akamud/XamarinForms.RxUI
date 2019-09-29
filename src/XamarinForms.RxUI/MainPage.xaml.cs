using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XamarinForms.RxUI
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-27.500146, -48.514595), Distance.FromMeters(300)));
        }
    }
}
