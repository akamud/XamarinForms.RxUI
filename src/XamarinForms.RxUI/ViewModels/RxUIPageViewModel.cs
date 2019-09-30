using MvvmHelpers;
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
    public class RxUIPageViewModel : ObservableObject
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
            get => cep;
            set
            {
                SetProperty(ref cep, value, onChanged: async () => ResultadoCEP = await BuscarCEP(CEP));
            }
        }

        private ResultadoCEP resultadoCEP;
        public ResultadoCEP ResultadoCEP
        {
            get => resultadoCEP;
            set => SetProperty(ref resultadoCEP, value);
        }

        public ObservableCollection<string> CEPsBuscados { get; set; }

        public RxUIPageViewModel()
        {
            CEPsBuscados = new ObservableCollection<string>();
        }
    }
}
