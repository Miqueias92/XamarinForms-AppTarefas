using AppTarefa.Models;
using AppTarefa.Models.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTarefa.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CadastroPage : ContentPage
	{
        private byte prioridade { get; set; }

		public CadastroPage ()
		{
			InitializeComponent ();
		}

        public void PrioridadeSelectAction(object sender, EventArgs args)
        {
            // Lista de stackLayouts
            var stacks = SLPrioridades.Children;
            //var stacks = SLPrioridades.Children as List<StackLayout>();

            foreach (var stack in stacks)
            {
                Label lblPrioridade = ((StackLayout)stack).Children[1] as Label;
                //Label lblPrioridade = stack.Children[1] as Label;
                lblPrioridade.TextColor = Color.Gray;
            }

            ((Label)((StackLayout)sender).Children[1]).TextColor = Color.Black;
            FileImageSource source = ((Image)((StackLayout)sender).Children[0]).Source as FileImageSource;

            var prioridadeVal = source.File.ToString().Replace("p", "").Substring(0, 1);

            prioridade = byte.Parse(prioridadeVal);
        }

        public void SalvarTarefa(object sender, EventArgs args)
        {
            bool error = false;

            if (!(TxtNome.Text.Trim().Length > 0))
            {
                error = true;
                DisplayAlert("ERRO", "Nome não preenchido", "OK");
            }

            if (!(this.prioridade > 0))
            {
                error = true;
                DisplayAlert("ERRO", "Prioridade não foi informada", "OK");
            }

            if (error == false)
            {
                var tarefa = new Tarefa();
                tarefa.Nome = TxtNome.Text.Trim();
                tarefa.Prioridade = this.prioridade;

                new GerenciadorTarefa().Salvar(tarefa);

                App.Current.MainPage = new NavigationPage(new MainPage());
            }
        }
    }
}