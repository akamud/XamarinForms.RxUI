using Bogus;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XamarinForms.RxUI
{
    public static class ConsultaCEPService
    {
        private static string regexPattern = @"\d{5}\-\d{3}";
        private static Random _random;

        static ConsultaCEPService()
        {
            _random = new Random();
        }
        public static async Task<ResultadoCEP> ConsultarCEP(string CEP)
        {
            var rand = new Random();
            await Task.Delay(rand.Next(500, 2000));
            return new ResultadoCEP
            {
                CEPBuscado = CEP,
                Endereço = (Regex.IsMatch(CEP, regexPattern) ? new Faker("pt_BR").Address.FullAddress() : "**CEP Inválido")
            };
        }
    }
}
