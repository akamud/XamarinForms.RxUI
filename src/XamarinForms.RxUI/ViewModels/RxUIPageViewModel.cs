using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForms.RxUI.ViewModels
{
    public class RxUIPageViewModel : ReactiveObject
    {
        private static string regexPattern = @"\d{5}\-\d{3}";

        private async Task<ResultadoCEP> BuscarCEP(string cep)
        {
            Device.BeginInvokeOnMainThread(() => CEPsBuscados.Insert(0, cep));
            return await ConsultaCEPService.ConsultarCEP(cep);
        }

        [Reactive]
        public string CEP { get; set; }

        public ResultadoCEP ResultadoCEP { [ObservableAsProperty] get; }

        public ObservableCollection<string> CEPsBuscados { get; set; }

        public RxUIPageViewModel()
        {
            CEPsBuscados = new ObservableCollection<string>();
            CEP = "";

            this.WhenAnyValue(x => x.CEP)
                .Throttle(TimeSpan.FromMilliseconds(250), RxApp.TaskpoolScheduler)
                .Select(x => x.Trim())
                .Where(s => Regex.IsMatch(s, regexPattern))
                .DistinctUntilChanged()
                .Select(async cep => await BuscarCEP(cep))
                .Switch()
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, e => e.ResultadoCEP);
        }
    }
}
