using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForms.RxUI
{
    public class MainPageViewModel : ReactiveObject
    {
        private static string regexPattern = @"\d{5}\-\d{3}";
        private async Task<ResultadoCEP> BuscarCEP(string cep)
        {
            Device.BeginInvokeOnMainThread(() => CEPsBuscados.Insert(0, cep));
            return await ConsultaCEPService.ConsultarCEP(cep);
        }

        private string cep;

        public string CEP
        {
            get { return cep; }
            set { this.RaiseAndSetIfChanged(ref cep, value); }
        }

        ObservableAsPropertyHelper<ResultadoCEP> resultadoCEP;

        public ResultadoCEP ResultadoCEP
        {
            get { return resultadoCEP.Value; }
        }

        public ObservableCollection<string> CEPsBuscados { get; set; }

        public MainPageViewModel()
        {
            CEPsBuscados = new ObservableCollection<string>();
            CEP = "";

            resultadoCEP = this.WhenAnyValue(x => x.CEP)
                .Throttle(TimeSpan.FromMilliseconds(250), RxApp.TaskpoolScheduler)
                .Select(x => x.Trim())
                .Where(s => Regex.IsMatch(s, regexPattern))
                .DistinctUntilChanged()
                .Select(async _ => await BuscarCEP(cep))
                .Switch()
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToProperty(this, e => e.ResultadoCEP);

            resultadoCEP.ThrownExceptions.Subscribe(x =>
            {
                Debug.WriteLine(x);
            });
        }
    }
}
