using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;

namespace XamarinForms.RxUI
{
    public class MainPageViewModel
    {
        public ObservableCollection<Pin> Pins { get; set; }

        public MainPageViewModel()
        {
            Pins = new ObservableCollection<Pin>
            {
                new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(-27.500146, -48.514595),
                    Label = "Xamarin Summit",
                    Address = "Faculdade Cesusc (Sede SC 401)"
                }
            };
        }
    }
}
