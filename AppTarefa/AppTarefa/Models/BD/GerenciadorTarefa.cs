using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTarefa.Models.BD
{
    public class GerenciadorTarefa
    {
        private List<Tarefa> listaDetarefas { get; set; }

        public void FinalizarTafera(int indice, Tarefa tarefa)
        {
            listaDetarefas = ListagemNoProperties();

            listaDetarefas.RemoveAt(indice);

            tarefa.DataFinalizacao = DateTime.Now;

            listaDetarefas.Add(tarefa);

            SalvarNoProperties(listaDetarefas);
        }

        public void Salvar(Tarefa tarefa)
        {
            listaDetarefas = ListagemNoProperties();

            listaDetarefas.Add(tarefa);

            SalvarNoProperties(listaDetarefas);
        }

        public List<Tarefa> Listagem()
        {
            return ListagemNoProperties();
        }

        public void Deletar(int index)
        {
            listaDetarefas = ListagemNoProperties();

            this.listaDetarefas.RemoveAt(index);

            SalvarNoProperties(listaDetarefas);
        }

        private void SalvarNoProperties(List<Tarefa> tarefas)
        {
            if (App.Current.Properties.ContainsKey("tarefa"))
            {
                App.Current.Properties.Remove("tarefa");
            }

            var jsonVal = JsonConvert.SerializeObject(tarefas);

            App.Current.Properties.Add("tarefa", jsonVal);
        }

        private List<Tarefa> ListagemNoProperties()
        {
            if (App.Current.Properties.ContainsKey("tarefa"))
            {
                String JsonVal = (String)App.Current.Properties["tarefa"];

                var list = JsonConvert.DeserializeObject<List<Tarefa>>(JsonVal);

                return list;
            }
            return new List<Tarefa>();
        }
    }
}
