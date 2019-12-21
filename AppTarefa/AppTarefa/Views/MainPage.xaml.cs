using AppTarefa.Models;
using AppTarefa.Models.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTarefa.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            DataHoje.Text = DateTime.Now.DayOfWeek.ToString() + ", " + DateTime.Now.ToString("dd/MM");

            CarregarTarefas();
        }

        private void ActionGoCadastro(object sender, EventArgs args)
        {
            Navigation.PushAsync(new CadastroPage());
        }

        private void CarregarTarefas()
        {
            SLTarefa.Children.Clear();

            var lista = new GerenciadorTarefa().Listagem();

            int i = 0;
            foreach (var item in lista)
            {
                LinhasStackLayout(item, i);
                i++;
            }
        }

        public void LinhasStackLayout(Tarefa tarefa, int index)
        {
            Image delete = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("Delete.png") };
            if (Device.RuntimePlatform == Device.UWP)
            {
                delete.Source = ImageSource.FromFile("Resources/Delete.png");
            }

            TapGestureRecognizer deleteTap = new TapGestureRecognizer();
            deleteTap.Tapped += delegate
            {
                new GerenciadorTarefa().Deletar(index);
                CarregarTarefas();
            };

            delete.GestureRecognizers.Add(deleteTap);

            Image prioridade = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("p" + tarefa.Prioridade + ".png") };
            if (Device.RuntimePlatform == Device.UWP)
            {
                prioridade.Source = ImageSource.FromFile("Resources/p" + tarefa.Prioridade + ".png");
            }

            View stackCentral = null;

            if (tarefa.DataFinalizacao == null)
            {
                stackCentral = new Label() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, Text = tarefa.Nome };
            }
            else
            {
                stackCentral = new StackLayout() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, Spacing = 0 };
                ((StackLayout)stackCentral).Children.Add(new Label() { Text = tarefa.Nome, TextColor = Color.Gray });

                ((StackLayout)stackCentral).Children.Add(new Label() { Text = "Finalizado em " + tarefa.DataFinalizacao.Value.ToString("dd/MM/yyyy - hh:mm") + "h", TextColor = Color.Gray, FontSize = 10 });
            }
            
            Image check = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("CheckOff.png") };
            if (Device.RuntimePlatform == Device.UWP)
            {
                check.Source = ImageSource.FromFile("Resources/CheckOff.png");
            }
            if (tarefa.DataFinalizacao != null)
            {
                check.Source = ImageSource.FromFile("CheckOn.png");
                if (Device.RuntimePlatform == Device.UWP)
                {
                    check.Source = ImageSource.FromFile("Resources/CheckOn.png");
                }
            }

            TapGestureRecognizer checkTap = new TapGestureRecognizer();
            checkTap.Tapped += delegate
            {
                new GerenciadorTarefa().FinalizarTafera(index, tarefa);
                CarregarTarefas();
            };

            check.GestureRecognizers.Add(checkTap);

            StackLayout linha = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 15 };
            linha.Children.Add(check);
            linha.Children.Add(stackCentral);
            linha.Children.Add(prioridade);
            linha.Children.Add(delete);

            SLTarefa.Children.Add(linha);
        }
    }
}
