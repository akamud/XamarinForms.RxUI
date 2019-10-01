# ReactiveUI com Xamarin.Forms

A ideia deste sample é mostrar como ReactiveUI te ajuda a começar a usar Reactive Extensions num projeto Xamarin.Forms com o mínimo de impacto possível para o seu código pré-existente e sua produtividade.

Esse projeto usa ReactiveUI apenas para facilitar a criação de Observables para Binding no XAML, sem utilizar os outros recursos do framework (commands, ciclo de vida, validação e afins), apenas o pacote Core está instalado.

O ReactiveUI.Fody é usado para auto-gerar properties com as definições necessárias para funcionar com Reactive Extensions mantendo uma sintaxe limpa nas ViewModels.

Licença [MIT](https://github.com/akamud/XamarinForms.RxUI/blob/master/LICENSE)