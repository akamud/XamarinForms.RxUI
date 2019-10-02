using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;

namespace XamarinForms.RxUI.ViewModels
{
    public class RxPageViewModel
    {
        private readonly Entry entry;
        public ObservableCollection<string> Itens { get; set; }

        public RxPageViewModel(Entry entry)
        {
            // NÃO FAÇA ISSO EM PRODUÇÃO, para deixar a demo focada apenas no que interessava
            // eu fiz essa gambiarra xavosa que você nunca deve repetir ;)
            this.entry = entry;
            Itens = new ObservableCollection<string>();

            // Operadores

            Range();

            //Interval();

            //EventPattern();

            //Buffer();

            //Merge();

            //CombineLatest();

            //ObservableAsync();
        }

        private void Range()
        {
            var rangeStream = Observable.Range(1, 10);

            rangeStream
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(num => AdicionarItem(num.ToString()),
                    () => Completed());
        }

        private void Interval()
        {
            var timerStream = Observable.Interval(TimeSpan.FromSeconds(2));

            timerStream
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(num => AdicionarItem(num.ToString()));
        }

        private void EventPattern()
        {
            var textStream = Observable.FromEventPattern<EventHandler<TextChangedEventArgs>, TextChangedEventArgs>(
                                            x => entry.TextChanged += x,
                                            x => entry.TextChanged -= x
                                        );
                //.Select(args => args.EventArgs.NewTextValue);

            textStream
                //.Where(text => text.Contains("Xamarin Summit"))
                //.Throttle(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(text => AdicionarItem(text.EventArgs.NewTextValue));
        }

        private void Buffer()
        {
            var rangeStream = Observable.Interval(TimeSpan.FromMilliseconds(7));

            rangeStream
                //.Buffer(TimeSpan.FromMilliseconds(500))
                .ObserveOn(RxApp.MainThreadScheduler)
                //.Subscribe(text => AdicionarItem($"Qtde: {text.Count}, Total: {text.Last()}"));
                .Subscribe(text => AdicionarItem($"Total: {text}"));
        }

        private void Merge()
        {
            var textStream = Observable.FromEventPattern<EventHandler<TextChangedEventArgs>, TextChangedEventArgs>(
                                            x => entry.TextChanged += x,
                                            x => entry.TextChanged -= x
                                        )
                                .Select(args => args.EventArgs.NewTextValue);

            var rangeStream = Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(num => num.ToString());

            textStream
                .Merge(rangeStream)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(text => AdicionarItem(text));
        }

        private void CombineLatest()
        {
            var textStream = Observable.FromEventPattern<EventHandler<TextChangedEventArgs>, TextChangedEventArgs>(
                                            x => entry.TextChanged += x,
                                            x => entry.TextChanged -= x
                                        )
                                .Select(args => args.EventArgs.NewTextValue);

            var rangeStream = Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(num => num.ToString());

            textStream
                .CombineLatest(rangeStream, (text, num) => text + " #" + num)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(text => AdicionarItem(text));
        }

        private void ObservableAsync()
        {
            entry.Unfocused += (sender, e) =>
            {
                Itens.Clear();
                var asyncStream = Observable.FromAsync(_ => ConsultaCEPService.ConsultarCEP(((Entry)sender).Text));

                asyncStream
                    //.Timeout(TimeSpan.FromSeconds(1.2))
                    //.Catch<ResultadoCEP, Exception>(ex => Observable.Return<ResultadoCEP>(null))
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(resultadoCEP =>
                    {
                        if (resultadoCEP == null)
                            AdicionarItem("Erro");
                        else
                            AdicionarItem(resultadoCEP.Endereço);
                    });
            };
        }

        private void AdicionarItem(string item)
        {
            Itens.Insert(0, item);
        }

        private void Completed()
        {
            Itens.Insert(0, "Completed");
        }
    }
}
